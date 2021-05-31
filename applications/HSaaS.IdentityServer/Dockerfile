#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["applications/HSaaS.IdentityServer/HSaaS.IdentityServer.csproj", "applications/HSaaS.IdentityServer/"]
COPY ["modules/account/src/HSaaS.Account.Web/HSaaS.Account.Web.csproj", "modules/account/src/HSaaS.Account.Web/"]
COPY ["modules/account/src/HSaaS.Account.HttpApi/HSaaS.Account.HttpApi.csproj", "modules/account/src/HSaaS.Account.HttpApi/"]
COPY ["modules/account/src/HSaaS.Account.Application.Contracts/HSaaS.Account.Application.Contracts.csproj", "modules/account/src/HSaaS.Account.Application.Contracts/"]
COPY ["modules/account/src/HSaaS.Account.Domain.Shared/HSaaS.Account.Domain.Shared.csproj", "modules/account/src/HSaaS.Account.Domain.Shared/"]
COPY ["themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.csproj", "themes/Abp.AspNetCore.Mvc.UI.Theme.AdminLTE/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
COPY ["src/HSaaS.Application.Contracts/HSaaS.Application.Contracts.csproj", "src/HSaaS.Application.Contracts/"]
COPY ["src/HSaaS.Domain.Shared/HSaaS.Domain.Shared.csproj", "src/HSaaS.Domain.Shared/"]
COPY ["modules/account/src/HSaaS.Account.Web.IdentityServer/HSaaS.Account.Web.IdentityServer.csproj", "modules/account/src/HSaaS.Account.Web.IdentityServer/"]
COPY ["shared/HSaaS.Host.Shared/HSaaS.Host.Shared.csproj", "shared/HSaaS.Host.Shared/"]
RUN dotnet restore "applications/HSaaS.IdentityServer/HSaaS.IdentityServer.csproj"
COPY . .
WORKDIR "/src/applications/HSaaS.IdentityServer"
RUN dotnet build "HSaaS.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HSaaS.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HSaaS.IdentityServer.dll"]