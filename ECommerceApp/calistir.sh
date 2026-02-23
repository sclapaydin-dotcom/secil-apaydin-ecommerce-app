#!/bin/bash

KIRMIZI='\033[0;31m'
YESIL='\033[0;32m'
SARI='\033[1;33m'
SIFIRLA='\033[0m'


if ! command -v docker &> /dev/null; then
    echo -e "${KIRMIZI}[HATA] Docker bulunamadi. https://docker.com adresinden yukleyin.${SIFIRLA}"
    exit 1
fi

if ! docker info &> /dev/null; then
    echo -e "${KIRMIZI}[HATA] Docker calismıyor. Docker Desktop'ı başlatın.${SIFIRLA}"
    exit 1
fi

echo -e "${SARI}[1/3] SQL Server container kontrol ediliyor...${SIFIRLA}"

if docker ps -a --format '{{.Names}}' | grep -q "^mssql-ecommerce$"; then
    if docker ps --format '{{.Names}}' | grep -q "^mssql-ecommerce$"; then
        echo -e "${YESIL}      SQL Server zaten calisiyor.${SIFIRLA}"
    else
        echo "      Container durdu, yeniden baslatiliyor..."
        docker start mssql-ecommerce > /dev/null
        echo -e "${YESIL}      SQL Server baslatildi.${SIFIRLA}"
        sleep 5
    fi
else
    echo "      Container bulunamadi, yeni olusturuluyor..."
    docker run -d \
        --name mssql-ecommerce \
        -e "ACCEPT_EULA=Y" \
        -e "SA_PASSWORD=Ecommerce@2024!" \
        -e "MSSQL_PID=Express" \
        -p 1433:1433 \
        mcr.microsoft.com/mssql/server:2019-latest > /dev/null
    echo -e "${YESIL}      SQL Server olusturuldu, hazir olana kadar bekleniyor...${SIFIRLA}"
    sleep 12
fi

echo ""
echo -e "${SARI}[2/3] .NET SDK aranıyor...${SIFIRLA}"

DOTNET_CMD=""

if command -v dotnet &> /dev/null; then
    DOTNET_CMD="dotnet"
elif [ -f "/Users/$USER/.dotnet-arm64/dotnet" ]; then
    DOTNET_CMD="/Users/$USER/.dotnet-arm64/dotnet"
    export DOTNET_ROOT="/Users/$USER/.dotnet-arm64"
elif [ -f "$HOME/.dotnet/dotnet" ]; then
    DOTNET_CMD="$HOME/.dotnet/dotnet"
    export DOTNET_ROOT="$HOME/.dotnet"
else
    echo -e "${KIRMIZI}[HATA] .NET SDK bulunamadi. https://dotnet.microsoft.com adresinden yukleyin.${SIFIRLA}"
    exit 1
fi

DOTNET_VERSION=$($DOTNET_CMD --version 2>/dev/null)
echo -e "${YESIL}      .NET $DOTNET_VERSION bulundu: $DOTNET_CMD${SIFIRLA}"

echo ""
echo -e "${SARI}[3/3] Uygulama baslatiliyor...${SIFIRLA}"
echo ""
echo -e "${YESIL}Tarayicida acin:${SIFIRLA} http://localhost:5053"
echo -e "${YESIL}Admin paneli:  ${SIFIRLA} http://localhost:5053/Admin/Account/Login"
echo -e "${YESIL}Admin giris:   ${SIFIRLA} admin@ecommerce.com / Admin123!"
echo ""
echo "Durdurmak icin: Ctrl+C"
echo "================================================"
echo ""

cd "$(dirname "$0")"
$DOTNET_CMD run
