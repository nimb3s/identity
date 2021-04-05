#Prerequisites
- Install [docker](https://www.docker.com/products/docker-desktop)
- [Download Microsoft SQL Server docker image](https://hub.docker.com/_/microsoft-mssql-server):
  - run in commandline `docker pull mcr.microsoft.com/mssql/server:2019-latest`
  - start a mssql-server instance running as the SQL Express edition: `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=1234qwerASDF" -e 'MSSQL_PID=Express' -p 1434:1434 --name Nimb3s.Identity.Database mcr.microsoft.com/mssql/server:2019-latest`
  
  