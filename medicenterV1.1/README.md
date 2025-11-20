# MEDICENTER – Sistema de Gestión Médica en Consola

Proyecto de consola en C# que simula un centro médico sencillo. Todo se ejecuta en memoria, usando solo estructuras básicas (variables, condicionales, bucles, arreglos, listas, colas, árboles simples, grafos simples y una IA tipo Naive Bayes).

No usa archivos, ni bases de datos, ni interfaz gráfica.

---

## 1. Cómo ejecutar el programa

1. Abrir una terminal en la carpeta del proyecto (donde está `MEDICENTER.csproj`).
2. Ejecutar:

```bash path=null start=null
dotnet run
```

El programa mostrará el menú principal en la consola.

### Credenciales de prueba

Estos usuarios ya existen al iniciar el sistema:

- Paciente:
  - ID: `P001`
  - Password: `pass123`
- Doctor:
  - ID: `D001`
  - Password: `doc123`

---

## 2. Flujo general del programa

El punto de entrada está en `Program.cs`. Allí se crea un objeto `Menu` y se llama al menú principal.

### Menú principal (`Menu.cs`)

Opciones:

1. Iniciar sesión
2. Registrar usuario (Paciente o Doctor)
0. Salir
Al iniciar sesión:

- Si el ID empieza por `P` → se trata como Paciente.
- Si el ID empieza por `D` → se trata como Doctor.

Según el tipo de usuario se muestra el menú correspondiente.

### Menú de Paciente (resumen)

Opciones principales para el paciente:

1. Ver / actualizar perfil (muestra ID, Nombre, Cédula, si tiene seguro médico, contacto de emergencia).
2. Ingresar síntomas (puede elegir de una lista de síntomas frecuentes o escribirlos a mano).
3. Obtener diagnóstico IA (usa Naive Bayes casero y luego hace unas pocas preguntas adicionales con un árbol de decisión sencillo).
4. Ver historial (muestra los registros R0001, R0002… con síntomas, diagnóstico y estado confirmado o no).
5. Solicitar consulta al doctor (elige un registro y escribe un mensaje que queda guardado para el doctor).
6. (beta) Comparar clínicas (muestra solo el mensaje de versión beta).
0. Cerrar sesión.

Cuando la IA encuentra síntomas que no existen en su base de conocimientos, muestra exactamente este mensaje:

"Alerta: Algunos síntomas no se encuentran en nuestra base. Se recomienda consultar directamente con un doctor."

Y en ese momento el paciente puede enviar una solicitud de consulta al doctor con un mensaje detallado.

### Menú de Doctor (resumen)

Opciones principales para el doctor:

1. Revisar consultas pendientes (cola de solicitudes enviadas por los pacientes).
2. Validar / corregir diagnósticos automáticos (puede confirmar el diagnóstico sugerido o escribir uno nuevo; los casos confirmados actualizan la base de frecuencias de la IA).
3. Gestionar historial médico del paciente (buscar por ID o cédula y ver / eliminar registros en memoria).
4. Ver estadísticas IA (muestra por pantalla cuántas enfermedades se conocen y la frecuencia aproximada de cada síntoma por enfermedad).
5. Actualizar base de conocimientos IA (actualmente muestra mensaje de versión beta).
6. (beta) Clínica (muestra mensaje de versión beta).
7. Entrenar IA con casos confirmados (beta) — el entrenamiento real se hace automáticamente al confirmar diagnósticos, esta opción solo muestra el mensaje de versión beta.
8. Ver información personal del doctor.
0. Cerrar sesión.

---

## 3. Archivos y responsabilidades

### `Program.cs`

- Punto de entrada `Main` del programa.
- Crea una instancia de `Menu`.
- Llama a `MostrarMenuPrincipal()`.

**Temas de programación**

- Método `Main`.
- Creación de objetos.

---

### `Menu.cs`

- Controla todos los menús del sistema (único menú).
- Trabaja con una instancia de `Sistema`.
- Maneja la navegación de:
  - Menú principal.
  - Menú de Paciente.
  - Menú de Doctor.

**Temas de programación**

- Condicionales `if` / `else`.
- `switch` para opciones de menú.
- Bucles `do/while` para mantener el menú activo.
- Lectura de datos por consola (`Console.ReadLine`).
- Llamadas a métodos de otras clases (`Sistema`, `Paciente`, `Doctor`).

**Variables importantes**

