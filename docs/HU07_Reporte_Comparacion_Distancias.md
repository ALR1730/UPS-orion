# Documentación HU07: Reporte de Comparación de Distancias

## 📌 Definición de la Historia 7
Como **Supervisor del Piloto**, quiero ver un reporte que compare los kilómetros reales recorridos versus el promedio histórico manual anterior para validar si el algoritmo ahorra distancias.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Cálculo Métrico de Comparación**:
   - `SupervisorController.cs`:
     - Consulta la entidad `Route` y contrasta el promedio histórico manual (`BaselineDistanceKm`) contra los kilómetros reales registrados por el odómetro del conductor (`FinalKm - InitialKm`) o la distancia calculada por el secuenciador.
     - Calcula los kilómetros netos ahorrados (`TotalSavedKm`) y el porcentaje de eficiencia de combustible (`TotalFuelSavingsPercentage`).
     - Calcula el volumen estimado en litros de combustible no emitidos/ahorrados.

2. **Visualización en Gráfico de Barras**:
   - Vista [Index.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Supervisor/Index.cshtml):
     - Tarjetas KPI con los métricos globales de ahorro y porcentaje.
     - Gráfico de barras interactivo generado dinámicamente en HTML5 Canvas con `Chart.js`, comparando barra a barra el **Histórico Manual** contra el **Recorrido ORION**.
     - Tabla detallada por ruta con badges de porcentaje de reducción de combustible.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Calcula la diferencia entre kilómetros registrados por el conductor y la base de datos histórica | ✅ Cumplido | Implementado en `SupervisorController.cs` (`BaselineDistanceKm - RealKm`). |
| Genera un gráfico de barras simple que muestra el porcentaje de ahorro de combustible estimado | ✅ Cumplido | Integrado con `Chart.js` en `Views/Supervisor/Index.cshtml` mostrando porcentaje de ahorro y comparación directa. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU07: Reporte comparativo de kilómetros reales vs histórico manual y gráfico de barras de porcentaje de ahorro"
```
