echo off
cls

echo Apagando diretorios temporarios
rd /S /Q coverareport
rd /S /Q .\People.Tests\TestResults

echo.
echo Instalando tool reportgenerator
dotnet tool install -g dotnet-reportgenerator-globaltool

echo.
echo Executando testes unitarios
dotnet test --collect:"XPlat Code Coverage" --nologo -v q /clp:"ErrorsOnly"

echo.
echo Gerando relatorio
reportgenerator.exe -reports:".\People.Tests\TestResults\*\coverage.cobertura.xml" -targetdir:"coverareport" -reporttypes:Html

start coverareport\index.html

echo on