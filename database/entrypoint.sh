#!/bin/bash

cd /src/MasterData.HttpApi.Host

until dotnet ef database update -v --no-build; do
>&2 echo "SQL Server is starting up"
sleep 1
done

cd /src/DocumentManagement.HttpApi.Host && dotnet ef database update -v --no-build
cd /src/AuthServer.Host && dotnet ef database update -v --no-build