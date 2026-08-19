# Documentación HU01: Base de Datos Local e Inicialización de Tablas

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-2 (HU01)
* **Épica:** SCRUM-41 - Infraestructura de Datos e Ingesta de Artículos (Prioridad: Highest)
* **Story Points:** 2
* **Responsable:** Anthonny Brayhan Soriano Franco (20242266@itla.edu.do)
* **Subtareas Jira:** SCRUM-45, SCRUM-46, SCRUM-47

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Equipo de Desarrollo,  
> **quiero** estructurar una base de datos centralizada e independiente,  
> **para** almacenar la información de los artículos, las direcciones de entrega y los choferes de forma aislada sin afectar sistemas de producción.

---

## ⚙️ Implementación Técnica

1. **Motor de Persistencia y Modelado (EF Core)**:
   * `OrionDbContext.cs`: Configuración del contexto relacional con Entity Framework Core SQLite (`orion.db`).
   * Creación de entidades normalizadas:
     * `Driver.cs`: Almacena información de los conductores del piloto y estado de su jornada.
     * `Route.cs`: Estructura la jornada operativa, asignación a conductor, odómetros inicial/final y distancia recorrida.
     * `RouteStop.cs`: Modela cada parada/artículo, incluyendo orden (`Sequence`), coordenadas (`Latitude`, `Longitude`), cliente, dirección y estado (`Pendiente`, `Entregado`, `No entregado`).
     * `OdometerLog.cs`: Auditoría temporal de registros de kilometraje.
     * `HistoricalRoute.cs`: Comparativa de referencia para auditoría de ahorro de combustible.

2. **Inicialización y Poblado de Datos Base (Seed Data)**:
   * Inicialización automática mediante `context.Database.EnsureCreated()` en el arranque del sistema (`Program.cs`).
   * Sembrado inicial en `OrionDbContext.OnModelCreating` de los 5 conductores fijos del piloto:
     * Conductor 1: *Carlos Santana*
     * Conductor 2: *Manuel Rodríguez*
     * Conductor 3: *José Gómez*
     * Conductor 4: *David Martínez*
     * Conductor 5: *Pedro Almonte*

3. **Verificación de Salud de Conexión**:
   * `DatabaseHealthService.cs` / `IDatabaseHealthService.cs`: Verificación activa de conectividad y conteo de registros para asegurar el correcto funcionamiento del almacenamiento.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Desplegar e inicializar la instancia de base de datos exclusiva para el piloto. | ✅ Cumplido | Inicializado en SQLite / EF Core mediante `EnsureCreated()` en `Program.cs`. |
| 2 | Crear tablas: `Conductores`, `Rutas` y `Articulos` (`RouteStops`) con sus llaves foráneas. | ✅ Cumplido | Modelos `Driver`, `Route` y `RouteStop` mapeados con relaciones 1:N y llaves foráneas en `OrionDbContext.cs`. |
| 3 | Poblar de forma manual/seed los perfiles de los 5 conductores fijos del piloto. | ✅ Cumplido | 5 conductores precargados en `OnModelCreating` de `OrionDbContext.cs`. |
| 4 | Probar la conexión mediante un script / servicio de persistencia básico. | ✅ Cumplido | Implementado con `DatabaseHealthService.cs` y verificado en el arranque. |

---

## 🖼️ Evidencias en Jira Software

### Detalles del Ticket HU01 (SCRUM-2)
![Detalles del Ticket HU01 en Jira (SCRUM-2) - Asignación, Épica SCRUM-41 y Estimación](Screenshot%202026-08-19%20124017.png)

### Criterios y Desglose de Subtareas (SCRUM-45, SCRUM-46, SCRUM-47)
![Criterios y Desglose de Subtareas de HU01 (SCRUM-45, SCRUM-46, SCRUM-47)](Screenshot%202026-08-19%20123914.png)

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU01: Base de datos relacional SQLite, esquemas EF Core y poblado de los 5 conductores fijos (SCRUM-2)"
```
