# HSaaS
Build SaaS platform using microservice architecture
# Running Infrastructure
Docker-compose is used to run the pre requirements with ease as default. If you don't have it, you can download and start using Docker for Windows from here on windows environment.
Run the command docker-compose -f docker-compose.infrastructure.yml -f docker-compose.infrastructure.override.yml up -d at MicroserviceDemo directory or run the powershell script __Run_Infrastructure.ps1 located at HSaaS/_run directory.
If you don't want to use docker for pre required services and install them on your local development, you need to update appsettings.json files of the projects in the HSaaS solution accordingly.
# Backend Admin:
![Backend Admin](https://github.com/hoangnc/HSaaS/blob/main/BackendAdmin.PNG?raw=true "Backend Admin")
