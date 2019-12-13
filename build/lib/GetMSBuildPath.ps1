Write-Host 'Getting MSBUILD Path For vS2017..'

$operatingSystemMsbuildVariable = "msbuild"

if (Get-Command $operatingSystemMsbuildVariable -errorAction SilentlyContinue)
{
    $msBuildPath = Get-Command $operatingSystemMsbuildVariable | Select-Object -ExpandProperty Source
    return $msBuildPath;
}

if ($env:MsbuildExePath) 
{
    return $env:MsbuildExePath
}

if ($env:MSBUILD) 
{
    return $env:MSBUILD
}

$msBuildPossiblePaths = 
@(
    'C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe',
    'C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\msbuild.exe',
    'C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe'
)

foreach ($msBuildPossiblePath in $msBuildPossiblePaths) 
{   
    if ([System.IO.File]::Exists($msBuildPossiblePath)) 
    {
        [Environment]::SetEnvironmentVariable("MsbuildExePath", $msBuildPossiblePath)
        return $env:MsbuildExePath
    }
}

if(!$env:MsbuildExePath)
{
     throw [System.IO.FileNotFoundException] "MSBUILD Path not found."
}