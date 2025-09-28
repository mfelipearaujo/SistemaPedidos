# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar arquivos de solução e projetos
COPY *.sln .
COPY SistemaPedidos.API/*.csproj ./SistemaPedidos.API/
COPY SistemaPedidos.Application/*.csproj ./SistemaPedidos.Application/
COPY SistemaPedidos.Domain/*.csproj ./SistemaPedidos.Domain/
COPY SistemaPedidos.Infrastructure/*.csproj ./SistemaPedidos.Infrastructure/

# Restaurar dependências (somente do projeto da API)
RUN dotnet restore SistemaPedidos.API/SistemaPedidos.API.csproj

# Copiar todo o código e publicar
COPY . .
RUN dotnet publish SistemaPedidos.API/SistemaPedidos.API.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "SistemaPedidos.API.dll"]
