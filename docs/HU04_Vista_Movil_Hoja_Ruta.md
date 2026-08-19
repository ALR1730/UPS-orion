# Documentación HU04: Vista Móvil de Hoja de Ruta y Artículos Asignados

## 📌 Identificación del Ticket de Jira
* **Ticket ID:** SCRUM-5 (HU04)
* **Épica:** SCRUM-43 - Interfaz Móvil y Operación del Conductor (Prioridad: Medium)
* **Story Points:** 2
* **Responsable:** Luis Manuel Ortega Mejia (20221134@itlaedudo.onmicrosoft.com)
* **Subtareas Jira:** SCRUM-54, SCRUM-55, SCRUM-56

---

## 🎯 Descripción de la Historia de Usuario

> **Como** Conductor del Piloto,  
> **quiero** seleccionar mi perfil y ver en mi teléfono la lista de artículos que debo entregar,  
> **para** conocer mi secuencia exacta de paradas del día sin procesos complejos de inicio de sesión.

---

## ⚙️ Implementación Técnica

1. **Selector Rápido de Conductor (Sin fricción de autenticación)**:
   * Menú desplegable `<select id="driverSelect">` en `Driver/Index.cshtml` que lista los 5 conductores fijos del piloto.
   * Al seleccionar un conductor, se envía una petición GET `Driver/RouteDetails?driverId={id}` para cargar reactivamente la ruta activa del día.

2. **Renderizado Responsive de Tarjetas de Entrega**:
   * Lista vertical de paradas ordenadas ascendentemente por el campo `Sequence` (1..N).
   * Cada tarjeta informativa muestra:
     * **Número de Parada:** Badge circular visible (`#1`, `#2`, etc.).
     * **Artículo / Paquete:** Descripción clara del envío.
     * **Cliente:** Nombre del destinatario.
     * **Dirección de Texto:** Dirección física de entrega.
     * **Estado Visual:** Badge de color según el estado (`Pendiente`, `Entregado`, `No entregado`).

3. **Diseño Mobile-First (Glassmorphic UPS Theme)**:
   * Estilos CSS adaptables con flexbox, media queries (`@media (max-width: 768px)`), botones táctiles accesibles (mínimo 48px de alto) y contraste visual alto apto para uso bajo luz solar.

---

## ✅ Criterios de Aceptación

| # | Criterio de Aceptación | Estado | Validación Técnica |
|---|---|---|---|
| 1 | Renderizar un menú desplegable inicial (`<select>`) con los nombres de los 5 conductores fijos. | ✅ Cumplido | Implementado en `Driver/Index.cshtml` consumiendo los conductores de la base de datos. |
| 2 | Al seleccionar el conductor, renderizar la lista de artículos ordenados de forma ascendente según `Secuencia`. | ✅ Cumplido | Consulta LINQ `.OrderBy(s => s.Sequence)` en `DriverController.cs`. |
| 3 | Cada tarjeta informativa debe indicar: Orden de parada, Artículo, Cliente y Dirección de Texto. | ✅ Cumplido | Tarjeta visual en `Driver/Index.cshtml` con todos los campos requeridos. |
| 4 | Garantizar que los elementos visuales se auto-ajusten a resoluciones móviles comunes. | ✅ Cumplido | Grid y contenedores fluidos testeados en Viewport móvil (375px a 768px). |

---

## 🖼️ Evidencias en Jira Software

### Detalles del Ticket HU04 (SCRUM-5)
![Detalles del Ticket HU04 en Jira (SCRUM-5) - Asignación, Épica SCRUM-43 y Estimación](Screenshot%202026-08-19%20124110.png)

### Criterios y Desglose de Subtareas (SCRUM-54, SCRUM-55, SCRUM-56)
![Criterios y Desglose de Subtareas de HU04 (SCRUM-54, SCRUM-55, SCRUM-56)](Screenshot%202026-08-19%20124038.png)

---

## 🔍 Instrucciones para Commit de Git

```bash
git add .
git commit -m "HU04: Vista móvil responsive de hoja de ruta y selector rápido de conductor (SCRUM-5)"
```
