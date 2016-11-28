ECHO off
cls

@echo Starting WebApp build script
@echo ============================

call "%VS120COMNTOOLS%vsvars32.bat"

set CURR=%~dp0
set SRC=%CURR%\adir.photography

@echo  Starting MSBuild
call msbuild.exe /verbosity:n adir.photography.sln /p:Configuration=Debug /l:FileLogger,Microsoft.Build.Engine;logfile=build.log

Echo **********   Build Finished
pause
