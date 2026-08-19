# Documentación Oficial de Scrum: Proyecto Final ORION (Prueba Piloto MVP)

## 📋 SECCIÓN 1: ROLES, CONFIGURACIÓN E INFRAESTRUCTURA BASE

### 1. Definición del Scrum Team y Responsabilidades
*   **Product Owner (PO):** Angel Luis Rosario (20240079@itla.edu.do)
    *   *Responsabilidad:* Gestión y priorización del Product Backlog, validación de criterios de aceptación de valor comercial y alineación con los objetivos estratégicos de reducción de kilómetros.
*   **Scrum Master (SM):** Juan Ectiversom Celedonio Solano (20241562@itla.edu.do)
    *   *Responsabilidad:* Facilitación de las ceremonias ágiles, remoción proactiva de impedimentos técnicos, protección del enfoque de desarrollo express (24 horas) y mantenimiento del flujo en el tablero de Jira.
*   **Developers (Equipo Técnico y Componentes):**
    *   **Anthonny Brayhan Soriano Franco (20242266@itla.edu.do):** Arquitectura Cloud, Diseño de Esquemas e Inicialización de la Base de Datos (HU01).
    *   **Cesar Reyes (20241308@itla.edu.do):** Backend de Ingesta, Validación de Carga Masiva CSV y Lógica del Algoritmo de Secuenciación Lineal (HU02, HU03).
    *   **Luis Manuel Ortega Mejia (20221134@itlaedudo.onmicrosoft.com):** Frontend Móvil Responsive de la Lista de Hojas de Ruta del Conductor (HU04).
    *   **Angel Gabriel Morillo Rosario (20230554@itla.edu.do):** Integración Externa de Navegación por Enlaces Profundos a Google Maps Web (HU05).
    *   **Jostin Wilmer Perez (20221096@itlaedudo.onmicrosoft.com):** Captura de Métricas en Campo y Validación Numérica de Odómetros (HU06).
    *   **Emil Hari Montilla Salvador (20220287@itlaedudo.onmicrosoft.com):** Frontend Administrativo del Panel de Control Consolidado del Supervisor (HU07).
    *   **Jhois Collado (20211124@itla.edu.do):** Backend de Analítica, Cálculos Automáticos de Kilómetros y Exportador CSV (HU07).
    *   **Josteen Mayobanex Del Orbe (20240270@itla.edu.do):** Aseguramiento de la Calidad (QA), Pruebas Unitarias de Rutas y Validación de Criterios de Aceptación.
    *   **yassil del orbe (20242536@itla.edu.do):** Pruebas de Carga e Integración de Datos, Simulación de Campo de los 5 Conductores Piloto.

