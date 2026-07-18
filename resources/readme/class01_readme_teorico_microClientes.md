# Aplicaciones Distribuidas
# Semana 1 – Fundamentos de Microservicios y Desarrollo de MicroClientes

**Carrera:** Desarrollo de Software  
**Asignatura:** Aplicaciones Distribuidas  
**Unidad:** Unidad 1 – Fundamentos de Aplicaciones Distribuidas

---

# Objetivo de aprendizaje

Al finalizar esta semana el estudiante comprenderá los fundamentos de una arquitectura basada en microservicios y desarrollará el primer componente del proyecto integrador: **MicroClientes**, implementando una API REST con ASP.NET Core, Entity Framework Core y SQL Server.

---

# ¿Qué aprendimos esta semana?

Durante esta primera semana iniciamos el desarrollo del proyecto integrador construyendo el primer microservicio denominado **MicroClientes**.

Este proyecto permitió comprender cómo se estructura una aplicación distribuida moderna utilizando arquitectura por capas, principios SOLID y el patrón Repository.

---

# ¿Qué es una Aplicación Distribuida?

Una aplicación distribuida es un sistema compuesto por varios componentes independientes que trabajan conjuntamente para ofrecer una solución completa.

Cada componente puede ejecutarse en un servidor diferente, utilizar una tecnología distinta y poseer su propia base de datos.

Ejemplo:

```text
Frontend

     │

     ▼

API Gateway

     │

 ┌───┴───────────────┐

 ▼                   ▼

MicroClientes     MicroPedidos

```

---

# ¿Qué es un Microservicio?

Un microservicio es una aplicación pequeña, independiente y especializada en una única responsabilidad del negocio.

Cada microservicio:

- Tiene su propio proyecto.
- Posee su propia base de datos.
- Se despliega de forma independiente.
- Puede desarrollarse con una tecnología diferente.

Ejemplo:

```text
MicroClientes

↓

Administra Clientes
```

---

# Arquitectura del Proyecto Integrador

Durante el curso construiremos tres microservicios.

```text
MicroClientes

↓

MicroPedidos

↓

MicroFacturas
```

Cada uno tendrá:

- API REST
- Base de datos propia
- Docker
- Comunicación mediante RabbitMQ

---

# ¿Qué es una API REST?

Una API REST es un conjunto de servicios HTTP que permiten que diferentes aplicaciones intercambien información.

Los métodos más utilizados son:

| Método | Acción |
|---------|--------|
| GET | Consultar información |
| POST | Crear información |
| PUT | Actualizar información |
| DELETE | Eliminar información |

Ejemplo:

```http
GET /api/clientes

POST /api/clientes

PUT /api/clientes/5

DELETE /api/clientes/5
```

---

# Arquitectura por Capas

Durante esta semana implementamos una arquitectura organizada en capas para separar responsabilidades y facilitar el mantenimiento del código.

```text
Controller

↓

Service

↓

Repository

↓

Entity Framework

↓

SQL Server
```

Cada capa tiene una responsabilidad específica.

---

# Controller

Es la capa que recibe las solicitudes HTTP provenientes del cliente.

Ejemplo:

```text
GET

POST

PUT

DELETE
```

El Controller no contiene reglas de negocio; únicamente recibe la petición y delega el procesamiento al Service.

---

# Service

Contiene la lógica del negocio.

Su responsabilidad es validar la información, coordinar operaciones y comunicarse con el Repository.

Ejemplo:

```text
Registrar Cliente

Actualizar Cliente

Eliminar Cliente
```

---

# Repository

El Repository encapsula el acceso a la base de datos.

Gracias a este patrón, el resto de la aplicación no necesita conocer cómo se realizan las consultas.

Ejemplo:

```text
Insertar

Actualizar

Eliminar

Consultar
```

---

# Entity Framework Core

Entity Framework Core es un ORM (Object Relational Mapper) que permite trabajar con objetos de C# en lugar de escribir consultas SQL para las operaciones básicas.

Beneficios:

- Reduce la escritura de SQL.
- Facilita el mantenimiento.
- Implementa Code First.
- Genera automáticamente las tablas de la base de datos.

