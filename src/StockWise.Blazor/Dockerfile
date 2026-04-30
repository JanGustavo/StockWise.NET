# src/StockWise.Blazor/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/StockWise.Blazor/StockWise.Blazor.csproj", "src/StockWise.Blazor/"]
COPY ["src/StockWise.Domain/StockWise.Domain.csproj", "src/StockWise.Domain/"]

RUN dotnet restore "src/StockWise.Blazor/StockWise.Blazor.csproj"

COPY src/ src/
WORKDIR "/src/src/StockWise.Blazor"
RUN dotnet build "StockWise.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockWise.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Configurar o Nginx para servir a aplicação WASM
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY src/StockWise.Blazor/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80