#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["gateways/BackendAdminAppGateway.Host/BackendAdminAppGateway.Host.csproj", "gateways/BackendAdminAppGateway.Host/"]
COPY ["themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.csproj", "themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/"]
COPY ["modules/master-data/src/MasterData.Application/MasterData.Application.csproj", "modules/master-data/src/MasterData.Application/"]
COPY ["modules/master-data/src/MasterData.Domain/MasterData.Domain.csproj", "modules/master-data/src/MasterData.Domain/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/document-management/src/DocumentManagement.HttpApi/DocumentManagement.HttpApi.csproj", "modules/document-management/src/DocumentManagement.HttpApi/"]
COPY ["modules/document-management/src/DocumentManagement.Application.Contracts/DocumentManagement.Application.Contracts.csproj", "modules/document-management/src/DocumentManagement.Application.Contracts/"]
COPY ["modules/document-management/src/DocumentManagement.Domain.Shared/DocumentManagement.Domain.Shared.csproj", "modules/document-management/src/DocumentManagement.Domain.Shared/"]
COPY ["shared/HSaaS.Host.Shared/HSaaS.Host.Shared.csproj", "shared/HSaaS.Host.Shared/"]
COPY ["shared/BackendAdminApp.Shared/BackendAdminApp.Shared.csproj", "shared/BackendAdminApp.Shared/"]
COPY ["modules/master-data/src/MasterData.HttpApi/MasterData.HttpApi.csproj", "modules/master-data/src/MasterData.HttpApi/"]
RUN dotnet restore "gateways/BackendAdminAppGateway.Host/BackendAdminAppGateway.Host.csproj"
COPY . .
WORKDIR "/src/gateways/BackendAdminAppGateway.Host"
RUN dotnet build "BackendAdminAppGateway.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendAdminAppGateway.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendAdminAppGateway.Host.dll"]