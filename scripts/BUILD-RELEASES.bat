@echo off
REM ═══════════════════════════════════════════════════════════════════════════
REM   StockControl v1.0 - Build Releases
REM   Script para compilar executáveis para múltiplas plataformas
REM ═══════════════════════════════════════════════════════════════════════════

setlocal enabledelayedexpansion

REM Cores no console (simuladas com echo)
cls
color 0A

title StockControl v1.0 - Build Releases

REM Obter o diretório do projeto (pasta pai do script)
set PROJECT_PATH=%~dp0..
set RELEASES_DIR=%PROJECT_PATH%\releases

echo.
echo ╔═══════════════════════════════════════════════════════════════════════════╗
echo ║                   StockControl v1.0 - Build Releases                      ║
echo ║                                                                           ║
echo ║  Compilando executáveis para múltiplas plataformas...                     ║
echo ║                                                                           ║
echo ╚═══════════════════════════════════════════════════════════════════════════╝
echo.

echo Projeto: %PROJECT_PATH%
echo Output:  %RELEASES_DIR%
echo.

REM Criar diretório se não existir
if not exist "%RELEASES_DIR%" (
    mkdir "%RELEASES_DIR%"
    echo [INFO] Diretório de releases criado.
    echo.
)

set SUCCESS_COUNT=0
set FAIL_COUNT=0

echo ════════════════════════════════════════════════════════════════════════════
echo Iniciando compilação...
echo ════════════════════════════════════════════════════════════════════════════
echo.

REM Windows x64
call :compile_platform "Windows x64 (64-bit)" "win-x64" "StockControl-v1.0-Windows-x64"

REM Windows x86
call :compile_platform "Windows x86 (32-bit)" "win-x86" "StockControl-v1.0-Windows-x86"

REM Linux x64
call :compile_platform "Linux x64" "linux-x64" "StockControl-v1.0-Linux-x64"

REM macOS x64
call :compile_platform "macOS x64 (Intel)" "osx-x64" "StockControl-v1.0-macOS-x64"

REM macOS ARM64
call :compile_platform "macOS ARM64 (Apple Silicon)" "osx-arm64" "StockControl-v1.0-macOS-ARM64"

echo.
echo ════════════════════════════════════════════════════════════════════════════
echo Resumo Final
echo ════════════════════════════════════════════════════════════════════════════
echo ✓ Sucessos: %SUCCESS_COUNT%
echo ✗ Falhas:   %FAIL_COUNT%
echo.
echo Arquivos salvos em: %RELEASES_DIR%
echo.

if exist "%RELEASES_DIR%" (
    echo Plataformas compiladas:
    for /d %%D in ("%RELEASES_DIR%\*") do (
        echo   [+] %%~nxD
    )
)

echo.
echo ════════════════════════════════════════════════════════════════════════════
echo Próximos passos:
echo   1. Execute: compress-releases.bat (para comprimir)
echo   2. Upload dos arquivos no GitHub Releases
echo ════════════════════════════════════════════════════════════════════════════
echo.

pause
exit /b 0

REM ═══════════════════════════════════════════════════════════════════════════
REM Subroutina para compilação
REM ═══════════════════════════════════════════════════════════════════════════
:compile_platform
setlocal
set PLATFORM_NAME=%~1
set RID=%~2
set OUTPUT_NAME=%~3
set OUTPUT_PATH=%RELEASES_DIR%\%OUTPUT_NAME%

echo [INFO] Compilando para: %PLATFORM_NAME%
echo        RID: %RID%

REM Remover diretório antigo se existir
if exist "%OUTPUT_PATH%" (
    echo        Removendo build antigo...
    rmdir /s /q "%OUTPUT_PATH%" >nul 2>&1
)

REM Compilar
cd /d "%PROJECT_PATH%"
dotnet publish -c Release -r %RID% --self-contained -o "%OUTPUT_PATH%" >nul 2>&1

if %ERRORLEVEL% equ 0 (
    echo        [OK] Compilação concluída com sucesso!
    
    REM Calcular tamanho
    for /f "tokens=*" %%A in ('dir "%OUTPUT_PATH%" /s /b ^| find /c /v ""') do set FILE_COUNT=%%A
    echo        Arquivos criados: %FILE_COUNT%
    
    set /A "SUCCESS_COUNT+=1"
) else (
    echo        [ERRO] Falha na compilação!
    set /A "FAIL_COUNT+=1"
)

echo.
endlocal & set SUCCESS_COUNT=%SUCCESS_COUNT%&set FAIL_COUNT=%FAIL_COUNT%
exit /b 0

