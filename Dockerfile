# ETAPA 1: Compilación (SDK de .NET 9)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copiamos los archivos .csproj a carpetas que COINCIDAN con el comando restore
# Nota: He corregido "WebApi.WebApi/" por "CedulaManager.WebApi/"
COPY ["CedulaManager.WebApi/CedulaManager.WebApi.csproj", "CedulaManager.WebApi/"]
COPY ["CedulaManager.Persistence/CedulaManager.Persistence.csproj", "CedulaManager.Persistence/"]
COPY ["CedulaManager.Infrastructure/CedulaManager.Infrastructure.csproj", "CedulaManager.Infrastructure/"]
COPY ["CedulaManager.Domain/CedulaManager.Domain.csproj", "CedulaManager.Domain/"]
COPY ["CedulaManager.Application/CedulaManager.Application.csproj", "CedulaManager.Application/"]

# 2. Ahora el restore SÍ encontrará el archivo en la ruta correcta
RUN dotnet restore "CedulaManager.WebApi/CedulaManager.WebApi.csproj"

# 3. Copiamos el resto del código
COPY . .

# 4. Compilamos (Asegúrate de que la ruta del WORKDIR coincida con tu carpeta física)
WORKDIR "/src/CedulaManager.WebApi"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "CedulaManager.WebApi.dll"]