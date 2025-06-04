# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ../ ./

RUN dotnet restore CipherPlayground.API/CipherPlayground.API.csproj
RUN dotnet publish CipherPlayground.API/CipherPlayground.API.csproj -c Release -o /out --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "CipherPlayground.API.dll"]