@echo off
echo Compile and reloading script assembly...

.\bin\csc.exe /target:library /out:".\lib\EngineAPI.dll" ".\Engine\*.cs" /reference:System.dll
if errorlevel 1 (
    echo.
    echo ERROR: C# compilation failed.
    goto :end
)

setlocal EnableDelayedExpansion
set "VBFILES="
for /R ".\assets\Scripts" %%F in (*.vb) do (
    set "VBFILES=!VBFILES! "%%F""
)

.\bin\vbc.exe /target:library /reference:".\lib\EngineAPI.dll" /reference:"System.Linq.dll" /out:".\lib\Assembly-VBNET.dll" %VBFILES% /nowin32manifest
if errorlevel 1 (
    echo.
    echo ERROR: VB library compilation failed.
    goto :end
)


.\bin\vbc.exe /target:exe src\*.vb /reference:".\lib\Assembly-VBNET.dll" /nologo /win32icon:icon.ico /out:HacknetCMD.exe /nowin32manifest
if errorlevel 1 (
    echo.
    echo ERROR: EXE compilation failed.
    goto :end
)

copy .\lib\discord-rpc.dll . > nul

:end
pause