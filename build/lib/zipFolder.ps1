Param(
    [string]
    $SourceDirectory = 'output',

    [string]
    $TargetDirectory = 'publish',

    [string]
    $ZipFileName = 'app.zip'
)

# Compress published items as artifact.
# $zipContentPath = $(Resolve-Path $SourceDirectory).Path
$zipFilePath = [System.IO.Path]::Combine($TargetDirectory, $ZipFileName)

# Check if Target Directory exists. Create one if not.
if(!(Test-Path $TargetDirectory))
{
    New-Item -ItemType Directory -Force -Path $TargetDirectory
}
# Delete the zip file if already exists.
if (Test-Path $zipFilePath)
{
    Write-Host 'Delete existing file...'
    Remove-Item $zipFilePath -Force
}

Write-Host 'Started zipping...'
Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($SourceDirectory, $zipFilePath)

Write-Host 'Done zipping...'
