#!/usr/bin/env python3
import os
import json
import shutil
import argparse
import subprocess
from pathlib import Path
import signal
import threading

CURRENT_CONTEXT = {
    "db": None,
    "decomp": None,
    "jar": None,
    "cleanup_assembly": False
}

# ==========================================
# COMMON UTILITIES
# ==========================================

def run_command(cmd, timeout=None, cwd=None):
    """Executes a subprocess command with full output capture."""
    try:
        result = subprocess.run(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True,
            timeout=timeout,
            cwd=cwd
        )
        output = (result.stdout or "") + (result.stderr or "")
        return result.returncode == 0, output.strip()
    except subprocess.TimeoutExpired:
        return False, f"Timeout after {timeout}s\nCMD: {' '.join(cmd)}"


def save_json_atomic(data, path):
    """Atomically write JSON to disk."""
    tmp_path = f"{path}.tmp"
    with open(tmp_path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=4)
    os.replace(tmp_path, path)


# ==========================================
# SARIF UTILITIES
# ==========================================

def get_sarif_results(sarif_path):
    """Safely extract all results across runs."""
    if not os.path.exists(sarif_path):
        return []
    try:
        with open(sarif_path, 'r', encoding='utf-8') as f:
            data = json.load(f)

        results = []
        for run in data.get('runs', []):
            results.extend(run.get('results', []))
        return results

    except Exception as e:
        print(f"[ERROR] Failed to read SARIF {sarif_path}: {e}")
        return []


