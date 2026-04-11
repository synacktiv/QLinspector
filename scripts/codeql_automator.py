#!/usr/bin/env python3
import os
import sys
import json
import shutil
import argparse
import subprocess
from pathlib import Path

# ==========================================
# COMMON UTILITIES & SARIF
# ==========================================

def run_command(cmd, timeout=None, check=True, cwd=None):
    """Executes a subprocess command, handling timeouts and errors."""
    try:
        result = subprocess.run(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True,
            timeout=timeout,
            check=check,
            cwd=cwd
        )
        return True, result.stdout
    except subprocess.TimeoutExpired as e:
        return False, f"Timeout after {timeout}s"
    except subprocess.CalledProcessError as e:
        return False, e.stderr

def get_sarif_results(sarif_path):
    """Reads SARIF file and returns the results list."""
    if not os.path.exists(sarif_path):
        return []
    try:
        with open(sarif_path, 'r', encoding='utf-8') as f:
            data = json.load(f)
            runs = data.get('runs', [])
            if runs and 'results' in runs[0]:
                return runs[0]['results']
    except Exception as e:
        print(f"[ERROR] Failed to read SARIF {sarif_path}: {e}")
    return []

def filter_sarif_threadflows(sarif_path, output_path, max_steps=42):
    """Filters SARIF results where thread flow locations exceed max_steps."""
    if not os.path.exists(sarif_path):
        return

    with open(sarif_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    if not data.get('runs') or 'results' not in data['runs'][0]:
        return

    valid_results = []
    for result in data['runs'][0]['results']:
        code_flows = result.get('codeFlows', [])
        if not code_flows:
            valid_results.append(result)
            continue

        keep = True
        for cf in code_flows:
            for tf in cf.get('threadFlows', []):
                if len(tf.get('locations', [])) > max_steps:
                    keep = False
                    break
        if keep:
            valid_results.append(result)

    data['runs'][0]['results'] = valid_results

    with open(output_path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=2)

def get_shortest_sarif_paths(sarif_path, top_n=20):
    """Extracts the top N shortest thread flow lengths."""
    results = get_sarif_results(sarif_path)
    lengths = []

    for res in results:
        code_flows = res.get('codeFlows', [])
        for cf in code_flows:
            thread_flows = cf.get('threadFlows', [])
            if thread_flows:
                locs = len(thread_flows[0].get('locations', []))
                if locs > 0:
                    lengths.append(locs)

    lengths.sort()
    return lengths[:top_n]


# ==========================================
# COMMON PIPELINE LOGIC
# ==========================================

def execute_queries_and_update_json(args, asm, db_folder, target_name, json_data, json_path):
    """Shared logic to run CodeQL queries, parse results, and update the JSON state."""
    sarif_out = Path(args.sarif_out)

    for query in args.queries:
        query_sanitized = query.replace("/", "-").replace(":", "-")
        sarif_file = sarif_out / f"{target_name}_{query_sanitized}.sarif"

        # Setup JSON tracking object
        if query not in asm['QL']:
            asm['QL'][query] = {
                "Top20ShortestPaths": [],
                "ResultStatus": "",
                "NumberOfResults": 0,
                "Changed": False
            }

        cmd_analyze = [
            args.codeql_cmd, "database", "analyze", str(db_folder), str(query),
            "--rerun", "--no-sarif-add-file-contents", "--no-sarif-add-snippets",
            "--max-paths=2", "--format=sarif-latest", f"--output={sarif_file}"
        ]
        if args.ram:
            cmd_analyze.append(f"--ram={args.ram}")

        print(f"[INFO] Running query {query} on {target_name}...")
        success, err = run_command(cmd_analyze, timeout=args.timeout)

        if success:
            asm['QL'][query]["ResultStatus"] = "OK"
            results = get_sarif_results(sarif_file)
            result_count = len(results)

            # Filter if too many results
            if result_count >= 250:
                filtered_sarif = sarif_out / f"{target_name}-{query}-filtered.sarif"
                filter_sarif_threadflows(sarif_file, filtered_sarif)
                sarif_file = filtered_sarif
                results = get_sarif_results(sarif_file)
                result_count = len(results)

            # Update JSON tracking
            shortest = get_shortest_sarif_paths(sarif_file)
            prev_count = asm['QL'][query]["NumberOfResults"]

            asm['QL'][query]["Changed"] = (prev_count != result_count)
            asm['QL'][query]["NumberOfResults"] = result_count
            asm['QL'][query]["Top20ShortestPaths"] = shortest
            print(f"[+] Query completed successfully. Found {result_count} results.")
        else:
            status = "Timeout" if "Timeout" in str(err) else "ERROR"
            asm['QL'][query]["ResultStatus"] = status
            print(f"[x] Query {status.lower()} for {target_name}. Error: {err}")

    # Save JSON progress after each assembly/jar finishes all queries
    with open(json_path, 'w', encoding='utf-8') as f:
        json.dump(json_data, f, indent=4)


# ==========================================
# JAVA PIPELINE
# ==========================================

def process_java(args):
    """Java pipeline driven by the JSON state file."""
    print("[INFO] Starting Java CodeQL Pipeline...")

    json_path = Path(args.json_path)
    if not json_path.exists():
        print(f"[ERROR] JSON file not found: {json_path}")
        return

    with open(json_path, 'r', encoding='utf-8') as f:
        json_data = json.load(f)

    src_out = Path(args.src_out)
    db_out = Path(args.db_out)
    sarif_out = Path(args.sarif_out)

    src_out.mkdir(parents=True, exist_ok=True)
    db_out.mkdir(parents=True, exist_ok=True)
    sarif_out.mkdir(parents=True, exist_ok=True)

    for asm in json_data.get('assemblies', []):
        jar_name = asm.get('Name')
        container_path = asm.get('Path') # Treated as docker container path

        asm.setdefault('QL', {})

        skip = True
        for query in args.queries:
            if query not in asm['QL'] or asm['QL'][query].get("ResultStatus") != "OK":
                skip = False
                break

        if skip and not args.rerun:
            print(f"[INFO] Analysis already done for {jar_name}, skipping.")
            continue

        print(f"[*] Processing jar: {jar_name}")
        jar_local = src_out / f"{jar_name}.jar"
        decomp_dir = src_out / f"{jar_name}_decomp"
        db_folder = db_out / f"{jar_name}_db"

        # 1. Fetch or Copy JAR
        if not jar_local.exists():
            if args.docker:
                print(f"[INFO] Fetching {container_path} from docker container {args.docker}...")
                success, _ = run_command(["docker", "cp", f"{args.docker}:{container_path}", str(jar_local)])
                if not success:
                    print(f"[ERROR] Failed to fetch {container_path} from docker.")
                    continue
            else:
                # If no docker ID is provided, assume the path in the JSON is a local file
                if os.path.exists(container_path):
                    print(f"[INFO] Copying local file {container_path} to {jar_local}...")
                    shutil.copy2(container_path, jar_local)
                else:
                    print(f"[ERROR] File not found locally: {container_path} (and no --docker provided)")
                    continue

        asm.setdefault("SizeMB", get_file_size_mb(jar_local))

        # 2. Decompile with Fernflower/Vineflower
        if not decomp_dir.exists():
            print(f"[INFO] Decompiling to {decomp_dir}")
            decomp_dir.mkdir(parents=True, exist_ok=True)
            success, _ = run_command(["java", "-jar", args.decomp_jar, str(jar_local), str(decomp_dir)])
            if not success:
                print(f"[ERROR] Decompilation failed for {jar_name}")
                continue

        # 3. Create CodeQL DB
        if not db_folder.exists():
            print(f"[INFO] Creating CodeQL Database...")
            cmd_db = [
                args.codeql_cmd, "database", "create", str(db_folder),
                "--language=java", "--build-mode=none", f"--source-root={decomp_dir}"
            ]
            success, _ = run_command(cmd_db)
            if not success:
                print(f"[ERROR] DB creation failed for {jar_name}")
                continue

        # 4. Run Queries & Update JSON
        execute_queries_and_update_json(args, asm, db_folder, jar_name, json_data, json_path)

        # 5. Cleanup
        if args.delete:
            shutil.rmtree(db_folder, ignore_errors=True)
            shutil.rmtree(decomp_dir, ignore_errors=True)
            if jar_local.exists(): os.remove(jar_local)

    print("[INFO] All Java processing done!")


# ==========================================
# .NET PIPELINE
# ==========================================

def extract_dependencies(json_deps_path, root_dll_path, max_depth):
    """Simplified Python version of the recursive dependency extractor."""
    if not os.path.exists(json_deps_path):
        return [root_dll_path]
    return [root_dll_path]

def process_dotnet(args):
    """.NET pipeline driven by the JSON state file."""
    print("[INFO] Starting .NET CodeQL Pipeline...")

    json_path = Path(args.json_path)
    if not json_path.exists():
        print(f"[ERROR] JSON file not found: {json_path}")
        return

    with open(json_path, 'r', encoding='utf-8') as f:
        json_data = json.load(f)

    src_out = Path(args.src_out)
    db_out = Path(args.db_out)
    sarif_out = Path(args.sarif_out)

    src_out.mkdir(parents=True, exist_ok=True)
    db_out.mkdir(parents=True, exist_ok=True)
    sarif_out.mkdir(parents=True, exist_ok=True)

    for asm in json_data.get('assemblies', []):
        dll_name = asm.get('Name')
        dll_path = asm.get('Path') # Treated as local host path

        if not os.path.exists(dll_path):
            print(f"[WARNING] Local DLL path not found for '{dll_name}', skipping.")
            continue
        else:
            asm.setdefault("SizeMB", get_file_size_mb(dll_path))

        asm.setdefault('QL', {})

        skip = True
        for query in args.queries:
            if query not in asm['QL'] or asm['QL'][query].get("ResultStatus") != "OK":
                skip = False
                break

        if skip and not args.rerun:
            print(f"[INFO] Analysis already done for {dll_name}, skipping.")
            continue

        print(f"[*] Analyzing assembly: {dll_name}")
        sln_folder = src_out / dll_name
        db_folder = db_out / dll_name

        # 1. Decompile with dnSpy
        if not sln_folder.exists():
            sln_folder.mkdir(parents=True, exist_ok=True)
            dlls_to_decompile = [dll_path]
            if args.json_deps:
                dlls_to_decompile = extract_dependencies(args.json_deps, dll_path, args.max_depth)

            dnspy_cmd = [args.dnspy_cli] + dlls_to_decompile + ["-o", str(sln_folder), "--sln-name", f"{dll_name}.sln"]
            success, _ = run_command(dnspy_cmd)
            if not success:
                print(f"[ERROR] Failed to decompile {dll_name}")
                continue

        # 2. Create CodeQL DB
        if not db_folder.exists():
            cmd_db = [
                args.codeql_cmd, "database", "create", str(db_folder),
                "--language=csharp", f"--source-root={sln_folder}", "--build-mode=none"
            ]
            success, _ = run_command(cmd_db)
            if not success:
                print(f"[ERROR] Failed to create CodeQL DB for {dll_name}")
                continue

        # 3. Run Queries & Update JSON
        execute_queries_and_update_json(args, asm, db_folder, dll_name, json_data, json_path)

        # 4. Cleanup
        if args.delete:
            shutil.rmtree(db_folder, ignore_errors=True)
            # shutil.rmtree(sln_folder, ignore_errors=True)

    print("[INFO] All .NET processing done!")


def get_file_size_mb(path_str):
    """Returns file size in MB if path exists locally, else 0."""
    try:
        p = Path(path_str)
        if p.exists() and p.is_file():
            # .stat().st_size returns bytes
            size_mb = p.stat().st_size / (1024 * 1024)
            return round(size_mb, 2)
    except Exception:
        pass
    return 0

def generate_initial_json(input_path, output_json):
    """
    Parses a list file or a single file path to create the initial JSON structure.
    """
    entries = []
    input_p = Path(input_path)

    # Case 1: Input is a text/list file containing multiple paths
    if input_p.exists() and input_p.is_file():
        print(f"[INFO] Reading entries from list file: {input_path}")
        with open(input_p, 'r', encoding='utf-8') as f:
            paths = [line.strip() for line in f if line.strip()]
            for p in paths:
                entries.append({
                    "Name": Path(p).stem,
                    "Path": p,
                    "QL": {}
                })
    # Case 2: Input is a single file (JAR, DLL, or even a non-existing path string)
    else:
        print(f"[INFO] Treating input as a single entry: {input_path}")
        entries.append({
            "Name": input_p.stem,
            "Path": str(input_path),
            "QL": {}
        })

    json_data = {"assemblies": entries}

    with open(output_json, 'w', encoding='utf-8') as f:
        json.dump(json_data, f, indent=4)
    
    print(f"[+] Successfully created {output_json} with {len(entries)} entries.")

def run_init(args):
    generate_initial_json(args.input, args.json_path)

# ==========================================
# CLI PARSER
# ==========================================

def main():
    parser = argparse.ArgumentParser(description="Unified CodeQL Automation Script (Java & .NET)")
    subparsers = parser.add_subparsers(dest="pipeline", required=True, help="Choose the pipeline to run")

    # --- Common arguments template to keep CLI unified ---
    def add_common_args(subparser):
        subparser.add_argument("--json-path", required=True, help="Path to the assemblies JSON file")
        subparser.add_argument("--queries", required=True, nargs="+", help="List of QL queries or suites to run")
        subparser.add_argument("--src-out", default="./sources", help="Output directory for decompiled sources")
        subparser.add_argument("--db-out", default="./databases", help="Output directory for CodeQL databases")
        subparser.add_argument("--sarif-out", default="./sarif", help="Output directory for SARIF results")
        subparser.add_argument("--ram", type=int, default=8192, help="RAM limit in MB for analysis")
        subparser.add_argument("--timeout", type=int, default=3600, help="Timeout in seconds per query")
        subparser.add_argument("--delete", action="store_true", help="Delete CodeQL DBs/Sources after processing")
        subparser.add_argument("--rerun", action="store_true", help="Force rerun of queries even if marked OK in JSON")
        subparser.add_argument("--codeql-cmd", default="codeql", help="CodeQL executable path")

    # --- Java Parser ---
    java_parser = subparsers.add_parser("java", help="Run the Java pipeline")
    add_common_args(java_parser)
    java_parser.add_argument("--decomp-jar", required=True, help="Path to Vineflower/Fernflower JAR")
    java_parser.add_argument("--docker", help="Docker container ID/Name")
    java_parser.set_defaults(func=process_java)

    # --- .NET Parser ---
    dotnet_parser = subparsers.add_parser("dotnet", help="Run the .NET pipeline")
    add_common_args(dotnet_parser)
    dotnet_parser.add_argument("--dnspy-cli", default="dnSpy.Console.exe", help="Path to dnSpy CLI")
    dotnet_parser.add_argument("--json-deps", help="Optional JSON file for dependency mapping")
    dotnet_parser.add_argument("--max-depth", type=int, default=5, help="Max depth for dependency decompilation")
    dotnet_parser.set_defaults(func=process_dotnet)

    init_parser = subparsers.add_parser("init", help="Generate the JSON config file")
    init_parser.add_argument("--input", required=True, help="Path to a .lst file or a single file path")
    init_parser.add_argument("--json-path", required=True, help="Target path for the generated JSON")
    init_parser.set_defaults(func=run_init)

    args = parser.parse_args()
    args.func(args)

if __name__ == "__main__":
    main()
