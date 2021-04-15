FROM mcr.microsoft.com/mssql/server:2019-latest
 
COPY . /app
WORKDIR /app

# Elevate to root to install required packages
USER root

# Install Unzip
RUN apt-get update \
    && apt-get install unzip -y

# Install SQLPackage for Linux and make it executable
RUN wget -progress=bar:force -q -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=873926
RUN unzip -qq sqlpackage.zip -d /opt/sqlpackage
RUN chmod +x /opt/sqlpackage/sqlpackage
 
# Lower the privilege
USER mssql
 
# Add the DACPAC to the image
COPY src/Nimb3s.Identity.Database/Nimb3s.Identity.Database.dacpac /tmp/db/Nimb3s.Identity.Database.dacpac
 
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