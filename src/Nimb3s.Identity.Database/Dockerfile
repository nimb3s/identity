#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Nimb3s.Identity.Database/Nimb3s.Identity.Database.csproj", "src/Nimb3s.Identity.Database/"]
RUN dotnet restore "src/Nimb3s.Identity.Database/Nimb3s.Identity.Database.csproj"
COPY . .
WORKDIR "/src/src/Nimb3s.Identity.Database"
RUN dotnet build "Nimb3s.Identity.Database.csproj" -c ReleaseDatabase -o /app/build
RUN ls -R

FROM nimb3s/base-sql-server:2019-latest 
# Lower the privilege
USER mssql 
# Add the DACPAC to the image
WORKDIR /tmp/db/
COPY --from=build /app/build/Nimb3s.Identity.Database.dacpac /tmp/db/Nimb3s.Identity.Database.dacpac
RUN ls -R
# Configure external build arguments to allow configurability.
ARG DBNAME=Identity
ARG PASSWORD=1234qwerASDF
# Configure the required environmental variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$PASSWORD
# Launch SQL Server, confirm startup is complete, deploy the DACPAC, then terminate SQL Server.
# See https://stackoverflow.com/a/51589787/488695
RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/sqlpackage/sqlpackage /a:Publish /tsn:. /tdn:${DBNAME} /tu:sa /tp:$SA_PASSWORD /sf:/tmp/db/Nimb3s.Identity.Database.dacpac \
    && rm -r /tmp/db \
    && pkill sqlservr \
    && rm -r /opt/sqlpackage