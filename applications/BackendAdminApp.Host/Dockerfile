#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["applications/BackendAdminApp.Host/BackendAdminApp.Host.csproj", "applications/BackendAdminApp.Host/"]
COPY ["modules/account/src/HSaaS.Account.Web/HSaaS.Account.Web.csproj", "modules/account/src/HSaaS.Account.Web/"]
COPY ["modules/account/src/HSaaS.Account.HttpApi/HSaaS.Account.HttpApi.csproj", "modules/account/src/HSaaS.Account.HttpApi/"]
COPY ["modules/account/src/HSaaS.Account.Application.Contracts/HSaaS.Account.Application.Contracts.csproj", "modules/account/src/HSaaS.Account.Application.Contracts/"]
COPY ["modules/account/src/HSaaS.Account.Domain.Shared/HSaaS.Account.Domain.Shared.csproj", "modules/account/src/HSaaS.Account.Domain.Shared/"]
COPY ["themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.csproj", "themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
COPY ["modules/master-data/src/MasterData.Web/MasterData.Web.csproj", "modules/master-data/src/MasterData.Web/"]
COPY ["modules/master-data/src/MasterData.Application/MasterData.Application.csproj", "modules/master-data/src/MasterData.Application/"]
COPY ["modules/master-data/src/MasterData.Domain/MasterData.Domain.csproj", "modules/master-data/src/MasterData.Domain/"]
COPY ["modules/master-data/src/MasterData.HttpApi/MasterData.HttpApi.csproj", "modules/master-data/src/MasterData.HttpApi/"]
COPY ["shared/HSaaS.Host.Shared/HSaaS.Host.Shared.csproj", "shared/HSaaS.Host.Shared/"]
COPY ["modules/master-data/src/MasterData.HttpApi.Client/MasterData.HttpApi.Client.csproj", "modules/master-data/src/MasterData.HttpApi.Client/"]
RUN dotnet restore "applications/BackendAdminApp.Host/BackendAdminApp.Host.csproj"
COPY . .
WORKDIR "/src/applications/BackendAdminApp.Host"
RUN dotnet build "BackendAdminApp.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendAdminApp.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendAdminApp.Host.dll"]