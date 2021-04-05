FROM mcr.microsoft.com/mssql/server:2019-latest
 
COPY . /app
WORKDIR /app

# Elevate to root to install required packages
USER root
 
# Lower the privilege
USER mssql
 
# Add the DACPAC to the image
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
COPY *.dacpac /tmp/db
 
# Configure external build arguments to allow configurability.
ARG DBNAME=Identity
ARG PASSWORD
 
# Configure the required environmental variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$PASSWORD
 
# Launch SQL Server, confirm startup is complete, deploy the DACPAC, then terminate SQL Server.
# See https://stackoverflow.com/a/51589787/488695
RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/sqlpackage/sqlpackage /a:Import /tsn:. /tdn:${DBNAME} /tu:sa /tp:$SA_PASSWORD /sf:/tmp/db/Nimb3s.Identity.Database.dacpac \
    && rm -r /tmp/db \
    && pkill sqlservr \
    && rm -r /opt/sqlpackage











FROM mcr.microsoft.com/mssql/server:2019-latest
 
# Elevate to root to install required packages
USER root
RUN apt-get update \
    && apt-get install unzip libunwind8 -y
 
# Install SQLPackage for Linux and make it executable
RUN wget -progress=bar:force -q -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2113331 \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod +x /opt/sqlpackage/sqlpackage \
    && chown -R mssql /opt/sqlpackage \
    && mkdir /tmp/db \
    && chown -R mssql /tmp/db
 
# Lower the privilege
#USER mssql
 
# Add the DACPAC to the image
#COPY ["src\Nimb3s.IdentityTwo.Database\bin\Release\netstandard2.0\Nimb3s.IdentityTwo.Database.dacpac", "/tmp/db/"]
#COPY Nimb3s.IdentityTwo.Database.dacpac /tmp/db/Nimb3s.IdentityTwo.Database.dacpac
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Nimb3s.IdentityTwo.Database/Nimb3s.IdentityTwo.Database.csproj", "src/Nimb3s.IdentityTwo.Database/"]
RUN dotnet restore "src/Nimb3s.IdentityTwo.Database/Nimb3s.IdentityTwo.Database.csproj"
COPY . .
WORKDIR "/src/src/Nimb3s.IdentityTwo.Database"
RUN dotnet build "Nimb3s.IdentityTwo.Database.csproj" -c Release -o /app/build


# Configure external build arguments to allow configurability.
ARG DBNAME=Identity
ARG PASSWORD=1234qwerASDF
 
# Configure the required environmental variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$PASSWORD
 
# Launch SQL Server, confirm startup is complete, deploy the DACPAC, then terminate SQL Server.
# See https://stackoverflow.com/a/51589787/488695
RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/sqlpackage/sqlpackage /a:Import /tsn:. /tdn:${DBNAME} /tu:sa /tp:$SA_PASSWORD /sf:/tmp/db/Nimb3s.IdentityTwo.Database.dacpac \
    && rm -r /tmp/db \
    && pkill sqlservr \
    && rm -r /opt/sqlpackage