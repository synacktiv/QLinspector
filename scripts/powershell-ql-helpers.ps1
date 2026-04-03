<#
This code was almost entirely generated with AI because I'm too bad at powershell.
Any suggestions for improvement are greatly appreciated! :)
#>

function Set-CodeQLGlobalPaths {
    <#
    .SYNOPSIS
        Sets global environment variables for the CodeQL and dnSpy-based analysis pipeline.

    .DESCRIPTION
        This function initializes all required global paths for tools and output folders, including:
        - dnSpy CLI path
        - CodeQL CLI path
        - Sources output
        - Databases output
        - SARIF results path
        - CodeQL queries path

    .PARAMETER DnSpyPath
        Optional. Full path to dnSpy.Console.exe.

    .PARAMETER DnSpyOut
        Optional. Directory where dnSpy will output decompiled sources.

    .PARAMETER CodeQLPath
        Optional. Full path to codeql.exe.

    .PARAMETER CodeQLDbOut
        Optional. Root folder where CodeQL will output databases.

    .PARAMETER QueryPath
        Optional. Path to QLinspector C# query folder.

    .PARAMETER SarifOut
        Optional. Output path for SARIF result files.

    .PARAMETER JQPath
        Optional. Full path to jq.exe.

    .EXAMPLE
        Set-CodeQLGlobalPaths -CodeQLPath "D:\tools\codeql.exe"

    .EXAMPLE
        Set-CodeQLGlobalPaths
    #>

    # default value to match my own values.
    param (
        [string]$DnSpyExCliPath  = "F:\tools\dnSpy-net-win64\dnSpy.Console.exe",
        [string]$DnSpyOut        = "C:\Users\user\Documents\Pentest\RD\Gadgets\Sources\",
        [string]$CodeQLPath      = "C:\Users\user\Documents\Pentest\Tools\codeql-bundle\codeql\codeql.exe",
        [string]$CodeQLDbOut     = "C:\Users\user\Documents\Pentest\RD\Gadgets\Files\Codeql\Databases",
        [string]$QueryPath       = "F:\tools\QLinspector\ql\csharp\src\queries\",
        [string]$SarifOut        = "C:\Users\user\Documents\Pentest\RD\Gadgets\Files\Codeql\Sarif",
        [string]$JQPath          = "C:\Users\user\Documents\Pentest\Tools\jq-windows-amd64.exe"
    )

    $global:DnSpyExCliPath            = $DnSpyExCliPath
    $global:DnSpyOutputFolder         = $DnSpyOut
    $global:CodeQLCliPath             = $CodeQLPath
    $global:CodeQLDatabasesOutputRoot = $CodeQLDbOut
    $global:QLinspectorQueriesPath    = $QueryPath
    $global:SarifOutputRoot           = $SarifOut
    $global:JQLCliPath                = $JQPath

    Write-Host "[+] Global CodeQL environment paths set:"
    Write-Host "    dnSpy Path         : $DnSpyExCliPath"
    Write-Host "    dnSpy Output       : $DnSpyOutputFolder"
    Write-Host "    CodeQL Path        : $CodeQLCliPath"
    Write-Host "    CodeQL DB Output   : $CodeQLDatabasesOutputRoot"
    Write-Host "    Query Path         : $QLinspectorQueriesPath"
    Write-Host "    SARIF Output       : $SarifOutputRoot"
    Write-Host "    Jq path            : $JQLCliPath"
}


function Export-DotNetDlls {
    <#
    .SYNOPSIS
    Searches for .NET DLLs in a specified folder and exports their names, versions, and paths to a JSON file.

    .DESCRIPTION
    Recursively scans a folder for .NET assemblies (.dll files), retrieves their names, versions, and full paths, 
    and writes the information to a structured JSON file.

    .PARAMETER RootFolder
    Root directory to search for .NET DLL files.

    .PARAMETER DestinationFile
    Path to the JSON output file where DLL information will be saved.

    .EXAMPLE
    Export-DotNetDlls -RootFolder "C:\Projects" -DestinationFile "C:\output\DllList.json"

    .NOTES
    Non-.NET DLLs are skipped silently.
    #>
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$RootFolder,

        [Parameter(Mandatory = $true)]
        [string]$DestinationFile
    )

    if (-Not (Test-Path $RootFolder)) {
        Write-Error "The root folder '$RootFolder' does not exist."
        return
    }

    Write-Host "[+] Searching for .NET DLLs under: $RootFolder"
    Write-Host "[+] JSON results will be saved to: $DestinationFile"

    $assemblies = @()
    $seen = @{}

    Get-ChildItem -Path $RootFolder -Recurse -Filter "*.dll" -ErrorAction SilentlyContinue |
    ForEach-Object {
        try {
            $fileInfo = Get-Item $_.FullName
            $assembly = [System.Reflection.AssemblyName]::GetAssemblyName($_.FullName)
            $name = $assembly.Name.ToLowerInvariant()

            if (-not $seen.ContainsKey($name)) {
                $seen[$name] = $true

                # Calculate size in MB and cast to integer
                # Note: FileInfo.Length is in bytes.
                $sizeBytes = $fileInfo.Length
                $sizeMB = [Math]::Round($sizeBytes / 1MB, 2) # 1MB is a PowerShell automatic variable for 1024*1024 bytes

                $assemblies += [PSCustomObject]@{
                    Name    = $assembly.Name
                    Version = $assembly.Version.ToString()
                    Path    = $_.FullName
                    SizeMB  = $sizeMB
                    QL      = @{}
                }
            }
        } catch {
            # Skip non-.NET DLLs silently
        }
    }

    $result = [PSCustomObject]@{
        rootPath   = $RootFolder
        assemblies = $assemblies
    }

    $json = $result | ConvertTo-Json -Depth 5

    $json | Out-File -FilePath $DestinationFile -Encoding UTF8

    Write-Host "[+] JSON export complete."
}



