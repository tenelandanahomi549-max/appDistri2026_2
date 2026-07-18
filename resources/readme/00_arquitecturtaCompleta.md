# Arquitectura del Proyecto Integrador
# Aplicaciones Distribuidas – Arquitectura basada en Microservicios

## Objetivo

El proyecto integrador tiene como objetivo desarrollar una arquitectura basada en microservicios utilizando diferentes tecnologías y mecanismos de comunicación síncrona y asíncrona.

Durante las cinco semanas del curso, los estudiantes construirán una solución distribuida compuesta por tres microservicios independientes que se comunicarán mediante **RabbitMQ** y serán consumidos desde una aplicación Frontend.

La arquitectura busca que cada microservicio sea completamente independiente, tenga su propia base de datos y comparta información únicamente mediante eventos.

---

# Arquitectura General

```text
                     Frontend
        (Angular | React | Vue | Flutter)
                        |
                        |
                  API Gateway Kong
                        |
     -------------------------------------------------
     |                       |                       |
     |                       |                       |
     v                       v                       v
MicroClientes          MicroPedidos          MicroFacturas
 ASP.NET Core             FastAPI             Spring Boot
 SQL Server               MySQL              PostgreSQL
     |                       |                     |
     |                       |                     |
     |                       |                     |
     +----------- RabbitMQ -----------+
```

---

# Tecnologías

| Componente | Tecnología |
|------------|------------|
| Frontend | Angular / React / Vue / Flutter |
| API Gateway | Kong |
| MicroClientes | ASP.NET Core (.NET 10) |
| MicroPedidos | Python FastAPI |
| MicroFacturas | Java Spring Boot |
| Mensajería | RabbitMQ |
| Base de datos Clientes | SQL Server |
| Base de datos Pedidos | MySQL |
| Base de datos Facturas | PostgreSQL |
| Contenedores | Docker |
| Orquestación | Docker Compose |

---

# Principios de la arquitectura

La solución sigue los principios de una arquitectura basada en microservicios.

- Cada microservicio posee su propia base de datos.
- Ningún microservicio consulta directamente la base de datos de otro.
- La comunicación entre microservicios se realiza mediante eventos.
- Cada microservicio puede desplegarse de forma independiente.
- Cada proyecto utiliza una tecnología distinta para demostrar interoperabilidad.

---

# Arquitectura de MicroClientes

## Responsabilidad

Administrar la información de clientes.

Este microservicio será desarrollado con:

- ASP.NET Core
- Entity Framework Core
- SQL Server

---

## Funcionalidades

- CRUD de Clientes
- CRUD de Direcciones
- Relación Cliente → Direcciones
- Publicación del evento clienteDireccionEvent

---

## Modelo

```text
Cliente
---------
ClienteId
Nombres
Apellidos
Email
Telefono
Cedula

Direccion
------------
DireccionId
ClienteId
Provincia
Ciudad
CallePrincipal
CalleSecundaria
NumeroCasa
Referencia
```

---

## Evento publicado

Cuando un cliente tenga registrada una dirección válida, el microservicio publicará el siguiente DTO.

```csharp
public class ClienteDireccionEventDto
{
    public int ClienteId { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string DireccionCompleta { get; set; } = string.Empty;
}
```

Ejemplo

```json
{
    "clienteId": 15,
    "nombreCompleto": "Carlos Pérez",
    "email": "carlos@email.com",
    "direccionCompleta": "Av. Amazonas y Naciones Unidas"
}
```

Publica:

```
clienteDireccionEvent
```

---

# Arquitectura de MicroPedidos

## Responsabilidad

Administrar los pedidos.

Este microservicio será desarrollado con

- FastAPI
- SQLAlchemy
- MySQL

---

## Funcionalidades

- Consumir clienteDireccionEvent
- Guardar ClienteDireccion localmente
- CRUD de Pedidos
- Publicar pedidoRegistradoEvent

---

## Modelo

### ClienteDireccion

```text
Id
ClienteId
NombreCompleto
Email
DireccionCompleta
```

Esta tabla es una copia local de la información enviada por MicroClientes.

---

### Pedido

```text
PedidoId
ClienteDireccionId
Descripcion
Producto
Cantidad
PrecioUnitario
Total
Estado
FechaPedido
```

---

## Flujo

Cuando llega un evento desde RabbitMQ

```text
clienteDireccionEvent
```

el Worker almacena la información en

```text
ClienteDireccion
```

Posteriormente el usuario registra un pedido utilizando esa información.

---

## Evento publicado

Cuando el pedido se registra correctamente se publica

```
pedidoRegistradoEvent
```

DTO

