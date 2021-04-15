# Getting Started
## Windows Users - Install Choco
Reference [install instructions from choco.org](https://chocolatey.org/install).
## Mac Users - Install Brew
Referece [brew.sh](https://brew.sh/)

After installing brew [install dotnet](https://formulae.brew.sh/cask/dotnet)

```brew
brew install --cask dotnet
```

## Install Visual Studio Code
Download and install VS Code [from the official stie]((https://code.visualstudio.com/download)).

After installing, install these extensions:
- [vscode-solution-explorer](https://marketplace.visualstudio.com/items?itemName=fernandoescolar.vscode-solution-explorer)
- [Kubernetes](https://marketplace.visualstudio.com/items?itemName=ms-kubernetes-tools.vscode-kubernetes-tools)
- [Docker](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker)
- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [Remote - Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-container)
- [GitLens](https://marketplace.visualstudio.com/items?itemName=eamodio.gitlens)
- [DotENV](https://marketplace.visualstudio.com/items?itemName=mikestead.dotenv)

## Docker and Kubernetes

Install Docker from the [official site](https://www.docker.com/products/docker-desktop)

Enable Kuberenetes and Deploy Docker Stacks to Kubernetes by default. Take a look at the [docker docs on how to enable](https://docs.docker.com/desktop/kubernetes/).

Bonus points. Test docker install by running SQL Express in a docker container. [mcr.microsoft.com/mssql/server:2019-latest](https://hub.docker.com/_/microsoft-mssql-server):
  - run in commandline `docker pull mcr.microsoft.com/mssql/server:2019-latest`
  - start a mssql-server instance and run the SQL Express edition: `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=1234qwerASDF" -e 'MSSQL_PID=Express' -p 1434:1434 --name Nimb3s.Identity.Database mcr.microsoft.com/mssql/server:2019-latest`

## Github Actions
To run actions locally install nektos/act. Reference the [nektos/act](https://github.com/nektos/act) repo on how to install.

Before you isntall act, make sure docker is installed.

## Git repo cleanup before .gitignore file is committed
Just in case, run these git commands to cleanup a git repo when files are added before a .gitignore:
```
git rm -rf --cached .
git add .
git commit -am "cleanup"
```