---

# Code First

Code First consiste en crear primero las clases del proyecto y posteriormente generar automáticamente la base de datos.

Flujo:

```text
Clase C#

↓

DbContext

↓

Migraciones

↓

SQL Server
```

---

# ¿Qué es una Entidad?

Una entidad representa una tabla de la base de datos.

Ejemplo:

```csharp
Cliente
```

```text
Cliente

ClienteId

Nombres

Apellidos

Email

Telefono
```

---

# ¿Qué es un DTO?

DTO significa **Data Transfer Object**.

Es un objeto utilizado para transportar información entre capas o entre aplicaciones.

No representa directamente una tabla de la base de datos.

Ejemplo:

```csharp
ClienteDto
```

El DTO permite enviar únicamente la información necesaria al cliente.

---

# DbContext

El DbContext representa la conexión entre la aplicación y la base de datos.

Es el encargado de administrar:

- Consultas
- Inserciones
- Actualizaciones
- Eliminaciones

---

# SQL Server

SQL Server será la base de datos utilizada por el primer microservicio.

En esta semana aprendimos a:

- Crear la base de datos.
- Configurar la cadena de conexión.
- Ejecutar migraciones.
- Persistir información mediante Entity Framework Core.

---

# CRUD

CRUD representa las cuatro operaciones básicas sobre cualquier entidad.

| Operación | Método HTTP |
|------------|-------------|
| Create | POST |
| Read | GET |
| Update | PUT |
| Delete | DELETE |

Estas operaciones fueron implementadas para:

- Clientes
- Direcciones

---

# Relación Cliente – Dirección

Cada cliente puede tener una o varias direcciones.

```text
Cliente

      1

      │

      │

      N

Dirección
```

Esta relación se implementó mediante claves foráneas utilizando Entity Framework Core.

---

# Docker

Aunque la dockerización completa se realizará en semanas posteriores, durante esta semana se introdujo el concepto de contenedores.

Docker permite empaquetar una aplicación junto con todas sus dependencias para que pueda ejecutarse de forma consistente en cualquier entorno.

Beneficios:

- Portabilidad.
- Despliegue rápido.
- Aislamiento de dependencias.
- Facilidad para trabajar con microservicios.

---

# Arquitectura desarrollada en la práctica

Durante la práctica se implementó la siguiente arquitectura.

```text
Cliente HTTP

      │

      ▼

Controller

      │

      ▼

Service

      │

      ▼

Repository

      │

      ▼

Entity Framework Core

      │

      ▼

SQL Server
```

---

# Relación con el Proyecto Integrador

El microservicio desarrollado durante esta semana constituye la base del proyecto integrador.

En la siguiente semana este microservicio evolucionará para publicar eventos hacia RabbitMQ.

```text
Semana 1

MicroClientes

↓

Semana 2

RabbitMQ

↓

MicroPedidos
```

Por ello era indispensable construir correctamente el CRUD de Clientes y Direcciones antes de comenzar la comunicación entre microservicios.

---

# Competencias desarrolladas

Al finalizar esta semana el estudiante es capaz de:

- Comprender qué es una aplicación distribuida.
- Diferenciar una arquitectura monolítica de una arquitectura basada en microservicios.
- Comprender la arquitectura por capas.
- Implementar una API REST utilizando ASP.NET Core.
- Aplicar el patrón Repository.
- Utilizar Entity Framework Core con Code First.
- Implementar CRUD completos.
- Modelar relaciones entre entidades.
- Persistir información en SQL Server.
- Preparar el microservicio para integrarlo posteriormente mediante RabbitMQ.

---

# Conclusión

La primera semana establece las bases del proyecto integrador. El estudiante desarrolla el microservicio **MicroClientes**, comprende la organización de una API moderna y aplica buenas prácticas de desarrollo. Estos conocimientos serán esenciales para la siguiente etapa del curso, donde el microservicio comenzará a comunicarse con **MicroPedidos** mediante RabbitMQ, dando inicio a una arquitectura distribuida basada en eventos.