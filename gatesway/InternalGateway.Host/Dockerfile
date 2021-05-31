#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["gatesway/InternalGateway.Host/InternalGateway.Host.csproj", "gatesway/InternalGateway.Host/"]
COPY ["modules/document-management/src/DocumentManagement.HttpApi/DocumentManagement.HttpApi.csproj", "modules/document-management/src/DocumentManagement.HttpApi/"]
COPY ["modules/document-management/src/DocumentManagement.Application.Contracts/DocumentManagement.Application.Contracts.csproj", "modules/document-management/src/DocumentManagement.Application.Contracts/"]
COPY ["modules/document-management/src/DocumentManagement.Domain.Shared/DocumentManagement.Domain.Shared.csproj", "modules/document-management/src/DocumentManagement.Domain.Shared/"]
COPY ["modules/document-management/src/DocumentManagement.Domain/DocumentManagement.Domain.csproj", "modules/document-management/src/DocumentManagement.Domain/"]
COPY ["shared/HSaaS.Host.Shared/HSaaS.Host.Shared.csproj", "shared/HSaaS.Host.Shared/"]
COPY ["modules/master-data/src/MasterData.HttpApi/MasterData.HttpApi.csproj", "modules/master-data/src/MasterData.HttpApi/"]
COPY ["modules/master-data/src/MasterData.Application.Contracts/MasterData.Application.Contracts.csproj", "modules/master-data/src/MasterData.Application.Contracts/"]
COPY ["modules/master-data/src/MasterData.Domain.Shared/MasterData.Domain.Shared.csproj", "modules/master-data/src/MasterData.Domain.Shared/"]
RUN dotnet restore "gatesway/InternalGateway.Host/InternalGateway.Host.csproj"
COPY . .
WORKDIR "/src/gatesway/InternalGateway.Host"
RUN dotnet build "InternalGateway.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InternalGateway.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InternalGateway.Host.dll"]