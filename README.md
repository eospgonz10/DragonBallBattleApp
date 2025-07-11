#  DragonBall Battles API

Este proyecto corresponde a una prueba técnica para la vacante de desarrollador. El proyecto está construido utilizando una arquitectura limpia (Clean Architecture) y teniendo como base el patrón de **Arquitectura Cebolla (Onion Architecture)**. El aplicativo en su parte de back está construido para organizar cronogramas de batallas del universo Dragon Ball, implementando autenticación JWT y medidas de seguridad avanzadas.


## 🚀 Características Principales

- ✅ **Arquitectura Cebolla (Onion Architecture)** - Domain, Application, Infrastructure, API
- ✅ **Clean Architecture** - Separación clara de responsabilidades por capas
- ✅ **Principios SOLID** aplicados en toda la solución
- ✅ **Clean Code** - Convenciones de naming, métodos focalizados
- ✅ **Autenticación JWT** - Sistema de tokens seguro
- ✅ **Pruebas Unitarias** - xUnit framework
- ✅ **Documentación API** - Swagger/OpenAPI
- ✅ **Medidas de Seguridad** - Validaciones, CORS, manejo de errores

## 🏗️ Arquitectura de Programación

```
📁 src/
├── 🎯 DragonBallBattles.Domain/          # Núcleo - Entidades, Interfaces, Excepciones
├── 🔧 DragonBallBattles.Application/     # Casos de Uso - Servicios, DTOs, Validaciones
├── 🌐 DragonBallBattles.Infrastructure/  # Capa Externa - Servicios Externos, HTTP Clients, Repository
└── 🚀 DragonBallBattles.API/            # Capa de Presentación - Controllers, Configuración, JWT

📁 tests/
├── 🧪 DragonBallBattles.Tests.Unit/     # Pruebas Unitarias
└── 🔬 DragonBallBattles.Tests.Integration/ # Pruebas de Integración
```

### Patrones Implementados
- **Arquitectura Cebolla (Onion Architecture)** - Capas concéntricas con dependencias hacia el interior
- **Clean Architecture** - Separación clara de responsabilidades
- **Repository Pattern** - Abstracción de acceso a datos
- **Dependency Injection** - Inversión de dependencias
- **SOLID Principles** - Principios de diseño orientado a objetos

## Comenzando 🚀
Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas.

## Pre-requisitos 📋
El backend está construido bajo .NET 8. Por lo tanto, para su correcto funcionamiento se requiere que el servidor o equipo donde vaya a ser utilizado dicha aplicación cuente con el runtime de .NET 8 instalado.

- **Runtime y SDK ASP .NET 8**
- **Windows 10/11 - Visual Studio 2022** (recomendado)
- **Linux/macOS** - Visual Studio Code
- **Git** - Control de versiones

## 🔐 Configuración Inicial

## 🚀 Inicio Rápido

### Instalación

1. **Clonar el repositorio**
   ```bash
   git clone <repo-url>
   cd DragonBallApp
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Ejecutar la aplicación**
   ```bash
   dotnet run --project src/DragonBallBattles.API
   ```

4. **Acceder a la documentación**
   - API: `https://localhost:7001`
   - Swagger: `https://localhost:7001/swagger`

## Ejecutando las pruebas ⚙️
El proyecto cuenta con pruebas unitarias e integración. Estas se encuentran en la solución de Visual Studio:

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar solo pruebas unitarias
dotnet test tests/DragonBallBattles.Tests.Unit

# Ejecutar pruebas de integración
dotnet test tests/DragonBallBattles.Tests.Integration
```

Las pruebas se encuentran en:
- `tests/DragonBallBattles.Tests.Unit/` - Pruebas unitarias
- `tests/DragonBallBattles.Tests.Integration/` - Pruebas de integración

## Despliegue 📦
Puede acceder a la URL y utilizar el API.

**Visitar**: `https://localhost:7001/swagger` (desarrollo local)

## 📊 Uso de la API

### Autenticación
```bash
# Obtener token JWT
POST /api/auth/login
{
  "username": "admin@pruebasemtelco.com",
  "password": "admin123456!/*-"
}
```

### Generar Cronograma de Batallas
```bash
# Generar batallas (requiere autenticación)
POST /api/battles/{numeroParticipantes}/schedule
Authorization: Bearer <jwt-token>
```

### Respuesta Esperada
```json
{
  "batallas": [
    {
      "batalla": "Goku vs Vegeta",
      "fecha": "2025/08/07"
    },
    {
      "batalla": "Gohan vs Piccolo", 
      "fecha": "2025/08/07"
    }
  ]
}
```

## Construido con 🛠️

### Arquitectura de Programación
- **Arquitectura Cebolla (Onion Architecture)** - Capas concéntricas con dependencias hacia el interior
- **Clean Architecture** - Arquitectura limpia con separación de capas
- **Repository Pattern** - Patrón de acceso a datos
- **SOLID Principles** - Principios de diseño orientado a objetos

### Tecnologías
- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Serilog** - Logging estructurado
- **FluentValidation** - Validaciones
- **xUnit** - Testing framework
- **Swagger/OpenAPI** - Documentación de API
- **JWT Bearer** - Autenticación
- **HttpClient** - Consumo de APIs externas

## Wiki 📖
Información útil sobre los frameworks y plugins utilizados en el proyecto:

- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/) - Framework web
- [FluentValidation](https://docs.fluentvalidation.net/) - Validaciones
- [xUnit](https://xunit.net/) - Testing framework
- [Swagger](https://swagger.io/) - Documentación API

## 🔧 Configuración

### Variables de Configuración

| Variable | Descripción | Valor Actual |
|----------|-------------|--------------|
| `Jwt:Key` | **Obligatorio** - Clave secreta JWT | (Definida en `appsettings.json`) |
| `Jwt:Issuer` | Emisor del token JWT | DragonBallBattlesAPI |
| `Jwt:Audience` | Audiencia del token JWT | DragonBallBattlesAPIUsers |
| `DragonBallApi:BaseUrl` | URL base de la API externa | https://dragonball-api.com |
| `DragonBallApi:CharactersEndpoint` | Endpoint de personajes | /api/characters |

> **Nota:** Las variables sensibles y de endpoints están definidas directamente en `appsettings.json` para facilitar la ejecución y pruebas del reto técnico.

### Archivos de Configuración

- `appsettings.json` - Configuración base y sensible
- `appsettings.Development.json` - Configuración de desarrollo

## 🛡️ Seguridad

- ✅ **Configuración sensible** en `appsettings.json` (por simplicidad del reto)
- ✅ **Validación de entrada** con FluentValidation
- ✅ **Manejo de excepciones** centralizado
- ✅ **CORS** configurado apropiadamente
- ✅ **Headers de seguridad** implementados
- ✅ **Logging** sin exposición de datos sensibles

## Autores ✒️
- **Estiven Ospina González** - [LinkedIn](https://www.linkedin.com/in/estiven-ospina/)



## Licencia 📄
Este proyecto está bajo propiedad pública para propósitos de prueba técnica.

## 🔗 Enlaces Útiles

- [Documentación de la API Dragon Ball](https://dragonball-api.com/api/characters?page=1&limit={numeroParticipantes})
- [Perfil LinkedIn - Estiven Ospina](https://www.linkedin.com/in/estiven-ospina/)

---

⚡ **Hecho con .NET 8.0**  
*Prueba Técnica - Programador Plataforma*