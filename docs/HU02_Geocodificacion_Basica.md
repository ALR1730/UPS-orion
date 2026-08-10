# Documentación HU02: Geocodificación Básica de Direcciones

## 📌 Definición de la Historia 2
Como **Motor del Sistema**, quiero convertir las direcciones de texto en coordenadas (latitud/longitud) usando una API externa para que el algoritmo pueda calcular las distancias métricas.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Servicios de Geocodificación Geoespacial**:
   - `IGeocodingService.cs` y `NominatimGeocodingService.cs`:
     - Conexión HTTP asíncrona mediante `HttpClient` con la API externa de OpenStreetMap Nominatim (`https://nominatim.openstreetmap.org/search`).
     - Decodificación de respuesta JSON para extraer `Latitud` y `Longitud`.
     - Mecanismo de respaldo (*fallback*) determinista para asegurar la disponibilidad offline y continua en el piloto.

2. **Detección de Errores y Marcado en Rojo**:
   - Si una dirección es errónea, incompleta o no se encuentra en la API externa, la entidad `RouteStop` actualiza sus banderas: `IsGeocoded = false` y `HasGeocodingError = true`.
   - En la interfaz ([RouteDetails.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Dispatch/RouteDetails.cshtml)), la tarjeta de la parada se resalta visualmente en **rojo neón** (`border: 2px solid var(--accent-red); background: rgba(239, 68, 68, 0.12);`) junto con una etiqueta de alerta `Dirección Errónea / No Encontrada`.

3. **Edición Manual e Interactiva en Pantalla**:
   - Cada parada resaltada o existente incluye el botón `Editar Texto Manualmente`.
   - Al presionar el botón se despliega un formulario inline que permite modificar la `Calle`, `Altura` y `Ciudad`.
   - Al guardar, el controlador `UpdateStopAddress` ejecuta una re-geocodificación inmediata con la API externa y actualiza las coordenadas en la base de datos SQLite.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Conexión con API externa para traducir direcciones a coordenadas (`Lat/Lng`) | ✅ Cumplido | Implementado en `NominatimGeocodingService.cs` con respuesta JSON. |
| Marcado en rojo para direcciones erróneas | ✅ Cumplido | `HasGeocodingError = true` aplica borde y fondo rojo en `RouteDetails.cshtml`. |
| Edición manual del texto en pantalla para corregir y re-geocodificar | ✅ Cumplido | Formulario interactivo en `RouteDetails.cshtml` invocando `UpdateStopAddress`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU02: Geocodificación básica con API externa, resaltado en rojo para errores y edición manual en pantalla"
```
