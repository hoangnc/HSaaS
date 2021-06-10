#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["microservices/TenantManagementService.Host/TenantManagementService.Host.csproj", "microservices/TenantManagementService.Host/"]
COPY ["shared/HSaaS.Host.Shared/HSaaS.Host.Shared.csproj", "shared/HSaaS.Host.Shared/"]
RUN dotnet restore "microservices/TenantManagementService.Host/TenantManagementService.Host.csproj"
COPY . .
WORKDIR "/src/microservices/TenantManagementService.Host"
RUN dotnet build "TenantManagementService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TenantManagementService.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TenantManagementService.Host.dll"]