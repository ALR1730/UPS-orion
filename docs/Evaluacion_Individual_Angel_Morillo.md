# 👤 Evaluación Individual - Desarrollador

**Nombre del Integrante:** Angel Gabriel Morillo Rosario
**Rol en el Sprint:** Desarrollador (implementación de historia de usuario HU05)
**Fecha de Evaluación:** 19 de Agosto de 2026

---

## 1. ¿Cómo fue su experiencia trabajando en equipo?

Mi experiencia fue positiva en términos de aprendizaje, aunque marcada por los mismos retos de coordinación que afectaron a todo el equipo durante los primeros días del Sprint. Al enfocarme en una historia de usuario concreta (HU05, navegación GPS del conductor), pude mantener un avance relativamente autónomo, pero sentí la falta de sincronización con las áreas de backend y base de datos, especialmente en cuanto al formato en que llegarían las coordenadas de cada tarjeta. Una vez el Product Owner intervino para alinear expectativas, el trabajo en equipo mejoró notablemente y logramos integrar mi componente sin mayores fricciones.

## 2. ¿Cómo fue trabajar utilizando la metodología Scrum?

Trabajar bajo Scrum me permitió entender el valor de dividir el trabajo en historias de usuario pequeñas y verificables. Al tener HU05 claramente delimitada (un solo story point, un objetivo específico: botón de navegación por tarjeta), pude enfocar mi esfuerzo sin dispersarme. También experimenté de primera mano cómo un Sprint corto de una semana exige que cualquier bloqueo se comunique de inmediato, ya que no hay margen de tiempo para resolver ambigüedades a mitad de camino.

## 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?

**A nivel metodológico:** Aprendí que documentar bien una historia de usuario -incluyendo su criterio de aceptación y el responsable del ticket- facilita enormemente el trabajo individual dentro de un Sprint, incluso cuando el equipo tiene fricciones de coordinación.

**A nivel técnico:** Profundicé en la integración de funcionalidades de navegación externa dentro de una vista Razor (.cshtml), usando deep links de Google Maps con coordenadas en formato invariante (`CultureInfo.InvariantCulture`) y un mecanismo de respaldo por dirección (`Uri.EscapeDataString`) para los casos donde la geolocalización no estuviera disponible. También reforcé el uso de Bootstrap y Font Awesome para mantener consistencia visual dentro del módulo del conductor.

## 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?

El principal bloqueo fue la incertidumbre inicial sobre el formato exacto en que llegarían las coordenadas (lat/lng) desde la capa de datos, lo cual me obligó a esperar la definición del contrato de datos antes de poder cerrar completamente la lógica del botón de navegación. También hubo cierta ambigüedad sobre qué hacer cuando una tarjeta de artículo no tuviera coordenadas válidas, lo que resolví implementando el fallback por dirección.

## 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?

Creo que debimos definir el contrato de datos (formato de coordenadas, manejo de nulos) desde el Sprint Planning, en lugar de descubrirlo sobre la marcha durante el desarrollo. Esto habría evitado tiempo de espera innecesario para historias como la mía, que dependen directamente de cómo llega la información desde otras capas del sistema.

## 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?

En un próximo Sprint propondría:
1. Definir y firmar los contratos de datos entre frontend y backend antes de iniciar la implementación de cualquier historia que dependa de ellos.
2. Reportar bloqueos de dependencia el mismo día que se detectan, sin esperar a la Daily.
3. Documentar desde el inicio los casos borde (como datos faltantes o inválidos) como parte del criterio de aceptación, para no descubrirlos a mitad del desarrollo.
