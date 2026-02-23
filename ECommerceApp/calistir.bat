@echo off
chcp 65001 > nul
setlocal EnableDelayedExpansion

where docker >nul 2>&1
if %errorlevel% neq 0 (
    echo [HATA] Docker bulunamadi. https://docker.com adresinden yukleyin.
    pause
    exit /b 1
)

docker info >nul 2>&1
if %errorlevel% neq 0 (
    echo [HATA] Docker calismıyor. Docker Desktop'ı başlatın ve tekrar deneyin.
    pause
    exit /b 1
)

echo [1/3] SQL Server container kontrol ediliyor...

docker ps -a --format "{{.Names}}" | findstr /x "mssql-ecommerce" >nul 2>&1
if %errorlevel% equ 0 (
    docker ps --format "{{.Names}}" | findstr /x "mssql-ecommerce" >nul 2>&1
    if %errorlevel% equ 0 (
        echo       SQL Server zaten calisiyor.
    ) else (
        echo       Container durdu, yeniden baslatiliyor...
        docker start mssql-ecommerce >nul
        echo       SQL Server baslatildi.
        timeout /t 5 /nobreak >nul
    )
) else (
    echo       Container bulunamadi, yeni olusturuluyor...
    docker run -d ^
        --name mssql-ecommerce ^
        -e "ACCEPT_EULA=Y" ^
        -e "SA_PASSWORD=Ecommerce@2024!" ^
        -e "MSSQL_PID=Express" ^
        -p 1433:1433 ^
        mcr.microsoft.com/mssql/server:2019-latest >nul
    echo       SQL Server olusturuldu, hazir olana kadar bekleniyor...
    timeout /t 12 /nobreak >nul
)

echo.
echo [2/3] .NET SDK kontrol ediliyor...

where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo [HATA] .NET SDK bulunamadi. https://dotnet.microsoft.com adresinden yukleyin.
    pause
    exit /b 1
)

for /f "tokens=*" %%v in ('dotnet --version 2^>nul') do set DOTNET_VERSION=%%v
echo       .NET !DOTNET_VERSION! bulundu.

echo.
echo [3/3] Uygulama baslatiliyor...
echo.
echo Tarayicida acin:  http://localhost:5053
echo Admin paneli:     http://localhost:5053/Admin/Account/Login
echo Admin giris:      admin@ecommerce.com / Admin123!
echo.
echo Durdurmak icin: Ctrl+C
echo ================================================
echo.

cd /d "%~dp0"
dotnet run
