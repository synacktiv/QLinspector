#!/bin/bash

# Configuration
JAR_LIST="$1"                     # Text file containing paths to jar files in container
SRC_DIR="/home/user/Documents/pentest/RD/gadget/sources/keycloack"
DB_DIR="/home/user/Documents/pentest/RD/gadget/files/codeql/databases"
SARIF_DIR="/home/user/Documents/pentest/RD/gadget/files/codeql/sarif"
DECOMP_JAR="/home/user/Documents/pentest/tools/vineflower-1.11.2.jar" # Path to FernFlower jar
CODEQL_CMD="codeql"      # Path to codeql executable
QLS_FILE="/mnt/mount/tools/QLinspector/ql/java/src/suites/qlinspector-java.qls"  # CodeQL query suite
STATE_FILE="$SRC_DIR/.processed_jars"  # Track last processed jars
DOCKER_KC="1b9fcd33885f"


mkdir -p "$SRC_DIR"
mkdir -p "$DB_DIR"
mkdir -p "$SARIF_DIR"

# Function: fetch jar from docker
fetch_jar() {
    local container_path="$1"
    local local_path="$2"
    echo "[INFO] Fetching $container_path to $local_path"
    docker cp "$DOCKER_KC:$container_path" "$local_path" || { echo "[ERROR] Failed to fetch $container_path"; return 1; }
}

# Function: decompile using FernFlower
decompile_jar() {
    local src_dir="$1"
    local decomp_dir="$2"
    mkdir -p "$decomp_dir"
    echo "[INFO] Decompiling $src_dir -> $decomp_dir"
    java -jar "$DECOMP_JAR" "$src_dir" "$decomp_dir" || { echo "[ERROR] Decompilation failed"; return 1; }
}

# Function: create CodeQL database
create_codeql_db() {
    local decomp_dir="$1"
    local db_dir="$2"
    mkdir -p "$db_dir"
    echo "[INFO] Creating CodeQL database in $db_dir"
    "$CODEQL_CMD" database create "$db_dir" --overwrite --language=java --build-mode=none --source-root="$decomp_dir" || { echo "[ERROR] CodeQL DB creation failed"; return 1; }
}

# Function: run CodeQL queries
run_codeql_queries() {
    local db_dir="$1"
    local sarif_output="$2"
    echo "[INFO] Running CodeQL queries on $db_dir -> $sarif_output"
    "$CODEQL_CMD" database analyze "$db_dir" "$QLS_FILE" --rerun --no-sarif-add-file-contents --no-sarif-add-snippets --max-paths=2 --format=sarif-latest --output="$sarif_output" || { echo "[ERROR] CodeQL analysis failed"; return 1; }
}

# Function: cleanup if no results
cleanup_if_empty() {
    local jar_name="$1"
    local src_dir="$2"
    local decomp_dir="$3"
    local db_dir="$4"
    local sarif_output="$5"
    local jar_local="$6"

    if [ -f "$sarif_output" ]; then
        # Parse SARIF with jq to see if "results" array is empty
        local result_count
        result_count=$(jq '[.runs[].results[]?] | length' "$sarif_output" 2>/dev/null || echo 0)

        if [ "$result_count" -eq 0 ]; then
            echo "[INFO] No CodeQL results for $jar_name, cleaning up..."
            rm -rf "$src_dir" "$decomp_dir"
            rm -rf "$db_dir"
            rm -f "$sarif_output"
            rm -f "$jar_local"
        else
            echo "[INFO] Found $result_count results for $jar_name, keeping files."
        fi
    else
        echo "[INFO] SARIF file not found for $jar_name, cleaning up..."
        rm -rf "$src_dir" "$decomp_dir"
        rm -rf "$db_dir"
    fi
}

# Function: process single jar
process_jar() {
    local jar_path="$1"

    local jar_name=$(basename "$jar_path" .jar)
    local jar_local="$SRC_DIR/$jar_name.jar"
    local jar_src_dir="$SRC_DIR/$jar_name"
    local decomp_dir="$SRC_DIR/${jar_name}_decomp"
    local db_dir="$DB_DIR/${jar_name}_db"
    local sarif_output="$SARIF_DIR/${jar_name}.sarif"


    # Fetch jar if not present
    if [ ! -f "$jar_local" ]; then
        fetch_jar "$jar_path" "$jar_local" || return
    else
        echo "[INFO] Jar $jar_local already exists, skipping fetch."
    fi

    # Decompile if decomp folder does not exist
    if [ ! -d "$decomp_dir" ]; then
        decompile_jar "$jar_local" "$decomp_dir" || return
    else
        echo "[INFO] Decompiled folder $decomp_dir already exists, skipping decompile."
    fi

    create_codeql_db "$decomp_dir" "$db_dir" || return
    run_codeql_queries "$db_dir" "$sarif_output" || return
    cleanup_if_empty "$jar_name" "$jar_src_dir" "$decomp_dir" "$db_dir" "$sarif_output" "$jar_local"

    # Update state file for resuming
    echo "$jar_path" >> "$STATE_FILE"
}

# Main loop
resume=false
[ ! -f "$STATE_FILE" ] && resume=true

last_processed=""
[ -f "$STATE_FILE" ] && last_processed=$(tail -n 1 "$STATE_FILE")

while IFS= read -r jar_path || [[ -n "$jar_path" ]]; do
    # Skip until last processed jar
    if ! $resume; then
        if [[ "$jar_path" == "$last_processed" ]]; then
            resume=true
            continue    # start AFTER this one
        fi
        continue
    fi


    echo "[INFO] Processing jar: $jar_path"
    process_jar "$jar_path"
done < "$JAR_LIST"

echo "[INFO] All done!"