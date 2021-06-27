FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS build
#RUN apk add --no-cache bash
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef
WORKDIR /src
COPY . .

WORKDIR "/microservices/master-data/MasterData.HttpApi.Host"
RUN dotnet restore "MasterData.HttpApi.Host.csproj" -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release

WORKDIR "/microservices/document-management/DocumentManagement.HttpApi.Host"
RUN dotnet restore "DocumentManagement.HttpApi.Host.csproj" -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release

WORKDIR "/applications/HSaaS.IdentityServer"
RUN dotnet restore "HSaaS.IdentityServer.csproj" -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release

FROM build AS final
WORKDIR /src
COPY --from=build /microservices/MasterData.HttpApi.Host ./MasterData.HttpApi.Host
COPY --from=build /microservices/DocumentManagement.HttpApi.Host ./DocumentManagement.HttpApi.Host
COPY --from=build /applications/HSaaS.IdentityServer ./HSaaS.IdentityServer
COPY --from=build /databases/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
