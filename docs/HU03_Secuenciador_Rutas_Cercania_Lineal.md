# Documentación HU03: Secuenciador Automático de Rutas por Cercanía Lineal

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-4 (HU03)
* **Épica:** SCRUM-42 - Motor de Optimización y Secuenciación (Prioridad: High)
* **Story Points:** 2
* **Responsable:** Cesar Reyes (20241308@itla.edu.do)

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Despachador del Piloto,  
> **quiero** que el sistema ordene automáticamente los artículos cargados basándose en sus coordenadas geográficas,  
> **para** agruparlos en una secuencia de paradas optimizada que reduzca la distancia total del recorrido.

---

## ⚙️ Implementación Técnica

1. **Algoritmo de Optimización por Vecino Más Cercano (Nearest Neighbor)**:
   * `NearestNeighborOptimizerService.cs` / `IRouteOptimizerService.cs`:
     * Calcula la distancia euclidiana / haversine entre coordenadas `(latitud, longitud)`.
     * Define un punto base / depósito central fijo (`DepotLatitude: 18.4861, DepotLongitude: -69.9312` - Santo Domingo Centro).
     * Itera recursivamente seleccionando en cada paso el artículo o parada no visitada con la menor distancia lineal desde la ubicación actual.
     * Asigna enteros correlativos `1, 2, 3... N` al campo `Sequence` de cada parada.

2. **Manejo de Errores y Casos Borde**:
   * Control de coordenadas nulas o corruptas: si una parada no cuenta con latitud/longitud válidas, el optimizador maneja la excepción de forma controlada y conserva el orden por defecto evitando caídas del sistema.

3. **Integración con el Flujo de Despacho**:
   * `DispatchController.Optimize(int routeId)`:
     * Dispara la ejecución del optimizador para la ruta seleccionada.
     * Actualiza la base de datos de manera masiva persistiendo los nuevos valores de `Sequence`.
     * Retorna a la vista con la lista reordenada y la distancia estimada total.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Capturar los artículos almacenados para la jornada (HU02) tomando un punto de inicio (depósito) fijo. | ✅ Cumplido | Implementado en `NearestNeighborOptimizerService.OptimizeStops` tomando coordenadas base del depósito. |
| 2 | Calcular la secuencia aplicando la lógica matemática de vecino más cercano por coordenadas. | ✅ Cumplido | Algoritmo determinístico de aproximación greedy euclidiana en `NearestNeighborOptimizerService.cs`. |
| 3 | Actualizar la base de datos inyectando un entero correlativo (1..N) en el campo `Secuencia` de cada artículo. | ✅ Cumplido | Persistencia masiva en `DispatchController.Optimize` asignando `stop.Sequence = sequence++`. |
| 4 | Validar el flujo arrojando una lista vacía o controlada si existen parámetros de latitud/longitud corruptos o nulos. | ✅ Cumplido | Validación de rangos geoespaciales y control de excepciones. |

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU03: Algoritmo de secuenciación lineal por vecino más cercano y asignación correlativa de paradas (SCRUM-4)"
```
