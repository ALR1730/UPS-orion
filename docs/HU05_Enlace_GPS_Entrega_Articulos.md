# Documentación HU05: Enlace GPS para Entrega de Artículos

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-6 (HU05)
* **Épica:** SCRUM-43 - Interfaz Móvil y Operación del Conductor (Prioridad: Medium)
* **Story Points:** 1
* **Responsable:** Angel Gabriel Morillo Rosario (20230554@itla.edu.do)

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Conductor del Piloto,  
> **quiero** presionar un botón de navegación en el artículo seleccionado,  
> **para** abrir su ubicación en Google Maps externo y recibir indicaciones GPS en tiempo real hacia el destino.

---

## ⚙️ Implementación Técnica

1. **Formato Universal de Enlace Profundo (Deep Link)**:
   * Implementación de la URL estándar de Google Maps:
     ```html
     <a href="https://www.google.com/maps/search/?api=1&query=@stop.Latitude.ToString(CultureInfo.InvariantCulture),@stop.Longitude.ToString(CultureInfo.InvariantCulture)" 
        target="_blank" 
        rel="noopener noreferrer" 
        class="btn btn-gps">
         <i class="fas fa-location-arrow"></i> Ir a la entrega
     </a>
     ```
   * En caso de no contar con coordenadas numéricas válidas, se utiliza un fallback automático codificando la dirección de texto:
     `https://www.google.com/maps/search/?api=1&query=@Uri.EscapeDataString(stop.Address)`

2. **Integración en la Vista Móvil**:
   * Ubicado directamente dentro de cada tarjeta de artículo en `Driver/Index.cshtml`.
   * Estilizado con un botón de acción rápida destacado en color primario con icono representativo (`fa-location-arrow`).
   * Abre en pestaña/aplicación nativa externa sin cerrar la aplicación web de ORION.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Insertar un botón de acción rápida con el texto o icono "Ir a la entrega" en cada tarjeta de artículo de la HU04. | ✅ Cumplido | Botón visual renderizado en cada parada en `Driver/Index.cshtml`. |
| 2 | Formatear dinámicamente el hipervínculo utilizando el estándar web oficial `https://www.google.com/maps/search/?api=1&query={lat},{lng}`. | ✅ Cumplido | Construcción dinámica de la URL con cultura invariante para separar decimales con puntos. |
| 3 | Al hacer clic, activar la llamada nativa para desplegar el mapa externo en una ventana/aplicación independiente. | ✅ Cumplido | Atributo `target="_blank"` y `rel="noopener noreferrer"` configurados correctamente. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU05: Enlace profundo universal a Google Maps web para navegación GPS por parada (SCRUM-6)"
```
