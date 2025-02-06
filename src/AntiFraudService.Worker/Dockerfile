# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AntiFraudService.Worker/AntiFraudService.Worker.csproj", "src/AntiFraudService.Worker/"]
COPY ["src/AntiFraudService.Infrastructure/AntiFraudService.Infrastructure.csproj", "src/AntiFraudService.Infrastructure/"]
COPY ["src/AntiFraudService.Application/AntiFraudService.Application.csproj", "src/AntiFraudService.Application/"]
COPY ["src/AntiFraudService.Contracts/AntiFraudService.Contracts.csproj", "src/AntiFraudService.Contracts/"]
COPY ["src/AntiFraudService.Domain/AntiFraudService.Domain.csproj", "src/AntiFraudService.Domain/"]
COPY ["src/AntiFraudService.Consumers/AntiFraudService.Consumers.csproj", "src/AntiFraudService.Consumers/"]
RUN dotnet restore "./src/AntiFraudService.Worker/AntiFraudService.Worker.csproj"
COPY . .
WORKDIR "/src/src/AntiFraudService.Worker"
RUN dotnet build "./AntiFraudService.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AntiFraudService.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AntiFraudService.Worker.dll"]