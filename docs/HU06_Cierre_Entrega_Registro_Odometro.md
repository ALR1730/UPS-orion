# Documentación HU06: Cierre de Entrega de Artículos y Registro de Odómetro

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-7 (HU06)
* **Épica:** SCRUM-43 - Interfaz Móvil y Operación del Conductor (Prioridad: Medium)
* **Story Points:** 2
* **Responsable:** Jostin Wilmer Perez (20221096@itlaedudo.onmicrosoft.com)

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Conductor del Piloto,  
> **quiero** registrar el kilometraje de mi vehículo al comenzar y terminar el día, y marcar cada artículo completado,  
> **para** guardar el reporte de distancia real de mi jornada de entregas.

---

## ⚙️ Implementación Técnica

1. **Flujo de Bloqueo por Odómetro Inicial**:
   * Al seleccionar la ruta, si `Route.InitialKm` es nulo o 0, la vista móvil bloquea la interacción con las paradas mostrando un modal o formulario requerido para ingresar el odómetro de inicio.
   * `DriverController.StartRoute(int routeId, double initialKm)`: Persiste el kilometraje de inicio y actualiza el estado del conductor a `En Ruta`.

2. **Control de Estado de Entregas (Marcar Entregado)**:
   * Cada parada cuenta con un botón interactivo "Marcar Entregado".
   * `DriverController.UpdateStopStatus(int stopId, string status)`: Actualiza el estado a `Entregado` o `No entregado` y registra la marca temporal.

3. **Bloqueo y Validación de Odómetro Final**:
   * El formulario de "Kilometraje Final" permanece deshabilitado hasta que el 100% de los artículos de la lista hayan sido marcados como `Entregado` (o resueltos).
   * **Validación Client-Side y Server-Side:** Se verifica estrictamente que `FinalKm >= InitialKm`. Si el conductor ingresa un valor menor, se emite una alerta de validación inmediata impidiendo el cierre.
   * `DriverController.CompleteRoute(int routeId, double finalKm)`: Computa la distancia total (`TotalDistanceKm = FinalKm - InitialKm`), actualiza el estado del conductor a `Finalizado` y audita el evento en `OdometerLog`.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Presentar una caja de captura numérica para el "Kilometraje Inicial" al cargar la ruta (bloquea la visualización hasta ser guardada). | ✅ Cumplido | Modal / Input bloqueante implementado en `Driver/Index.cshtml` y procesado en `DriverController.StartRoute`. |
| 2 | Integrar un botón de cambio de estado ("Marcar Entregado") al lado de cada artículo en la lista. | ✅ Cumplido | Botón interactivo con llamada AJAX a `Driver/UpdateStopStatus`. |
| 3 | Habilitar el botón y campo "Kilometraje Final" únicamente cuando el 100% de los artículos pasen a estado "Entregado". | ✅ Cumplido | Validación condicional en JavaScript y C# verificando que no existan paradas pendientes. |
| 4 | Validar del lado del cliente y servidor que el valor numérico final sea estrictamente mayor o igual al valor inicial. | ✅ Cumplido | Regla de validación `finalKm >= route.InitialKm` en `DriverController.CompleteRoute` y control HTML5 `min`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU06: Captura de odómetro inicial/final, validación de kilometraje y flujo de cierre de entregas (SCRUM-7)"
```
