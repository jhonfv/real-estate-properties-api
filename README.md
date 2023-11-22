# RealState API Service V1.0.0

![Badge](https://img.shields.io/badge/.NET-6-blue)
![Badge](https://img.shields.io/badge/Hosting-Azure-blueviolet)
![Badge](https://img.shields.io/badge/Database-SqlServer-orange)
![Badge](https://img.shields.io/badge/Authentication-cors-green)

>API gestion y consulta de propiedades desarrollada en .NET 6 y alojada en Azure. La base de datos utilizada es SQL Server y la autenticación se basa en politica de CORS.

## 🌍 URIs de la API

- **Uri de publicación**: [properties-api-millionandup.azurewebsites.net](https://properties-api-millionandup.azurewebsites.net)
- **Uri de documentación**: [properties-api-millionandup.azurewebsites.net/api-docs](https://properties-api-millionandup.azurewebsites.net/api-docs/)

## 💼 Características

- Desarrollada con .NET 6
- Base de datos en SQL Server
- Alojamiento en Azure
- Autenticación basada en politca de CORS
- FakeCDN desallo en proyecto mvc
- Micro ORM Dapper
- Documentación en debug Swagger y producción ReDocs

## 📄 Notas
Para levantar el entorno local se debe contar con la base de datos ya implementada.
### 1 Base de datos
Se puede restaurar el backup que esta en la ruta docs->sql->RealState.bk o ejecutar los scripts de esta misma ruta.
>Recomendación: en tu base de datos local ingresa y ejecuta los scripts, luego ajusta la cadena de conexion en appSetting.json
```json
"ConnectionStrings": {
    "LocalDataBase": "Server=(localdb)\\mssqllocaldb;Database=Properties;Trusted_Connection=True;"
  }
```
>si el nombre de la conexion cambia se debe actualizar en el program.
```c#
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("LocalDataBase")));
```
Una vez configurada la base de datos se debe ejecutar el proyecto de api en la carpeta de servicios, si se desea probar en debug el fakeCDN en la propiedades de la soluccion se debe elejir la opcion de ejecutar multpiples proyectos y elejir la api y el fakeCDN, luego actualizar la url en el appSettings.json
```json
"FakeCDN": "https://localhost:55078"
```

---