- `sistema` → instancia única de `Sistema` que centraliza toda la lógica.

---

### `Sistema.cs`

Clase principal que contiene la lógica del sistema médico.

**Responsabilidades principales**

- Manejo de usuarios:
  - Listas de `Paciente` y `Doctor`.
  - Registro de nuevos pacientes y doctores.
  - Búsqueda de usuarios por ID y password.
- Manejo de síntomas y registros médicos:
  - Ingresar síntomas (desde lista de síntomas frecuentes o manualmente).
  - Crear y almacenar objetos `RegistroMedico` en el historial del paciente.
- Diagnóstico automático:
  - Árbol de decisión con `DecisionNode`.
  - IA Naive Bayes con `SistemaIA`.
- Manejo de cola de pacientes para consulta con doctor (`Queue<string>`).
- Estadísticas generales (número de pacientes, doctores, registros, etc.).
- Comparación de clínicas usando una matriz `int[ , ]`.
- Uso de un grafo simple de síntomas relacionados (diccionario de vecinos).

**Estructuras usadas**

- `List<Paciente> pacientes`
- `List<Doctor> doctores`
- `Queue<string> colaPacientes`
- `DecisionNode arbolDiagnosticoRoot`
- `string[] sintomasFrecuentes`
- `int[,] statsClinica`
- `Dictionary<string, List<string>> grafoSintomas`
- Contadores `contadorPacientes`, `contadorDoctores`, `contadorRegistros`.
- Motor de IA: `SistemaIA sistemaIA`.

**Métodos importantes**

- Inicialización:
  - `InicializarDatosPrueba()` → crea P001/pass123 y D001/doc123.
  - `InicializarGrafoSintomas()` → construye un grafo simple de síntomas relacionados.
  - `InicializarArbolDiagnostico()` → crea el árbol de decisión de diagnósticos.
  - `EntrenarIAConDatosIniciales()` → crea registros simulados y entrena la IA.

- Usuarios:
  - `RegistrarPaciente()` / `RegistrarDoctor()`.
  - `BuscarPaciente(id, password)`.
  - `BuscarDoctor(id, password)`.

- Síntomas e historial:
  - `IngresarSintomas(Paciente paciente)` → crea un `RegistroMedico` y rellena `Sintomas`.
  - `MostrarHistorial(Paciente paciente)` → recorre la lista `Historial`.

- Diagnósticos:
  - `ObtenerDiagnosticoInteractivo(Paciente paciente)` → usa el árbol de decisión (`DecisionNode` + `Hijos`).
  - `ObtenerDiagnosticoIA(Paciente paciente)` → usa la IA Naive Bayes (`SistemaIA`).

- Cola de pacientes:
  - `SolicitarConsultaDoctor(Paciente paciente)` → encola una clave `PacienteID|RegistroID`.
  - `RevisarColaPacientes()` → muestra la cola sin desencolar.
  - `ValidarDiagnosticos(Doctor doctor)` → toma un paciente de la cola y permite confirmar o modificar un diagnóstico.

- Estadísticas:
  - `VerEstadisticas()` → general (pacientes, doctores, registros, confirmados, cola).
  - `CompararClinicas()` → muestra datos de `statsClinica` (matriz 2D).
  - `MostrarSintomasRelacionados(RegistroMedico registro)` → recorre el grafo de síntomas.
  - `MostrarEstadisticasIA()` → llama a `SistemaIA.MostrarEstadisticas()`.

Todo está implementado con:

- `if`, `else`, `switch`.
- `for`, `while`, `do/while`, `foreach`.
- Arreglos, listas, colas, diccionarios.

No hay propiedades automáticas ni acceso a archivos.

---

### `Usuario.cs`

- Clase base para todos los usuarios.

**Variables principales**

- `Id`, `Nombre`, `Email`, `Password` (todas `string`).

Se usa como base para `Paciente` y `Doctor` mediante herencia simple.

---

### `Paciente.cs`

- Clase que representa a un paciente, derivada de `Usuario`.

**Variables principales**

- `Edad` (int)
- `ContactoEmergencia` (string)
- `Historial` (`List<RegistroMedico>`) → lista de consultas del paciente.

**Métodos**

- `MostrarInformacion()` → imprime los datos y el número de registros en el historial.

---

### `Doctor.cs`

- Clase que representa a un doctor, derivada de `Usuario`.

**Variables principales**

- `Especialidad` (string)
- `PacientesAsignados` (`List<string>`) → IDs de pacientes atendidos.

