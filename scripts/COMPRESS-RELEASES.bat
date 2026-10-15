@echo off
REM ═══════════════════════════════════════════════════════════════════════════
REM   StockControl v2.0 - Compress Releases
REM   Script para comprimir releases para upload no GitHub
REM ═══════════════════════════════════════════════════════════════════════════

setlocal enabledelayedexpansion

cls
color 0A

title StockControl v2.0 - Compress Releases

REM Obter o diretório do projeto
set PROJECT_PATH=%~dp0..
set RELEASES_DIR=%PROJECT_PATH%\releases
set COMPRESSED_DIR=%PROJECT_PATH%\releases-compressed

echo.
echo ╔═══════════════════════════════════════════════════════════════════════════╗
echo ║                 StockControl v2.0 - Compress Releases                     ║
echo ║                                                                           ║
echo ║  Comprimindo executáveis para upload no GitHub...                         ║
echo ║                                                                           ║
echo ╚═══════════════════════════════════════════════════════════════════════════╝
echo.

echo Projeto:    %PROJECT_PATH%
echo Releases:   %RELEASES_DIR%
echo Comprimido: %COMPRESSED_DIR%
echo.

REM Verificar se a pasta de releases existe
if not exist "%RELEASES_DIR%" (
    echo [ERRO] Pasta de releases não encontrada!
    echo        Execute BUILD-RELEASES.bat primeiro.
    echo.
    pause
    exit /b 1
)

REM Criar diretório para comprimidos
if not exist "%COMPRESSED_DIR%" (
    mkdir "%COMPRESSED_DIR%"
    echo [INFO] Diretório criado: %COMPRESSED_DIR%
    echo.
)

echo ════════════════════════════════════════════════════════════════════════════
echo Iniciando compressão...
echo ════════════════════════════════════════════════════════════════════════════
echo.

set SUCCESS_COUNT=0
set FAIL_COUNT=0

REM Listar plataformas
set PLATFORMS[0]=StockControl-v2.0-Windows-x64
set PLATFORMS[1]=StockControl-v2.0-Windows-x86
set PLATFORMS[2]=StockControl-v2.0-Linux-x64
set PLATFORMS[3]=StockControl-v2.0-macOS-x64
set PLATFORMS[4]=StockControl-v2.0-macOS-ARM64

REM Verificar se PowerShell está disponível e usar Compress-Archive
for /L %%i in (0,1,4) do (
    set PLATFORM=!PLATFORMS[%%i]!
    set SOURCE_PATH=%RELEASES_DIR%\!PLATFORM!
    set OUTPUT_FILE=%COMPRESSED_DIR%\!PLATFORM!.zip
    
    if exist "!SOURCE_PATH!" (
        echo [INFO] Comprimindo: !PLATFORM!
        
        REM Usar PowerShell para comprimir
        powershell -Command "Compress-Archive -Path '!SOURCE_PATH!' -DestinationPath '!OUTPUT_FILE!' -Force" >nul 2>&1
        
        if !ERRORLEVEL! equ 0 (
            echo        [OK] Arquivo criado: !PLATFORM!.zip
            set /A "SUCCESS_COUNT+=1"
        ) else (
            echo        [ERRO] Falha ao comprimir !PLATFORM!
            set /A "FAIL_COUNT+=1"
        )
    ) else (
        echo [AVISO] Pasta não encontrada: !PLATFORM!
    )
    
    echo.
)

echo ════════════════════════════════════════════════════════════════════════════
echo Resumo Final
echo ════════════════════════════════════════════════════════════════════════════
echo ✓ Sucessos: %SUCCESS_COUNT%
echo ✗ Falhas:   %FAIL_COUNT%
echo.
echo Arquivos comprimidos salvos em: %COMPRESSED_DIR%
echo.

if exist "%COMPRESSED_DIR%" (
    echo Arquivos prontos para GitHub:
    for %%F in ("%COMPRESSED_DIR%\*.zip") do (
        set FILE=%%~nxF
        for /f "tokens=*" %%S in ('powershell -Command "[math]::Round((Get-Item '%%F').Length / 1MB, 2)"') do set SIZE=%%S
        echo   [+] !FILE! (!SIZE! MB)
    )
)

echo.
echo ════════════════════════════════════════════════════════════════════════════
echo Próximos passos:
echo   1. Vá para: https://github.com/seu-usuario/StockControl/releases
echo   2. Clique em "Create a new release"
echo   3. Tag: v2.0
echo   4. Upload dos arquivos em: %COMPRESSED_DIR%
echo ════════════════════════════════════════════════════════════════════════════
echo.

pause
exit /b 0