function Decompile-DllWithDnSpyEx {
    <#
    .SYNOPSIS
        Decompiles a .NET DLL using dnSpy Console and saves the source code.

    .DESCRIPTION
        Uses dnSpy.Console.exe to decompile a .NET DLL and generate a Visual Studio solution in the output folder.
        If a JSON dependency file exists, it will decompile all referenced DLLs recursively.

    .PARAMETER DllPath
        Path to the .NET DLL file to decompile.

    .PARAMETER OutputFolder
        Optional path to the folder where the solution will be generated.

    .PARAMETER JsonDependencies
        Optional path to the JSON file containing dependency info.

    .PARAMETER MaxDepth
        Maximum recursion depth for dependency decompilation (default: 5).
    #>

    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$DllPath,

        [string]$OutputFolder,

        [string]$JsonDependencies,

        [int]$MaxDepth = 5
    )

    if (-not (Test-Path $DllPath)) {
        Write-Error "DLL path '$DllPath' does not exist."
        return
    }

    if (-not (Test-Path $DnSpyOutputFolder)) {
        New-Item -ItemType Directory -Path $DnSpyOutputFolder -Force | Out-Null
    }

    $dllName = [System.IO.Path]::GetFileNameWithoutExtension($DllPath)

    if (-not $OutputFolder) {
        $outputFolder = Join-Path -Path $DnSpyOutputFolder -ChildPath $dllName
    }

    if (-not (Test-Path $outputFolder)) {
        New-Item -ItemType Directory -Path $outputFolder -Force | Out-Null
    }

    $dlls = @()
    if ($JsonDependencies -and (Test-Path $JsonDependencies)) {
        Write-Host "[*] JSON dependency file found: $JsonDependencies"

        # Call your function that recursively extracts the DLL paths from JSON
        $dlls = Get-AssemblyReferencesFromJson -JsonFilePath $JsonDependencies -AssemblyPath $DllPath -MaxDepth $MaxDepth
    }

    $dlls += $DllPath
    $dlls = $dlls | Sort-Object -Unique

    & $DnSpyExCliPath  $dlls -o "$outputFolder" --sln-name "$dllName.sln"

    if ($LASTEXITCODE -eq 0) {
        Write-Host "[+] Solution created at: $outputFolder"
    } else {
        Write-Error "[x] Failed to create solution."
    }
}

function Get-AssemblyReferencesFromJson {
    <#
    .SYNOPSIS
    Recursively retrieves assembly reference file paths from a JSON dependency file.

    .DESCRIPTION
    Reads a JSON file describing assemblies and their references. 
    You must specify either an AssemblyName (to search by name, preferring GAC), or an AssemblyPath (to search by exact file path).
    Recursively collects all referenced DLL paths, limited by MaxDepth.

    .PARAMETER JsonFilePath
    Path to the JSON file containing assembly dependencies.

    .PARAMETER AssemblyName
    The root assembly name to start resolving references from. Mutually exclusive with AssemblyPath.

    .PARAMETER AssemblyPath
    The full file path of the root assembly. Mutually exclusive with AssemblyName.

    .PARAMETER MaxDepth
    Maximum recursion depth. Defaults to 3.

    .EXAMPLE
    Get-AssemblyReferencesFromJson -JsonFilePath "assemblies.json" -AssemblyName "PresentationFramework" -MaxDepth 2

    .EXAMPLE
    Get-AssemblyReferencesFromJson -JsonFilePath "assemblies.json" -AssemblyPath "C:\Path\To\My.dll" -MaxDepth 2
    #>

    [CmdletBinding(DefaultParameterSetName='ByName')]
    param(
        [Parameter(Mandatory, ParameterSetName='ByName')]
        [string]$AssemblyName,

        [Parameter(Mandatory, ParameterSetName='ByPath')]
        [string]$AssemblyPath,

        [Parameter(Mandatory)]
        [string]$JsonFilePath,

        [int]$MaxDepth = 3
    )

    # Load JSON into objects
    $assemblies = Get-Content $JsonFilePath -Raw | ConvertFrom-Json

    $visited = @{}  # To avoid cycles

    function RecurseReferences($assembly, $remainingDepth) {
        if ($null -eq $assembly -or $visited.ContainsKey($assembly.FilePath) -or $remainingDepth -le 0) {
            return @()
        }

        $visited[$assembly.FilePath] = $true
        $paths = @($assembly.FilePath)

        foreach ($ref in $assembly.References) {
            $paths += $ref.FilePath
            $paths += RecurseReferences $ref ($remainingDepth - 1)
        }

        return $paths
    }

    # Find root assembly
    $root = $null

    if ($PSCmdlet.ParameterSetName -eq 'ByPath') {
        # Match by full FilePath
        $root = $assemblies | Where-Object { $_.FilePath -ieq (Resolve-Path $AssemblyPath).Path } | Select-Object -First 1
        if (-not $root) {
            Write-Warning "Assembly at path '$AssemblyPath' not found in $JsonFilePath"
            return @()
        }
    }
    elseif ($PSCmdlet.ParameterSetName -eq 'ByName') {
        $rootCandidates = $assemblies | Where-Object { $_.AssemblyName -eq $AssemblyName }

        # Prefer GAC
        $root = $rootCandidates | Where-Object { $_.FilePath -match '\\assembly\\GAC_' } | Select-Object -First 1

        # Fallback to any match
        if (-not $root) {
            $root = $rootCandidates | Select-Object -First 1
        }

        if (-not $root) {
            Write-Warning "Assembly '$AssemblyName' not found in $JsonFilePath"
            return @()
        }
    }

    # Recurse dependencies and return unique, sorted paths
    $allPaths = RecurseReferences $root $MaxDepth
    return $allPaths | Sort-Object -Unique
}





