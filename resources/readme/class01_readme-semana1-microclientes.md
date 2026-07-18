# Aplicaciones Distribuidas

# Semana 1 - Microservicio **app.microCliente.api**

**Carrera:** Desarrollo de Software
**Asignatura:** Aplicaciones Distribuidas
**Docente:** Ing. Giovanny Cholca, MSc.

---

# Objetivo

Construir un microservicio utilizando **ASP.NET Core 8**, **Entity Framework Core 8**, **SQL Server** y **RabbitMQ**, implementando una arquitectura por capas que permita desarrollar un CRUD para la gestión de clientes y direcciones.

---

# Arquitectura de la solución

La solución está compuesta por cinco proyectos:

```text
app.microCliente.api
│
├── app.microCliente.api
├── app.microCliente.services
├── app.microCliente.dataAccess
├── app.microCliente.entities
└── app.microCliente.common
```

## Responsabilidad de cada proyecto

| Proyecto                        | Responsabilidad                                            |
| ------------------------------- | ---------------------------------------------------------- |
| **app.microCliente.api**        | Expone la API REST y recibe las peticiones HTTP.           |
| **app.microCliente.services**   | Contiene la lógica del negocio y coordinación entre capas. |
| **app.microCliente.dataAccess** | Acceso a datos mediante Entity Framework Core.             |
| **app.microCliente.entities**   | Entidades persistentes del sistema.                        |
| **app.microCliente.common**     | DTO, respuestas, eventos y objetos compartidos.            |

---

# Arquitectura por capas

```text
Cliente HTTP / Swagger
        │
        ▼
ClienteController
DireccionClienteController
        │
        ▼
IClienteService
IDireccionClienteService
        │
        ▼
ClienteService
DireccionClienteService
        │
        ▼
IClienteRepository
IDireccionClienteRepository
        │
        ▼
ClienteRepository
DireccionClienteRepository
        │
        ▼
CrudGenericService<TEntity>
        │
        ▼
AppDbContext
        │
        ▼
Entity Framework Core
        │
        ▼
SQL Server
```

Cuando se registra o actualiza una dirección también se genera un evento:

```text
DireccionClienteService
        │
        ▼
IRabbitMQService
        │
        ▼
RabbitMQService
        │
        ▼
RabbitMQ
```

---

# Modelo de datos

La aplicación administra dos entidades.

```text
Cliente
   │
   │ 1
   │
   └────────────── N
                  │
                  ▼
         DireccionCliente
```

Un cliente puede tener múltiples direcciones.

---

# Flujo de una petición

```text
1. Cliente realiza una petición HTTP.

2. El Controller recibe la solicitud.

3. El Controller invoca un Service.

4. El Service aplica las reglas de negocio.

5. El Service utiliza un Repository.

6. El Repository utiliza Entity Framework Core.

7. AppDbContext ejecuta la operación sobre SQL Server.

8. El resultado retorna al Service.

9. El Service construye un BaseResponse<T>.

10. El Controller devuelve una respuesta JSON.
```

---

# Tecnologías utilizadas

* ASP.NET Core 8 Web API
* Entity Framework Core 8
* SQL Server
* RabbitMQ
* Swagger / OpenAPI
* Dependency Injection
* Code First
* Docker Desktop
* Visual Studio Community 2022

---

# Estructura del proyecto

```text
app.microCliente.api
│
├── app.microCliente.api
│   ├── Controllers
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
│
├── app.microCliente.services
│   ├── Interfaces
│   └── Services
│
├── app.microCliente.dataAccess
│   ├── Context
│   ├── Repositories
│   └── Migrations
│
├── app.microCliente.entities
│   ├── EntityBase
│   ├── Cliente
│   └── DireccionCliente
│
└── app.microCliente.common
    ├── DTO
    ├── BaseResponse
    ├── RabbitMQSettings
    └── EventDTO
```

---

# Desarrollo de la práctica

Durante esta semana se desarrollarán las siguientes actividades:

1. Crear la solución y los cinco proyectos.
2. Configurar las referencias entre proyectos.
3. Crear las entidades `Cliente` y `DireccionCliente`.
4. Configurar `AppDbContext`.
5. Configurar SQL Server mediante Entity Framework Core.
6. Implementar los repositorios.
7. Implementar la capa de servicios.
8. Configurar la inyección de dependencias.
9. Crear los controladores REST.
10. Configurar RabbitMQ.
11. Crear las migraciones Code First.
12. Ejecutar la API y validar los endpoints con Swagger.

---

# Endpoints principales

## Cliente

| Método | Endpoint                   |
| ------ | -------------------------- |
| GET    | /api/cliente/obtener-todos |
| GET    | /api/cliente/{id}          |
| POST   | /api/cliente               |
| PUT    | /api/cliente/{id}          |
| DELETE | /api/cliente/{id}          |

## Dirección

| Método | Endpoint                            |
| ------ | ----------------------------------- |
| GET    | /api/direccioncliente/obtener-todos |
| GET    | /api/direccioncliente/{id}          |
| POST   | /api/direccioncliente               |
| PUT    | /api/direccioncliente/{id}          |
| DELETE | /api/direccioncliente/{id}          |

---

# Resultado esperado

Al finalizar la práctica, el estudiante será capaz de:

* Comprender una arquitectura por capas.
* Implementar un microservicio profesional con ASP.NET Core.
* Aplicar el patrón Repository.
* Implementar la capa de servicios.
* Utilizar Entity Framework Core con Code First.
* Administrar relaciones uno a muchos.
* Publicar eventos mediante RabbitMQ.
* Probar una API REST utilizando Swagger.
* Comprender el flujo completo de una petición desde el cliente hasta la base de datos.
