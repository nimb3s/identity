FROM alexellisio/msbuild:12.0
SHELL ["powershell"]

COPY . 'C:\\build\\'
WORKDIR 'C:\\build\\'

#RUN ["C:\\Program Files (x86)\\MSBuild\\12.0\\Bin\\msbuild.exe", "src/Nimb3s.Identity.Database/Nimb3s.Identity.Database.sqlproj"]
#RUN ["powershell Get-ChildItem -Path C:\\Program Files (x86)\\"]

## Usage: build image, then create container and copy out the bin directory.

CMD ["powershell Get-ChildItem -Path C:\\Program Files (x86)\\"]