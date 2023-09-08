# Tecnologias Usadas

- .NET Core 6 
- APIS
- AutoMapper
- Entity Framework Core
-  MVC Application - Razor

# Estructura del Proyecto

Este proyecto sigue una arquitectura Monolitico, en la cual las diferentes partes de la aplicación están organizadas en capas, la cual, esta estructurada de la siguiente manera:

- DPWDR.Technical.Interview.API
- DPWDR.Technical.Interview.Data
- DPWDR.Technical.Interview.Services
- DPWDR.Technical.Interview.Web


# Cambiar el Connection String para correr el proyecto

 Configurar el Connection String

1. Abrir el appsettings.json que esta ubicado en la solución API
2. localizar  el  ConnectionStrings.
3. Actualizar el  DefaultConnection  con la instancia de su BD.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;User Id=YourUsername;Password=YourPassword;"
}

```
