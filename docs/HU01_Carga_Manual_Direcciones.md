# Documentación HU01: Carga Manual de Direcciones (Excel/CSV)

## 📌 Definición de la Historia 1
Como **Despachador del Piloto**, quiero subir un archivo CSV o XLSX con las direcciones de entrega del día para cargarlas en el sistema rápidamente sin integraciones complejas.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Servicio de Procesamiento e Importación**:
   - `AddressImportService.cs`: Procesa archivos `.csv` mediante `CsvHelper` y archivos `.xlsx` mediante `ExcelDataReader`.
   - Normalización de encabezados con alias (soporta `Calle` / `Dirección`, `Altura` / `Número`, `Ciudad` / `Localidad`, `Cliente`).
   - Validación estricta de las columnas obligatorias: `Calle`, `Altura`, `Ciudad`.

2. **Controlador y Flujo de Datos**:
   - `DispatchController.cs`: 
     - `Upload(IFormFile file, int? driverId)`: Valida la carga, procesa el archivo, crea la entidad `Route` en `OrionDbContext` con sus correspondientes `RouteStop` y la asigna al conductor.
     - `DownloadSampleCsv()`: Proporciona la descarga directa de un archivo CSV de plantilla precargado para pruebas del piloto.

3. **Interfaz de Usuario (UI/UX)**:
   - Formulario Drag & Drop visual con validación de extensiones `.csv` y `.xlsx`.
   - Notificación emergente (badge verde) con el total de direcciones leídas con éxito.
   - Mensaje de alerta (badge rojo) en caso de que falte alguna columna obligatoria indicando exactamente cuáles columnas faltan.
   - Tabla interactiva con el historial de rutas cargadas, cantidad de direcciones y estado.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Permite seleccionar y subir archivo local `.csv` o `.xlsx` | ✅ Cumplido | Implementado en `Dispatch/Index.cshtml` y procesado en `AddressImportService.cs`. |
| Valida que contenga columnas obligatorias (`Calle`, `Altura`, `Ciudad`) | ✅ Cumplido | Método `ValidateHeaders` verifica presencia y reporta columnas faltantes. |
| Muestra mensaje con el total de direcciones leídas con éxito | ✅ Cumplido | `TempData["SuccessMessage"]` notifica la cantidad leída en pantalla. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU01: Carga manual de direcciones mediante archivos CSV y Excel (.xlsx) con validación"
```
