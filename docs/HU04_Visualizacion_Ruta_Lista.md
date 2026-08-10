# Documentación HU04: Visualización de Ruta en Formato Lista (App Conductor)

## 📌 Definición de la Historia 4
Como **Conductor del Piloto**, quiero ver en mi teléfono celular la lista ordenada de mis paradas del 1 al N para saber exactamente el orden de entrega sugerido para el día.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Controlador para Vista Móvil**:
   - `DriverController.cs`:
     - Consulta la ruta asignada al conductor seleccionado (`driverId`).
     - Ordena las paradas secuencialmente por `SequenceOrder` de 1 a N.

2. **Diseño Visual Móvil (UI Mobile-First)**:
   - Vista [Index.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Driver/Index.cshtml):
     - Maquetación adaptada a pantallas de dispositivos móviles (`max-width: 540px`).
     - Lista vertical continua con tarjetas independientes por parada.
     - Indicador numérico destacado `#1`, `#2`, ... `#N` con contraste de alta legibilidad en color oro de UPS.
     - Despliegue de **Número de parada**, **Dirección Completa** (`Street`, `Number`, `City`) y **Nombre del Cliente** (`CustomerName`).
     - Insignia de estado de la entrega (`Pendiente`, `Entregado`, `No entregado`).

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Interfaz móvil despliega paradas numeradas secuencialmente de 1 a N de forma vertical y legible | ✅ Cumplido | Implementado en `Views/Driver/Index.cshtml` con layout vertical optimizado para teléfonos. |
| Muestra de forma clara: número de parada, dirección completa y nombre del cliente | ✅ Cumplido | Cada tarjeta despliega el badge `#N`, `FullAddress` y `CustomerName`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU04: Visualización móvil de ruta en formato lista vertical de paradas numeradas de 1 a N"
```
