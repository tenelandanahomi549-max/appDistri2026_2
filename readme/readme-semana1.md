# Aplicaciones Distribuidas
# Semana 1 - Introducción a las Aplicaciones Distribuidas y Desarrollo del Primer Microservicio para Clientes

**Carrera:** Desarrollo de Software  
**Asignatura:** Aplicaciones Distribuidas  
**Semana:** 1  
**Duración:** 4 Horas  
**Modalidad:** Teórico - Práctica

---

# Objetivo de la Semana

Al finalizar esta semana el estudiante será capaz de:

- Comprender los fundamentos de las aplicaciones distribuidas.
- Diferenciar una arquitectura monolítica de una arquitectura basada en microservicios.
- Comprender qué es una API REST y cómo funciona.
- Desarrollar un Microservicio utilizando ASP.NET Core 8.
- Implementar persistencia mediante Entity Framework Core Code First.
- Crear una Base de Datos SQL Server utilizando migraciones.
- Documentar automáticamente la API mediante Swagger.
- Implementar un CRUD completo utilizando buenas prácticas.

---

# Competencias

Al finalizar la semana el estudiante podrá:

- Analizar una arquitectura distribuida.
- Comprender la comunicación Cliente - API.
- Diseñar un microservicio independiente.
- Aplicar el patrón Repository.
- Implementar DTO para intercambio de información.
- Utilizar Entity Framework Core.
- Utilizar SQL Server desde Docker.
- Construir una API REST profesional.

---

# Resultado esperado

Al finalizar la clase el estudiante tendrá funcionando la siguiente arquitectura.

```text
                 Swagger

                     │

             MicroClientes.API

                     │

          Entity Framework Core

                     │

            SQL Server (Docker)
```

La API deberá permitir:

- GET Clientes
- GET Cliente por Id
- POST Cliente
- PUT Cliente
- DELETE Cliente

---

# Cronograma de la Clase

| Tiempo | Actividad |
|---------|-----------|
|30 min|Introducción a Aplicaciones Distribuidas|
|30 min|Arquitectura Web API y REST|
|20 min|HTTP y Métodos REST|
|20 min|Entity Framework Core|
|2h 20 min|Laboratorio|

La clase será aproximadamente:

- 25 % teoría
- 75 % práctica

---

# Arquitectura del Proyecto Integrador

Durante las cinco semanas construiremos la siguiente arquitectura.

```text
                    FrontEnd

                        │

                 API Gateway (Kong)

        ┌────────────┼─────────────┐

        │            │             │

 MicroClientes   MicroProductos   MicroVentas

    (.NET)         (Python)         (Java)

 SQL Server         MySQL         PostgreSQL

            RabbitMQ (Eventos)

              Docker Compose

                 Docker Hub
```

Durante esta primera semana únicamente construiremos el Microservicio de Clientes.

---

# Parte Teórica

## 1. ¿Qué es una Aplicación Distribuida?

Una aplicación distribuida es un sistema cuyos componentes se ejecutan en diferentes procesos, servidores o contenedores, comunicándose mediante una red.

Ejemplo:

```text
Cliente

↓

API

↓

Base de Datos
```

Una aplicación distribuida puede dividir el negocio en múltiples servicios independientes.

---

## 2. Arquitectura Monolítica

En una arquitectura monolítica toda la aplicación se encuentra en un solo proyecto.

```text
Usuario

↓

Sistema

↓

Base de Datos
```

### Desventajas

- Alto acoplamiento
- Difícil mantenimiento
- Difícil escalabilidad
- Un error afecta todo el sistema
- Todos los módulos deben desplegarse nuevamente

---

## 3. Arquitectura basada en Microservicios

```text
Usuario

↓

API Gateway

↓

Clientes

Productos

Ventas

↓

Bases de Datos Independientes
```

### Ventajas

- Escalabilidad
- Independencia
- Despliegue independiente
- Tecnologías diferentes
- Alta disponibilidad

---

# API REST

## ¿Qué es una API?

Una API permite que diferentes aplicaciones puedan comunicarse.

Ejemplo

```text
Aplicación Móvil

↓

API

↓

Base de Datos
```

---

## Endpoint

Ejemplo

```
/api/clientes
```

---

## URL

Ejemplo

```
http://localhost:5000/api/clientes
```

---

## Recurso

Ejemplos

```
Clientes

Productos

Ventas
```

---

## JSON

Ejemplo

```json
{
    "id":1,
    "nombre":"Carlos",
    "apellido":"Perez"
}
```

---

# Métodos HTTP

| Método | Acción |
|---------|--------|
|GET|Consultar|
|POST|Crear|
|PUT|Actualizar|
|DELETE|Eliminar|
|PATCH|Actualización parcial|
|OPTIONS|Consultar métodos disponibles|
|HEAD|Consultar encabezados|

---

## Query Parameters

Ejemplos

```
GET /clientes

GET /clientes/10

GET /clientes?nombre=Carlos

GET /clientes?page=1&pageSize=20

GET /clientes?estado=true
```

