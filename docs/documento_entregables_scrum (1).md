# Documentación Oficial de Scrum: Proyecto Final ORION (Prueba Piloto MVP)

## 📋 SECCIÓN 1: ROLES, CONFIGURACIÓN E INFRAESTRUCTURA BASE

### 1. Definición del Scrum Team y Responsabilidades
*   **Product Owner (PO):** Angel Luis Rosario (20240079@itla.edu.do)
    *   *Responsabilidad:* Gestión y priorización del Product Backlog, validación de criterios de aceptación de valor comercial y alineación con los objetivos estratégicos de reducción de kilómetros.
*   **Scrum Master (SM):** Juan Ectiversom Celedonio Solano (20241562@itla.edu.do)
    *   *Responsabilidad:* Facilitación de las ceremonias ágiles, remoción proactiva de impedimentos técnicos, protección del enfoque de desarrollo express (24 horas) y mantenimiento del flujo en el tablero de Jira.
*   **Developers (Equipo Técnico y Componentes):**
    *   **Anthonny Brayhan Soriano Franco (20242266@itla.edu.do):** Arquitectura Cloud, Diseño de Esquemas e Inicialización de la Base de Datos (HU01).
    *   **Cesar Reyes (20241308@itla.edu.do):** Backend de Ingesta y Validación de Carga Masiva CSV (HU02).
    *   **Luis Manuel Ortega Mejia (20221134@itlaedudo.onmicrosoft.com):** Frontend Móvil Responsive de la Lista de Hojas de Ruta del Conductor (HU04).
    *   **Angel Gabriel Morillo Rosario (20230554@itla.edu.do):** Integración Externa de Navegación por Enlaces Profundos a Google Maps Web (HU05).
    *   **Jostin Wilmer Perez (20221096@itlaedudo.onmicrosoft.com):** Captura de Métricas en Campo y Validación Numérica de Odómetros (HU06).
    *   **Emil Hari Montilla Salvador (20220287@itlaedudo.onmicrosoft.com):** Frontend Administrativo del Panel de Control Consolidado del Supervisor (HU07).
    *   **Jhois Collado (20211124@itla.edu.do):** Backend de Analítica, Cálculos Automáticos de Kilómetros y Exportador CSV (HU07).
    *   **Josteen Mayobanex Del Orbe (20240270@itla.edu.do):** Aseguramiento de la Calidad (QA), Pruebas Unitarias de Rutas y Validación de Criterios de Aceptación.
    *   **yassil del orbe (20242536@itla.edu.do):** Pruebas de Carga e Integración de Datos, Simulación de Campo de los 5 Conductores Piloto.
    *   **Jasuel De Los Santos (20231983@itla.edu.do):** Motor de Optimización y Lógica del Algoritmo de Secuenciación Lineal (HU03).

