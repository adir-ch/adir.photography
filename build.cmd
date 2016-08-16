ECHO off
cls

@echo Starting WebApp build script
@echo ============================

call "%VS120COMNTOOLS%vsvars32.bat"

set CURR=%~dp0
set SRC=%CURR%\adir.photography
SET DOTHIS=N

@echo:You are about to delete all dlls and pdbs under "%SRC%".

SET /P DOTHIS=Continue? (N to skip, otherwise to delete...)

if /I $%DOTHIS% == $Y (
	echo "Deleting old build data"
	del /S /F /Q %CURR%\adir.photography\bin\*.*
	del /S /F /Q %CURR%\PhotosRepository\bin\*.*
)

pause
@echo  Starting MSBuild
call msbuild.exe /verbosity:n adir.photography.sln /p:Configuration=Debug /l:FileLogger,Microsoft.Build.Engine;logfile=build.log

rem rd .\Output /S /Q
rem XCOPY .\adir.photography\Bin\*.* .\Output\

Echo **********   Build Finished

pause
