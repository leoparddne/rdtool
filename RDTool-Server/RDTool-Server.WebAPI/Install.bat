set serviceName=RDTool_Server.WebAPI
set serviceFilePath=%~dp0RDTool.Server.WebAPI.exe
set serviceDescription=ScaffoldWebAPIService

sc create %serviceName%  BinPath=%serviceFilePath%
sc config %serviceName%    start=auto  
sc description %serviceName%  %serviceDescription%
sc start  %serviceName%
pause