### 2. Herramientas de Gestión, Repositorio de Código y Video de Exposición
*   **Tablero de Gestión (Jira Software):** [Tablero Jira ORION - ITLA](https://itla-adm.atlassian.net/jira/software/projects/SCRUM/boards/1)
*   **Repositorio Oficial de Código (GitHub):** [GitHub - ALR1730/UPS-orion](https://github.com/ALR1730/UPS-orion)
*   **Video de Exposición y Demostración del Proyecto:** [Exposición del Software ORION MVP en YouTube (https://youtu.be/527MzZ8nduo)](https://youtu.be/527MzZ8nduo)
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
*   **Responsable:** Jasuel De Los Santos
*   **Contexto de Negocio:** Es el cerebro del producto. Automatiza la toma de decisiones al reemplazar el criterio visual/manual del despachador por una secuencia matemática de paradas calculada bajo cercanía física en línea recta.
*   **Formato de Historia:** Como Despachador del Piloto, quiero que el sistema ordene las paradas usando un algoritmo matemático lineal, para proveer una secuencia óptima que disminuya el desperdicio de combustible y kilómetros.
*   **Criterios de Aceptación de la Épica:**
    *   El motor procesa de forma nativa arreglos de coordenadas geoespaciales.
    *   El ordenamiento se calcula con base en la menor distancia matemática lineal desde el depósito base.
    *   Se escribe el orden de parada resultante de manera masiva en los registros de la base de datos.

#### 📄 HU03 - Secuenciador Automático de Rutas por Cercanía Lineal
*   **Story Points:** 2 | **Responsable:** Jasuel De Los Santos
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

### 2. Minuta de la Daily Scrum Extraordinaria (Realineación del Equipo)
*   **Fecha de la Sesión:** 19 de Agosto de 2026
*   **Facilitador:** Scrum Master (Juan Ectiversom Celedonio Solano)
*   **Participantes:** Equipo Completo (Product Owner, Developers y QA Engineers)
*   **Contexto Crítico:** Sesión de sincronización obligatoria convocada tras un **retraso crítico acumulado de 4 días** al inicio del proyecto, provocado por una marcada falta de comunicación entre los integrantes, desalineación en las responsabilidades de cada módulo y ausencia de canales de coordinación técnica unificados.

*   **Registro de Intervenciones de la Dinámica Ágil (Las 3 Preguntas de Scrum):**
    *   **Juan Solano (Scrum Master - Facilitador):**  
        *"Durante los primeros 4 días el equipo trabajó en silos aislados: el backend esperaba especificaciones que frontend no había definido y QA no tenía criterios claros de prueba. Esta descoordinación y falta de comunicación nos costó 4 días de atraso sobre el cronograma inicial. Hoy abrimos esta Daily para transparentar bloqueos, sincronizar interfaces y definir el plan de choque que nos permita entregar el MVP al 100%."*
    *   **Angel Rosario (Product Owner):**  
        *"Mi bloqueo en los primeros 4 días fue que las dudas sobre el alcance del MVP (si incluir o no autenticación compleja y mapas embebidos) se discutieron de forma dispersa sin llegar a acuerdos claros. Ayer redefiní el alcance recortando lo superfluo y priorizando el flujo nuclear (CSV -> Secuenciación -> Conductor -> Reporte). Hoy estaré disponible en tiempo real para validar las historias. No tengo más bloqueos."*
    *   **Anthonny Soriano (Base de Datos & Arquitectura):**  
        *   *¿Qué hice?:* "Estuve frenado 4 días porque no teníamos acordada la estructura de los datos ni qué base de datos usaríamos (SQL vs NoSQL). Ayer definí el esquema relacional en SQLite/Supabase con las entidades `Drivers`, `Routes` y `RouteStops`, y sembré los 5 choferes fijos del piloto."  
        *   *¿Qué haré hoy?:* "Proveer la cadena de conexión e integrar el contexto de datos con la ingesta CSV de Cesar."  
        *   *Bloqueos:* "Ninguno actualmente tras acordar el esquema común."
    *   **Cesar Reyes (Ingesta CSV & Algoritmo de Secuenciación):**  
        *   *¿Qué hice?:* "Perdí los primeros 4 días programando un módulo de carga aislado sin saber el formato exacto de coordenadas que entregarían las QA ni la tabla donde se guardaría. Ayer sincronicé con Anthonny y completé la lectura de archivos CSV y el algoritmo Nearest Neighbor con Haversine."  
        *   *¿Qué haré hoy?:* "Conectar la inserción automática y la reordenación correlativa (1..N) para alimentar la vista del conductor."  
        *   *Bloqueos:* "Resuelto el desfase de comas/puntos decimales mediante sanitización interna."
    *   **Luis Ortega (Frontend Móvil Conductor):**  
        *   *¿Qué hice?:* "Estuve esperando durante 4 días los endpoints del backend para maquetar la interfaz móvil. Ayer decidí avanzar con la UI Glassmorphism en HTML5/CSS y crear el selector interactivo de choferes."  
        *   *¿Qué haré hoy?:* "Conectar las tarjetas de entrega y la pantalla de bloqueo por odómetro inicial con los datos reales de la ruta."  
        *   *Bloqueos:* "Ninguno; ya coordiné directamente con Jostin los nombres de los campos de odómetro."
    *   **Angel Morillo (Integración GPS & Navegación):**  
        *   *¿Qué hice?:* "Al inicio hubo confusión sobre si debíamos integrar un SDK de mapas de pago o usar URLs directas, lo que nos estancó. Ayer construí la plantilla de Deep Linking universal a Google Maps Web (`query={lat},{lng}`)."  
        *   *¿Qué haré hoy?:* "Inyectar el botón 'Ir a la entrega' dentro de cada tarjeta maquetada por Luis."  
        *   *Bloqueos:* "Sin impedimentos técnicos."
    *   **Jostin Perez (Módulo de Odómetros & Control de Jornada):**  
        *   *¿Qué hice?:* "No teníamos claro si el odómetro se registraba por parada o por día completo, lo que retrasó la lógica de negocio. Ayer definí la validación de inicio obligatorio (Odómetro Inicial) y cierre de jornada (Odómetro Final >= Inicial)."  
        *   *¿Qué haré hoy?:* "Implementar el cálculo de distancia neta (`Final - Inicial`) y disparar el estado 'Finalizado'."  
        *   *Bloqueos:* "Ninguno."
    *   **Emil Montilla & Jhois Collado (Panel Administrativo Supervisor):**  
        *   *¿Qué hicieron?:* "El retraso de 4 días en la base de datos nos impedía estructurar las consultas de analítica. Ayer diseñamos la tabla de monitoreo en tiempo real y el cálculo de ahorro de combustible contra la línea base histórica."  
        *   *¿Qué harán hoy?:* "Vincular el botón de exportación CSV con codificación compatible con Excel (UTF-8 BOM) y verificar el resumen de KPIs."  
        *   *Bloqueos:* "Todo coordinado con el equipo backend."
    *   **Josteen Del Orbe & Yassil Del Orbe (QA Engineers / Pruebas):**  
        *   *¿Qué hicieron?:* "La falta de un canal centralizado de comunicación impidió que tuviéramos los casos de prueba listos a tiempo. Ayer construimos la batería de pruebas automatizadas y el dataset piloto de 15 artículos urbanos en Santo Domingo."  
        *   *¿Qué harán hoy?:* "Ejecutar pruebas End-to-End en vivo (HU01 a HU07) simulando el flujo completo de los 5 conductores y validando la descarga del reporte final."  
        *   *Bloqueos:* "Ninguno; ambiente de pruebas listo y operativo."

### 3. Registro de Impedimentos (Gestión de Bloqueos)
*   **Impedimento Principal #1 (Organizacional - 4 Días de Atraso):**  
    *   *Descripción:* "Falta de comunicación asertiva y coordinación operativa durante los primeros 4 días del Sprint. Los desarrolladores iniciaron tareas individuales sin un canal de comunicación oficial, sin contratos de datos acordados y sin claridad en la arquitectura, provocando retrabajo y un desfase crítico en la entrega."  
    *   *Gestión del Scrum Master (Juan Solano):* Convocó a una sesión de emergencia presencial/virtual de realineación técnica. Se estableció un canal único de Discord/WhatsApp con hilos por épica, se unificó el repositorio Git con ramas estandarizadas y se acordó un contrato de datos estricto para las 7 Historias de Usuario. El equipo recuperó el ritmo en una jornada intensiva de desarrollo continuo.
*   **Impedimento Técnico #2 (Formato Decimal en Ingesta CSV):**  
    *   *Descripción:* "El archivo CSV de prueba arrojaba excepciones en la base de datos debido a que los valores de latitud y longitud venían formateados con comas en lugar de puntos decimales anglosajones, corrompiendo la geolocalización."  
    *   *Gestión y Resolución:* El Scrum Master coordinó una sesión técnica exprés entre Cesar Reyes (Backend) y las QA (Josteen y Yassil). Se implementó sanitización automática en la HU02 (`AddressImportService`) que convierte comas a puntos decimales con `CultureInfo.InvariantCulture`. Bloqueo solucionado en 45 minutos.

---

## 🏁 SECCIÓN 4: CIERRE, REVISIÓN Y APRENDIZAJE

### 1. Sprint Review (Presentación de Software Funcional)
*   **Video de Exposición y Demostración Oficial:** [Ver Grabación en YouTube (https://youtu.be/527MzZ8nduo)](https://youtu.be/527MzZ8nduo)
*   **Enfoque de la Demostración:** Presentación en vivo ante los evaluadores del flujo completo del sistema ORION corriendo sobre código real en lugar de diapositivas estáticas, demostrando la recuperación total tras el retraso inicial.
*   **Flujo de la Demo Realizada:**
    1.  El Product Owner (Angel Rosario) abre el panel administrativo y carga el archivo `.csv` oficial de 15 paradas urbanas.
    2.  El sistema procesa el archivo, ejecuta el algoritmo Nearest Neighbor y secuencia las paradas del 1 al 15 en menos de 10 milisegundos.
    3.  El Developer Luis Ortega abre la vista móvil responsive y selecciona al Conductor #3 (*José Gómez* - `UPS-TRUCK-309`).
    4.  Aparece la pantalla de bloqueo de inicio de jornada; introduce el odómetro inicial "50,200 km" y se desbloquea la lista ordenada de artículos.
    5.  Se presiona el botón "Ir a la entrega" de la parada #1, abriendo Google Maps Web con las coordenadas exactas de Santo Domingo.
    6.  Se marcan las 15 paradas como "Entregado" y se digita el kilometraje final "50,245 km".
    7.  El supervisor Emil Montilla refresca su tablero, visualizando los 45.0 KM netos recorridos, 12.0 KM ahorrados (21% de reducción de combustible) y descarga el reporte consolidado `.csv` compatible con Excel.
*   **Estatus de Historias:** 7 Historias de Usuario completadas al 100% bajo conformidad del Product Owner al cumplir con la *Definition of Done*.

### 2. Sprint Retrospective (Plan de Mejora Continua)
*   **¿Qué salió bien?:** La capacidad de reacción del equipo tras la Daily Scrum extraordinaria de realineación. La simplificación del alcance y el enfoque en micro-componentes desacoplados permitieron desarrollar, probar y desplegar las 7 Historias de Usuario en tiempo récord.
*   **¿Qué no salió bien?:** La severa falta de comunicación y coordinación al arranque del proyecto nos costó 4 días de inactividad efectiva, forzando un esfuerzo extraordinario al final del Sprint para compensar el tiempo perdido.
*   **¿Qué podemos mejorar?:** No iniciar ninguna línea de código sin antes haber celebrado la Sprint Planning con contratos de interfaces claros, canales de chat activos y asignación explícita de responsabilidades desde el Día 1.
*   **Acción de Mejora Concreta (Próximo Sprint):** "Establecer de forma innegociable Dailies síncronas a primera hora del día, matrices de contratos de API/Datos firmadas entre Frontend y Backend antes de programar, y un canal de alertas tempranas para que ningún bloqueo dure más de 2 horas sin escalar al Scrum Master."

---

## ✍️ SECCIÓN 5: REFLEXIÓN INDIVIDUAL (FORMATO DE ENTREGA)

*Nota: Cada integrante debe completar esta plantilla de forma personal respondiendo con base en su experiencia técnica y metodológica en el rol asignado.*

### 📄 Plantilla de Evaluación Individual (En Blanco para el Equipo)
1. **¿Cómo fue su experiencia trabajando en equipo?**
2. **¿Cómo fue trabajar utilizando la metodología Scrum?**
3. **¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?**
4. **¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?**
5. **¿Qué considera que pudo haberse realizado mejor en la organización del equipo?**
6. **¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?**

---

### 👤 Evaluación Individual - Product Owner (Angel Luis Rosario)

* **Nombre del Integrante:** Angel Luis Rosario  
* **Rol en el Sprint:** Product Owner (PO)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia en este proyecto fue sumamente retadora y formativa. Al inicio del Sprint enfrentamos una marcada falta de comunicación y descoordinación interna, donde los miembros trabajaban de manera aislada sin compartir avances ni reportar bloqueos a tiempo, lo que generó un estancamiento de 4 días en el cronograma. Esta situación me exigió asumir un rol de liderazgo constante y proactivo para alinear las expectativas, guiar las decisiones de diseño y asegurar que todos los componentes convergieran en un producto coherente. A pesar de las fricciones iniciales, logramos canalizar la energía del equipo en la recta final para concretar una entrega funcional y exitosa."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Trabajar con Scrum me permitió comprender que la agilidad no radica en la velocidad con la que se escribe código, sino en la disciplina de la comunicación, la transparencia y la capacidad de adaptación. El marco Scrum evidenció con rapidez las fallas de coordinación tempranas y nos brindó las herramientas necesarias (el refinamiento de historias, la redefinición del Backlog con base en el Product Goal y la Daily de emergencia) para corregir el rumbo y entregar valor tangible en un ciclo corto de 1 semana."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel metodológico:** *"Aprendí la vital importancia de establecer Criterios de Aceptación (AC) milimétricamente detallados y contratos de datos rigurosos desde el primer momento. Si el PO no delimita con absoluta claridad qué entra y qué no entra en el MVP, el equipo tiende a dispersarse en funcionalidades accesorias."*
> * **A nivel técnico:** *"Profundicé en el valor de las arquitecturas desacopladas (Clean MVC en C# .NET, bases de datos ligeras con SQLite y cálculo geométrico de distancias por Haversine), comprendiendo cómo una infraestructura bien estructurada facilita las pruebas automatizadas y la integración continua sin dependencias externas pesadas."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"El mayor obstáculo fue la barrera de comunicación y la falta de sincronización inicial entre las diferentes partes del sistema (base de datos, ingesta de archivos y frontend móvil). Tuve que intervenir de manera continua para disipar dudas de negocio, clarificar los flujos de los 5 conductores, estructurar los formatos de datos (como la sanitización de coordenadas decimales y los odómetros) y supervisar que cada historia cumpliera estrictamente la Definition of Done para salvar la entrega del proyecto tras el desfase inicial."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Considero que debimos establecer acuerdos de trabajo en equipo (Team Working Agreements) y canales oficiales de comunicación unificados desde el Día 1, en lugar de asumir que la coordinación se daría de forma espontánea. Asimismo, una sesión de Sprint Planning más exhaustiva con validación cruzada de dependencias técnicas entre desarrolladores y QA hubiese evitado los 4 días de retraso inicial."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint aplicaría como regla innegociable: (1) Dailies síncronas obligatorias a primera hora con un límite estricto de tiempo para detectar bloqueos en menos de 24 horas; (2) firmas de contratos de interfaces de datos (JSON/CSV) antes de tirar la primera línea de código; y (3) fomentar una cultura de comunicación abierta y temprana donde cualquier duda sobre el alcance se plantee de inmediato al Product Owner sin esperar a que se acumulen retrasos."*

---

### 👤 Evaluación Individual - Scrum Master (Juan Ectiversom Celedonio Solano)

* **Nombre del Integrante:** Juan Ectiversom Celedonio Solano  
* **Matrícula:** 2024-1562  
* **Rol en el Sprint:** Scrum Master (SM)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia como Scrum Master fue sumamente intensa y formativa. El principal desafío fue guiar al equipo a superar la desalineación de los primeros 4 días del Sprint, donde la falta de comunicación generó bloqueos silenciosos. Convocar la Daily extraordinaria de emergencia y reestructurar los canales de comunicación permitió al equipo recuperar la cohesión y trabajar enfocado en los 14 Story Points comprometidos hasta lograr una entrega exitosa."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Scrum demostró ser fundamental para hacer visible la realidad del proyecto. El tablero de Jira y las ceremonias nos permitieron identificar los cuellos de botella con rapidez, redefinir prioridades junto al Product Owner y asegurar que cada miembro tuviera claridad absoluta sobre sus criterios de aceptación y la Definition of Done."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel metodológico:** *"Aprendí que el rol del Scrum Master no es solo coordinar reuniones, sino proteger proactivamente el tiempo del equipo, facilitar la resolución ágil de impedimentos técnicos y mantener alta la moral y la disciplina en momentos de alta presión."*
> * **A nivel técnico:** *"Comprendí a fondo la arquitectura Clean MVC del sistema ORION y la importancia de los contratos de datos (formatos de coordenadas e ingesta CSV) para que las dependencias entre células no detengan el avance del desarrollo."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"La principal dificultad fue romper el aislamiento inicial de los desarrolladores y gestionar el impedimento crítico de los 4 días de desfase. A nivel técnico, coordinar la rápida resolución del formato de comas decimales en el archivo CSV entre backend y QA requirió mediación técnica inmediata para evitar que afectara las pruebas finales."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Debimos establecer desde el primer día un canal único y centralizado de comunicación con reglas claras de escalamiento de bloqueos, así como un Sprint Planning más detallado donde se firmaran las interfaces de datos antes de iniciar la programación."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint aplicaría: (1) Dailies síncronas estrictas de máximo 15 minutos a primera hora del día; (2) un protocolo de escalamiento donde ningún impedimento técnico supere las 2 horas sin ser atendido; y (3) seguimiento continuo en Jira para asegurar que todas las subtareas se actualicen en tiempo real."*

---

### 👤 Evaluación Individual - Arquitectura Cloud & Base de Datos (Anthonny Brayhan Soriano Franco)

* **Nombre del Integrante:** Anthonny Brayhan Soriano Franco  
* **Matrícula:** 2024-2266  
* **Rol en el Sprint:** Arquitectura Cloud, Diseño de Esquemas e Inicialización de la Base de Datos (HU01)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia trabajando en equipo fue buena y me permitió entender mejor la importancia de la comunicación entre los diferentes integrantes. En mi caso, trabajé principalmente en la parte de la base de datos y tuve que coordinar con los compañeros que estaban trabajando en el backend y en las demás partes del sistema para que la estructura de los datos fuera compatible con sus necesidades. Al principio tuvimos algunas dificultades de comunicación y organización, pero después logramos coordinarnos mejor y avanzar de manera más fluida."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Trabajar con Scrum fue una experiencia interesante porque permitió dividir el proyecto en historias de usuario y asignar responsabilidades específicas. El uso de Jira ayudó a tener una mejor visión de las tareas pendientes y del progreso del Sprint. También pude entender mejor la importancia de las Daily Scrum para comunicar qué se hizo, qué se va a hacer y si existe algún bloqueo. Considero que Scrum nos ayudó especialmente cuando tuvimos que reorganizarnos debido a los retrasos iniciales."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Aprendí más sobre cómo estructurar una base de datos para un proyecto que tiene varios módulos que dependen de ella. Trabajé en la creación y organización de las tablas relacionadas con los conductores, rutas y artículos, además de la inicialización de los datos necesarios para el piloto."*
> * **A nivel metodológico:** *"Aprendí que antes de comenzar a desarrollar es importante tener claro cómo se van a manejar los datos y cómo se van a comunicar los diferentes módulos. También aprendí que una tarea no debe verse solamente de forma individual, sino teniendo en cuenta cómo va a afectar o integrarse con el trabajo de los demás compañeros."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"La principal dificultad fue la falta de coordinación durante los primeros días del Sprint. Al principio no estaban completamente definidos algunos aspectos de la estructura de datos y de la integración con los demás módulos, por lo que fue necesario esperar a que se aclararan ciertas decisiones antes de continuar. También fue necesario coordinar con el compañero encargado del backend para asegurar que los datos provenientes del CSV pudieran almacenarse correctamente en la estructura de la base de datos. Una vez que se establecieron los campos y relaciones necesarias, el trabajo pudo avanzar con mayor facilidad."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Considero que pudimos haber definido desde el primer día la arquitectura general del proyecto, las estructuras de datos y las responsabilidades de cada integrante. También habría sido mejor establecer desde el inicio un canal principal de comunicación para evitar que las informaciones importantes se dispersaran. De esta manera, los integrantes que dependían del trabajo de otros compañeros hubieran podido avanzar más rápido y se habría reducido el retraso que tuvimos al inicio del Sprint."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint trataría de definir primero las estructuras y contratos de datos que utilizarán los diferentes módulos antes de comenzar a desarrollar. También comunicaría cualquier bloqueo o duda lo antes posible para evitar que un problema pequeño termine afectando el trabajo de otros compañeros. Además, mantendría una comunicación más constante con los integrantes cuyos módulos dependen directamente de la base de datos y utilizaría Jira y Git de manera más organizada para que todos puedan conocer el estado de las tareas y los cambios realizados."*

---

### 👤 Evaluación Individual - Backend Ingesta CSV (Cesar Reyes)

* **Nombre del Integrante:** Cesar Reyes  
* **Matrícula:** 2024-1308  
* **Rol en el Sprint:** Developer (Backend de Ingesta y Validación de Carga Masiva CSV - HU02)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Trabajar con el equipo me ayudó a entender mejor cómo se organiza un proyecto de software y la importancia de la comunicación constante. Durante el desarrollo del proyecto trabajé en la automatización de la carga masiva de datos y su integración backend. Aunque al principio tuvimos algunas dificultades de coordinación durante ciertas etapas del desarrollo, logramos alinearnos para sacar adelante la solución funcional."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Trabajar con Scrum permitió estructurar los objetivos del Sprint y entender cómo una necesidad real del usuario se convierte en una funcionalidad dentro de un sistema. El seguimiento de las Historias de Usuario dio una visión más clara del progreso, aunque al inicio fue un reto trasladar los requisitos descritos a la solución técnica."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Aprendí a automatizar la ingesta masiva de artículos mediante archivos CSV (HU02), evitando el registro manual uno por uno. También comprendí cómo procesar y sanitizar datos geoespaciales (coordenadas decimales) para que puedan ser consumidos correctamente por el sistema y utilizados para establecer un orden de recorrido."*
> * **A nivel metodológico:** *"Comprendí la importancia del procesamiento de datos en etapas tempranas y cómo la colaboración entre backend y QA es esencial para evitar la propagación de inconsistencias en el sistema."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"La principal dificultad fue comprender inicialmente cómo llevar los requisitos de las historias de usuario a la solución funcional del backend, además de resolver discrepancias en el formato de coordenadas decimales (comas vs. puntos) durante la lectura de los archivos CSV para garantizar una persistencia correcta."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Considero que el equipo pudo haber mejorado la organización y la comunicación interna durante las primeras etapas del desarrollo, definiendo contratos de datos claros antes de iniciar la programación."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint planificaría mejor las tareas desde el principio, aclararía las responsabilidades de cada integrante y realizaría las pruebas de integración con más anticipación para detectar y resolver posibles problemas antes de la entrega final."*

---

### 👤 Evaluación Individual - Secuenciación & Algoritmos (Jasuel De Los Santos)

* **Nombre del Integrante:** Jasuel De Los Santos  
* **Matrícula:** 2023-1983  
* **Rol en el Sprint:** Developer (Motor de Optimización y Secuenciación - HU03: Secuenciador Automático por Cercanía Lineal)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Fue una experiencia bastante colaborativa y coordinada. Al estar divididos en células funcionales, la comunicación con el equipo de backend e ingesta de datos fue constante para coordinar la lógica del algoritmo de secuenciación lineal y asegurar que el ordenamiento de paradas se integrara correctamente con los datos consumidos en la app del conductor y el panel web."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Muy dinámica y enfocada. Tener un Sprint corto nos obligó a priorizar lo que realmente aportaba valor al MVP. El seguimiento mediante el tablero de Jira y las reuniones rápidas permitieron que todos supiéramos cómo se calculaba la secuencia de paradas y cómo el servicio de optimización afectaba el flujo de las demás Historias de Usuario."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Aprendí a diseñar e implementar el algoritmo de ordenamiento por vecino más cercano (Nearest Neighbor) con cálculo de distancia por coordenadas geoespaciales, asegurando que los artículos cargados se ordenen de forma lineal desde el depósito base e inyectando enteros correlativos (1, 2, 3... N) a cada parada."*
> * **A nivel metodológico:** *"Aprendí la importancia de apegarse estrictamente a la Definition of Done (DoD) para no dar por terminado un servicio de optimización hasta haber validado los casos borde de coordenadas nulas o inconsistentes."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"El principal reto fue manejar la lógica matemática del vecino más cercano para controlar excepciones con coordenadas nulas o corruptas (como el formato de comas decimales provenientes de los CSV), garantizando que el ordenamiento asignara la secuencia correctamente en la base de datos sin detener la ejecución del sistema."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Estandarizar desde las primeras horas del Sprint la estructura y sanitización de las coordenadas geoespaciales entre la ingesta CSV y el algoritmo de secuenciación, para evitar pequeños reajustes de formato al momento de ejecutar la optimización."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"Implementaría pruebas unitarias automatizadas del algoritmo de secuenciación con diversos datasets geográficos desde el Día 1 antes de integrar el servicio con la base de datos y la interfaz gráfica."*

---

### 👤 Evaluación Individual - QA & Integración (Josteen Mayobanex Del Orbe)

* **Nombre del Integrante:** Josteen Mayobanex Del Orbe  
* **Matrícula:** 2024-0270  
* **Rol en el Sprint:** Developer QA / Integración  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia comenzó con dificultades debido a la desalineación inicial de 4 días en el equipo. Sin embargo, tras la Daily extraordinaria, logramos coordinarnos activamente para ejecutar las pruebas End-to-End del piloto."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Scrum nos dio la estructura para recuperar el tiempo perdido y asegurar que las 7 historias de usuario cumplieran la Definition of Done."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico y metodológico:** *"Aprendí a crear baterías de pruebas automatizadas con datasets piloto y a diagnosticar fallas de compatibilidad en la ingesta de datos CSV."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"El principal obstáculo fue la falta de comunicación inicial y la ausencia de un canal unificado para sincronizar las pruebas a tiempo. A nivel técnico, enfrentamos la corrupción de datos geográficos provocada por el uso de comas en lugar de puntos decimales en las coordenadas del archivo CSV."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Acordar canales oficiales de comunicación, estructuras de datos claras y dinámicas de trabajo conjunto desde el primer día, evitando asumir que la coordinación ocurriría de forma espontánea."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"Exigir contratos de datos firmados antes de programar y usar Dailies síncronas para resolver bloqueos en menos de 2 horas."*

---

### 👤 Evaluación Individual - Frontend Móvil Conductor (Luis Manuel Ortega Mejía)

* **Nombre del Integrante:** Luis Manuel Ortega Mejía  
* **Matrícula:** 2022-1134  
* **Rol en el Sprint:** Developer (Célula Frontend & App Móvil - HU04: Vista Móvil de Hoja de Ruta y Artículos Asignados)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Fue una experiencia muy fluida y colaborativa. Trabajar por células nos permitió estar completamente sincronizados. En mi caso, la comunicación constante con la célula de backend fue clave para asegurar que los endpoints de datos de las rutas y conductores se integraran correctamente con la vista móvil responsive que estaba desarrollando en el frontend."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Al ser un Sprint de solo 1 semana, la metodología Scrum nos dio una estructura sumamente ágil y orientada a resultados. El tablero de Jira fue fundamental para dar seguimiento visual al avance de las subtareas de la HU04 (SCRUM-54, SCRUM-55 y SCRUM-56), permitiendo identificar cuellos de botella a tiempo y entregar un MVP plenamente funcional."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Profundicé en el desarrollo de interfaces mobile-first adaptativas bajo el concepto Glassmorphic UPS Theme, manejando estados dinámicos mediante peticiones GET asíncronas para el selector de conductores y ordenamiento en cliente/servidor mediante LINQ (.OrderBy(s => s.Sequence))."*
> * **A nivel metodológico:** *"Reforcé el valor de la documentación rigurosa en Markdown dentro del proyecto y el cumplimiento de los Criterios de Aceptación para alinearse estrictamente con la Definition of Done (DoD)."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"El mayor desafío técnico estuvo en garantizar que la interfaz se auto-ajustara correctamente a diversas resoluciones de pantallas móviles (entre 375px y 768px) manteniendo un alto contraste para lectura en entornos con luz solar, además de estructurar la tarjeta informativa para que mostrara claramente toda la información crítica del paquete (secuencia, cliente, dirección y estado) sin saturar la pantalla."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Pudo haberse definido un conjunto global de estilos CSS y componentes UI compartidos desde el primer día del Sprint entre las distintas historias de la interfaz del conductor, evitando así pequeños ajustes de alineación y consistencia visual al momento de unir la vista de hoja de ruta con otras pantallas del flujo operativo."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"Aplicaría la buena práctica de realizar pruebas continuas en dispositivos móviles físicos desde las primeras etapas del maquetado (en lugar de confiar únicamente en el emulador del navegador) e implementaría Smart Commits en Git enlazados directamente a las subtareas de Jira desde el día uno para automatizar aún más el rastreo del código."*

---

### 👤 Evaluación Individual - Navegación GPS Móvil (Angel Gabriel Morillo Rosario)

* **Nombre del Integrante:** Angel Gabriel Morillo Rosario  
* **Matrícula:** 2023-0554  
* **Rol en el Sprint:** Developer (Integración Externa de Navegación por Enlaces Profundos a Google Maps Web - HU05)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia fue positiva en términos de aprendizaje, aunque marcada por los mismos retos de coordinación que afectaron a todo el equipo durante los primeros días del Sprint. Al enfocarme en una historia de usuario concreta (HU05, navegación GPS del conductor), pude mantener un avance relativamente autónomo, pero sentí la falta de sincronización con las áreas de backend y base de datos, especialmente en cuanto al formato en que llegarían las coordenadas de cada tarjeta. Una vez el Product Owner intervino para alinear expectativas, el trabajo en equipo mejoró notablemente y logramos integrar mi componente sin mayores fricciones."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Trabajar bajo Scrum me permitió entender el valor de dividir el trabajo en historias de usuario pequeñas y verificables. Al tener HU05 claramente delimitada (un solo story point, un objetivo específico: botón de navegación por tarjeta), pude enfocar mi esfuerzo sin dispersarme. También experimenté de primera mano cómo un Sprint corto de una semana exige que cualquier bloqueo se comunique de inmediato, ya que no hay margen de tiempo para resolver ambigüedades a mitad de camino."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Profundicé en la integración de funcionalidades de navegación externa dentro de una vista Razor (.cshtml), usando deep links de Google Maps con coordenadas en formato invariante (`CultureInfo.InvariantCulture`) y un mecanismo de respaldo por dirección (`Uri.EscapeDataString`) para los casos donde la geolocalización no estuviera disponible. También reforcé el uso de Bootstrap y Font Awesome para mantener consistencia visual dentro del módulo del conductor."*
> * **A nivel metodológico:** *"Aprendí que documentar bien una historia de usuario -incluyendo su criterio de aceptación y el responsable del ticket- facilita enormemente el trabajo individual dentro de un Sprint, incluso cuando el equipo tiene fricciones de coordinación."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"El principal bloqueo fue la incertidumbre inicial sobre el formato exacto en que llegarían las coordenadas (lat/lng) desde la capa de datos, lo cual me obligó a esperar la definición del contrato de datos antes de poder cerrar completamente la lógica del botón de navegación. También hubo cierta ambigüedad sobre qué hacer cuando una tarjeta de artículo no tuviera coordenadas válidas, lo que resolví implementando el fallback por dirección."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Creo que debimos definir el contrato de datos (formato de coordenadas, manejo de nulos) desde el Sprint Planning, en lugar de descubrirlo sobre la marcha durante el desarrollo. Esto habría evitado tiempo de espera innecesario para historias como la mía, que dependen directamente de cómo llega la información desde otras capas del sistema."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint propondría: (1) Definir y firmar los contratos de datos entre frontend y backend antes de iniciar la implementación de cualquier historia que dependa de ellos; (2) Reportar bloqueos de dependencia el mismo día que se detectan, sin esperar a la Daily; y (3) Documentar desde el inicio los casos borde (como datos faltantes o inválidos) como parte del criterio de aceptación."*

---

### 👤 Evaluación Individual - Frontend Administrativo Supervisor (Emil Hari Montilla Salvador)

* **Nombre del Integrante:** Emil Hari Montilla Salvador  
* **Matrícula:** 2022-0287  
* **Rol en el Sprint:** Frontend Administrativo del Panel de Control Consolidado del Supervisor (HU07)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia trabajando en equipo fue buena, aunque al principio tuvimos algunos problemas de comunicación y organización. Hubo varios días en los que no estaba muy claro qué tenía que hacer cada persona ni cómo se iban a conectar las diferentes partes del sistema, y eso nos atrasó bastante. Después de que nos organizamos mejor, pudimos dividir las responsabilidades y avanzar más rápido. En mi caso, trabajé principalmente en el frontend del panel administrativo junto con la persona encargada del backend, así que también tuve que coordinarme con él para saber qué datos iba a recibir y cómo los iba a mostrar en la tabla. Al final siento que logramos trabajar mucho mejor como equipo y completar el proyecto."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Trabajar con Scrum me ayudó a entender mejor cómo se puede organizar un proyecto entre varias personas. Algo que noté es que no basta con dividir las tareas, sino que también es importante estar comunicando constantemente qué se está haciendo y si existe algún problema. En nuestro caso, cuando empezamos a comunicarnos mejor y a tener más claras las historias de usuario y las responsabilidades de cada integrante, el trabajo comenzó a avanzar mucho más rápido. Creo que Scrum fue útil especialmente para detectar los bloqueos que teníamos y reorganizarnos antes de que fuera demasiado tarde."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Aprendí más sobre cómo se conecta el frontend con el resto de un sistema. Mi parte consistía en diseñar la tabla administrativa del supervisor, pero para poder mostrar la información correctamente dependía de los datos que venían del backend, como los artículos entregados, el kilometraje inicial y final y la distancia recorrida. Eso me ayudó a entender mejor que cada parte de un proyecto está relacionada con las demás."*
> * **A nivel metodológico:** *"Aprendí que es muy importante tener claras las tareas y los criterios de aceptación desde el principio. Cuando cada integrante sabe exactamente qué tiene que entregar y cómo se conecta su trabajo con el de los demás, se evitan muchos problemas y retrabajos."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"La principal dificultad fue que mi parte dependía de información que tenía que venir de otras partes del sistema. Al principio, como todavía no estaba completamente definida la estructura de los datos y hubo problemas de comunicación entre el equipo, se hacía difícil saber exactamente cómo debía organizar la tabla y qué información iba a recibir desde el backend. Después de coordinarnos mejor y definir los campos que utilizaríamos, pude tener mucho más claro cómo debía funcionar el panel administrativo."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Creo que lo principal que pudo hacerse mejor fue la comunicación desde el inicio. Debimos definir desde el primer día las responsabilidades de cada integrante y explicar claramente qué necesitaba cada módulo de los demás. De esa manera, las personas encargadas del frontend no tendrían que esperar para saber qué datos recibirían y el backend también tendría claro qué información debía proporcionar. Considero que una mejor organización desde el comienzo nos habría evitado gran parte del atraso que tuvimos."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint trataría de tener una comunicación más constante desde el primer día. También definiría desde el inicio cómo se van a conectar el frontend, el backend y la base de datos, para que cada persona pueda avanzar sin tener que esperar a que otra parte esté completamente terminada. Otra cosa que aplicaría sería informar los bloqueos lo antes posible. Si alguien tiene una duda o necesita algo de otro integrante, es mejor comunicarlo de una vez y buscar una solución entre todos en lugar de dejar pasar varios días. Creo que con eso el equipo podría trabajar de una manera mucho más organizada y aprovechar mejor el tiempo del Sprint."*

---

### 👤 Evaluación Individual - Odómetros & QA Integración (Jostin Wilmer Perez Santana)

* **Nombre del Integrante:** Jostin Wilmer Perez Santana  
* **Matrícula:** 2022-1096  
* **Rol en el Sprint:** Captura de Métricas en Campo y Validación Numérica de Odómetros (HU06) / Developer QA & Integración  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Al principio fue un poco complicado porque hubo falta de comunicación y varias partes del sistema dependían unas de otras. Después de organizarnos mejor en la Daily extraordinaria, pudimos coordinarnos, revisar el flujo completo del sistema y validar las funcionalidades antes de cerrar el Sprint."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Trabajar con Scrum nos ayudó a dividir el proyecto en historias de usuario y tener claro qué debía cumplir cada una. También nos permitió identificar los bloqueos, reorganizarnos cuando surgieron retrasos y verificar que las funcionalidades llegaran a cumplir con la Definition of Done."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico y metodológico:** *"Aprendí la importancia de probar una funcionalidad no solamente de forma individual, sino también integrada con el resto del sistema. En mi caso pude entender mejor las validaciones del flujo del conductor, como registrar el odómetro inicial, completar las entregas, validar el odómetro final y comprobar que la distancia recorrida se calculara correctamente. También aprendí cómo las pruebas End-to-End ayudan a comprobar un proceso completo."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"Una de las principales dificultades fue que al inicio no estaban completamente definidos algunos detalles del flujo, por ejemplo cómo se manejaría el registro del odómetro y cómo se conectarían los datos entre los diferentes módulos. Además, como equipo tuvimos problemas de comunicación durante los primeros días. Luego se aclararon los criterios y pudimos validar el flujo completo, incluyendo que el kilometraje final no fuera menor que el inicial."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Considero que desde el primer día debimos tener una comunicación más constante y definir mejor las dependencias entre cada historia de usuario. Eso habría permitido que desarrollo y QA trabajaran de forma más coordinada y que las pruebas se fueran realizando a medida que se terminaba cada funcionalidad, en vez de acumular trabajo para el final."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint trataría de involucrar QA desde el inicio, preparar los casos de prueba tomando como base los criterios de aceptación y probar cada historia tan pronto esté disponible. También mantendría las Dailies cortas y constantes para comunicar cualquier bloqueo rápidamente y evitar que un problema pequeño termine retrasando al resto del equipo."*

---

### 👤 Evaluación Individual - Backend Analítica & Reportes (Jhois Enmanuel Collado Fulcar)

* **Nombre del Integrante:** Jhois Enmanuel Collado Fulcar  
* **Matrícula:** 2021-1124  
* **Rol en el Sprint:** Backend de Analítica, Cálculos Automáticos de Kilómetros y Exportador CSV (HU07)  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia trabajando en equipo fue positiva. Pudimos dividir las responsabilidades según las tareas de cada integrante y colaborar para completar la historia de usuario. También fue importante mantener la comunicación para poder integrar correctamente el trabajo de cada uno."*

#### 2. ¿Cómo fue trabajar utilizando Scrum?
> *"Trabajar con Scrum ayudó a organizar las tareas y establecer objetivos claros durante el Sprint. Las historias de usuario y los criterios de aceptación permitieron tener una mejor idea de lo que debíamos completar."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico y metodológico:** *"Durante este Sprint aprendí principalmente la importancia de la organización y la comunicación dentro de un equipo. Al tener un tiempo limitado, fue necesario priorizar las tareas y concentrarse en cumplir los objetivos principales. También comprendí que dividir correctamente las responsabilidades facilita el trabajo, pero es necesario mantener una buena coordinación para que todas las partes del proyecto funcionen juntas."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"La principal dificultad fue el tiempo limitado del Sprint. También surgieron algunos momentos en los que era necesario coordinar el avance de las diferentes partes del proyecto para evitar retrasos."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Considero que se pudo mejorar la planificación inicial y tener una comunicación más constante sobre el progreso de cada tarea. Esto habría permitido identificar posibles problemas con mayor anticipación."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"En un próximo Sprint organizaría mejor las tareas desde el inicio y definiría con más claridad las responsabilidades y prioridades. También realizaría revisiones más frecuentes durante el desarrollo, en lugar de esperar hasta el final para revisar el trabajo completo. Creo que estas prácticas ayudarían a evitar bloqueos, mejorar la coordinación del equipo y aprovechar mejor el tiempo disponible."*

---

### 👤 Evaluación Individual - QA Engineer & Simulación Piloto (Yassil Del Orbe)

* **Nombre del Integrante:** Yassil Del Orbe  
* **Matrícula:** 2024-2536  
* **Rol en el Sprint:** QA Engineer / Pruebas de Carga e Integración de Datos, Simulación de Campo de los 5 Conductores Piloto  
* **Fecha de Evaluación:** 19 de Agosto de 2026  

#### 1. ¿Cómo fue su experiencia trabajando en equipo?
> *"Mi experiencia fue exigente pero enriquecedora. Al inicio del Sprint enfrentamos una desalineación de 4 días que afectó directamente mi trabajo, ya que no podía ejecutar pruebas de integración reales sin que backend y frontend tuvieran sus endpoints estables, una vez que el equipo se alineó, la coordinación mejoró notablemente y pude trabajar de la mano con Josteen para validar el flujo completo del sistema."*

#### 2. ¿Cómo fue trabajar utilizando la metodología Scrum?
> *"Scrum fue clave para poder simular en condiciones controladas el comportamiento real de los 5 conductores piloto en un tiempo tan corto. El tablero de Jira me permitió dar seguimiento a cada Historia de Usuario (HU01 a HU07) para saber exactamente cuándo cada módulo estaba listo para pruebas de integración y las Dailies nos ayudaron a detectar rápidamente inconsistencias entre los datos generados por AddressImportService y lo que consumía la vista del conductor."*

#### 3. ¿Qué aprendió a nivel técnico o metodológico durante este Sprint de 1 semana?
> * **A nivel técnico:** *"Aprendí a diseñar y ejecutar pruebas de carga e integración sobre una arquitectura ASP.NET Core MVC con Entity Framework Core y SQLite, validando que el flujo de datos entre AddressImportService (ingesta CSV) y NearestNeighborOptimizerService (secuenciación) se mantuviera íntegro extremo a extremo. También simulé el comportamiento concurrente de los 5 conductores piloto navegando entre las vistas /Dispatch, /Driver y /Supervisor."*
> * **A nivel metodológico:** *"Entendí la importancia de tener un ambiente de pruebas y un dataset piloto (15 artículos urbanos en Santo Domingo) listos desde el inicio del Sprint, para no depender de que otros módulos estuvieran terminados para empezar a validar."*

#### 4. ¿Qué dificultades o bloqueos encontró en el desarrollo de sus tareas?
> *"El principal bloqueo fue la falta de un canal centralizado de comunicación durante los primeros días, lo que retrasó la definición de los casos de prueba. A nivel técnico, tuve que trabajar junto a Cesar Reyes para resolver la corrupción de coordenadas geográficas por el uso de comas en vez de puntos decimales en el CSV, y verificar que la sanitización con CultureInfo.InvariantCulture no afectara la persistencia de datos en la capa de EF Core."*

#### 5. ¿Qué considera que pudo haberse realizado mejor en la organización del equipo?
> *"Debimos establecer desde el Día 1 un ambiente de pruebas y contratos de datos claros entre los módulos, en lugar de esperar a que cada Historia de Usuario estuviera 'terminada' para empezar a integrar, eso habría permitido detectar antes el problema del formato decimal en el CSV."*

#### 6. ¿Qué haría diferente o qué buenas prácticas aplicaría en un próximo Sprint?
> *"Implementaría pruebas de integración continuas desde el primer día (no solo al final del Sprint), automatizaría la validación del dataset piloto antes de cada carga masiva, y establecería checkpoints diarios de QA con backend para detectar errores de formato o persistencia en menos de 2 horas, en línea con la acción de mejora acordada por el equipo."*


