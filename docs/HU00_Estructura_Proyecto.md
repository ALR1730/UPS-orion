# Documentación HU00: Estructura del Proyecto y Configuración Base

## 📌 Descripción de la Historia 0
Como **Desarrollador / Arquitecto**, se establece la infraestructura base en C# (.NET 10 ASP.NET Core MVC) para el sistema MVP **UPS ORION**, incluyendo la configuración del motor de base de datos relacional ligero (Entity Framework Core con SQLite), el sistema de diseño visual responsive (UPS ORION Glassmorphism UI) y la navegación unificada por roles.

---

## 🛠️ Modificaciones y Cambios Realizados

1. **Inicialización de Proyecto C#**:
   - Creación de la solución `OrionMVP` utilizando `.NET 10.0 SDK`.
   - Incorporación de paquetes NuGet:
     - `Microsoft.EntityFrameworkCore.Sqlite`
     - `Microsoft.EntityFrameworkCore.Design`
     - `CsvHelper`

2. **Modelos del Dominio de Datos**:
   - `Driver.cs`: Entidad para conductores y estado de su jornada (`No iniciado`, `En Ruta`, `Finalizado`).
   - `Route.cs`: Entidad de rutas con métricas de odómetro (`InitialKm`, `FinalKm`, `TotalDistanceKm`, `BaselineDistanceKm`).
   - `RouteStop.cs`: Entidad de paradas con secuencia 1..N, geocodificación (`Latitude`, `Longitude`, `IsGeocoded`, `HasGeocodingError`) y estado (`Pendiente`, `Entregado`, `No entregado`).
   - `OdometerLog.cs`: Registro auditado de odómetros.
   - `HistoricalRoute.cs`: Base de comparación histórica para ahorro de combustible.

3. **Configuración de Persistencia (EF Core)**:
   - `OrionDbContext.cs`: Definición de `DbSet` para las 5 entidades principales y sembrado (*seed data*) inicial de conductores y rutas históricas para pruebas.
   - Conexión configurada en `Program.cs` con base de datos SQLite `orion.db` generada automáticamente al iniciar la app (`EnsureCreated()`).

4. **Interfaz Visual y Navegación**:
   - Sistema de diseño visual con paleta oficial de UPS (Oro `#FFB500`, Oscuro Obsidiana, Acentos Neón).
   - Layout responsive `_Layout.cshtml` con navegación a los 3 módulos:
     - **Despacho** (`/Dispatch`) -> HU01 - HU03.
     - **Conductor** (`/Driver`) -> HU04 - HU06, HU10.
     - **Supervisor** (`/Supervisor`) -> HU07, HU08, HU09.

---

## ✅ Criterios de Aceptación Cumplidos

| Criterio | Estado | Comentario |
|---|---|---|
| Proyecto C# compilable sin errores | ✅ Cumplido | Verificado con `dotnet build`. |
| Configuración de Base de Datos SQLite | ✅ Cumplido | Configurado con EF Core 10 y `EnsureCreated()`. |
| Definición de Modelos del Dominio | ✅ Cumplido | `Driver`, `Route`, `RouteStop`, `OdometerLog`, `HistoricalRoute`. |
| Interfaz base visual responsive | ✅ Cumplido | CSS custom, Glassmorphic UI y FontAwesome icons. |
| Estructura lista para ramificación en Git | ✅ Cumplido | Documentación lista en `docs/HU00_Estructura_Proyecto.md`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU00: Estructura del proyecto C# ASP.NET Core, EF Core SQLite y UI base"
```
