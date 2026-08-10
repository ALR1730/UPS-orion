# Documentación HU05: Vínculo Directo a Navegador Externo

## 📌 Definición de la Historia 5
Como **Conductor del Piloto**, quiero poder hacer clic en la dirección actual y que abra la ubicación en Google Maps o Waze externo para navegar hacia la entrega sin programar un mapa desde cero.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Generador de URLs de Navegación Nativa**:
   - `RouteStop.cs`:
     - Método `GetGoogleMapsUrl()`: Construye el esquema `https://www.google.com/maps/search/?api=1&query={Latitude},{Longitude}` (o mediante la dirección texto `FullAddress` codificada como fallback).
     - Método `GetWazeUrl()`: Construye el Intent `https://waze.com/ul?ll={Latitude},{Longitude}&navigate=yes`.

2. **Integración con la Interfaz del Conductor (UI Mobile)**:
   - Vista [Index.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Driver/Index.cshtml):
     - Botones dedicados en cada tarjeta de parada: `Google Maps` y `Waze`.
     - Apertura automática en la aplicación nativa de mapas del sistema operativo (Android/iOS) con la coordenada geoespacial o dirección precargada en el navegador vehicular.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Al presionar el botón "Navegar", abre la aplicación externa nativa de mapas (Google Maps o Waze) | ✅ Cumplido | Enlaces directos `target="_blank"` con protocolos `api=1` y `navigate=yes`. |
| La coordenada o dirección exacta se precarga automáticamente en el buscador externo | ✅ Cumplido | Parámetros `query` y `ll` con coordenadas `Lat` y `Lng` formateadas de forma precisa. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU05: Vínculo directo y precarga de coordenadas a aplicaciones externas de navegación (Google Maps y Waze)"
```
