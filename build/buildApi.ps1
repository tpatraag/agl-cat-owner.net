Param(
    [switch]
    $Test,

    [switch]
    $Publish,

    [string]
    $Configuration = 'Release',

    [string]
    $BuildNumber = 'local',

    [string]
    $Solution = '*.sln'
)

# $sourceDir = [System.IO.Path]::Combine($PSScriptRoot, '..', 'src')

# Push-Location $sourceDir

# Restore packages
& dotnet restore $Solution
if($LASTEXITCODE -ne 0) { exit 1 }

# Run builds
& dotnet build --no-restore --configuration $Configuration --version-suffix $BuildNumber $Solution
if($LASTEXITCODE -ne 0) { exit 2 }
