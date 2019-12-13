Param(
    [Parameter(Mandatory = $true)]    
    [string]
    $OutputDir,

    [Parameter(Mandatory = $true)]
    [string]
    $AppDir,

    [Parameter(Mandatory = $true)]
    [string]
    $AppDll,

    # [string]
    # $SwaggerFileName = 'swagger.json'

    [string]
    $ApiVersions = 'v1'
)

# Set environment variables for placeholders that will be flowed through to the application.
# The Swagger file will be generated with these placeholders included in it.
# During continuous deployment, these will be replaced with actual values related to the environment and published to APIM.
$env:SWAGGER_ENVIRONMENT_NAME = "{{SWAGGER_ENVIRONMENT_NAME}}"
$env:SWAGGER_API_HOST = "{{SWAGGER_API_HOST}}"
$env:SWAGGER_API_BASE_PATH = "{{SWAGGER_API_BASE_PATH}}"
$env:SWAGGER_SCHEME = "{{SWAGGER_SCHEME}}"

# Set to development hosting environment to skip Mutual Authentication (client certificate).
$Env:ASPNETCORE_ENVIRONMENT = "Development"

if (!(Test-Path $OutputDir)) {
    Write-Host 'Creating Swagger output directory: '$OutputDir
    New-Item -ItemType Directory -Force -Path $OutputDir
}

# Add provition to create swagger file for multiple versions
$allApiVersions = $ApiVersions -split ","

foreach ($apiVersion in $allApiVersions) {
    $sanitizedApiVersion = $apiVersion.Trim()

	# Set to development hosting environment to skip Mutual Authentication (client certificate).
	$Env:ASPNETCORE_ENVIRONMENT = "Development"

    # Start a web server to host the API
    Write-Host 'Creating swagger for version:'$sanitizedApiVersion
    Write-Host 'Starting API to retrieve Swagger definition...'
    $jobName = 'dotnet-api-host'
    $apiDllPath = [System.IO.Path]::Combine($AppDir, $AppDll)
    Write-Host 'Starting API from '$apiDllPath
    Start-Job -Name $jobName -ScriptBlock { Set-Location -LiteralPath $args[0]; dotnet $args[1] } -ArgumentList $AppDir, $apiDllPath
    Start-Sleep -s 10 # wait a few seconds for it to start
    $job = Get-Job

    try {
        # Get the Swagger file from the API
        $swaggerFile = [System.IO.Path]::Combine($OutputDir, "swagger_$sanitizedApiVersion.json") # Join-Path $OutputDir 'swagger.json'
        Invoke-RestMethod `
            -Uri http://localhost:5000/swagger/$sanitizedApiVersion/swagger.json `
            -OutFile $swaggerFile
        # Stop the API host server and print out any stdout logs
        Stop-Job -Job $job
        Write-Host 'Swagger file saved at: '$swaggerFile
    }
    catch {
        $ErrorMessage = $_.Exception.Message
        Write-Host 'An exception has occurred in the Swagger job : '
        Write-Host $ErrorMessage 
    }
    finally {
        $Env:ASPNETCORE_ENVIRONMENT = ""
        Receive-Job -Job $job
    }
}
