#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["microservices/DocumentManagement.HttpApi.Host/DocumentManagement.HttpApi.Host.csproj", "microservices/DocumentManagement.HttpApi.Host/"]
COPY ["microservices/DocumentManagement.Host.Shared/DocumentManagement.Host.Shared.csproj", "microservices/DocumentManagement.Host.Shared/"]
COPY ["modules/document-management/src/DocumentManagement.HttpApi/DocumentManagement.HttpApi.csproj", "modules/document-management/src/DocumentManagement.HttpApi/"]
COPY ["modules/document-management/src/DocumentManagement.Application.Contracts/DocumentManagement.Application.Contracts.csproj", "modules/document-management/src/DocumentManagement.Application.Contracts/"]
COPY ["modules/document-management/src/DocumentManagement.Domain.Shared/DocumentManagement.Domain.Shared.csproj", "modules/document-management/src/DocumentManagement.Domain.Shared/"]
COPY ["modules/document-management/src/DocumentManagement.EntityFrameworkCore/DocumentManagement.EntityFrameworkCore.csproj", "modules/document-management/src/DocumentManagement.EntityFrameworkCore/"]
COPY ["modules/document-management/src/DocumentManagement.Domain/DocumentManagement.Domain.csproj", "modules/document-management/src/DocumentManagement.Domain/"]
COPY ["modules/document-management/src/DocumentManagement.Application/DocumentManagement.Application.csproj", "modules/document-management/src/DocumentManagement.Application/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
COPY ["modules/master-data/src/MasterData.HttpApi.Client/MasterData.HttpApi.Client.csproj", "modules/master-data/src/MasterData.HttpApi.Client/"]
RUN dotnet restore "microservices/DocumentManagement.HttpApi.Host/DocumentManagement.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/microservices/DocumentManagement.HttpApi.Host"
RUN dotnet build "DocumentManagement.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentManagement.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentManagement.HttpApi.Host.dll"]