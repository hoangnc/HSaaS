#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["microservices/MasterData.HttpApi.Host/MasterData.HttpApi.Host.csproj", "microservices/MasterData.HttpApi.Host/"]
COPY ["modules/master-data/src/MasterData.Application/MasterData.Application.csproj", "modules/master-data/src/MasterData.Application/"]
COPY ["modules/master-data/src/MasterData.Domain/MasterData.Domain.csproj", "modules/master-data/src/MasterData.Domain/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/master-data/src/MasterData.EntityFrameworkCore/MasterData.EntityFrameworkCore.csproj", "modules/master-data/src/MasterData.EntityFrameworkCore/"]
COPY ["microservices/MasterData.Host.Shared/MasterData.Host.Shared.csproj", "microservices/MasterData.Host.Shared/"]
COPY ["modules/master-data/src/MasterData.HttpApi/MasterData.HttpApi.csproj", "modules/master-data/src/MasterData.HttpApi/"]
RUN dotnet restore "microservices/MasterData.HttpApi.Host/MasterData.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/microservices/MasterData.HttpApi.Host"
RUN dotnet build "MasterData.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterData.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterData.HttpApi.Host.dll"]