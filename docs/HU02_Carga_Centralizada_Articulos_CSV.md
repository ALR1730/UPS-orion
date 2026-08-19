# Documentación HU02: Carga Centralizada de Artículos (El CRUD Express por CSV)

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-3 (HU02)
* **Épica:** SCRUM-41 - Infraestructura de Datos e Ingesta de Artículos (Prioridad: Highest)
* **Story Points:** 2
* **Responsable:** Cesar Reyes (20241308@itla.edu.do)

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Despachador del Piloto,  
> **quiero** subir un archivo CSV con los artículos del día y sus ubicaciones,  
> **para** registrarlos masivamente en el sistema de manera rápida y sin formularios complejos de captura.

---

## ⚙️ Implementación Técnica

1. **Servicio de Ingesta y Procesamiento CSV**:
   * `AddressImportService.cs`: Utiliza la librería `CsvHelper` con cultura invariante y configuración flexible para procesar archivos de artículos y coordenadas.
   * **Sanitización Decimal:** Normalización automática de valores numéricos de latitud y longitud (reemplazo de comas por puntos) para evitar corrupción de datos geográficos.
   * Validación estricta de columnas obligatorias: `Articulo` (o alias `Nombre`/`Descripcion`), `Cliente`, `Direccion`, `Latitud`, `Longitud`.

2. **Controlador y Flujo de Despacho**:
   * `DispatchController.cs`:
     * `Upload(IFormFile file, int? driverId)`: Recibe el archivo `.csv`, valida la extensión, invoca `AddressImportService.cs`, crea la entidad `Route` y persiste masivamente los `RouteStop` asociados en la base de datos.
     * `DownloadSampleCsv()`: Proporciona la plantilla oficial descargable con los encabezados estandarizados para el despachador.

3. **Interfaz de Usuario (UI/UX)**:
   * Vista `Dispatch/Index.cshtml`:
     * Zona Drag & Drop visual para seleccionar y subir archivos `.csv`.
     * Alertas dinámicas de retroalimentación: badge de éxito indicando la cantidad exacta de artículos cargados o alerta de error si faltan columnas requeridas o el formato es inválido.
     * Selección opcional de conductor para asignación inmediata.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Mostrar un botón web en el panel administrativo para seleccionar y subir archivos `.csv`. | ✅ Cumplido | Componente interactivo de carga implementado en `Dispatch/Index.cshtml`. |
| 2 | Validar que el archivo contenga las columnas obligatorias: `Articulo`, `Cliente`, `Direccion`, `Latitud`, `Longitud`. | ✅ Cumplido | Validador de encabezados en `AddressImportService.ValidateHeaders`. |
| 3 | Insertar los registros directamente en la tabla `Articulos` (`RouteStops`) enlazándolos a la jornada del día. | ✅ Cumplido | Inserción en lote mediante EF Core en `DispatchController.Upload`. |
| 4 | Emitir una alerta en pantalla detallando la cantidad total de artículos cargados con éxito. | ✅ Cumplido | Mensaje informativo enriquecido mediante `TempData["SuccessMessage"]`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU02: Carga masiva de artículos vía CSV con sanitización de coordenadas y validación de columnas (SCRUM-3)"
```
