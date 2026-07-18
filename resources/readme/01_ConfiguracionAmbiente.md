# Configuración del Ambiente de Desarrollo

## Carrera
**Desarrollo de Software**

## Materia
**Aplicaciones Distribuidas**

---

# Objetivo

Al finalizar esta guía el estudiante tendrá instalado y configurado todo el software necesario para desarrollar las prácticas de la materia **Aplicaciones Distribuidas**, incluyendo herramientas para desarrollar APIs, crear contenedores Docker, consumir servicios REST y administrar proyectos con GitHub.

---

# Software a instalar

| Software | Obligatorio |
|-----------|-------------|
| Docker Desktop | ✅ |
| WSL2 | ✅ |
| Visual Studio 2022 Community | ✅ |
| .NET SDK 10 | ✅ |
| Python 3.13 o superior | ✅ |
| Visual Studio Code | ✅ |
| GitHub | ✅ |
| Postman | ✅ |

---

# 1. Instalar Docker Desktop

Docker permitirá ejecutar aplicaciones dentro de contenedores, facilitando la creación de ambientes de desarrollo consistentes y portables.

## Descargar Docker

Sitio oficial:

https://www.docker.com/products/docker-desktop/

Seleccione la versión correspondiente a Windows.

---

## Instalación

1. Ejecutar el instalador.
2. Aceptar los términos de licencia.
3. Mantener las opciones predeterminadas.
4. Permitir que Docker instale WSL2 si lo solicita.
5. Reiniciar el computador.

---

## Verificar la instalación

Abrir una consola (CMD o PowerShell) y ejecutar:

```bash
docker --version
```

Resultado esperado:

```
Docker version XX.XX.X
```

---

## Verificar que Docker esté iniciado

```bash
docker ps
```

Resultado esperado:

```
CONTAINER ID   IMAGE   COMMAND   CREATED   STATUS   PORTS   NAMES
```

Si no existen contenedores, la lista aparecerá vacía.

---

## Mostrar todos los contenedores

```bash
docker ps -a
```

Este comando muestra:

- Contenedores ejecutándose.
- Contenedores detenidos.
- Fecha de creación.
- Estado.
- Nombre.

Ejemplo:

```
CONTAINER ID   IMAGE      STATUS
9a3bc22        python     Exited
4ad8bc9        nginx      Up 3 hours
```

---

# 2. Validar WSL

Docker Desktop utiliza WSL2 para ejecutar contenedores Linux de forma eficiente.

## Verificar si WSL está instalado

Abrir PowerShell como Administrador.

Ejecutar:

```bash
wsl --status
```

Si aparece información similar a:

```
Default Version: 2
```

WSL ya se encuentra instalado.

---

## Verificar distribuciones instaladas

```bash
wsl --list --verbose
```

Ejemplo:

```
Ubuntu
Version 2
Running
```

---

# 3. Instalar WSL

Si el comando anterior genera error, instalar WSL.

Abrir PowerShell como Administrador.

Ejecutar:

```bash
wsl --install
```

Esperar que termine la instalación.

Reiniciar el computador.

---

## Confirmar instalación

```bash
wsl --status
```

Debe mostrar información de la versión instalada.

---

# 4. Instalar Visual Studio 2022 Community

Visual Studio será utilizado para desarrollar aplicaciones .NET.

## Descargar

https://visualstudio.microsoft.com/es/thank-you-downloading-visual-studio/?sku=Community&channel=Stable&version=VS18&source=VSLandingPage&passive=false&cid=2500

---

## Durante la instalación seleccionar

✔ Desarrollo de ASP.NET y Web

✔ Desarrollo de escritorio con .NET

✔ Herramientas de Git

✔ SDK de Windows

No es necesario instalar todas las cargas de trabajo disponibles.

---

## Verificar instalación

Abrir Visual Studio.

Debe iniciar sin errores.

---

# 5. Instalar .NET SDK 10

El SDK permite crear y compilar aplicaciones .NET.

## Descargar

https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-10.0.301-windows-x64-installer

---

## Verificar instalación

Abrir una consola.

Ejecutar:

```bash
dotnet --version
```

Resultado esperado:

```
10.0.xxx
```

---

## Ver información del SDK

```bash
dotnet --info
```

Este comando muestra:

- SDK instalado
- Runtime instalado
- Arquitectura
- Sistema operativo

---

# 6. Instalar Python

Python será utilizado para desarrollar microservicios y aplicaciones de apoyo.

## Descargar

https://www.python.org/downloads/

Instalar la versión más reciente disponible (3.13 o superior).

---

## Importante

Durante la instalación marcar la opción:

```
Add Python to PATH
```

---

## Verificar instalación

Abrir CMD.

Ejecutar:

```bash
python --version
```

Resultado esperado:

```
Python 3.13.x
```

También puede verificarse con:

```bash
python
```

Debe abrir el intérprete interactivo.

Salir con:

```python
exit()
```

---

# 7. Instalar Visual Studio Code

Visual Studio Code será utilizado para desarrollar proyectos Python, Docker y otros lenguajes.

## Descargar

https://code.visualstudio.com/

---

## Durante la instalación seleccionar

✔ Agregar al PATH

✔ Registrar como editor predeterminado

✔ Agregar opción "Abrir con Code"

---

## Extensiones recomendadas

Instalar las siguientes extensiones:

- Python
- Docker
- C#
- GitHub Copilot (opcional)
- REST Client
- Material Icon Theme

---

# 8. Crear una cuenta en GitHub

GitHub será utilizado para almacenar los proyectos desarrollados durante la materia.

## Crear cuenta

https://github.com

---

## Recomendaciones

Utilizar un nombre profesional.

Ejemplo:

```
juanperez
```

Evitar nombres poco formales como:

```
juan12345lol
```

---

## Verificar acceso

Ingresar al sitio web.

Crear un repositorio llamado:

```
AplicacionesDistribuidas
```

---

# 9. Instalar Postman

Postman será utilizado para probar APIs REST desarrolladas durante el curso.

## Descargar

https://www.postman.com/downloads/

---

## Verificar instalación

Abrir Postman.

Crear una colección llamada:

```
Aplicaciones Distribuidas
```

Crear una petición GET de prueba.

---

# 10. Verificación Final

Antes de iniciar las clases, comprobar que todos los programas se encuentran instalados correctamente.

## Docker

```bash
docker --version
```

---

## Contenedores activos

```bash
docker ps
```

---

## Todos los contenedores

```bash
docker ps -a
```

---

## Estado de WSL

```bash
wsl --status
```

---

## Versiones instaladas de WSL

```bash
wsl --list --verbose
```

---

## SDK de .NET

```bash
dotnet --version
```

---

## Información del SDK

```bash
dotnet --info
```

---

## Python

```bash
python --version
```

---

## Git (opcional)

```bash
git --version
```

---

# Resultado esperado

Al finalizar esta guía el estudiante deberá contar con:

- Docker Desktop instalado y funcionando.
- WSL2 instalado y configurado.
- Visual Studio 2022 Community.
- .NET SDK 10.
- Python instalado y agregado al PATH.
- Visual Studio Code.
- Cuenta activa en GitHub.
- Postman instalado.
- Ambiente listo para desarrollar aplicaciones distribuidas durante el semestre.

---

# Recomendaciones

- Reiniciar el equipo después de instalar Docker y WSL.
- Mantener actualizado Docker Desktop.
- Utilizar siempre versiones estables del software.
- Crear una carpeta de trabajo, por ejemplo:

```
C:\Desarrollo\
```

Dentro de esta carpeta se almacenarán todos los proyectos desarrollados durante la asignatura.