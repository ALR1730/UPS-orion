# Documentación HU10: Botón de Cancelación de Parada

## 📌 Definición de la Historia 10
Como **Conductor del Piloto**, quiero poder marcar una parada como "No entregada" y pasar a la siguiente de la lista para continuar la ruta establecida si un cliente no se encuentra en casa.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Controlador y Cambio de Estado de Entrega**:
   - `DriverController.cs`:
     - Endpoint `CancelStop(int stopId, string reason)`:
       - Actualiza la parada a `Status = "No entregado"`.
       - Almacena el motivo de cancelación seleccionado (`CancellationReason`).
       - Desbloquea visualmente la navegación para la siguiente parada `#N+1`.
     - Endpoint `MarkDelivered(int stopId)`:
       - Actualiza la parada a `Status = "Entregado"`.

2. **Interfaz Móvil e Interacción (UI Mobile)**:
   - Vista [Index.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Driver/Index.cshtml):
     - Botón `Cancelar Parada` en cada tarjeta de entrega.
     - Menú desplegable interactivo con opciones rápidas de motivo:
       - `Cliente ausente`
       - `Dirección incorrecta`
       - `Zona inaccesible`
       - `Rechazado por el cliente`
       - `Vehículo averiado`
     - Al guardar, la parada actual adopta la insignia roja de cancelación con el motivo descriptivo y se habilita la continuidad hacia las entregas restantes.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Al presionar "Cancelar Parada", despliega menú desplegable con opciones rápidas de motivo | ✅ Cumplido | Implementado en `Views/Driver/Index.cshtml` con un modal desplegable. |
| Al guardar el motivo, el sistema marca la parada actual y desbloquea visualmente la navegación de la siguiente entrega | ✅ Cumplido | Actualiza `Status = "No entregado"`, guarda el motivo y continúa el orden secuencial. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU10: Botón de cancelación de parada con menú desplegable de motivos y desbloqueo de entregas"
```
