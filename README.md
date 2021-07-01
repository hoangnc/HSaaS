# HSaaS
Build SaaS platform using microservice architecture
# Running Infrastructure
* Docker-compose is used to run the pre requirements with ease as default. If you don't have it, you can download and start using [Docker for Windows](https://docs.docker.com/docker-for-windows/) from [here](https://docs.docker.com/docker-for-windows/install/) on windows environment.
* Run the command __docker-compose -f docker-compose.infrastructure.yml -f docker-compose.infrastructure.override.yml up -d__ at HSaaS directory or run the powershell script __Run_Infrastructure.ps1 located at HSaaS/_run directory.
* If you don't want to use docker for pre required services and install them on your local development, you need to update appsettings.json files of the projects in the HSaaS solution accordingly. [read more...](https://docs.abp.io/en/abp/latest/Samples/Microservice-Demo#running-infrastructure)
# Backend Admin:
![Login](https://github.com/hoangnc/HSaaS/blob/main/Login.PNG?raw=true "Login")
![Home Page](https://github.com/hoangnc/HSaaS/blob/main/HomePage.PNG?raw=true "Login")
![Backend Admin](https://github.com/hoangnc/HSaaS/blob/main/BackendAdmin.PNG?raw=true "Backend Admin")

---
Sources:
[ABP Framework Document](https://docs.abp.io/)
