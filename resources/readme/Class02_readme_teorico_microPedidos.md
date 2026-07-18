````markdown
# Aplicaciones Distribuidas
# Semana 2 – Comunicación Asíncrona con RabbitMQ y Arquitectura del MicroPedidos

**Carrera:** Desarrollo de Software  
**Asignatura:** Aplicaciones Distribuidas  
**Unidad:** Unidad 2 – Comunicación entre Microservicios

---

# Objetivo de aprendizaje

Al finalizar esta semana el estudiante comprenderá cómo dos microservicios pueden comunicarse mediante eventos utilizando RabbitMQ, implementando una arquitectura desacoplada donde un microservicio publica información y otro la consume de forma independiente.

---

# ¿Qué aprenderemos esta semana?

Durante la primera semana se desarrolló el microservicio **MicroClientes**, encargado de administrar clientes y direcciones.

En esta segunda semana construiremos el segundo microservicio denominado **MicroPedidos**, el cual recibirá automáticamente información enviada por MicroClientes mediante RabbitMQ.

Esta práctica permitirá comprender cómo se comunican los microservicios sin necesidad de compartir la misma base de datos ni realizar llamadas HTTP entre ellos.

---

# Arquitectura que desarrollaremos

```text
                 Frontend

                     │

                     ▼

            API Gateway (Kong)

                     │

         ┌───────────┴───────────┐

         ▼                       ▼

MicroClientes             MicroPedidos

ASP.NET Core                 FastAPI

SQL Server                    MySQL

         │

         │ Publica Evento

         ▼

      RabbitMQ

         │

         │ Consume Evento

         ▼

   Worker MicroPedidos
```

---

# ¿Qué es RabbitMQ?

RabbitMQ es un **Message Broker** o intermediario de mensajes.

Su función principal consiste en recibir mensajes enviados por una aplicación y entregarlos posteriormente a otra aplicación interesada.

En lugar de que dos microservicios se comuniquen directamente, ambos utilizan RabbitMQ como intermediario.

```text
Micro A

   │

   ▼

RabbitMQ

   │

   ▼

Micro B
```

Este mecanismo reduce el acoplamiento entre aplicaciones y permite construir arquitecturas distribuidas más escalables y resilientes.

---

# Comunicación síncrona vs comunicación asíncrona

## Comunicación síncrona

En una comunicación síncrona un microservicio espera la respuesta del otro para continuar su ejecución.

Ejemplo:

```text
MicroClientes

      │

HTTP Request

      ▼

MicroPedidos

      │

HTTP Response

      ▼

MicroClientes continúa
```

Si MicroPedidos deja de funcionar, MicroClientes también fallará.

---

## Comunicación asíncrona

En la comunicación asíncrona el productor únicamente publica un mensaje y continúa trabajando.

No necesita esperar ninguna respuesta.

```text
MicroClientes

      │

Publica mensaje

      ▼

RabbitMQ

      │

      ▼

MicroPedidos

(consume cuando esté disponible)
```

Esta es la arquitectura que implementaremos durante esta semana.

---

# Concepto de Productor (Publisher)

El **Productor** es el componente encargado de enviar mensajes hacia RabbitMQ.

En nuestro proyecto:

```text
MicroClientes

↓

Publisher

↓

RabbitMQ
```

Cuando un cliente registra una dirección, MicroClientes construye un objeto y lo envía automáticamente a RabbitMQ.

---

# Concepto de Consumidor (Consumer)

El **Consumidor** es el componente encargado de escuchar continuamente una cola.

Cuando aparece un mensaje nuevo, lo procesa.

En nuestro proyecto:

```text
RabbitMQ

↓

Consumer

↓

MicroPedidos
```

---

# ¿Qué es un Evento?

Un evento representa algo importante que ocurrió dentro del sistema.

Ejemplos:

- Cliente registrado
- Pedido registrado
- Factura emitida
- Producto actualizado

En nuestro caso:

```
clienteDireccionEvent
```

significa:

> "Existe un cliente con una dirección registrada."

---

# DTO de Eventos

No es recomendable enviar entidades completas entre microservicios.

En su lugar se envían **DTOs** (Data Transfer Objects).

Nuestro evento será:

```csharp
public class ClienteDireccionEventDto
{
    public int ClienteId { get; set; }

    public string NombreCompleto { get; set; }

    public string Email { get; set; }

    public string DireccionCompleta { get; set; }
}
```