**Métodos**

- `MostrarInformacion()` → imprime los datos del doctor y cuántos pacientes ha atendido.

---

### `RegistroMedico.cs`

- Clase que representa un registro médico de un paciente.

**Variables principales**

- `IdRegistro` (string)
- `Fecha` (DateTime)
- `Sintomas` (`List<string>`)
- `Diagnostico` (string)
- `Confirmado` (bool)
- `ObservacionDoctor` (string)

**Método**

- `MostrarRegistro()` → imprime toda la información del registro, incluyendo síntomas y observaciones.

---

### `DecisionNode.cs`

- Nodo del árbol de decisión usado en el diagnóstico automático.

**Variables principales**

- `Id` (string)
- `Pregunta` (string)
- `Diagnostico` (string, si es hoja)
- `Hijos` (`List<DecisionNode>`)
- `RespuestaEsperada` (string) → normalmente "si" o "no" para navegar en el árbol.

**Métodos**

- `EsHoja()` → indica si el nodo es un diagnóstico final.
- `AgregarHijo(DecisionNode hijo)` → arma el árbol agregando hijos a un nodo.

Este árbol se recorre en `Sistema.ObtenerDiagnosticoInteractivo`.

---

### `SistemaIA.cs`

Implementa una IA muy simple basada en Naive Bayes.

**Estructuras internas**

- `Dictionary<string, Dictionary<string, int>> baseEnfermedades`
  - Clave: nombre de la enfermedad (string, en minúsculas).
  - Valor: diccionario `sintoma -> frecuencia`.
- `Dictionary<string, int> totalCasosPorEnfermedad` → cuántos registros confirmados hay por enfermedad.
- `int totalEntrenamientos` → cuántos registros se usaron para entrenar.

**Métodos principales**

- `Entrenar(RegistroMedico registro)`
  - Usa `registro.Confirmado`, `registro.Sintomas` y `registro.Diagnostico`.
  - Solo entrena si el registro está confirmado por un doctor.
  - Suma frecuencias de síntomas por enfermedad.

- `Predecir(List<string> sintomasPaciente)`
  - Normaliza los síntomas (minúsculas, sin espacios extra).
  - Calcula probabilidades `P(Síntomas | Enfermedad)` con suavizado.
  - Devuelve un `ResultadoIA` con:
    - `diagnostico` sugerido.
    - `confianza` (float entre 0 y 1).
    - `mensaje` explicativo.

- `MostrarEstadisticas()`
  - Muestra:
    - Total de entrenamientos.
    - Número de enfermedades aprendidas.
    - Para cada enfermedad, los síntomas más frecuentes y su porcentaje.

- `ObtenerTotalEntrenamientos()` y `ObtenerNumeroEnfermedades()` → para estadísticas.

### `ResultadoIA`

- Estructura simple para guardar resultado de la predicción.

**Variables**

- `diagnostico` (string)
- `confianza` (float)
- `mensaje` (string)

**Método**

- `Mostrar()` → imprime el diagnóstico, el porcentaje de confianza y el nivel (ALTA / MEDIA / BAJA).

---

## 4. Resumen de temas de programación usados

En este proyecto se usan solo los temas básicos que mencionaste:

- Variables y tipos de datos básicos (`int`, `string`, `bool`, `float`, `DateTime`).
- Condicionales:
  - `if`, `else if`, `else`.
  - `switch` para menús y opciones.
- Bucles:
  - `for`, `while`, `do/while`, `foreach`.
- Arreglos:
  - 1D (`string[]` de síntomas).
  - 2D (`int[,]` para estadísticas de clínicas).
- Estructuras de datos dinámicas:
  - `List<T>` para listas de pacientes, doctores, registros y síntomas.
  - `Queue<T>` para cola de pacientes.
  - Árbol n‑ario (`DecisionNode` con `List<DecisionNode>`).
  - Grafo simple (`Dictionary<string, List<string>>` de síntomas relacionados).
- Métodos normales (sin propiedades) en todas las clases.
- Comentarios con `//` explicando:
  - De qué se trata cada clase.
  - Qué hace cada método.
  - Para qué sirve cada variable importante.
- Solo versión consola, todo en memoria:
  - No hay lectura/escritura de archivos.
  - No hay bases de datos.

Con este README tenés una guía única del proyecto, de cómo usarlo y de qué tema de programación se muestra en cada parte del código.