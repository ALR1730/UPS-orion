# Documentación HU07: Panel de Control Administrativo y Reporte Consolidado

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-8 (HU07)
* **Épica:** SCRUM-44 - Panel de Control Administrativo y Analítica (Prioridad: Low)
* **Story Points:** 3
* **Responsable:** Emil Hari Montilla Salvador (Frontend - 20220287@itlaedudo.onmicrosoft.com) / Jhois Collado (Backend - 20211124@itla.edu.do)

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Supervisor del Piloto,  
> **quiero** visualizar un tablero centralizado con el estado de las rutas y los kilómetros recorridos por cada chofer,  
> **para** analizar los datos reales de la operación y exportar el reporte de rendimiento del día.

---

## ⚙️ Implementación Técnica

1. **Dashboard Administrativo Centralizado**:
   * Ruta `/Supervisor`: Panel exclusivo con vista resumen para directores y supervisores operativos.
   * `SupervisorController.Index()`: Consulta el consolidado de conductores del piloto, rutas del día, métricas de paradas y odómetros.

2. **Grilla Dinámica de Auditoría Operativa**:
   * Vista `Supervisor/Index.cshtml`:
     * Renderiza una tabla dinámica con las columnas requeridas:
       * **Conductor:** Nombre completo del chofer asignado.
       * **Artículos Entregados / Totales:** Progreso porcentual y cuantitativo (`X de Y entregados`).
       * **Odómetro Inicial (km):** Kilometraje registrado al inicio del turno.
       * **Odómetro Final (km):** Kilometraje registrado al cierre.
       * **Distancia Total Recorrida (km):** Cálculo matemático automático (`Odómetro Final - Odómetro Inicial`).
       * **Ahorro vs. Línea Base (%):** Comparativa del algoritmo ORION contra la ruta histórica sin optimizar.

3. **Motor de Exportación a Archivo CSV**:
   * `SupervisorController.ExportCsv()`:
     * Genera un archivo plano estructurado `reporte_rendimiento_orion_{fecha}.csv` con formato `text/csv`.
     * Incluye todas las métricas procesadas para auditoría de alta gerencia e insumo financiero.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Crear una interfaz web exclusiva para el supervisor accesible mediante una ruta administrativa plana. | ✅ Cumplido | Implementado en el controlador `SupervisorController` y vista `Supervisor/Index.cshtml`. |
| 2 | Renderizar una grilla/tabla dinámica con los datos del día: Conductor, Artículos Entregados/Totales, Odómetro Inicial y Final. | ✅ Cumplido | Tabla estructurada en `Supervisor/Index.cshtml` iterando el modelo de vista consolidado. |
| 3 | Computar en tiempo real la columna de "Distancia Total Recorrida" mediante la resta matemática de los odómetros de la HU06. | ✅ Cumplido | Cálculo en backend `route.TotalDistanceKm = route.FinalKm - route.InitialKm` y mostrado en la grilla. |
| 4 | Incorporar un botón funcional de exportación que genere la descarga inmediata de la matriz visible en formato `.csv`. | ✅ Cumplido | Endpoint `SupervisorController.ExportCsv` retornando `File(bytes, "text/csv", filename)`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU07: Panel administrativo consolidado del supervisor y exportador de métricas en CSV (SCRUM-8)"
```
