# personapi-dotnet

Este proyecto es una aplicación web construida con **ASP.NET Core** y **Entity Framework Core**, utilizando **Docker** para la configuración y ejecución de la base de datos SQL Server y la aplicación. La API de la aplicación gestiona datos relacionados con personas, profesiones, estudios y teléfonos.

## Requisitos previos

Asegúrate de tener instalados los siguientes programas en tu máquina antes de ejecutar el proyecto:

- **Docker** y **Docker Compose**
- **Git**

## Instalación y ejecución

Sigue los pasos a continuación para clonar el repositorio, construir los contenedores y ejecutar la aplicación:

### Clonar el repositorio desde GitHub:

Abre una terminal y ejecuta el siguiente comando para clonar el repositorio en tu máquina local:

```bash
git clone https://github.com/majogomezg/personapi-dotnet
```

## Configuracion de Docker

El proyecto incluye un archivo docker-compose.yml que configura la aplicación y la base de datos SQL Server. Ejecuta el siguiente comando para construir y ejecutar los contenedores necesarios:

```bash
docker-compose up --build -d
```

## Ingresar la URL de la apliacion

Una vez que los contenedores estén en ejecución, podrás acceder a la aplicación desde tu navegador web utilizando la siguiente URL:

```bash
http://localhost:5233
```