set serviceName=RDTool_Server.WebAPI

sc stop   %serviceName% 
sc delete %serviceName% 

pause