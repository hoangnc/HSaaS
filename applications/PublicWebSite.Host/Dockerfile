#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["applications/PublicWebSite.Host/PublicWebSite.Host.csproj", "applications/PublicWebSite.Host/"]
COPY ["themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.csproj", "themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/"]
COPY ["modules/master-data/src/MasterData.Web/MasterData.Web.csproj", "modules/master-data/src/MasterData.Web/"]
COPY ["modules/master-data/src/MasterData.HttpApi/MasterData.HttpApi.csproj", "modules/master-data/src/MasterData.HttpApi/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
COPY ["modules/document-management/src/DocumentManagement.Web/DocumentManagement.Web.csproj", "modules/document-management/src/DocumentManagement.Web/"]
COPY ["modules/document-management/src/DocumentManagement.Application/DocumentManagement.Application.csproj", "modules/document-management/src/DocumentManagement.Application/"]
COPY ["modules/document-management/src/DocumentManagement.Application.Contracts/DocumentManagement.Application.Contracts.csproj", "modules/document-management/src/DocumentManagement.Application.Contracts/"]
COPY ["modules/document-management/src/DocumentManagement.Domain.Shared/DocumentManagement.Domain.Shared.csproj", "modules/document-management/src/DocumentManagement.Domain.Shared/"]
COPY ["modules/document-management/src/DocumentManagement.Domain/DocumentManagement.Domain.csproj", "modules/document-management/src/DocumentManagement.Domain/"]
COPY ["modules/master-data/src/MasterData.HttpApi.Client/MasterData.HttpApi.Client.csproj", "modules/master-data/src/MasterData.HttpApi.Client/"]
COPY ["modules/document-management/src/DocumentManagement.HttpApi/DocumentManagement.HttpApi.csproj", "modules/document-management/src/DocumentManagement.HttpApi/"]
COPY ["shared/HSaaS.Host.Shared/HSaaS.Host.Shared.csproj", "shared/HSaaS.Host.Shared/"]
COPY ["modules/document-management/src/DocumentManagement.HttpApi.Client/DocumentManagement.HttpApi.Client.csproj", "modules/document-management/src/DocumentManagement.HttpApi.Client/"]
RUN dotnet restore "applications/PublicWebSite.Host/PublicWebSite.Host.csproj"
COPY . .
WORKDIR "/src/applications/PublicWebSite.Host"
RUN dotnet build "PublicWebSite.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PublicWebSite.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublicWebSite.Host.dll"]