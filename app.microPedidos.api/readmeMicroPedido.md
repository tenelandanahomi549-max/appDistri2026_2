# app.microPedidos.api

Microservicio de pedidos construido con **FastAPI**, **SQLAlchemy**, **MySQL** y **RabbitMQ**. La aplicación tiene dos puntos de entrada independientes:

- Una **API HTTP**, responsable del CRUD de pedidos y de publicar el evento `pedidoRegistradoEvent`.
- Un **worker**, responsable de consumir `clienteDireccionEvent`, enviado por `app.microCliente.api`, y almacenar localmente la dirección del cliente.

Ambos procesos comparten los modelos, servicios, configuración y acceso a la misma base de datos, pero deben ejecutarse como procesos separados.

## Arquitectura

```mermaid
flowchart LR
    Client["Cliente HTTP"] -->|"CRUD /pedidos"| API["FastAPI<br/>main_api.py"]
    API --> PS["PedidoService"]
    PS -->|"Guardar pedido"| DB[("MySQL<br/>EcPedidos")]
    PS -->|"Publicar"| PQ["RabbitMQ<br/>pedidoRegistradoEvent"]

    MC["app.microCliente.api"] -->|"Publicar"| CQ["RabbitMQ<br/>clienteDireccionEvent"]
    CQ -->|"Consumir"| WK["Worker<br/>main_worker.py"]
    WK --> GS["GenericService"]
    GS -->|"Guardar dirección"| DB

    Other["Otros consumidores"] -->|"Consumir pedidos registrados"| PQ
```

### Responsabilidades por capa

```text
app.microPedidos.api/
├── main_api.py                    # Punto de entrada de la API FastAPI
├── main_worker.py                 # Punto de entrada del consumidor RabbitMQ
├── requirements.txt               # Dependencias de Python
└── app/
    ├── api/
    │   └── routes.py              # Endpoints HTTP de pedidos
    ├── core/
    │   ├── config.py              # Configuración de MySQL y RabbitMQ
    │   ├── database.py            # Engine, sesiones y Base de SQLAlchemy
    │   └── rabbitmq_producer.py   # Publicador de eventos RabbitMQ
    ├── models/
    │   └── models.py              # Entidades Pedido y ClienteDireccion
    ├── schemas/
    │   └── schemas.py             # Contratos Pydantic de la API
    ├── services/
    │   ├── generic_service.py     # Operaciones CRUD reutilizables
    │   ├── pedido_service.py      # Creación de pedido y publicación del evento
    │   ├── authService.py         # Validación Bearer/JWT
    │   └── jwt_manager.py         # Creación y lectura del token JWT
    └── worker/
        └── consumer.py            # Consumidor de clienteDireccionEvent
```

## Requisitos previos

- Python 3.10 o superior.
- MySQL disponible en `localhost:3307`.
- RabbitMQ disponible en `localhost:5672`.
- Una base de datos MySQL llamada `EcPedidos`.
- El microservicio `app.microCliente.api` para generar el evento de dirección de cliente.

La configuración actual se encuentra en `app/core/config.py`:

```python
DATABASE_URL = "mysql+mysqlconnector://root:admin@localhost:3307/EcPedidos"

RABBITMQ = {
    "username": "admin",
    "password": "admin",
    "virtualHost": "/",
    "port": 5672,
    "hostname": "localhost",
    "queue": "clienteDireccionEvent",
    "pedido_queue": "pedidoRegistradoEvent"
}
```

Adapte estos valores a su ambiente antes de iniciar los procesos. En producción, las credenciales y secretos deben obtenerse desde variables de entorno o un gestor de secretos.

## 1. Crear la base de datos

Ingrese a MySQL y ejecute:

```sql
CREATE DATABASE BddMicroPedidos;
```

El proyecto usa SQLAlchemy Code First y ejecuta `Base.metadata.create_all(...)` al iniciar la API o el worker.

> **Importante:** antes de arrancar, compruebe que la clave foránea de `Pedido` en `app/models/models.py` apunte al nombre real de la tabla de direcciones. La entidad declara la tabla `cliente_direcciones`; por tanto, la referencia coherente es `cliente_direcciones.id`. Si conserva la referencia actual a `direcciones_clientes.id`, SQLAlchemy generará `NoReferencedTableError` durante el inicio.

## 2. Crear el ambiente virtual `myenv`

Desde la raíz de `app.microPedidos.api`, cree el ambiente virtual:

### Windows PowerShell