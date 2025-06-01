# Run as `..\build-and-zip.ps1 -ProjectName "CipherPlayground.CLI" -ExecutableName "CipherCLI" -Tag "v1.0.0"` from the project folder
# The call above: builds a standalone for the 3 big platforms, renames the executables to a simpler CipherCLI, and then packages them individually into zips for GitHub Releases

param (
    [string]$ProjectName = "CipherPlayground.CLI",
    [string]$ExecutableName = "CipherCLI",
    [string]$Tag = "v1.0.0"
)

$ErrorActionPreference = "Stop"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$logPath = Join-Path $scriptDir "build.log"
Start-Transcript -Path $logPath -Force

Write-Host "Script Directory: $scriptDir"

$publishRoot = Join-Path $scriptDir "bin\Release\net8.0\publish"
$finalZipOutput = Join-Path $scriptDir "publish"

if (-not (Test-Path $finalZipOutput)) {
    New-Item -ItemType Directory -Force -Path $finalZipOutput | Out-Null
}

$targets = @(
    @{ Rid = "win-x64";  Ext = ".exe"; Platform = "win" },
    @{ Rid = "linux-x64"; Ext = "";     Platform = "linux" },
    @{ Rid = "osx-x64";   Ext = "";     Platform = "osx" }
)

foreach ($target in $targets) {
    $rid = $target.Rid
    $ext = $target.Ext
    $platform = $target.Platform
    $publishDir = Join-Path $publishRoot $platform

    Write-Host "`nPublishing for $platform ($rid)..."

    if (Test-Path $publishDir) {
        Remove-Item -Recurse -Force $publishDir
    }
    New-Item -ItemType Directory -Path $publishDir | Out-Null

    try {
        dotnet publish `
            -c Release `
            -r $rid `
            --self-contained true `
            -p:PublishSingleFile=true `
            -p:IncludeNativeLibrariesForSelfExtract=true `
            -p:EnableCompressionInSingleFile=true `
            -o $publishDir

        Write-Host "Published to $publishDir"
    }
    catch {
        Write-Error "dotnet publish failed for $platform ($rid): $($_.Exception.Message)"
        continue
    }

    $foundExe = Get-ChildItem -Path $publishDir -Filter "*$ext" | Where-Object { $_.Name -like "*$ProjectName*$ext" } | Select-Object -First 1

    if ($foundExe) {
        $desiredPath = Join-Path $publishDir ($ExecutableName + $ext)
        Move-Item -Path $foundExe.FullName -Destination $desiredPath -Force
        Write-Host "Renamed $($foundExe.Name) to $ExecutableName$ext"
    }
    else {
        Write-Warning "Could not find executable to rename in $publishDir"
    }

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
