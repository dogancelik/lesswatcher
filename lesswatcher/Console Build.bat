@echo off

echo lesswatcher Command Line Build Tool
echo ===================================
echo This is a helper tool for building lesswatcher
echo This tool is optional so you don't really have to use this.
echo You can build the project from Visual Studio itself
echo.

echo Options
echo =======
echo Use "build" argument to build without ILMerge (ex: build.bat build)
echo Use "buildmerge" argument to build with ILMerge (ex: build.bat buildmerge)
echo.

if "%1"=="build" goto build1
if "%1"=="buildmerge" goto build2

if not "%1"=="" goto console
echo No argument found.
goto console

:console
echo Please choose a build:
echo 	(1) Build with ILMerge
echo 	(2) Build without ILMerge
echo 	(Anything except 1,2) Exit
set /p buildtype= (1/2)?

set props=Configuration=Release

if "%buildtype%"=="1" goto build1
if "%buildtype%"=="2" goto build2
goto end

:build1
echo You selected to build with ILMerge
msbuild /property:%props% lesswatcher.csproj
goto end

:build2
goto preparebuild
echo You selected to build without ILMerge
copy incl/dotless.Core.dll output/dotless.Core.dll
msbuild /property:%props%;PostBuildEvent= lesswatcher.csproj
goto end

:end
echo Build End