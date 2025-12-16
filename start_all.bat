@echo off
REM -- Start the ASP.NET Core Backend --
REM Change "ServerProjectName" to the actual name of your ASP.NET Core project folder/assembly name.
echo Starting ASP.NET Core backend...
cd %1
start cmd /k "dotnet run"
cd ..

echo Starting vue front-end...
cd %2
start cmd /k "vue serve"
cd ..