```python
class PedidoRegistradoEventDto:

    pedidoId:int

    clienteId:int

    nombreCliente:str

    email:str

    direccionEntrega:str

    producto:str

    cantidad:int

    precioUnitario:float

    total:float
```

Ejemplo

```json
{
    "pedidoId": 25,
    "clienteId": 15,
    "nombreCliente": "Carlos Pérez",
    "email": "carlos@email.com",
    "direccionEntrega": "Av. Amazonas y Naciones Unidas",
    "producto": "Laptop Lenovo",
    "cantidad": 1,
    "precioUnitario": 850,
    "total": 850
}
```

---

# Arquitectura de MicroFacturas

## Responsabilidad

Administrar las facturas.

Este microservicio será desarrollado con

- Spring Boot
- Spring Data JPA
- PostgreSQL

---

## Funcionalidades

- Consumir pedidoRegistradoEvent
- Guardar una copia local del pedido
- CRUD de Facturas

---

## Modelo Pedido

```text
PedidoId
ClienteId
NombreCliente
Email
DireccionEntrega
Producto
Cantidad
PrecioUnitario
TotalPedido
FechaPedido
```

Esta tabla es una copia local del pedido recibido desde RabbitMQ.

---

## Modelo Factura

```text
FacturaId
PedidoId
NumeroFactura
FechaEmision
Subtotal
IVA
Total
Estado
```

---

## Flujo de Facturación

Cuando el usuario desea emitir una factura únicamente selecciona un pedido previamente recibido.

Ejemplo

```text
Pedido 25
```

El sistema calcula automáticamente

```text
Subtotal

IVA

Total
```

y almacena la factura.

---

# RabbitMQ

La arquitectura únicamente utiliza dos eventos.

## Evento 1

```
clienteDireccionEvent
```

Publicador

```
MicroClientes
```

Consumidor

```
MicroPedidos
```

---

## Evento 2

```
pedidoRegistradoEvent
```

Publicador

```
MicroPedidos
```

Consumidor

```
MicroFacturas
```

---

# Flujo completo de la solución

## Paso 1

El usuario registra un cliente.

↓

## Paso 2

Registra una dirección.

↓

## Paso 3

MicroClientes publica

```
clienteDireccionEvent
```

↓

## Paso 4

MicroPedidos consume el evento.

↓

## Paso 5

Guarda la información del cliente.

↓

## Paso 6

El usuario registra un pedido.

↓

## Paso 7

PedidoService publica

```
pedidoRegistradoEvent
```

↓

## Paso 8

MicroFacturas consume el evento.

↓

## Paso 9

Guarda el pedido.

↓

## Paso 10

El usuario genera la factura.

↓

## Paso 11

La factura queda registrada.

---

# Diagrama de Arquitectura

```text
                            FRONTEND
                 Angular | React | Vue | Flutter
                                   │
                                   │
                          API Gateway Kong
                                   │
        ┌──────────────────────────┼──────────────────────────┐
        │                          │                          │
        ▼                          ▼                          ▼
┌─────────────────┐      ┌─────────────────┐      ┌─────────────────┐
│ MicroClientes   │      │ MicroPedidos    │      │ MicroFacturas   │
│ ASP.NET Core    │      │ FastAPI         │      │ Spring Boot      │
│ SQL Server      │      │ MySQL           │      │ PostgreSQL       │
└────────┬────────┘      └────────┬────────┘      └────────┬────────┘
         │                        ▲                        ▲
         │                        │                        │
         │ PUB                    │ SUB                    │ SUB
         ▼                        │                        │
       RabbitMQ ───────────────► clienteDireccionEvent
         │
         │ PUB
         ▼
       RabbitMQ ───────────────► pedidoRegistradoEvent
```

---

# Distribución por semanas

## Semana 1

MicroClientes

- CRUD Clientes
- CRUD Direcciones

---

## Semana 2

Dockerización

- Docker
- Docker Hub
- Kong
- JWT

---

## Semana 3

MicroPedidos

- Worker RabbitMQ
- CRUD Pedidos

---

## Semana 4

MicroFacturas

- Consumidor RabbitMQ
- CRUD Facturas

---

## Semana 5

Proyecto Integrador

- Docker Compose
- RabbitMQ
- Kong
- Integración completa
- Pruebas entre microservicios
- Consumo desde Frontend

---

# Resultado esperado

Al finalizar el curso el estudiante habrá desarrollado una arquitectura distribuida conformada por:

- 3 Microservicios
- 3 Bases de datos
- 2 Eventos RabbitMQ
- API Gateway Kong
- Frontend Web o Móvil
- Docker Compose para el despliegue completo

La solución implementará buenas prácticas de desacoplamiento, comunicación asíncrona y arquitectura basada en eventos, permitiendo comprender el funcionamiento de un sistema distribuido moderno.