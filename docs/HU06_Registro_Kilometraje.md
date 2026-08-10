# Documentación HU06: Registro Manual de Kilometraje Inicial y Final

## 📌 Definición de la Historia 6
Como **Conductor del Piloto**, quiero digitar el odómetro de mi vehículo al salir y al regresar al centro de distribución para registrar manualmente la distancia total real recorrida.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Controlador y Lógica de Negocio**:
   - `DriverController.cs`:
     - Endpoint `SaveOdometer(int routeId, int initialKm, int finalKm)`:
       - Validación de valores positivos (`initialKm > 0` y `finalKm > 0`).
       - Validación lógica (`finalKm >= initialKm`).
       - Registro de auditoría en la tabla `OdometerLogs` y actualización de las propiedades `InitialKm` y `FinalKm` en la entidad `Route`.
       - Cambio de estado de la ruta y conductor a `Finalizado`.

2. **Formulario Visual e Interfaz Móvil**:
   - Vista [Index.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Driver/Index.cshtml):
     - Formulario interactivo con dos campos obligatorios: `KM Inicial (Al salir)` y `KM Final (Al regresar)`.
     - Atributos HTML5 estritos: `type="number" min="1" step="1" required`.
     - Banner dinámico que calcula la diferencia en tiempo real y muestra la **Distancia Real Recorrida (KM)**.
     - Alertas informativas de éxito y mensajes de error descriptivos si se intenta ingresar valores inválidos o un KM Final menor al Inicial.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Formulario con dos campos de entrada obligatorios: "KM Inicial" y "KM Final" | ✅ Cumplido | Implementado en `Views/Driver/Index.cshtml` con `name="initialKm"` y `name="finalKm"`. |
| Validación de que solo se ingresen enteros positivos | ✅ Cumplido | Validación HTML5 (`min="1"`, `step="1"`) y validación backend C# en `DriverController.cs`. |
| Validación lógica de que KM Final sea >= KM Inicial | ✅ Cumplido | Comprobación en C# que muestra mensaje descriptivo si `finalKm < initialKm`. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU06: Formulario de registro de odómetro inicial y final con validación de enteros positivos"
```
