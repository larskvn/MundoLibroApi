# 📚 MundoLibroApi - Gestión Bibliotecaria con .NET 8

**MundoLibroApi** es una API REST moderna construida con .NET 8 que implementa arquitectura hexagonal para ofrecer un sistema robusto de gestión bibliotecaria. Maneja el catálogo completo de libros, editoriales, solicitantes y préstamos con un diseño modular y altamente escalable.

## 🏗️ Arquitectura Hexagonal

Nuestra solución sigue principios de **arquitectura hexagonal** (ports & adapters) para garantizar máxima flexibilidad y mantenibilidad:
![image](https://github.com/user-attachments/assets/eadf3854-2704-4cce-81a4-8cf0a48aee1d)


### Capas Principales

| Capa              | Responsabilidad                              | Tecnologías Clave           |
|-------------------|---------------------------------------------|----------------------------|
| **Api**           | Endpoints REST, ,  | ASP.NET Core, Swagger      |
| **Aplicación**    | Lógica de negocio, validaciones, DTOs       | AutoMapper, FluentValidation|
| **Dominio**       | Entidades core, interfaces de repositorio    | .NET 8, Clean Architecture |
| **Infraestructura**| Persistencia, servicios externos            | EF Core, SQL Server ,EntityFramework       |

## 🧩 Componentes Técnicos

### BookNet.Api 
```bash
dotnet add package Swashbuckle.AspNetCore 
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design 
```
- **Swagger UI** integrado para prueba interactiva de endpoints

### BookNet.Application 
```bash
dotnet add package AutoMapper 
dotnet add package FluentValidation 
```
- Validaciones complejas con FluentValidation
- Inyección de dependencias con Autofac

### BookNet.Infraestructura 
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
```


## 🚀 Primeros Pasos

### Requisitos Mínimos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/sql-server) 
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

