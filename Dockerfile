# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything from your repo into the container
COPY . ./

# Restore NuGet packages for the solution or the API project
RUN dotnet restore CipherPlayground.API/CipherPlayground.API.csproj

# Publish the API project (release mode)
RUN dotnet publish CipherPlayground.API/CipherPlayground.API.csproj -c Release -o /out --no-restore

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /out ./

# Define the startup command
ENTRYPOINT ["dotnet", "CipherPlayground.API.dll"]