---

# Códigos HTTP

|Código|Descripción|
|-------|-----------|
|200|OK|
|201|Creado|
|204|Sin contenido|
|400|Solicitud incorrecta|
|401|No autorizado|
|403|Prohibido|
|404|No encontrado|
|409|Conflicto|
|500|Error interno|

---

# Entity Framework Core

## ¿Qué es un ORM?

Un ORM permite convertir objetos del lenguaje de programación en tablas de Base de Datos.

---

## Conceptos

- Entity Framework Core
- DbContext
- DbSet
- Code First
- Database First
- DataAnnotations
- Fluent API
- Migraciones

---

# Arquitectura del Microservicio

Durante la práctica construiremos la siguiente arquitectura.

```text
Controller

↓

Repository

↓

Entity Framework Core

↓

SQL Server
```

---

# Laboratorio

## Paso 1

Crear el proyecto

```
MicroClientes.API
```

Objetivo

Crear una Web API utilizando ASP.NET Core 8.

---

## Paso 2

Explicar la estructura del proyecto

```
Controllers

Models

DTO

Repository

Data

Services
```

Explicar la responsabilidad de cada carpeta.

---

## Paso 3

Instalar paquetes NuGet

```
Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools
```

Explicar para qué sirve cada uno.

---

## Paso 4

Crear EntityBase

Campos

- Id
- Estado
- FechaIngreso
- FechaModificacion

Explicar reutilización mediante herencia.

---

## Paso 5

Crear la entidad Cliente

Campos

- Id
- Nombre
- Apellido
- Cedula
- Email
- Telefono
- FechaNacimiento

Explicar DataAnnotations.

---

## Paso 6

Crear AppDbContext

Explicar

- DbContext
- DbSet
- Relación entre entidades y tablas

---

## Paso 7

Configurar SQL Server

- appsettings.json
- Connection String
- Program.cs

Explicar Inyección de Dependencias.

---

## Paso 8

Generar Migración

```
Add-Migration InitialCreate
```

Explicar qué genera Entity Framework.

---

## Paso 9

Crear Base de Datos

```
Update-Database
```

Verificar la creación de tablas.

---

## Paso 10

Crear DTO

Explicar

¿Por qué no devolver la entidad directamente?

Arquitectura

```text
Entidad

↓

DTO

↓

JSON

↓

Frontend
```

---

## Paso 11

Implementar Repository

Crear

- Interface
- Implementación

Explicar Repository Pattern.

---

## Paso 12

Registrar Dependencias

Registrar Repository

```
builder.Services.AddScoped()
```

Explicar:

- Scoped
- Singleton
- Transient

---

## Paso 13

Crear Controller

Implementar

- GET
- GET by Id
- POST
- PUT
- DELETE

Explicar los códigos HTTP adecuados.

---

## Paso 14

Probar la API

Utilizar Swagger.

Verificar

- GET
- POST
- PUT
- DELETE

Validar códigos de respuesta.

---

# Arquitectura Final de la Semana

```text
                Swagger

                    │

          ClientesController

                    │

         IClienteRepository

                    │

          ClienteRepository

                    │

            AppDbContext

                    │

       Entity Framework Core

                    │

        SQL Server (Docker)
```

---

# Resultado esperado

Al finalizar la práctica el estudiante dispondrá de:

- Proyecto ASP.NET Core 8.
- API REST funcional.
- CRUD completo.
- Entity Framework Core.
- SQL Server Docker.
- Base de Datos creada mediante Code First.
- Swagger funcionando.
- Arquitectura por capas.
- Patrón Repository.
- DTO.
- Inyección de Dependencias.

---

# Errores comunes

- Cadena de conexión incorrecta.
- SQL Server Docker apagado.
- No ejecutar Update-Database.
- DbContext no registrado.
- Repository no registrado.
- Error en DataAnnotations.
- Error de migraciones.

---

# Recursos

- Visual Studio 2022
- .NET 8 SDK
- Docker Desktop
- SQL Server
- Swagger
- Entity Framework Core

---

# Tarea

Cada estudiante deberá ampliar el microservicio implementando el módulo **Direcciones del Cliente**.

La solución deberá incluir:

- Entidad DireccionCliente.
- Relación 1:N con Cliente.
- CRUD completo.
- DTO.
- Repository.
- Migración adicional.
- Actualización de la Base de Datos.
- Pruebas mediante Swagger.

---

# Preparación para la Semana 2

En la siguiente semana se trabajará sobre el mismo proyecto para:

- Introducción a Git y GitHub.
- Docker.
- Dockerfile.
- Docker Compose.
- Docker Hub.
- API Gateway Kong.
- Dockerizar el Microservicio.
- Publicar la imagen en Docker Hub.
- Consumir la API mediante Kong.

De esta manera el Microservicio desarrollado en esta primera semana pasará a formar parte de la arquitectura distribuida que se construirá durante el resto del curso.