function Create-CodeQLDatabase {
    <#
    .SYNOPSIS
    Creates a CodeQL database from a Visual Studio solution.

    .DESCRIPTION
    Runs CodeQL CLI to generate a database for the C# source code located in the specified folder.

    .PARAMETER SolutionFolder
    Folder containing the .sln file for the project.

    .PARAMETER DBName
    Specify the CodeQL DB name. If not present it's extracted from the sln file.

    .EXAMPLE
    Create-CodeQLDatabase -SolutionFolder "C:\Decompiled\MyLibrary"
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$SolutionFolder,

        [Parameter()]
        [string]$DBName
    )

    $slnFile = Get-ChildItem -Path $SolutionFolder -Filter *.sln | Select-Object -First 1

    if (-not $slnFile) {
        Write-Error "No .sln file found in folder: $SolutionFolder"
        return
    }

    if($DBName){
        $outputFolder = Join-Path -Path $CodeQLDatabasesOutputRoot -ChildPath $DBName
    }else{
        $solutionName = [System.IO.Path]::GetFileNameWithoutExtension($slnFile.Name)
        $outputFolder = Join-Path -Path $CodeQLDatabasesOutputRoot -ChildPath $solutionName
    }

    if (-not (Test-Path $outputFolder)) {
        New-Item -ItemType Directory -Path $outputFolder -Force | Out-Null
    }

    & $CodeQLCliPath database create "$outputFolder" `
        --language=csharp `
        --source-root "$SolutionFolder" `
        --build-mode=none

    if ($LASTEXITCODE -eq 0) {
        Write-Host "[+] CodeQL database created at: $outputFolder"
    } else {
        Write-Error "[x] Failed to create CodeQL database."
    }
}


function Get-SarifResultCount {
    <#
    .SYNOPSIS
        Counts the number of results in a SARIF report using jq.

    .DESCRIPTION
        Uses the jq CLI to parse the SARIF file and count results in the first run block.

    .PARAMETER SarifPath
        Path to the SARIF file.

    .EXAMPLE
        Get-SarifResultCount -SarifPath "C:\Reports\MyLibrary.sarif"
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$SarifPath
    )

    if (-Not (Test-Path $SarifPath)) {
        Write-Error "SARIF file not found: $SarifPath"
        return
    }

    try {
        $jqQuery = 'if (.runs | length) > 0 and .runs[0].results then .runs[0].results | length else 0 end'
        $resultCount = & $JQLCliPath $jqQuery "$SarifPath" 2>$null

        if (-Not $?) {
            Write-Error "jq failed to parse the SARIF file."
            return
        }

        if ($resultCount -match '^\d+$') {
            Write-Host "[+] Total results found: $resultCount"
            return [int]$resultCount
        } else {
            Write-Warning "[!] No results found or unexpected format."
            return 0
        }
    }
    catch {
        Write-Error "Unexpected error: $_"
        return
    }
}

function Filter-SarifByThreadFlowLength {
    <#
    .SYNOPSIS
        Filters SARIF results based on maximum thread flow step count using jq.

    .PARAMETER InputSarif
        Path to the input SARIF file.

    .PARAMETER OutputSarif
        (Optional) Path to write the filtered SARIF file. If not specified, appends -filter-<max>.sarif to the input name.

    .PARAMETER MaxSteps
        Maximum allowed steps (locations) in threadFlows[0]. Only results below or equal to this are kept.
        Default is 30.

    .EXAMPLE
        Filter-SarifByThreadFlowLength -InputSarif "report.sarif"
        # Filters thread flows with max 30 steps and outputs report-filter-30.sarif
    #>

    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$InputSarif,

        [string]$OutputSarif,

        [int]$MaxSteps = 42
    )

    if (-Not (Test-Path $InputSarif)) {
        Write-Error "Input SARIF file not found: $InputSarif"
        return
    }

    if (-not $OutputSarif) {
        $base = [System.IO.Path]::GetFileNameWithoutExtension($InputSarif)
        $dir = [System.IO.Path]::GetDirectoryName($InputSarif)
        $OutputSarif = Join-Path $dir "$base-filtered-$MaxSteps.sarif"
    }

    $jqQuery = @"
.runs[0].results |= map(
    select(
        (.codeFlows | not) or
        (
            .codeFlows | all(
                .threadFlows | all(
                    (.locations | length) <= $MaxSteps
                )
            )
        )
    )
)
"@

    try {
        & $JQLCliPath $jqQuery "$InputSarif" | Out-File -FilePath "$OutputSarif" -Encoding UTF8

        if ($LASTEXITCODE -ne 0) {
            Write-Error "jq failed to filter the SARIF file."
            return
        }

        Write-Host "[+] Filtered SARIF written to: $OutputSarif"
    }
    catch {
        Write-Error "Unexpected error while running jq: $_"
    }
}

function Get-ShortestSarifPaths {
    <#
    .SYNOPSIS
        Extracts and returns the shortest thread flow path lengths from a SARIF file using jq.

    .PARAMETER SarifPath
        Path to the SARIF file.

    .PARAMETER Top
        The number of shortest path lengths to return. Default is 20.

    .OUTPUTS
        An array of integers representing thread flow lengths.

    .EXAMPLE
        $paths = Get-ShortestSarifPaths -SarifPath "report.sarif"
        # Stores the 20 shortest lengths in $paths

        Get-ShortestSarifPaths -SarifPath "report.sarif" -Top 5
        # Returns the 5 shortest
    #>

    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$SarifPath,

        [int]$Top = 20
    )

    if (-Not (Test-Path $SarifPath)) {
        Write-Error "SARIF file not found: $SarifPath"
        return $null
    }

    $jqQuery = "[.runs[].results[]?.codeFlows[]?.threadFlows[0]?.locations? | length | select(. > 0)] | sort | .[:$Top]"

    try {
        $shortestPathsJson = & $JQLCliPath "$jqQuery" "$SarifPath" 2>$null

        if ($LASTEXITCODE -ne 0 -or -not $shortestPathsJson) {
            Write-Error "Failed to extract data using jq."
            return $null
        }

        return $shortestPathsJson | ConvertFrom-Json
    }
    catch {
        Write-Error "Error while processing SARIF file: $_"
        return $null
    }
}



function Analyze-DllWithCodeQL {
    <#
    .SYNOPSIS
        Complete pipeline to decompile a DLL, create a CodeQL database, run one or more CodeQL queries,
        and optionally delete the database afterward. Supports timeouts and RAM limits.

    .DESCRIPTION
        Decompiles the specified DLL and creates a CodeQL database for it.

        You can provide either a direct DLL path or an assembly name from a JSON file. After database creation,
        the function runs one or more specified CodeQL queries unless disabled via -RunQuery:$false.

        Optionally, the database and temporary solution files can be deleted using -Delete.
        The process supports custom timeout and memory constraints for large analyses.

    .PARAMETER DllPath
        Full path to the DLL to analyze. Required if using the 'ByDllPath' parameter set.

    .PARAMETER AssemblyName
        Name of the assembly to look up in the JSON file to find the DLL path. Required if using the 'ByAssemblyName' parameter set.

    .PARAMETER JsonPath
        Optional path to the JSON metadata file (used only with AssemblyName lookup).

    .PARAMETER Queries
        One or more CodeQL query files or QL packs to run against the DLL's CodeQL database.

    .PARAMETER RunQuery
        Indicates whether to run the CodeQL query or queries after creating the database. Defaults to $true.

    .PARAMETER Delete
        If specified, deletes the generated CodeQL database and temporary files after analysis.

    .PARAMETER TimeoutSeconds
        Optional. The maximum allowed time (in seconds) to run each CodeQL query. Default is 3600 seconds (1 hour).

    .PARAMETER Ram
        Optional. The amount of RAM (in MB) to allocate for the CodeQL analysis.

    .PARAMETER JsonDependencies
        Optional path to the JSON file containing dependency info.

    .PARAMETER MaxDepth
        Maximum recursion depth for dependency decompilation (default: 5).

    .EXAMPLE
        Analyze-DllWithCodeQL -DllPath "C:\Projects\MyLibrary.dll" -Queries ".\queries\taint.ql"

    .EXAMPLE
        Analyze-DllWithCodeQL -AssemblyName "MyLibrary" -JsonPath ".\assemblies.json" -Queries "taint.ql" -Delete

    #>
    [CmdletBinding(DefaultParameterSetName = 'ByDllPath')]
    param (
        [Parameter(Mandatory = $true, ParameterSetName = 'ByDllPath')]
        [string]$DllPath,

        [Parameter(Mandatory = $true, ParameterSetName = 'ByAssemblyName')]
        [string]$AssemblyName,

        [Parameter(Mandatory = $false, ParameterSetName = 'ByAssemblyName')]
        [string]$JsonPath,

        [Parameter(Mandatory=$true, HelpMessage="One or more ql query.")]
        [string[]]$Queries,

        [Parameter()]
        [bool]$RunQuery = $true,

        [Parameter()]
        [switch]$Delete,

        [Parameter()]
        [int]$TimeoutSeconds = 3600,  # 1 hour default

        [Parameter()]
        [int]$Ram,

        [Parameter()]
        [string]$JsonDependencies,

        [Parameter()]
        [int]$MaxDepth = 5

    )

    if ($PSCmdlet.ParameterSetName -eq 'ByAssemblyName') {
        if (-not (Test-Path $JsonPath)) {
            Write-Error "JSON metadata file not found: $JsonPath"
            return
        }

        try {
            $json = Get-Content -Path $JsonPath -Raw | ConvertFrom-Json
            $match = $json.assemblies | Where-Object { $_.Name -eq $AssemblyName }

            if (-not $match) {
                Write-Error "Assembly '$AssemblyName' not found in JSON file."
                return
            }

            $DllPath = $match.Path
            if (-not (Test-Path $DllPath)) {
                Write-Error "Resolved DLL path '$DllPath' does not exist."
                return
            }

        } catch {
            Write-Error "Failed to parse JSON file: $_"
            return
        }
    }

    $DllName = ([System.Reflection.AssemblyName]::GetAssemblyName((Get-Item $DllPath).FullName)).Name

    Write-Host "[+] Step 1: Decompiling $DllName..."
    Decompile-DllWithDnSpyEx -DllPath $DllPath -JsonDependencies $JsonDependencies -MaxDepth $MaxDepth

    $SolutionFolder = Join-Path -Path $DnSpyOutputFolder -ChildPath $DllName
    Write-Host "[+] Step 2: Creating CodeQL database for $DllName..."
    Create-CodeQLDatabase -SolutionFolder $SolutionFolder

    if ($RunQuery) {
        $DatabaseFolder = Join-Path -Path $CodeQLDatabasesOutputRoot -ChildPath $DllName
        Write-Host "[+] Step 3: Running CodeQL queries on $DllName..."

        foreach ($query in $Queries) {
            Run-CodeQLQuery-And-Update -AssemblyName $DllName -JsonPath $JsonPath -QueryName $query -TimeoutSeconds $TimeoutSeconds -Ram $Ram
        }

        if ($Delete -and (Test-Path $DatabaseFolder)) {
            Write-Host "[+] Deleting CodeQL database folder: $DatabaseFolder"
            Remove-Folder-Robust -Path $DatabaseFolder
            #Remove-Item -Recurse -Force -Path $SolutionFolder
        }
    } else {
        Write-Host "[+] Step 3: Skipped running CodeQL query."
    }

    Write-Host "[+] Analysis complete for: $DllName"
}

function Analyze-AllAssemblies {
    <#
    .SYNOPSIS
        Runs CodeQL analysis on multiple .NET assemblies, skipping those already processed.

    .DESCRIPTION
        This function reads a JSON file containing a list of .NET assemblies (DLLs) —
        typically the output of `Export-DotNetDlls`. It checks each entry to see if
        CodeQL analysis results already exist under the "QL" section.

        For each DLL that has not yet been analyzed, it runs the specified CodeQL query/queries.
        The analysis is subject to an optional timeout and memory usage limit.

        After analysis, results are saved or updated accordingly in the JSON file.

    .PARAMETER JsonPath
        The full path to the input JSON file containing the list of assemblies and their metadata.

    .PARAMETER Queries
        One or more CodeQL query files or QL packs to run against each DLL.

    .PARAMETER TimeoutSeconds
        Optional. The maximum allowed time (in seconds) for analyzing a single DLL.
        Default is 3600 seconds (1 hour).

    .PARAMETER Ram
        Optional. The amount of RAM (in MB) to allocate for each CodeQL database analyze run.

    .PARAMETER ReRun
        Re-run queries even if analysis was already done.

    .PARAMETER JsonDependencies
        Optional path to the JSON file containing dependency info.

    .PARAMETER MaxDepth
        Maximum recursion depth for dependency decompilation (default: 5).

    .EXAMPLE
        Analyze-AllAssemblies -JsonPath ".\assemblies.json" -Queries QLinspector.ql

        Analyzes all unprocessed DLLs in `assemblies.json` using the specified CodeQL query.

    .EXAMPLE
        Analyze-AllAssemblies -JsonPath ".\assemblies.json" -Queries QLinspector.ql,ObjectMethodSinkFinder.ql -Ram 8192 -TimeoutSeconds 1800 -ReRun

        Analyzes each DLL using multiple QL packs, with a 30-minute timeout and 8 GB RAM limit.
    #>
    param (
        [Parameter(Mandatory = $true)]
        [string]$JsonPath,

        [Parameter(Mandatory=$true, HelpMessage="One or more ql query.")]
        [string[]]$Queries,

        [Parameter()]
        [int]$TimeoutSeconds = 3600,  # 1 hour default

        [Parameter()]
        [int]$Ram,

        [Parameter()]
        [switch]$ReRun,

        [Parameter()]
        [string]$JsonDependencies,

        [Parameter()]
        [int]$MaxDepth = 5
    )

    if (-not (Test-Path $JsonPath)) {
        Write-Error "JSON file not found at path: $JsonPath"
        return
    }

    $jsonContent = Load-Or-Initialize-DllJson -JsonPath $JsonPath

    foreach ($assembly in $jsonContent.assemblies) {
        $dllName = $assembly.Name
        $dllPath = $assembly.Path

        if (-not (Test-Path $dllPath)) {
            Write-Warning "DLL path not found for assembly '$dllName': $dllPath. Skipping."
            continue
        }

        $index = Get-AssemblyIndex -JsonContent $JsonContent -AssemblyName $DllName

        $skip = $true
        foreach ($query in $Queries) {
            if (-not $JsonContent.assemblies[$index].QL.$query) {
                $skip = $false
                break
            }
        }
    
        if($skip -and !$ReRun){
            Write-Host "[+] Analysis already done for $DllName, skipping."
        }else{
            Write-Host "[*] Analyzing assembly: $dllName"
            Analyze-DllWithCodeQL -AssemblyName $dllName -JsonPath $JsonPath -Delete -Queries $Queries -Ram $Ram -TimeoutSeconds $TimeoutSeconds -JsonDependencies $JsonDependencies -MaxDepth $MaxDepth
        }
    }
}


# function for the result file manipulation
function Load-Or-Initialize-DllJson {
    param (
        [Parameter(Mandatory = $true)]
        [string]$JsonPath
    )

    if (-Not (Test-Path $JsonPath)) {
        Write-Error "JSON file not found: $JsonPath"
        return $null
    }

    try {
        $jsonContent = Get-Content $JsonPath -Raw | ConvertFrom-Json
        return $jsonContent
    }
    catch {
        Write-Error "Failed to load or parse JSON file: $_"
        return $null
    }
}

function Save-Json {
    param (
        [Parameter(Mandatory = $true)]
        [psobject]$JsonObject,

        [Parameter(Mandatory = $true)]
        [string]$JsonPath
    )

    try {
        $JsonObject | ConvertTo-Json -Depth 10 | Out-File -FilePath $JsonPath -Encoding UTF8
        Write-Host "[+] JSON saved to $JsonPath"
    }
    catch {
        Write-Error "Failed to save JSON to $JsonPath. $_"
    }
}

enum QueryResultStatus {
    OK
    Timeout
    ERROR
}

function Set-QueryStatus {
    param (
        [Parameter(Mandatory = $true)]
        [psobject]$JsonContent,

        [Parameter(Mandatory = $true)]
        [string]$AssemblyName,

        [Parameter(Mandatory = $true)]
        [string]$QueryName,

        [Parameter(Mandatory = $true)]
        [QueryResultStatus]$Status
    )

    $index = Get-AssemblyIndex -JsonContent $JsonContent -AssemblyName $AssemblyName 

    if ($null -ne $index) {
        # Direct reference
        $assemblyRef = $JsonContent.assemblies[$index]
        $assemblyRef.QL.$QueryName.ResultStatus = $Status.ToString()

        # Reassign it
        $JsonContent.assemblies[$index] = $assemblyRef
    }
}


function Update-QueryResults {
    param (
        [Parameter(Mandatory = $true)]
        [psobject]$JsonContent,

        [Parameter(Mandatory = $true)]
        [string]$AssemblyName,

        [Parameter(Mandatory = $true)]
        [string]$QueryName,

        [Parameter(Mandatory = $true)]
        [string]$SarifPath
    )

    if (-Not (Test-Path $SarifPath)) {
        Write-Error "SARIF file not found: $SarifPath"
        return
    }

    try {
        $resultCount = Get-SarifResultCount -SarifPath $SarifPath

        if($resultCount -ge 250){
            $base = [System.IO.Path]::GetFileNameWithoutExtension($SarifPath)
            $dir = [System.IO.Path]::GetDirectoryName($InputSarif)
            $OutputSarif = Join-Path $dir "$base-filtered.sarif"
            Filter-SarifByThreadFlowLength -InputSarif $SarifPath -OutputSarif $OutputSarif

            $SarifPath = $OutputSarif
        }

        $shortest = Get-ShortestSarifPaths -SarifPath $SarifPath

        $index = Get-AssemblyIndex -JsonContent $JsonContent -AssemblyName $AssemblyName 

        if ($null -ne $index) {

            $assemblyRef = $JsonContent.assemblies[$index]

            if ($assemblyRef.QL.$QueryName.NumberOfResults -ne $resultCount){
                $assemblyRef.QL.$QueryName.Changed = $true
            }else{
                $assemblyRef.QL.$QueryName.Changed = $false
            }

            $assemblyRef.QL.$QueryName.NumberOfResults = $resultCount
            $assemblyRef.QL.$QueryName.Top20ShortestPaths = @($shortest) # weird behavior with return value

            $JsonContent.assemblies[$index] = $assemblyRef
        }
    }
    catch {
        Write-Error "Failed to parse SARIF file: $_"
    }
}



function Run-CodeQLQuery-And-Update {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$AssemblyName,

        [Parameter(Mandatory = $true)]
        [string]$JsonPath,

        [Parameter()]
        [string]$QueryName = "QLinspector.ql",

        [Parameter()]
        [int]$TimeoutSeconds = 3600,  # 1 hour default

        [Parameter()]
        [int]$Ram
    )

    $jsonExists = Test-Path $JsonPath

    # Construct full query path
    if (-not (Test-Path $QueryName)) {
        $QueryPath = Join-Path -Path $QLinspectorQueriesPath -ChildPath $QueryName
        if (-not (Test-Path $QueryPath)) {
            Write-Error "Query file not found: $QueryPath"
            return
        }
    }

    $DatabaseFolder = Join-Path -Path $CodeQLDatabasesOutputRoot -ChildPath $AssemblyName
    $SarifFile = Join-Path -Path $SarifOutputRoot -ChildPath "$AssemblyName-$QueryName.sarif"

    if (-not (Test-Path $DatabaseFolder)) {
        Write-Error "CodeQL database folder not found: $DatabaseFolder"
        return
    }

    if (-not (Test-Path $SarifOutputRoot)) {
        New-Item -ItemType Directory -Path $SarifOutputRoot -Force | Out-Null
    }

    $skipQuery = $false

    if ($jsonExists) {

        # Load JSON
        $JsonContent = Load-Or-Initialize-DllJson -JsonPath $JsonPath
        if (-not $JsonContent) {
            Write-Error "Failed to load JSON, aborting."
            return
        }

        # Find Assembly Entry
        $index = Get-AssemblyIndex -JsonContent $JsonContent -AssemblyName $AssemblyName 
        if ($index -eq $null) {
            Write-Error "Assembly '$AssemblyName' not found in JSON."
            return
        }

        # Create 'QL' if missing or not PSCustomObject
        if (-not $JsonContent.assemblies[$index].PSObject.Properties['QL'] -or
            -not ($JsonContent.assemblies[$index].QL -is [PSCustomObject])) {
            $JsonContent.assemblies[$index] | Add-Member -MemberType NoteProperty -Name 'QL' -Value ([PSCustomObject]@{}) -Force
        }

        # Create QueryName entry inside QL if missing or not PSCustomObject
        if (-not $JsonContent.assemblies[$index].QL.PSObject.Properties[$QueryName] -or
            -not ($JsonContent.assemblies[$index].QL.$QueryName -is [PSCustomObject])) {

            $queryResultObject = [PSCustomObject]@{
                Top20ShortestPaths = @()
                ResultStatus       = ''
                NumberOfResults    = 0
                Changed            = $false
            }
            $JsonContent.assemblies[$index].QL | Add-Member -MemberType NoteProperty -Name $QueryName -Value $queryResultObject -Force
        }

        # Check if results already exist
        #if ($JsonContent.assemblies[$index].QL.$QueryName.ResultStatus) {
        #    Write-Host "[*] Results already exist for '$QueryName' on assembly '$AssemblyName'. Skipping."
        #    return
        #}
    }

    if (-not $skipQuery) {
        $status = Invoke-CodeQLQuery -DatabaseFolder $DatabaseFolder -QueryPath $QueryPath -SarifFile $SarifFile -TimeoutSeconds $TimeoutSeconds -Ram $Ram
    }

    if ($jsonExists) {
        Set-QueryStatus -JsonContent $JsonContent -AssemblyName $AssemblyName -QueryName $QueryName -Status $status
        if ($status -eq [QueryResultStatus]::OK) {
            Update-QueryResults -JsonContent $JsonContent -AssemblyName $AssemblyName -QueryName $QueryName -SarifPath $SarifFile
        }

        # Save updated JSON
        Save-Json -JsonObject $JsonContent -JsonPath $JsonPath
    }

    if ($status -eq [QueryResultStatus]::OK) {
        Write-Host "[+] Query $QueryName completed for '$AssemblyName'."
    }
    elseif ($status -eq [QueryResultStatus]::Timeout) {
        Write-Error "[x] Query $QueryName timed out for '$AssemblyName'."
    }
    else {
        Write-Error "[x] Query $QueryName failed for '$AssemblyName'."
    }
}



function Invoke-CodeQLQuery {
    <#
    .SYNOPSIS
        Executes a CodeQL query against a specified database and outputs results to a SARIF file.

    .DESCRIPTION
        This function invokes the CodeQL CLI to run a query on a specified CodeQL database.
        It formats the output as SARIF and writes it to a specified file. It also handles timeouts
        and basic error checking, returning a result status indicating success, timeout, or failure.

    .PARAMETER DatabaseFolder
        The path to the CodeQL database folder (e.g., output of `codeql database create`).

    .PARAMETER QueryPath
        The full path to the CodeQL query (`.ql`) file you want to run.

    .PARAMETER SarifFile
        The output SARIF file path where the query results should be saved.

    .PARAMETER TimeoutSeconds
        (Optional) Timeout in seconds for the CodeQL query to complete. Default is 3600 (1 hour).

    .PARAMETER Ram
        (Optional) Maximum RAM (in MB) for the CodeQL process. Passed via `--ram`.

    .EXAMPLE
        Invoke-CodeQLQuery -DatabaseFolder "C:\CodeQL\DBs\MyApp" -QueryPath ".\QL\FindVulns.ql" -SarifFile ".\Results\output.sarif"

        Runs the `FindVulns.ql` query on the CodeQL database for "MyApp", and writes results to `output.sarif`.

    .RETURNS
        A [QueryResultStatus] enum indicating:
            - OK: Query succeeded
            - Timeout: Query exceeded the allowed time
            - Error: Query failed

    .NOTES
        This function requires that `$CodeQLCliPath` be set to the full path of the `codeql` executable.
        Ensure that the database and query are compatible with each other (language, schema, etc.).

    #>
    param (
        [string]$DatabaseFolder,
        [string]$QueryPath,
        [string]$SarifFile,
        [int]$TimeoutSeconds = 3600,  # 1 hour default
        [int]$Ram
    )

    if (-not (Test-Path $DatabaseFolder)) {
        Write-Error "CodeQL database folder not found: $DatabaseFolder"
        return [QueryResultStatus]::Error
    }
    if (-not (Test-Path $QueryPath)) {
        Write-Error "Query file not found: $QueryPath"
        return [QueryResultStatus]::Error
    }

    $DatabaseFolder=(Get-Item $DatabaseFolder).FullName
    $QueryPath=(Get-Item $QueryPath).FullName

    if (-not (Test-Path $SarifFile)) {
        New-Item -ItemType File -Path $SarifFile -Force | Out-Null
    }

    $SarifFile = (Get-Item $SarifFile).FullName

    $escapedDatabase = $DatabaseFolder.Replace('"', '""')
    $escapedQuery = $QueryPath.Replace('"', '""')
    $escapedSarif = $SarifFile.Replace('"', '""')

    $arguments = "database analyze `"$escapedDatabase`" `"$escapedQuery`" --rerun --no-sarif-add-file-contents --no-sarif-add-snippets --max-paths=1 --format=sarif-latest --output=`"$escapedSarif`""

     if ($null -ne $Ram) {
        $arguments += " --ram $Ram"
    }

    $processInfo = New-Object System.Diagnostics.ProcessStartInfo
    $processInfo.FileName = $CodeQLCliPath
    $processInfo.Arguments = $arguments
    $processInfo.RedirectStandardOutput = $true
    $processInfo.RedirectStandardError = $true
    $processInfo.UseShellExecute = $false
    $processInfo.CreateNoWindow = $true

    $process = New-Object System.Diagnostics.Process
    $process.StartInfo = $processInfo
    $process.Start() | Out-Null

    $stopWatch = [System.Diagnostics.Stopwatch]::StartNew()

    while (-not $process.HasExited) {
        Start-Sleep -Seconds 1
        if ($stopWatch.Elapsed.TotalSeconds -ge $TimeoutSeconds) {
            $process.Kill()
            Write-Error "CodeQL query timed out after $TimeoutSeconds seconds."
            return [QueryResultStatus]::Timeout
        }
    }

    $stdout = $process.StandardOutput.ReadToEnd()
    $stderr = $process.StandardError.ReadToEnd()

    if ($process.ExitCode -eq 0) {
        Write-Host "[+] CodeQL query completed successfully."
        if ($stdout) { Write-Host "[Output] $stdout" }
        return [QueryResultStatus]::OK
    }
    else {
        Write-Error "CodeQL query failed with exit code $($process.ExitCode)."
        if ($stderr) { Write-Error "[Error Output] $stderr" }
        return [QueryResultStatus]::Error
    }
}

function Get-AssemblyIndex {
    param (
        [psobject]$JsonContent,
        [string]$AssemblyName
    )
    for ($i = 0; $i -lt $JsonContent.assemblies.Count; $i++) {
        if ($JsonContent.assemblies[$i].Name -eq $AssemblyName) {
            return $i
        }
    }
    return $null
}

# PowerShell Remove-Item command fails when deleting CodeQL databases.
function Remove-Folder-Robust {
    param (
        [Parameter(Mandatory = $true)]
        [string]$Path
    )

    if (-not (Test-Path $Path)) {
        Write-Host "[!] Folder not found: $Path"
        return
    }

    $tempEmpty = Join-Path $env:TEMP "empty_dir"

    # Create empty temp folder if needed
    if (-not (Test-Path $tempEmpty)) {
        New-Item -ItemType Directory -Path $tempEmpty | Out-Null
    }

    # Use robocopy to wipe the contents
    robocopy $tempEmpty $Path /MIR /NFL /NDL /NJH /NJS /NC /NS > $null

    # Now remove the empty root
    try {
        Remove-Item -Path $Path -Recurse -Force -ErrorAction Stop
        Write-Host "[+] Successfully deleted: $Path"
    }
    catch {
        Write-Warning "[-] Failed to remove folder: $Path - $_"
    }
}
