# Use the appropriate base image for ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["MarketAssets.Api/MarketAssets.Api.csproj", "MarketAssets.Api/"]
COPY ["MarketAssets.DataAccess/MarketAssets.DataAccess.csproj", "MarketAssets.DataAccess/"]
COPY ["MarketAssets.Domain/MarketAssets.Domain.csproj", "MarketAssets.Domain/"]
COPY ["MarketAssets.Fintacharts.Authentication/MarketAssets.Fintacharts.Authentication.csproj", "MarketAssets.Fintacharts.Authentication/"]
COPY ["MarketAssets.Fintacharts.Historical/MarketAssets.Fintacharts.Historical.csproj", "MarketAssets.Fintacharts.Historical/"]
COPY ["MarketAssets.Fintacharts.RealTime/MarketAssets.Fintacharts.RealTime.csproj", "MarketAssets.Fintacharts.RealTime/"]
RUN dotnet restore "MarketAssets.Api/MarketAssets.Api.csproj"

# Copy entire source and build
COPY . .
WORKDIR "/src/MarketAssets.Api"
RUN dotnet build "MarketAssets.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MarketAssets.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage, using the base image for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "MarketAssets.Api.dll"]
