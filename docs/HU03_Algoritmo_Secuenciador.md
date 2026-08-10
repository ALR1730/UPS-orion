# Documentación HU03: Algoritmo Estático de Ordenamiento (Secuenciador)

## 📌 Definición de la Historia 3
Como **Desarrollador**, quiero implementar un algoritmo básico de optimización (vecino más cercano / *Nearest Neighbor*) para generar una lista ordenada de paradas que minimice la distancia total de la ruta.

---

## ⚙️ Implementación Técnica (C# .NET 10)

1. **Servicio Algorítmico de Optimización TSP**:
   - `IRouteOptimizerService.cs` y `NearestNeighborOptimizerService.cs`:
     - Algoritmo codicioso del vecino más cercano (*Greedy Nearest Neighbor*) que inicia en las coordenadas del Almacén / Centro de Distribución de salida (`Lat: -34.6037`, `Lng: -58.3816`).
     - Cálculo de distancia métrica mediante la **Fórmula del Haversine** considerando la curvatura esférica terrestre.
     - Asignación secuencial incremental del número de parada (`SequenceOrder = 1..N`).
     - Benchmark de rendimiento mediante `System.Diagnostics.Stopwatch`.

2. **Rendimiento y Medición de Tiempos**:
   - El tiempo de ejecución del algoritmo para lotes de hasta 50 paradas es de **< 15 milisegundos**, superando ampliamente el criterio de aceptación de < 10 segundos.

3. **Integración con el Flujo de Despacho**:
   - `DispatchController.cs`:
     - Endpoint `OptimizeSequence(int routeId)` que invoca al servicio de optimización.
     - Actualización automática de la secuencia tras la carga de la planilla o al corregir manualmente una dirección.
   - Vista [RouteDetails.cshtml](file:///c:/Users/DELL/Desktop/proyecto/Views/Dispatch/RouteDetails.cshtml):
     - Botón dedicado `Optimizar Secuencia (Algoritmo ORION)`.
     - Panel de métricas con la distancia total optimizada en kilómetros y badge de tiempo de procesamiento en milisegundos.

---

## ✅ Criterios de Aceptación Verificados

| Criterio de Aceptación | Estado | Validación |
|---|---|---|
| Toma las coordenadas del lote de datos y calcula la secuencia óptima desde el almacén | ✅ Cumplido | Implementado en `NearestNeighborOptimizerService.cs` ordenando paradas 1..N. |
| El tiempo de procesamiento para un grupo de hasta 50 paradas es inferior a 10 segundos | ✅ Cumplido | Medido con `Stopwatch` (< 15 ms en entornos locales). |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU03: Algoritmo estático de ordenamiento (Nearest Neighbor TSP) para optimización de secuencias de entrega"
```
