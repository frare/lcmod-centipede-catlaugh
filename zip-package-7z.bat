@echo off
setlocal

:: Define the zip file name
set ZIP_FILE="mod.zip"

:: Define the file names to include
set FILE1="CHANGELOG.md"
set FILE2="icon.png"
set FILE3="manifest.json"
set FILE4="README.md"

:: Define the file extension to include
set EXTENSION="*.dll"

:: Change to the current folder
cd /d "%~dp0"

:: Check if 7z is available
where 7z.exe >nul 2>&1
if errorlevel 1 (
    echo 7-Zip is not installed or not in PATH.
    pause
    exit /b
)

:: Create the zip file with the specified files
7z a %ZIP_FILE% %FILE1% %FILE2% %FILE3% %FILE4% %EXTENSION%

:: Check if the zip operation was successful
if errorlevel 1 (
    echo Failed to create zip archive.
    pause
    exit /b
)

echo Files successfully zipped into %ZIP_FILE%.
pause