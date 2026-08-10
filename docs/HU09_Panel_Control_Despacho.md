# Documentación HU09: Panel de Control Básico de Despacho (Supervisor)

## 📌 Definición de la Historia 9
Como **Supervisor del Piloto**, quiero ver qué rutas han sido asignadas y qué conductores ya iniciaron su jornada para supervisar el flujo del experimento desde la oficina.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **DTO y Controlador de Monitoreo de Flota**:
   - `SupervisorController.cs`:
     - Consulta la lista de conductores y cruza sus rutas asignadas con sus correspondientes paradas en la base de datos relacional.
     - Calcula el número de paradas completadas (`Entregado` o `No entregado`) y el porcentaje de avance de la ruta (`ProgressPercentage`).
     - Construye la colección de DTOs `DriverProgressDto`.

2. **Panel de Control del Supervisor (Dashboard Office)**:
   - Vista [Index.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Supervisor/Index.cshtml):
     - Tabla interactiva de monitoreo en tiempo real.
     - Muestra explícitamente:
       - **Nombre del Conductor** y **ID de Vehículo**.
       - **Ruta Asignada**.
       - **Estado de Jornada** con Badges (`No iniciado`, `En Ruta`, `Finalizado`).
       - **Progreso de Paradas** (Ej: `12 / 35 paradas`).
       - **Barra de Progreso Visual** multicolor animada.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Muestra una tabla con la lista de conductores en ruta | ✅ Cumplido | Implementado en `Views/Supervisor/Index.cshtml` mediante la tabla `DriverMonitoring`. |
| Cada registro detalla nombre del conductor, estado actual (No iniciado, En Ruta, Finalizado) y progreso de paradas | ✅ Cumplido | Columnas claras con badges de estado y barras de progreso porcentuales. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU09: Panel de control de despacho para supervisores con lista de conductores, estados y barras de progreso"
```