Este objeto únicamente contiene la información necesaria para que MicroPedidos pueda trabajar.

---

# ¿Por qué no compartir la base de datos?

En una arquitectura distribuida cada microservicio es propietario de su información.

Por ello:

MicroClientes posee su propia base de datos.

```text
SQL Server
```

MicroPedidos posee otra base distinta.

```text
MySQL
```

Nunca accederemos desde MicroPedidos a SQL Server.

La única forma de compartir información será mediante RabbitMQ.

---

# ¿Qué sucede cuando se registra un cliente?

El flujo será el siguiente.

```text
1.

Cliente registrado

↓

2.

Dirección registrada

↓

3.

MicroClientes construye

ClienteDireccionEventDto

↓

4.

Publica

clienteDireccionEvent

↓

5.

RabbitMQ almacena el mensaje

↓

6.

Worker consume el mensaje

↓

7.

MicroPedidos guarda ClienteDireccion
```

---

# ¿Qué es un Worker?

Un Worker es un proceso que permanece ejecutándose continuamente.

Su única responsabilidad consiste en escuchar RabbitMQ.

No expone APIs.

No recibe peticiones HTTP.

Simplemente espera mensajes.

```text
RabbitMQ

↓

Worker

↓

Guardar información
```

---

# ¿Por qué MicroPedidos tiene dos entradas?

Este proyecto posee dos responsabilidades diferentes.

## Primera entrada

```text
main_api.py
```

Responsabilidad:

- CRUD de Pedidos.
- Endpoints REST.
- Swagger.

---

## Segunda entrada

```text
main_worker.py
```

Responsabilidad:

- Escuchar RabbitMQ.
- Consumir eventos.
- Guardar ClienteDireccion.

---

Ambos procesos trabajan simultáneamente.

```text
          FastAPI

             │

             ▼

        CRUD Pedidos

             │

             ▼

            MySQL

             ▲

             │

      Worker RabbitMQ
```

---

# ¿Qué almacenará MicroPedidos?

Cuando llegue un evento, el Worker almacenará la información en una tabla local.

```text
ClienteDireccion

--------------------------

Id

ClienteId

NombreCompleto

Email

DireccionCompleta
```

Esta tabla NO pertenece a MicroClientes.

Es únicamente una copia local necesaria para trabajar.

---

# ¿Qué ventajas tiene esta arquitectura?

## Bajo acoplamiento

Los microservicios no dependen unos de otros.

---

## Escalabilidad

Cada microservicio puede crecer de manera independiente.

---

## Independencia tecnológica

Cada proyecto utiliza una tecnología diferente.

- ASP.NET Core
- FastAPI
- Spring Boot

---

## Independencia de base de datos

Cada microservicio administra su propia información.

---

## Tolerancia a fallos

Si MicroPedidos está apagado, RabbitMQ mantiene los mensajes hasta que vuelva a estar disponible.

---

# Relación con la práctica

Durante el laboratorio el estudiante implementará exactamente el flujo estudiado en este documento.

## Paso 1

Ejecutar MicroClientes.

---

## Paso 2

Registrar un cliente.

---

## Paso 3

Registrar una dirección.

---

## Paso 4

MicroClientes publicará automáticamente:

```
clienteDireccionEvent
```

---

## Paso 5

Levantar el Worker.

---

## Paso 6

Verificar que el Worker recibe el evento.

---

## Paso 7

Guardar ClienteDireccion en MySQL.

---

## Paso 8

Levantar la API FastAPI.

---

## Paso 9

Consultar la información almacenada.

---

# Resultado esperado

Al finalizar esta semana el estudiante comprenderá:

- La diferencia entre comunicación síncrona y asíncrona.
- El funcionamiento de RabbitMQ.
- El rol de un Publisher y un Consumer.
- El uso de DTOs para compartir información entre microservicios.
- La arquitectura de doble entrada (API + Worker).
- La importancia de mantener bases de datos independientes.
- Cómo implementar una comunicación basada en eventos entre MicroClientes y MicroPedidos.

Estos conocimientos servirán como base para la siguiente semana, donde **MicroPedidos** publicará el evento `pedidoRegistradoEvent`, que será consumido por el microservicio **MicroFacturas**, completando la cadena de integración del proyecto.
````