def filter_sarif_threadflows(sarif_path, output_path, max_steps=42):
    if not os.path.exists(sarif_path):
        return

    with open(sarif_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    runs = data.get('runs', [])
    if not runs or 'results' not in runs[0]:
        return

    original = len(runs[0]['results'])
    valid_results = []

    for result in runs[0]['results']:
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
            if not keep:
                break

        if keep:
            valid_results.append(result)

    runs[0]['results'] = valid_results
    filtered_out = original - len(valid_results)

    print(f"[INFO] Filtered out {filtered_out} noisy results")
    save_json_atomic(data, output_path)


def extract_shortest_paths_from_results(results, top_n=20):
    lengths = []
    for res in results:
        for cf in res.get('codeFlows', []):
            tfs = cf.get('threadFlows', [])
            if tfs:
                locs = len(tfs[0].get('locations', []))
                if locs > 0:
                    lengths.append(locs)
    return sorted(lengths)[:top_n]

def handle_ctrl_c(sig, frame):
    print("[WARN] Ctrl+C detected. Cleaning up current task...")

    try:
        if CURRENT_CONTEXT["db"]:
            shutil.rmtree(CURRENT_CONTEXT["db"], ignore_errors=True)

        if CURRENT_CONTEXT["decomp"]:
            shutil.rmtree(CURRENT_CONTEXT["decomp"], ignore_errors=True)

        if CURRENT_CONTEXT["cleanup_assembly"] and CURRENT_CONTEXT["jar"]:
            if CURRENT_CONTEXT["jar"].exists():
                os.remove(CURRENT_CONTEXT["jar"])

        print("[+] Cleanup complete. Exiting.")
    except Exception as e:
        print(f"[ERROR] Cleanup failed: {e}")

    os._exit(130)

# ==========================================
# COMMON PIPELINE
# ==========================================

def execute_queries_and_update_json(args, asm, db_folder, target_name, json_data, json_path):
    sarif_out = Path(args.sarif_out)
    any_results = False

    for query in args.queries:
        query_sanitized = query.replace("/", "-").replace(":", "-")
        sarif_file = sarif_out / f"{target_name}_{query_sanitized}.sarif"

        asm.setdefault('QL', {})
        asm['QL'].setdefault(query, {
            "Top20ShortestPaths": [],
            "ResultStatus": "",
            "NumberOfResults": 0,
            "Changed": False
        })

        if (
            not args.rerun
            and query in asm['QL']
            and asm['QL'][query].get("ResultStatus") == "OK"
        ):
            print(f"[INFO] Skipping {query} (already OK)")
            continue

        cmd = [
            args.codeql_cmd, "database", "analyze",
            str(db_folder), str(query),
            "--rerun",
            "--no-sarif-add-file-contents",
            "--no-sarif-add-snippets",
            "--max-paths=2",
            "--format=sarif-latest",
            f"--output={sarif_file}"
        ]

        if args.ram:
            cmd.append(f"--ram={args.ram}")
        if args.threads:
            cmd.append(f"--threads={args.threads}")

        print(f"[INFO] Running {query} on {target_name}")
        success, output = run_command(cmd, timeout=args.timeout)

        if not success:
            status = "Timeout" if "Timeout" in output else "ERROR"
            asm['QL'][query]["ResultStatus"] = status
            print(f"[ERROR] {query} failed on {target_name}")
            print(output)
            continue

        results = get_sarif_results(sarif_file)
        result_count = len(results)

        if result_count > 0:
            any_results = True

        if result_count >= 250:
            filtered = sarif_out / f"{target_name}-{query}-filtered.sarif"
            filter_sarif_threadflows(sarif_file, filtered)
            results = get_sarif_results(filtered)
            result_count = len(results)

        shortest = extract_shortest_paths_from_results(results)
        prev = asm['QL'][query]["NumberOfResults"]

        asm['QL'][query].update({
            "ResultStatus": "OK",
            "NumberOfResults": result_count,
            "Top20ShortestPaths": shortest,
            "Changed": prev != result_count
        })

        print(f"[+] {query}: {result_count} results")

    save_json_atomic(json_data, json_path)
    return any_results

def create_codeql_database(args, db_path, language, source_root):
    """
    Unified CodeQL database creation for Java and .NET.
    Uses args directly for configuration.
    Returns: (success: bool, output: str)
    """

    cmd = [
        args.codeql_cmd,
        "database",
        "create",
        str(db_path),
        f"--language={language}",
        "--build-mode=none",
        f"--source-root={source_root}"
    ]

    if args.ram:
        cmd.append(f"--ram={args.ram}")
    if args.threads:
        cmd.append(f"--threads={args.threads}")

    print(f"[INFO] Creating CodeQL DB ({language}) at {db_path}")

    success, output = run_command(cmd)

    if not success:
        print(f"[ERROR] CodeQL DB creation failed ({language})")
        print(output)

    return success, output

# ==========================================
# JAVA PIPELINE
# ==========================================

def process_java(args):
    print("[INFO] Starting Java pipeline")

    json_path = Path(args.json_path)
    if not json_path.exists():
        print("[ERROR] JSON not found")
        return

    with open(json_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    src_out = Path(args.src_out)
    db_out = Path(args.db_out)
    src_out.mkdir(exist_ok=True, parents=True)
    db_out.mkdir(exist_ok=True, parents=True)
    has_results = False

    for asm in data.get('assemblies', []):
        name = asm['Name']
        source_path = asm['Path']

        print(f"[INFO] {name}")

        jar_local = src_out / f"{name}.jar"
        jar_was_copied = False

        # Fetch / copy
        if not jar_local.exists():
            if args.docker:
                ok, out = run_command(["docker", "cp", f"{args.docker}:{source_path}", str(jar_local)])
                if not ok:
                    print(out)
                    continue
                jar_was_copied = True
            elif os.path.exists(source_path):
                jar_local = Path(source_path)
            else:
                print("[ERROR] Missing input")
                continue

        decomp = src_out / f"{name}_decomp"
        db = db_out / f"{name}_db"

        CURRENT_CONTEXT["db"] = db
        CURRENT_CONTEXT["decomp"] = decomp
        CURRENT_CONTEXT["jar"] = jar_local
        CURRENT_CONTEXT["cleanup_assembly"] = jar_was_copied

        if not decomp.exists():
            print(f"[INFO] Decompiling to {decomp}")
            decomp.mkdir(parents=True, exist_ok=True)
            if args.vineflower_jar:
                cmd = ["java", "-jar", args.vineflower_jar, str(jar_local), str(decomp)]
            else:
                cmd = ["vineflower", str(jar_local), str(decomp)]

            ok, out = run_command(cmd)

            if not ok:
                print(f"[ERROR] Decompilation failed for {name}")
                print(out)
                continue

        if not db.exists():
            success, _ = create_codeql_database(
                args,
                db,
                "java",
                decomp
            )

            if not success:
                continue

        has_results = execute_queries_and_update_json(args, asm, db, name, data, json_path)

        if args.delete and not has_results:
            shutil.rmtree(db, ignore_errors=True)
            shutil.rmtree(decomp, ignore_errors=True)
            if jar_was_copied and jar_local.exists():
                os.remove(jar_local)

    print("[INFO] Java done")


# ==========================================
# .NET PIPELINE
# ==========================================

def process_dotnet(args):
    print("[INFO] Starting .NET pipeline")

    json_path = Path(args.json_path)
    with open(json_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    src_out = Path(args.src_out)
    db_out = Path(args.db_out)
    src_out.mkdir(exist_ok=True, parents=True)
    db_out.mkdir(exist_ok=True, parents=True)
    has_results = False

    for asm in data.get('assemblies', []):
        name = asm['Name']
        dll = asm['Path']

        if not os.path.exists(dll):
            print(f"[WARN] Missing {dll}")
            continue

        print(f"[INFO] {name}")

        sln = src_out / name
        db = db_out / name

        CURRENT_CONTEXT["db"] = db
        CURRENT_CONTEXT["decomp"] = sln
        CURRENT_CONTEXT["jar"] = None
        CURRENT_CONTEXT["cleanup_assembly"] = False

        if not sln.exists():
            print(f"[INFO] Decompiling to {sln}")
            ok, out = run_command([args.dnspy_cli, dll, "-o", str(sln)])
            if not ok:
                print(f"[ERROR] Decompilation failed for {name}")
                print(out)
                continue

        if not db.exists():
            success, _ = create_codeql_database(
                args,
                db,
                "csharp",
                sln
            )

            if not success:
                continue

        has_results = execute_queries_and_update_json(args, asm, db, name, data, json_path)

        if args.delete and not has_results:
            shutil.rmtree(db, ignore_errors=True)
            shutil.rmtree(sln, ignore_errors=True)

    print("[INFO] .NET done")

def run_init(args):
    generate_initial_json(args.input, args.json_path)

def generate_initial_json(input_path, output_json):
    """
    Parses a list file or a single file path to create the initial JSON structure.
    """
    entries = []
    input_p = Path(input_path)

    # Case 1: Input is a list file
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
    else:
        # Case 2: Treat as single entry
        print(f"[INFO] Treating input as a single entry: {input_path}")
        entries.append({
            "Name": input_p.stem,
            "Path": str(input_path),
            "QL": {}
        })

    json_data = {"assemblies": entries}

    save_json_atomic(json_data, output_json)

    print(f"[+] Successfully created {output_json} with {len(entries)} entries.")

# ==========================================
# CLI
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
        subparser.add_argument("--ram", type=int, default=8192, help="RAM limit in MB for analysis (8192 by default)")
        subparser.add_argument("--threads", type=int, default=1, help="Use this many threads to evaluate queries (1 by default)")
        subparser.add_argument("--timeout", type=int, default=3600, help="Timeout in seconds per query (3600 by default)")
        subparser.add_argument("--delete", action="store_true", help="Delete CodeQL DBs/Sources after processing (only if no results found)")
        subparser.add_argument("--rerun", action="store_true", help="Force rerun of queries even if marked OK in JSON")
        subparser.add_argument("--codeql-cmd", default="codeql", help="CodeQL executable path")

    # --- Java Parser ---
    java_parser = subparsers.add_parser("java", help="Run the Java pipeline")
    add_common_args(java_parser)
    java_parser.add_argument("--vineflower-jar", help="Path to Vineflower JAR if not in the path.")
    java_parser.add_argument("--docker", help="Docker container ID/Name")
    java_parser.set_defaults(func=process_java)

    # --- .NET Parser ---
    dotnet_parser = subparsers.add_parser("dotnet", help="Run the .NET pipeline")
    add_common_args(dotnet_parser)
    dotnet_parser.add_argument("--dnspy-cli", default="dnSpy.Console.exe", help="Path to dnSpy CLI")
    dotnet_parser.add_argument("--json-deps", help="Optional JSON file for dependency mapping")
    dotnet_parser.add_argument("--max-depth", type=int, default=5, help="Max depth for dependency decompilation")
    dotnet_parser.set_defaults(func=process_dotnet)

    # --- Init Parser ---
    init_parser = subparsers.add_parser("init", help="Generate the JSON config file")
    init_parser.add_argument("--input", required=True, help="Path to a .lst file or a single file path")
    init_parser.add_argument("--json-path", required=True, help="Target path for the generated JSON")
    init_parser.set_defaults(func=run_init)

    signal.signal(signal.SIGINT, handle_ctrl_c)

    args = parser.parse_args()
    args.func(args)

if __name__ == "__main__":
    main()