### 2. Herramienta de Gestión (Jira Software)
*   **Enlace de Acceso:** [Tablero Jira ORION - ITLA](https://itla-adm.atlassian.net/jira/software/projects/SCRUM/boards/1)
*   **Evidencia del Tablero Activo en Jira (Flujo Scrum: Por hacer ➔ En curso ➔ En revisión ➔ Finalizado):**

![Tablero Activo en Jira Software - Flujo de Trabajo Scrum](docs/Screenshot%202026-08-19%20124257.png)

### 3. Product Goal
*   **Definición:** "Habilitar una plataforma digital centralizada y ligera que permita validar, en un entorno piloto controlado de 5 conductores y 1 zona urbana durante 6-8 semanas, si el ordenamiento automatizado de paradas reduce entre un 8% y 10% los kilómetros recorridos antes de proceder con inversiones de infraestructura a gran escala."

---

## 🗂️ SECCIÓN 2: ARQUITECTURA DE JIRA (ÉPICAS E HISTORIAS DE USUARIO)

### 🗂️ ÉPICA 1: SCRUM-41 - Infraestructura de Datos e Ingesta de Artículos
*   **Prioridad:** Highest
*   **Responsable:** Anthonny Brayhan Soriano Franco / Cesar Reyes
*   **Contexto de Negocio:** Representa el cimiento técnico de ORION. Consolida el almacenamiento en la nube y la ingesta rápida. Al unificar la creación del artículo con la carga geográfica, se elimina la necesidad de formularios individuales ("CRUD") que consumirían las 24 horas del Sprint.
*   **Formato de Historia:** Como Equipo de Desarrollo y Despacho, quiero disponer de una base de datos activa y un cargador masivo de archivos CSV, para registrar y persistir la información de todos los artículos y destinos de la jornada sin sistemas complejos de captura.
*   **Criterios de Aceptación de la Épica:**
    *   La base de datos Cloud (Supabase/Firebase) responde consultas de lectura y escritura.
    *   Las tablas base (`Conductores`, `Rutas`, `Articulos`) se encuentran normalizadas y relacionadas.
    *   La interfaz acepta archivos `.csv` válidos y rechaza extensiones incompatibles.

#### 📄 HU01 - Base de Datos Local e Inicialización de Tablas
*   **Story Points:** 2 | **Responsable:** Anthonny Brayhan Soriano Franco
*   **Resumen:** Despliegue del motor de base de datos en la nube y configuración del esquema de datos relacional para el piloto.
*   **Descripción:** Como Equipo de Desarrollo, quiero estructurar una base de datos centralizada e independiente, para almacenar la información de los artículos, las direcciones de entrega y los choferes de forma aislada sin afectar sistemas de producción.
*   **Criterios de Aceptación:**
    1. Desplegar e inicializar la instancia de base de datos en la nube exclusiva para el piloto.
    2. Crear tablas: `Conductores`, `Rutas` y `Articulos` con sus llaves foráneas correspondientes.
    3. Poblar de forma manual en consola los perfiles de los 5 conductores fijos del piloto (omitir CRUD de usuarios).
    4. Probar la conexión mediante un script de persistencia básico.

*   **Evidencias en Jira Software (Ticket SCRUM-2 / HU01):**

![Detalles del Ticket HU01 en Jira (SCRUM-2) - Asignación, Épica SCRUM-41 y Estimación](docs/Screenshot%202026-08-19%20124017.png)

![Criterios y Desglose de Subtareas de HU01 (SCRUM-45, SCRUM-46, SCRUM-47)](docs/Screenshot%202026-08-19%20123914.png)

#### 📄 HU02 - Carga Centralizada de Artículos (El CRUD Express por CSV)
*   **Story Points:** 2 | **Responsable:** Cesar Reyes
*   **Resumen:** Formulario web para la carga masiva de artículos y coordenadas del día desde un archivo plano.
*   **Descripción:** Como Despachador del Piloto, quiero subir un archivo CSV con los artículos del día y sus ubicaciones, para registrarlos masivamente en el sistema de manera rápida y sin formularios complejos de captura.
*   **Criterios de Aceptación:**
    1. Mostrar un botón web en el panel administrativo para seleccionar y subir archivos `.csv`.
    2. Validar que el archivo contenga las columnas obligatorias: `Articulo`, `Cliente`, `Direccion`, `Latitud`, `Longitud`.
    3. Insertar los registros directamente en la tabla `Articulos` enlazándolos a la jornada del día.
    4. Emitir una alerta en pantalla detallando la cantidad total de artículos cargados con éxito.

---

### 🗂️ ÉPICA 2: SCRUM-42 - Motor de Optimización y Secuenciación
*   **Prioridad:** High
*   **Responsable:** Cesar Reyes
*   **Contexto de Negocio:** Es el cerebro del producto. Automatiza la toma de decisiones al reemplazar el criterio visual/manual del despachador por una secuencia matemática de paradas calculada bajo cercanía física en línea recta.
*   **Formato de Historia:** Como Despachador del Piloto, quiero que el sistema ordene las paradas usando un algoritmo matemático lineal, para proveer una secuencia óptima que disminuya el desperdicio de combustible y kilómetros.
*   **Criterios de Aceptación de la Épica:**
    *   El motor procesa de forma nativa arreglos de coordenadas geoespaciales.
    *   El ordenamiento se calcula con base en la menor distancia matemática lineal desde el depósito base.
    *   Se escribe el orden de parada resultante de manera masiva en los registros de la base de datos.

#### 📄 HU03 - Secuenciador Automático de Rutas por Cercanía Lineal
*   **Story Points:** 2 | **Responsable:** Cesar Reyes
*   **Resumen:** Script en el backend que calcula el orden óptimo de paradas aplicando distancia lineal euclidiana.
*   **Descripción:** Como Despachador del Piloto, quiero que el sistema ordene automáticamente los artículos cargados basándose en sus coordenadas geográficas, para agruparlos en una secuencia de paradas optimizada que reduzca la distancia total del recorrido.
*   **Criterios de Aceptación:**
    1. Capturar los artículos almacenados para la jornada (HU02) tomando un punto de inicio (depósito) fijo.
    2. Calcular la secuencia aplicando la lógica matemática de vecino más cercano por coordenadas.
    3. Actualizar la base de datos de forma masiva inyectando un entero correlativo (1, 2, 3... N) en el campo `Secuencia` de cada artículo.
    4. Validar el flujo arrojando una lista vacía controlada si existen parámetros de latitud/longitud corruptos o nulos.

---

### 🗂️ ÉPICA 3: SCRUM-43 - Interfaz Móvil y Operación del Conductor
*   **Prioridad:** Medium
*   **Responsable:** Luis Manuel Ortega Mejia
*   **Contexto de Negocio:** Representa la interfaz de campo para los 5 conductores. Facilita la adopción al mitigar procesos de inicio de sesión complejos por medio de un menú rápido y resolver la navegación mediante enlaces profundos web.
*   **Formato de Historia:** Como Conductor del Piloto, quiero acceder a una lista responsive de mis artículos asignados y abrir sus ubicaciones en mapas externos, para guiarme en ruta y capturar el odómetro de mi camión sin fricciones en el software.
*   **Criterios de Aceptación de la Épica:**
    *   La interfaz se despliega correctamente de forma fluida en smartphones (iOS/Android).
    *   La selección del perfil filtra los datos correspondientes en tiempo real sin requerir contraseña.
    *   La activación del botón de GPS redirecciona exitosamente al entorno externo de Google Maps.

#### 📄 HU04 - Vista Móvil de Hoja de Ruta y Artículos Asignados
*   **Story Points:** 2 | **Responsable:** Luis Manuel Ortega Mejia
*   **Resumen:** Pantalla web responsive que lista las entregas del conductor en un orden correlativo riguroso.
*   **Descripción:** Como Conductor del Piloto, quiero seleccionar mi perfil y ver en mi teléfono la lista de artículos que debo entregar, para conocer mi secuencia exacta de paradas del día sin procesos complejos de inicio de sesión.
*   **Criterios de Aceptación:**
    1. Renderizar un menú desplegable inicial (`<select>`) con los nombres de los 5 conductores fijos.
    2. Al seleccionar el conductor, renderizar la lista de artículos ordenados de forma ascendente según su campo `Secuencia`.
    3. Cada tarjeta informativa en la interfaz móvil debe indicar: Orden de parada, Artículo, Cliente y Dirección de Texto.
    4. Garantizar que los elementos visuales se auto-ajusten a resoluciones móviles comunes.

*   **Evidencias en Jira Software (Ticket SCRUM-5 / HU04):**

![Detalles del Ticket HU04 en Jira (SCRUM-5) - Asignación, Épica SCRUM-43 y Estimación](docs/Screenshot%202026-08-19%20124110.png)

![Criterios y Desglose de Subtareas de HU04 (SCRUM-54, SCRUM-55, SCRUM-56)](docs/Screenshot%202026-08-19%20124038.png)

#### 📄 HU05 - Enlace GPS para Entrega de Artículos
*   **Story Points:** 1 | **Responsable:** Angel Gabriel Morillo Rosario
*   **Resumen:** Botón HTML que parametriza un enlace de búsqueda geográfica para invocar la aplicación nativa de Google Maps.
*   **Descripción:** Como Conductor del Piloto, quiero presionar un botón de navegación en el artículo seleccionado, para abrir su ubicación en Google Maps externo y recibir indicaciones GPS en tiempo real hacia el destino.
*   **Criterios de Aceptación:**
    1. Insertar un botón de acción rápida con el texto o icono "Ir a la entrega" en cada tarjeta de artículo mapeada en la HU04.
    2. Formatear dinámicamente el hipervínculo utilizando el estándar web oficial: `https://www.google.com/maps/search/?api=1&query={lat},{lng}`.
    3. Al hacer clic, activar la llamada nativa para desplegar el mapa externo en una ventana/aplicación independiente.

#### 📄 HU06 - Cierre de Entrega de Artículos y Registro de Odómetro
*   **Story Points:** 2 | **Responsable:** Jostin Wilmer Perez
*   **Resumen:** Módulo de captura numérica para bloquear el flujo al inicio y cierre del turno operativo del chofer.
*   **Descripción:** Como Conductor del Piloto, quiero registrar el kilometraje de mi vehículo al comenzar y terminar el día, y marcar cada artículo completado, para guardar el reporte de distancia real de mi jornada de entregas.
*   **Criterios de Aceptación:**
    1. Presentar una caja de captura numérica para el "Kilometraje Inicial" al cargar la ruta (bloquea la visualización hasta ser guardada).
    2. Integrar un botón de cambio de estado ("Marcar Entregado") al lado de cada artículo en la lista.
    3. Habilitar el botón y campo "Kilometraje Final" únicamente cuando el 100% de los artículos de la lista pasen a estado "Entregado".
    4. Validar del lado del cliente que el valor numérico final ingresado sea estrictamente mayor o igual al valor inicial.

---

### 🗂️ ÉPICA 4: SCRUM-44 - Panel de Control Administrativo y Analítica
*   **Prioridad:** Low
*   **Responsable:** Emil Hari Montilla Salvador / Jhois Collado
*   **Contexto de Negocio:** Es la herramienta de auditoría y cierre gerencial del proyecto. Permite realizar la comparación matemática para demostrar la reducción de distancias, generando el insumo financiero (.csv) que justifica la inversión a gran escala de ORION.
*   **Formato de Historia:** Como Supervisor del Piloto, quiero ver una tabla consolidada con las métricas de kilómetros de las rutas del día, para auditar la eficiencia del software y exportar informes hacia la alta gerencia.
*   **Criterios de Aceptación de la Épica:**
    *   La tabla web lee y procesa los datos del odómetro inyectados desde la app del conductor.
    *   Calcula de forma automática los indicadores de distancia recorrida real neta.
    *   Ofrece un disparador de descarga directa del set de datos en formato de texto plano estructurado.

#### 📄 HU07 - Panel de Control Administrativo y Reporte Consolidado
*   **Story Points:** 3 | **Responsable:** Emil Hari Montilla Salvador (Frontend) / Jhois Collado (Backend)
*   **Resumen:** Pantalla administrativa unificada que despliega el resumen de métricas del día y permite la descarga de reportes.
*   **Descripción:** Como Supervisor del Piloto, quiero visualizar un tablero centralizado con el estado de las rutas y los kilómetros recorridos por cada chofer, para analizar los datos reales de la operación y exportar el reporte de rendimiento del día.
*   **Criterios de Aceptación:**
    1. Crear una interfaz web exclusiva para el supervisor accesible mediante una ruta administrativa plana.
    2. Renderizar una grilla/tabla dinámica con los datos del día: Conductor, Artículos Entregados / Totales, Odómetro Inicial y Odómetro Final.
    3. Computar en tiempo real la columna de "Distancia Total Recorrida" mediante la resta matemática de los odómetros de la HU06.
    4. Incorporar un botón funcional de exportación que genere la descarga inmediata de la matriz visible en un formato `.csv`.

---

## 🏃‍♂️ SECCIÓN 3: DOCUMENTACIÓN DE LA EJECUCIÓN DEL SPRINT

### 1. Sprint Planning (Duración Simulada: 1 Semana)
*   **Sprint Goal:** "Habilitar la infraestructura de datos base que permita registrar artículos y direcciones masivamente mediante un CSV, calcular una secuencia optimizada de paradas por proximidad lineal, y desplegar la hoja de ruta responsive para que los conductores gestionen sus entregas apoyados en Google Maps externo, consolidando las métricas de kilometraje en un reporte final para el supervisor."
*   **Definition of Done (DoD) del Equipo:**
    *   [ ] El código compila limpiamente en el servidor local sin advertencias de sintaxis.
    *   [ ] Los campos interactivos y formularios cuentan con validaciones nativas contra datos nulos o tipos erróneos.
    *   [ ] Los datos se leen y persisten de forma correcta en las tablas de la nube (Supabase/Firebase).
    *   [ ] La interfaz se visualiza correctamente de forma responsiva en smartphones de prueba.
    *   [ ] El código cumple con el 100% de los criterios de aceptación funcionales definidos en su ticket de Jira.

*   **Evidencias del Sprint Planning y Product Backlog en Jira:**

![Configuración del Backlog y Épicas en Jira Software (Épicas SCRUM-41 y SCRUM-42)](docs/Screenshot%202026-08-19%20123806.png)

![Backlog del Sprint 1 con las 7 Historias de Usuario estimadas (14 Story Points) y Épicas SCRUM-43 y SCRUM-44](docs/Screenshot%202026-08-19%20123639.png)

### 2. Minuta de la Daily Scrum
*   **Fecha de la Sesión:** 19 de Agosto de 2026
*   **Participantes:** Developers y Scrum Master (Juan Solano)
*   **Registro de Respuestas de la Dinámica Ágile:**
    *   *Anthonny Soriano (Base de Datos):* "Ayer terminé el esquema de tablas en Supabase y poblé los 5 choferes fijos. Hoy apoyaré a Cesar conectando el backend del CSV. No tengo bloqueos técnicos."
    *   *Cesar Reyes (Ingesta e Inserción):* "Ayer programé la lectura básica del archivo `.csv` en Node.js/Python. Hoy conectaré la inserción masiva a la tabla de artículos de Anthonny y empezaré la lógica matemática del vecino más cercano. Sin impedimentos."
    *   *Luis Ortega (Frontend Móvil):* "Ayer maqueté la vista de lista responsive en HTML/CSS. Hoy conectaré la consulta para que el select de choferes jale las paradas directamente de la base de datos de forma dinámica. Todo fluye."
    *   *Angel Morillo (Integración GPS):* "Ayer estructuré la cadena de texto de los enlaces de Google Maps. Hoy integraré los botones en las tarjetas móviles que está desarrollando Luis. Todo bajo control."
    *   *Jostin Perez (Módulo Odómetro):* "Ayer diseñé los inputs numéricos de entrada. Hoy programaré la lógica de validación para asegurar que el kilometraje final no sea menor al inicial y conectaré el guardado masivo."
    *   *Emil Montilla & Jhois Collado (Panel Administrativo):* "Ayer maquetamos la tabla web del supervisor. Hoy inyectaremos las consultas SQL para cruzar los artículos de Cesar con los kilómetros finales de Jostin y generaremos el trigger de exportación a CSV."
    *   *Josteen Del Orbe & yassil del orbe (QA / Pruebas):* "Ayer estructuramos la plantilla CSV de prueba con coordenadas reales de la zona piloto. Hoy iniciaremos pruebas funcionales de extremo a extremo simulando el flujo de los 5 choferes sobre las vistas de Luis y los reportes de Emil."

### 3. Registro de Impedimentos (Gestión de Bloqueos)
*   **Impedimento Detectado:** "El archivo CSV de prueba del despachador arrojaba fallas en la base de datos debido a que los valores de latitud y longitud venían formateados con comas en lugar de puntos decimales anglosajones, corrompiendo la geolocalización."
*   **Gestión y Resolución (Acción del Scrum Master):** El Scrum Master (Juan Solano) convocó a una sesión técnica exprés de 15 minutos entre Cesar Reyes (Backend) y las QA (Josteen y Yassil). Se resolvió implementar una función de sanitización en el código de la HU02 que reemplace automáticamente cualquier coma por punto antes de la inserción SQL, y se actualizó la documentación de la plantilla compartida al despachador. Bloqueo removido en 45 minutos.

---

## 🏁 SECCIÓN 4: CIERRE, REVISIÓN Y APRENDIZAJE

### 1. Sprint Review (Presentación de Software Funcional)
*   **Enfoque de la Demostración:** Presentación en vivo ante los evaluadores del flujo completo del sistema ORION corriendo sobre código real en lugar de diapositivas estáticas.
*   **Flujo de la Demo Realizada:**
    1.  El Product Owner (Angel Rosario) abre el panel administrativo y carga un archivo `.csv` de prueba con 15 artículos de entrega para una zona urbana.
    2.  El sistema procesa el archivo, muestra la alerta de éxito y ejecuta el algoritmo, ordenando las paradas del 1 al 15 en base a cercanía lineal.
    3.  El Developer Luis Ortega abre la vista móvil responsive y simula ser el Conductor #3 desde el inspector del navegador. Selecciona su nombre en el menú rápido.
    4.  Aparece el formulario de kilometraje; introduce el odómetro inicial "50,200 km" y se despliega la lista ordenada de artículos con sus direcciones.
    5.  Se hace clic en el botón "Ir a la entrega" de la parada #1; el sistema invoca de forma externa la interfaz oficial de Google Maps web con las coordenadas correctas.
    6.  Se marcan todos los artículos como "Entregados" y se digita el kilometraje final "50,245 km".
    7.  El supervisor Emil Montilla refresca su tablero y muestra la fila actualizada del Conductor #3 detallando los 45 kilómetros netos recorridos, presionando el botón para descargar el reporte CSV final en su computadora.
*   **Estatus de Historias:** 7 Historias de Usuario completadas al 100% bajo conformidad del Product Owner al cumplir con la *Definition of Done*.

### 2. Sprint Retrospective (Plan de Mejora Continua)
*   **¿Qué salió bien?:** La estrategia ágil de recortar el alcance (eliminar logins pesados, geocodificación externa por texto y soporte Excel) permitió levantar la infraestructura de software funcional en menos de 24 horas. La división técnica por micro-componentes evitó colisiones en los repositorios de código.
*   **¿Qué no salió bien?:** La estimación inicial de algunos Story Points fue ajustada sobre la marcha debido a problemas con el formato decimal del CSV de entrada, lo que causó estrés en el backend durante las primeras horas del día.
*   **¿Qué podemos mejorar?:** Implementar de forma obligatoria un checklist o contrato de datos para cualquier archivo externo antes de iniciar la programación de la ingesta de datos.
*   **Acción de Mejora Concreta (Próximo Sprint):** "Diseñar un validador estricto de esquemas JSON/CSV del lado del cliente en el frontend antes de enviar los datos al servidor, asignando a Cesar Reyes como responsable técnico de su implementación para la Fase 2."

---

## ✍️ SECCIÓN 5: REFLEXIÓN INDIVIDUAL (FORMATO DE ENTREGA)

*Nota: Cada integrante debe completar esta plantilla de forma personal respondiendo con base en su experiencia técnica y metodológica en el rol asignado.*

### Plantilla de Evaluación Individual
1.  **¿Cómo fue su experiencia trabajando en equipo?**
2.  **¿Cómo fue trabajar utilizando la metodología Scrum?**
3.  **¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?**
4.  **¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?**
5.  **¿Qué considera que pudo haberse realizado mejor en la organización del equipo?**
6.  **¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?**
