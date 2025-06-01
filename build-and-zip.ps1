# Run as `..\build-and-zip.ps1 -ProjectName "CipherPlayground.CLI" -ExecutableName "CipherCLI" -Tag "v1.0.0"` from the project folder
# The call above: builds a standalone for the 3 big platforms, renames the executables to a simpler CipherCLI, and then packages them individually into zips for GitHub Releases
# If you have trouble with Powershell: `Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass`

param (
    [string]$ProjectName = "CipherPlayground.CLI",
    [string]$ExecutableName = "CipherCLI",
    [string]$Tag = "v1.0.0"
)

$ErrorActionPreference = "Stop"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$logPath = Join-Path $scriptDir "build.log"

if ($host.Name -eq "ConsoleHost") {
    try {
        Start-Transcript -Path $logPath -Force
    } catch {
        Write-Warning "Failed to start transcript: $($_.Exception.Message)"
    }
} else {
    Write-Warning "Transcript skipped: not in interactive console host"
}

Write-Host "Script Directory: $scriptDir"

$publishRoot = Join-Path $scriptDir "bin\Release\net8.0\publish"
$finalZipOutput = Join-Path $scriptDir "publish"

if (-not (Test-Path $finalZipOutput)) {
    New-Item -ItemType Directory -Force -Path $finalZipOutput | Out-Null
}

$targets = @(
    @{ Rid = "win-x64";           Ext = ".exe"; Platform = "win-x64" },
    @{ Rid = "win-arm64";         Ext = ".exe"; Platform = "win-arm64" },
    @{ Rid = "linux-x64";         Ext = "";     Platform = "linux-x64" },
    @{ Rid = "linux-arm64";       Ext = "";     Platform = "linux-arm64" },
    @{ Rid = "linux-arm";         Ext = "";     Platform = "linux-arm" },
    @{ Rid = "linux-musl-x64";    Ext = "";     Platform = "linux-musl-x64" },
    @{ Rid = "linux-musl-arm64";  Ext = "";     Platform = "linux-musl-arm64" },
    @{ Rid = "osx-x64";           Ext = "";     Platform = "osx-x64" },
    @{ Rid = "osx-arm64";         Ext = "";     Platform = "osx-arm64" }
)

foreach ($target in $targets) {
    $rid = $target.Rid
    $ext = $target.Ext
    $platform = $target.Platform
    $publishDir = Join-Path $publishRoot $platform

    Write-Host "`nPublishing for $platform ($rid)..."

    # Clean previous publish output
    if (Test-Path $publishDir) {
        Remove-Item -Recurse -Force $publishDir
    }
    New-Item -ItemType Directory -Path $publishDir | Out-Null

    # Base publish parameters
    $publishParams = @(
        "-c", "Release",
        "-r", $rid,
        "--self-contained", "true",
        "-p:IncludeNativeLibrariesForSelfExtract=true",
        "-p:EnableCompressionInSingleFile=true",
        "-p:PublishSingleFile=true",
        "-o", $publishDir
    )

    # NO UseAppHost parameter for any platform

    try {
        dotnet publish @publishParams
        Write-Host "Published to $publishDir"
    }
    catch {
        Write-Error "dotnet publish failed for $platform ($rid): $($_.Exception.Message)"
        continue
    }

    # Find and rename executable if exists
    $foundExe = Get-ChildItem -Path $publishDir -Filter "*$ext" | Where-Object { $_.Name -like "*$ProjectName*$ext" } | Select-Object -First 1

    if ($foundExe) {
        $desiredPath = Join-Path $publishDir ($ExecutableName + $ext)
        Move-Item -Path $foundExe.FullName -Destination $desiredPath -Force
        Write-Host "Renamed $($foundExe.Name) to $ExecutableName$ext"
    }
    else {
        Write-Warning "Could not find executable to rename in $publishDir"
    }

    # Prepare zip file path
    $zipName = "$ProjectName-$Tag-$platform.zip"
    $zipPath = Join-Path $finalZipOutput $zipName

    Write-Host "Creating zip: $zipName..."

    try {
        $filesToZip = Get-ChildItem -Path $publishDir -Recurse | ForEach-Object { $_.FullName }
        Compress-Archive -Path $filesToZip -DestinationPath $zipPath -Force
        Write-Host "Created zip at: $zipPath"
    }
    catch {
        Write-Error ("Failed to create zip for {0}: {1}" -f $platform, $_.Exception.Message)
    }
}

Write-Host "`nAll done! Zips are located at: $finalZipOutput"
Stop-Transcript