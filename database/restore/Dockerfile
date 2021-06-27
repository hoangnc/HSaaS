FROM alpine AS unzip
WORKDIR /src
RUN apk add --no-cache unzip
COPY /databases/HSaaS_Identity.zip .
COPY /databases/HSaaS_MasterData.zip .
COPY /databases/HSaaS_DocumentManagement.zip .
RUN unzip -q "*.zip"

FROM mcr.microsoft.com/mssql-tools AS restore
WORKDIR /src
COPY --from=unzip /src/HSaaS_Identity.bak .
COPY --from=unzip /src/HSaaS_MasterData.bak .
COPY --from=unzip /src/HSaaS_DocumentManagement.bak .
COPY /databases/restore/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh
ENTRYPOINT [ "./entrypoint.sh" ]