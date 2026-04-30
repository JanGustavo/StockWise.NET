# src/StockWise.API/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar apenas os ficheiros de projeto para tirar partido da cache do Docker
COPY ["src/StockWise.API/StockWise.API.csproj", "src/StockWise.API/"]
COPY ["src/StockWise.Domain/StockWise.Domain.csproj", "src/StockWise.Domain/"]
COPY ["src/StockWise.Application/StockWise.Application.csproj", "src/StockWise.Application/"]
COPY ["src/StockWise.Infrastructure/StockWise.Infrastructure.csproj", "src/StockWise.Infrastructure/"]

RUN dotnet restore "src/StockWise.API/StockWise.API.csproj"

# Copiar o resto do código e compilar
COPY src/ src/
WORKDIR "/src/src/StockWise.API"
RUN dotnet build "StockWise.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockWise.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "StockWise.API.dll"]