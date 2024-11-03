# Requisitos para ejecución del proyecto en local
Para ejecutar Auth API en un entorno local es necesario instalar solo los siguientes recursos.

- Docker o Docker Desktop (https://www.docker.com/products/docker-desktop/).
- Visual Studio Code (https://code.visualstudio.com/).
- Dev Containers de Microsoft, es una extensión de VS Code la cual puedes encontrar en la sección de extensiones.

# Ejecutar Terraform desde el ambiente local
Para ejecutar Terraform desde tu consola local se tienen que seguir los siguientes pasos:
## Setear valores de Service Principal para Terraform
Ejecutar los siguientes comandos en la consola:
- export ARM_CLIENT_ID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
- export ARM_CLIENT_SECRET="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
- export ARM_SUBSCRIPTION_ID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
- export ARM_TENANT_ID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"

Para validar que las variables fueron seteadas correctamente ejecutar los siguientes comandos:
- echo $ARM_CLIENT_ID
- echo $ARM_CLIENT_SECRET
- echo $ARM_SUBSCRIPTION_ID
- echo $ARM_TENANT_ID
