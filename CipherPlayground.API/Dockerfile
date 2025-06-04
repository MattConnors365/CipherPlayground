# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything from the current context
COPY . ./

# Restore and publish
RUN dotnet restore CipherPlayground.API.csproj
RUN dotnet publish CipherPlayground.API.csproj -c Release -o /out --no-restore

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENTRYPOINT ["dotnet", "CipherPlayground.API.dll"]
