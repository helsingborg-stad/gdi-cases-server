# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY ./gdi-cases-server/*.csproj ./
RUN dotnet restore
    
# Copy everything else and build
COPY ./gdi-cases-server/* ./
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

# This line is needed because otherwise CoreCLR refuses to start in read-only
ENV COMPlus_EnableDiagnostics=0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "gdi-cases-server.dll"]