$ErrorActionPreference = "Stop"

$assembliesIgnore = @(
"AppForeach.Framework.EntityFrameworkCore.Design"
)

$assemblies = Get-ChildItem $BaseDir -Recurse | Where-Object { $_.PSIsContainer -and $_.Name.StartsWith("AppForeach.Framework")} | Select-Object -ExpandProperty Name `
	| Where-Object { -Not ($assembliesIgnore -contains $_ )}

if(Test-Path .\out) {
    Write-Output "removing output..."
    Remove-Item .\out -Force -Recurse
}

[string]$tag = (git describe --exact-match --tags)
$foundVersion = $tag -match "v((\d+)\.(\d+)\.(\d+))"

if(!$foundVersion) {
	Write-Error "Version not found"
}

$version = $matches[1]

& dotnet restore --no-cache

foreach ($asm in $assemblies) {
	Push-Location $asm
	
	& dotnet build -c Release /property:Version=$version
	
	& dotnet pack -c Release --no-build /property:Version=$version -o ..\out
	
	Pop-Location
}