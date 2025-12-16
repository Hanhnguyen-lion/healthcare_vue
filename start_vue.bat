@echo off
cd %1
vue serve

cd %2
dotnet run
pause
