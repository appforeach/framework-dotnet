$ErrorActionPreference = "Stop"

$assemblies = @(
"AppForeach.Framework",
"AppForeach.Framework.Autofac",
"AppForeach.Framework.EntityFrameworkCore",
"AppForeach.Framework.EntityFrameworkCore.SqlServer",
"AppForeach.Framework.MassTransit",
"AppForeach.Framework.Microsoft.Extensions"
)

if(Test-Path .\out) {
    Write-Output "removing output..."
    Remove-Item .\out -Force -Recurse
}

& dotnet restore --no-cache

foreach ($asm in $assemblies) {
	Push-Location $asm
	
	& dotnet build -c Release
	
	& dotnet pack -c Release --no-build -o ..\out
	
	Pop-Location
}