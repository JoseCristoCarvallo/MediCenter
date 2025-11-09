using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Clase controladora principal
    // TEMA: Listas dinámicas, Colas, Árboles, Grafos, Arreglos
    // Clase Sistema que controla toda la lógica del programa MEDICENTER
    public class Sistema
    {
        // TEMA: Listas dinámicas - List<T> para almacenar pacientes y doctores
        public List<Paciente> pacientes;
        public List<Doctor> doctores;
        
        // TEMA: Colas - Queue<T> para manejar cola de pacientes
        public Queue<string> colaPacientes;
        
        // TEMA: Árboles binarios - Nodo raíz del árbol de decisión
        public DecisionNode arbolDiagnosticoRoot;
        
        // TEMA: Arreglos unidimensionales - Arreglo de síntomas frecuentes
        public string[] sintomasFrecuentes;
        
        // TEMA: Arreglos bidimensionales - Matriz para estadísticas de clínicas
        // Filas: clínicas, Columnas: [precisión%, tiempo promedio minutos]
        public int[,] statsClinica;
        
        // TEMA: Grafos simples - Diccionario para representar relaciones entre síntomas
        public Dictionary<string, List<string>> grafoSintomas;
        
        // TEMA: Variables - Contador para generar IDs únicos
        public int contadorPacientes;
        public int contadorDoctores;
        public int contadorRegistros;

        // TEMA: Métodos - Constructor
        // Constructor del Sistema - Inicializa todas las estructuras de datos
        public Sistema()
        {
            // TEMA: Listas dinámicas - Inicialización de listas
            pacientes = new List<Paciente>();
            doctores = new List<Doctor>();
            
            // TEMA: Colas - Inicialización de cola
            colaPacientes = new Queue<string>();
            
            // TEMA: Variables - Inicialización de contadores
            contadorPacientes = 1;
            contadorDoctores = 1;
            contadorRegistros = 1;
            
            // TEMA: Arreglos unidimensionales - Inicialización y asignación de valores
            sintomasFrecuentes = new string[]
            {
                "Fiebre",
                "Tos",
                "Dolor de cabeza",
                "Dolor de garganta",
                "Fatiga",
                "Nauseas",
                "Dolor abdominal",
                "Dificultad para respirar",
                "Mareos",
                "Dolor muscular"
            };
            
            // TEMA: Arreglos bidimensionales - Inicialización de matriz 4x2
            statsClinica = new int[4, 2]
            {
                { 85, 45 },  // Clínica 1
                { 92, 30 },  // Clínica 2
                { 78, 60 },  // Clínica 3
                { 88, 40 }   // Clínica 4
            };
            
            // TEMA: Grafos simples - Inicialización del grafo de síntomas
            grafoSintomas = new Dictionary<string, List<string>>();
            InicializarGrafoSintomas();
            
            // Inicializar datos de prueba
            InicializarDatosPrueba();
            
            // TEMA: Árboles binarios - Construcción del árbol de diagnóstico
            InicializarArbolDiagnostico();
        }

        // TEMA: Métodos - Inicializar datos de prueba
        // Crea usuarios por defecto: Paciente P001/pass123 y Doctor D001/doc123
        public void InicializarDatosPrueba()
        {
            // TEMA: Programación Orientada a Objetos - Creación de objetos
            Paciente pacienteTest = new Paciente(
                "P001",
                "Paciente Test",
                "paciente@test.com",
                "pass123",
                30,
                "118 - Policia Nacional de Nicaragua"
            );
            
            // TEMA: Listas dinámicas - Agregar elemento
            pacientes.Add(pacienteTest);
            contadorPacientes = 2;

            Doctor doctorTest = new Doctor(
                "D001",
                "Doctor Test",
                "doctor@test.com",
                "doc123",
                "Medicina General"
            );
            
            doctores.Add(doctorTest);
            contadorDoctores = 2;
        }

        // TEMA: Grafos simples - Inicialización del grafo de relaciones entre síntomas
        // Inicializa el grafo que relaciona síntomas entre sí
        public void InicializarGrafoSintomas()
        {
            // TEMA: Grafos simples - Agregar nodos y aristas al grafo
            grafoSintomas.Add("Fiebre", new List<string> { "Dolor de cabeza", "Fatiga", "Dolor muscular" });
            grafoSintomas.Add("Tos", new List<string> { "Dolor de garganta", "Dificultad para respirar" });
            grafoSintomas.Add("Dolor de cabeza", new List<string> { "Fiebre", "Mareos", "Fatiga" });
            grafoSintomas.Add("Nauseas", new List<string> { "Dolor abdominal", "Mareos" });
            grafoSintomas.Add("Dificultad para respirar", new List<string> { "Tos", "Fatiga" });
        }

        // TEMA: Árboles binarios - Construcción del árbol de decisión para diagnósticos
        // Construye el árbol de decisión con preguntas y diagnósticos
        public void InicializarArbolDiagnostico()
        {
            // TEMA: Árboles - Crear nodo raíz
            arbolDiagnosticoRoot = new DecisionNode("root", "Tiene fiebre?");
            
            // TEMA: Árboles - Crear ramas del árbol (respuesta SI a fiebre)
            DecisionNode nodoFiebreSi = new DecisionNode("fiebre_si", "Tiene tos persistente?");
            nodoFiebreSi.RespuestaEsperada = "si";
            
            // TEMA: Árboles - Subramas (fiebre SI + tos SI)
            DecisionNode nodoTosSi = new DecisionNode("tos_si", "Tiene dificultad para respirar?");
            nodoTosSi.RespuestaEsperada = "si";
            
            // TEMA: Árboles - Nodos hoja con diagnósticos
            DecisionNode diagnostico1 = new DecisionNode("diag1", "CRITICO - Posible Neumonia o COVID-19. CONSULTE DOCTOR INMEDIATAMENTE", true);
            diagnostico1.RespuestaEsperada = "si";
            
            DecisionNode diagnostico2 = new DecisionNode("diag2", "Posible Gripe o Resfriado Fuerte. Reposo y monitoreo", true);
            diagnostico2.RespuestaEsperada = "no";
            
            // TEMA: Árboles - Agregar hijos al nodo
            nodoTosSi.AgregarHijo(diagnostico1);
            nodoTosSi.AgregarHijo(diagnostico2);
            
            // TEMA: Árboles - Subramas (fiebre SI + tos NO)
            DecisionNode nodoTosNo = new DecisionNode("tos_no", "Tiene dolor de cabeza intenso?");
            nodoTosNo.RespuestaEsperada = "no";
            
            DecisionNode diagnostico3 = new DecisionNode("diag3", "Posible Migrana o Infeccion. Consulte medico", true);
            diagnostico3.RespuestaEsperada = "si";
            
            DecisionNode diagnostico4 = new DecisionNode("diag4", "Fiebre leve. Reposo y medicamento basico", true);
            diagnostico4.RespuestaEsperada = "no";
            
            nodoTosNo.AgregarHijo(diagnostico3);
            nodoTosNo.AgregarHijo(diagnostico4);
            
            nodoFiebreSi.AgregarHijo(nodoTosSi);
            nodoFiebreSi.AgregarHijo(nodoTosNo);
            
            // TEMA: Árboles - Crear ramas (respuesta NO a fiebre)
            DecisionNode nodoFiebreNo = new DecisionNode("fiebre_no", "Tiene nauseas o dolor abdominal?");
            nodoFiebreNo.RespuestaEsperada = "no";
            
            DecisionNode diagnostico5 = new DecisionNode("diag5", "Posible problema gastrointestinal. Dieta ligera", true);
            diagnostico5.RespuestaEsperada = "si";
            
            DecisionNode diagnostico6 = new DecisionNode("diag6", "Sintomas leves. Monitoreo general", true);
            diagnostico6.RespuestaEsperada = "no";
            
            nodoFiebreNo.AgregarHijo(diagnostico5);
            nodoFiebreNo.AgregarHijo(diagnostico6);
            
            // TEMA: Árboles - Conectar ramas principales con la raíz
            arbolDiagnosticoRoot.AgregarHijo(nodoFiebreSi);
            arbolDiagnosticoRoot.AgregarHijo(nodoFiebreNo);
        }

        // TEMA: Métodos - Registrar nuevo paciente
        // Registra un nuevo paciente en el sistema
        public void RegistrarPaciente()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      REGISTRO DE NUEVO PACIENTE");
            Console.WriteLine("========================================");
            
            // TEMA: Variables - Declaración y asignación
            // TEMA: Manejo de cadenas de texto - Lectura de entrada
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();
            
            Console.Write("Email: ");
            string email = Console.ReadLine();
            
            Console.Write("Password: ");
            string password = Console.ReadLine();
            
            // TEMA: Variables - Conversión de tipos con validación
            Console.Write("Edad: ");
            int edad = 0;
            
            // TEMA: Condicionales - Validación de entrada con TryParse
            if (!int.TryParse(Console.ReadLine(), out edad))
            {
                Console.WriteLine("\nEdad invalida. Registro cancelado.");
                return;
            }
            
            Console.Write("Contacto de emergencia (ej: 118 - Policia): ");
            string contacto = Console.ReadLine();
            
            // TEMA: Manejo de cadenas de texto - Formateo de ID con PadLeft
            string id = "P" + contadorPacientes.ToString().PadLeft(3, '0');
            contadorPacientes++;
            
            // TEMA: Programación Orientada a Objetos - Creación de objeto
            Paciente nuevoPaciente = new Paciente(id, nombre, email, password, edad, contacto);
            
            // TEMA: Listas dinámicas - Agregar elemento
            pacientes.Add(nuevoPaciente);
            
            Console.WriteLine("\nPaciente registrado exitosamente!");
            Console.WriteLine("Su ID es: " + id);
            Console.WriteLine("Guarde su ID y password para iniciar sesion.");
        }

        // TEMA: Métodos - Registrar nuevo doctor
        // Registra un nuevo doctor en el sistema
        public void RegistrarDoctor()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("       REGISTRO DE NUEVO DOCTOR");
            Console.WriteLine("========================================");
            
            // TEMA: Variables y Manejo de cadenas de texto
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();
            
            Console.Write("Email: ");
            string email = Console.ReadLine();
            
            Console.Write("Password: ");
            string password = Console.ReadLine();
            
            Console.Write("Especialidad: ");
            string especialidad = Console.ReadLine();
            
            // TEMA: Manejo de cadenas de texto - Formateo de ID
            string id = "D" + contadorDoctores.ToString().PadLeft(3, '0');
            contadorDoctores++;
            
            // TEMA: Programación Orientada a Objetos - Creación de objeto
            Doctor nuevoDoctor = new Doctor(id, nombre, email, password, especialidad);
            
            // TEMA: Listas dinámicas - Agregar elemento
            doctores.Add(nuevoDoctor);
            
            Console.WriteLine("\nDoctor registrado exitosamente!");
            Console.WriteLine("Su ID es: " + id);
            Console.WriteLine("Guarde su ID y password para iniciar sesion.");
        }

        // TEMA: Métodos - Buscar paciente por ID y password
        // Busca un paciente en la lista y valida credenciales
        public Paciente BuscarPaciente(string id, string password)
        {
            // TEMA: Bucles - foreach para recorrer lista
            foreach (Paciente p in pacientes)
            {
                // TEMA: Condicionales - Validación de credenciales
                if (p.Id == id && p.Password == password)
                {
                    return p;
                }
            }
            return null;
        }

        // TEMA: Métodos - Buscar doctor por ID y password
        // Busca un doctor en la lista y valida credenciales
        public Doctor BuscarDoctor(string id, string password)
        {
            // TEMA: Bucles - foreach para recorrer lista
            foreach (Doctor d in doctores)
            {
                if (d.Id == id && d.Password == password)
                {
                    return d;
                }
            }
            return null;
        }

        // TEMA: Métodos - Ingresar síntomas del paciente
        // TEMA: Listas dinámicas - Agregar elementos dinámicamente
        // TEMA: Arreglos unidimensionales - Uso del arreglo de síntomas frecuentes
        // Permite al paciente ingresar sus síntomas
        public void IngresarSintomas(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("        INGRESAR SINTOMAS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Seleccionar de sintomas frecuentes");
            Console.WriteLine("2. Escribir sintomas manualmente");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");
            
            string opcion = Console.ReadLine();
            
            // TEMA: Programación Orientada a Objetos - Crear nuevo registro
            RegistroMedico nuevoRegistro = new RegistroMedico();
            nuevoRegistro.IdRegistro = "R" + contadorRegistros.ToString().PadLeft(4, '0');
            contadorRegistros++;
            
            // TEMA: Condicionales - Switch
            switch (opcion)
            {
                case "1":
                    // TEMA: Arreglos unidimensionales - Recorrer arreglo con for
                    Console.WriteLine("\nSintomas frecuentes:");
                    
                    // TEMA: Bucles - for para recorrer arreglo
                    for (int i = 0; i < sintomasFrecuentes.Length; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + sintomasFrecuentes[i]);
                    }
                    
                    Console.WriteLine("\nIngrese los numeros de sintomas separados por coma (ej: 1,3,5):");
                    Console.Write("Sintomas: ");
                    string entrada = Console.ReadLine();
                    
                    // TEMA: Manejo de cadenas de texto - Split para separar cadena
                    string[] numeros = entrada.Split(',');
                    
                    // TEMA: Bucles - foreach para recorrer arreglo
                    foreach (string num in numeros)
                    {
                        // TEMA: Variables - Conversión de tipos
                        int indice;
                        if (int.TryParse(num.Trim(), out indice))
                        {
                            // TEMA: Condicionales - Validar índice
                            if (indice >= 1 && indice <= sintomasFrecuentes.Length)
                            {
                                // TEMA: Arreglos - Acceso por índice
                                // TEMA: Listas dinámicas - Agregar a lista
                                nuevoRegistro.Sintomas.Add(sintomasFrecuentes[indice - 1]);
                            }
                        }
                    }
                    break;
                    
                case "2":
                    Console.WriteLine("\nEscriba sus sintomas separados por coma:");
                    Console.Write("Sintomas: ");
                    string sintomasTexto = Console.ReadLine();
                    
                    // TEMA: Manejo de cadenas de texto - Split
                    string[] sintomasArray = sintomasTexto.Split(',');
                    
                    // TEMA: Bucles - foreach
                    foreach (string sintoma in sintomasArray)
                    {
                        // TEMA: Manejo de cadenas de texto - Trim para eliminar espacios
                        nuevoRegistro.Sintomas.Add(sintoma.Trim());
                    }
                    break;
                    
                default:
                    Console.WriteLine("\nOpcion no valida. Operacion cancelada.");
                    return;
            }
            
            // TEMA: Condicionales - Verificar si hay síntomas
            if (nuevoRegistro.Sintomas.Count > 0)
            {
                nuevoRegistro.Diagnostico = "Pendiente de diagnostico";
                
                // TEMA: Listas dinámicas - Agregar a historial
                paciente.Historial.Add(nuevoRegistro);
                
                Console.WriteLine("\nSintomas registrados exitosamente!");
                Console.WriteLine("ID del registro: " + nuevoRegistro.IdRegistro);
            }
            else
            {
                Console.WriteLine("\nNo se ingresaron sintomas.");
            }
        }

        // TEMA: Métodos - Diagnóstico interactivo usando árbol de decisión
        // TEMA: Árboles binarios - Recorrido de árbol
        // Realiza un diagnóstico interactivo usando el árbol de decisión
        public void ObtenerDiagnosticoInteractivo(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("     DIAGNOSTICO AUTOMATICO");
            Console.WriteLine("========================================");
            
            // TEMA: Condicionales - Verificar si hay registros sin diagnosticar
            // TEMA: Listas dinámicas - Buscar elemento con condición
            RegistroMedico registroPendiente = null;
            
            // TEMA: Bucles - foreach para buscar
            foreach (RegistroMedico reg in paciente.Historial)
            {
                // TEMA: Manejo de cadenas de texto - Contains para buscar subcadena
                if (reg.Diagnostico.Contains("Pendiente"))
                {
                    registroPendiente = reg;
                    break;
                }
            }
            
            // TEMA: Condicionales - if-else
            if (registroPendiente == null)
            {
                Console.WriteLine("\nNo hay registros pendientes de diagnostico.");
                Console.WriteLine("Primero ingrese sintomas (opcion 1).");
                return;
            }
            
            Console.WriteLine("Responda las siguientes preguntas con 'si' o 'no':\n");
            
            // TEMA: Árboles - Recorrido de árbol desde la raíz
            DecisionNode nodoActual = arbolDiagnosticoRoot;
            
            // TEMA: Bucles - while para navegar por el árbol
            while (!nodoActual.EsHoja())
            {
                Console.Write(nodoActual.Pregunta + " (si/no): ");
                string respuesta = Console.ReadLine().ToLower().Trim();
                
                // TEMA: Variables - Variable de control
                bool encontrado = false;
                
                // TEMA: Bucles - foreach para buscar hijo correspondiente
                // TEMA: Árboles - Navegar a hijos del nodo
                foreach (DecisionNode hijo in nodoActual.Hijos)
                {
                    // TEMA: Condicionales - Comparar respuesta
                    if (hijo.RespuestaEsperada == respuesta)
                    {
                        nodoActual = hijo;
                        encontrado = true;
                        break;
                    }
                }
                
                // TEMA: Condicionales - Validar entrada
                if (!encontrado)
                {
                    Console.WriteLine("Respuesta invalida. Use 'si' o 'no'.");
                }
            }
            
            // TEMA: Árboles - Obtener diagnóstico del nodo hoja
            string diagnosticoFinal = nodoActual.Diagnostico;
            
            registroPendiente.Diagnostico = diagnosticoFinal;
            
            Console.WriteLine("\n========================================");
            Console.WriteLine("         RESULTADO DEL DIAGNOSTICO");
            Console.WriteLine("========================================");
            Console.WriteLine(diagnosticoFinal);
            Console.WriteLine("========================================");
            
            // TEMA: Manejo de cadenas de texto - Contains para buscar palabra
            // TEMA: Condicionales - Verificar si es crítico
            if (diagnosticoFinal.Contains("CRITICO"))
            {
                Console.WriteLine("\nALERTA: Diagnostico critico detectado.");
                Console.WriteLine("Se recomienda solicitar consulta con doctor (opcion 5).");
            }
            
            Console.WriteLine("\nVersión beta: esta funcionalidad se implementará en versiones posteriores.");
        }

        // TEMA: Métodos - Mostrar historial médico
        // TEMA: Listas dinámicas - Recorrer lista
        // Muestra el historial médico completo del paciente
        public void MostrarHistorial(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("       HISTORIAL MEDICO");
            Console.WriteLine("========================================");
            
            // TEMA: Condicionales - Verificar si la lista está vacía
            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No hay registros medicos.");
                return;
            }
            
            // TEMA: Bucles - foreach para recorrer lista
            foreach (RegistroMedico registro in paciente.Historial)
            {
                registro.MostrarRegistro();
            }
        }

        // TEMA: Métodos - Comparar estadísticas de clínicas
        // TEMA: Arreglos bidimensionales - Acceso a matriz
        // Muestra comparación de precisión y tiempos entre clínicas
        public void CompararClinicas()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("     COMPARACION DE CLINICAS");
            Console.WriteLine("========================================");
            Console.WriteLine("Versión beta: esta funcionalidad se implementará en versiones posteriores.");
            Console.WriteLine("\nDatos simulados actuales:");
            Console.WriteLine("========================================");
            Console.WriteLine("Clinica | Precision(%) | Tiempo(min)");
            Console.WriteLine("--------|--------------|-------------");
            
            // TEMA: Bucles - for para recorrer filas de matriz
            // TEMA: Arreglos bidimensionales - GetLength para obtener dimensión
            for (int i = 0; i < statsClinica.GetLength(0); i++)
            {
                // TEMA: Arreglos bidimensionales - Acceso con [fila, columna]
                Console.WriteLine(
                    "   " + (i + 1) + "    |      " + 
                    statsClinica[i, 0] + "%      |     " + 
                    statsClinica[i, 1] + " min"
                );
            }
            Console.WriteLine("========================================");
        }

        // TEMA: Métodos - Solicitar consulta con doctor
        // TEMA: Colas - Encolar elemento
        // Agrega al paciente a la cola de consultas
        public void SolicitarConsultaDoctor(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("    SOLICITAR CONSULTA CON DOCTOR");
            Console.WriteLine("========================================");
            
            // TEMA: Condicionales - Verificar si hay registros
            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No tiene registros medicos.");
                Console.WriteLine("Primero ingrese sintomas (opcion 1).");
                return;
            }
            
            Console.WriteLine("Seleccione el registro para consulta:\n");
            
            // TEMA: Bucles - for con índice
            for (int i = 0; i < paciente.Historial.Count; i++)
            {
                RegistroMedico reg = paciente.Historial[i];
                Console.WriteLine((i + 1) + ". " + reg.IdRegistro + " - " + 
                                  reg.Fecha.ToString("dd/MM/yyyy") + " - " + 
                                  reg.Diagnostico);
            }
            
            Console.Write("\nNumero de registro: ");
            int seleccion;
            
            // TEMA: Condicionales - Validación con TryParse
            if (!int.TryParse(Console.ReadLine(), out seleccion) || 
                seleccion < 1 || seleccion > paciente.Historial.Count)
            {
                Console.WriteLine("\nSeleccion invalida.");
                return;
            }
            
            // TEMA: Listas dinámicas - Acceso por índice
            RegistroMedico registroSeleccionado = paciente.Historial[seleccion - 1];
            
            // TEMA: Manejo de cadenas de texto - Concatenar para crear clave
            string claveConsulta = paciente.Id + "|" + registroSeleccionado.IdRegistro;
            
            // TEMA: Colas - Encolar con Enqueue
            colaPacientes.Enqueue(claveConsulta);
            
            Console.WriteLine("\nSolicitud enviada exitosamente!");
            Console.WriteLine("Posicion en cola: " + colaPacientes.Count);
            Console.WriteLine("Un doctor revisara su caso pronto.");
            
            // TEMA: Grafos simples - Mostrar síntomas relacionados
            MostrarSintomasRelacionados(registroSeleccionado);
        }

        // TEMA: Métodos - Mostrar síntomas relacionados usando grafo
        // TEMA: Grafos simples - Navegar por grafo
        // Muestra síntomas relacionados basados en el grafo de síntomas
        public void MostrarSintomasRelacionados(RegistroMedico registro)
        {
            Console.WriteLine("\n--- Sintomas potencialmente relacionados ---");
            
            List<string> relacionados = new List<string>();
            
            // TEMA: Bucles - foreach para recorrer síntomas del registro
            foreach (string sintoma in registro.Sintomas)
            {
                // TEMA: Grafos simples - Buscar en el grafo
                // TEMA: Condicionales - ContainsKey para verificar existencia
                if (grafoSintomas.ContainsKey(sintoma))
                {
                    // TEMA: Grafos simples - Obtener nodos adyacentes
                    List<string> adyacentes = grafoSintomas[sintoma];
                    
                    // TEMA: Bucles - foreach para agregar relacionados
                    foreach (string rel in adyacentes)
                    {
                        // TEMA: Condicionales - Evitar duplicados
                        if (!relacionados.Contains(rel) && !registro.Sintomas.Contains(rel))
                        {
                            relacionados.Add(rel);
                        }
                    }
                }
            }
            
            // TEMA: Condicionales - Verificar si hay relacionados
            if (relacionados.Count > 0)
            {
                // TEMA: Manejo de cadenas de texto - Join para unir lista
                Console.WriteLine("Sintomas asociados: " + string.Join(", ", relacionados));
            }
            else
            {
                Console.WriteLine("No se encontraron sintomas adicionales relacionados.");
            }
        }

        // TEMA: Métodos - Actualizar datos del paciente
        // Permite al paciente actualizar su información personal
        public void ActualizarDatosPaciente(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("    ACTUALIZAR DATOS PERSONALES");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Actualizar nombre");
            Console.WriteLine("2. Actualizar email");
            Console.WriteLine("3. Actualizar contacto de emergencia");
            Console.WriteLine("0. Volver");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");
            
            string opcion = Console.ReadLine();
            
            // TEMA: Condicionales - Switch para opciones
            switch (opcion)
            {
                case "1":
                    Console.Write("Nuevo nombre: ");
                    paciente.Nombre = Console.ReadLine();
                    Console.WriteLine("\nNombre actualizado.");
                    break;
                case "2":
                    Console.Write("Nuevo email: ");
                    paciente.Email = Console.ReadLine();
                    Console.WriteLine("\nEmail actualizado.");
                    break;
                case "3":
                    Console.Write("Nuevo contacto de emergencia: ");
                    paciente.ContactoEmergencia = Console.ReadLine();
                    Console.WriteLine("\nContacto actualizado.");
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("\nOpcion no valida.");
                    break;
            }
        }

        // TEMA: Métodos - Revisar cola de pacientes
        // TEMA: Colas - Visualizar contenido de cola
        // Muestra los pacientes en la cola de consultas
        public void RevisarColaPacientes()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      COLA DE PACIENTES");
            Console.WriteLine("========================================");
            
            // TEMA: Condicionales - Verificar si cola está vacía
            // TEMA: Colas - Propiedad Count
            if (colaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes en cola.");
                return;
            }
            
            Console.WriteLine("Pacientes en espera: " + colaPacientes.Count);
            Console.WriteLine("========================================");
            
            // TEMA: Colas - Convertir a arreglo para visualizar sin desencolar
            string[] colaArray = colaPacientes.ToArray();
            
            // TEMA: Bucles - for para recorrer arreglo
            for (int i = 0; i < colaArray.Length; i++)
            {
                // TEMA: Manejo de cadenas de texto - Split para separar
                string[] partes = colaArray[i].Split('|');
                string idPaciente = partes[0];
                string idRegistro = partes[1];
                
                Console.WriteLine((i + 1) + ". Paciente: " + idPaciente + 
                                  " | Registro: " + idRegistro);
            }
            Console.WriteLine("========================================");
        }

        // TEMA: Métodos - Validar diagnósticos
        // TEMA: Colas - Desencolar elemento
        // Permite al doctor validar y modificar diagnósticos
        public void ValidarDiagnosticos(Doctor doctor)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("     VALIDAR DIAGNOSTICOS");
            Console.WriteLine("========================================");
            
            // TEMA: Condicionales - Verificar cola vacía
            if (colaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes en cola.");
                return;
            }
            
            // TEMA: Colas - Desencolar con Dequeue (FIFO)
            string claveConsulta = colaPacientes.Dequeue();
            
            // TEMA: Manejo de cadenas de texto - Split
            string[] partes = claveConsulta.Split('|');
            string idPaciente = partes[0];
            string idRegistro = partes[1];
            
            // TEMA: Listas dinámicas - Buscar paciente
            Paciente paciente = null;
            
            // TEMA: Bucles - foreach para buscar
            foreach (Paciente p in pacientes)
            {
                if (p.Id == idPaciente)
                {
                    paciente = p;
                    break;
                }
            }
            
            // TEMA: Condicionales - Verificar si se encontró
            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }
            
            // TEMA: Listas dinámicas - Buscar registro
            RegistroMedico registro = null;
            
            foreach (RegistroMedico r in paciente.Historial)
            {
                if (r.IdRegistro == idRegistro)
                {
                    registro = r;
                    break;
                }
            }
            
            if (registro == null)
            {
                Console.WriteLine("Registro no encontrado.");
                return;
            }
            
            // Mostrar información
            Console.WriteLine("\nPaciente: " + paciente.Nombre + " (" + paciente.Id + ")");
            Console.WriteLine("Edad: " + paciente.Edad);
            registro.MostrarRegistro();
            
            Console.WriteLine("\n1. Confirmar diagnostico");
            Console.WriteLine("2. Modificar diagnostico");
            Console.WriteLine("3. Agregar observaciones");
            Console.WriteLine("0. Volver a la cola");
            Console.Write("\nSeleccione una opcion: ");
            
            string opcion = Console.ReadLine();
            
            // TEMA: Condicionales - Switch
            switch (opcion)
            {
                case "1":
                    registro.Confirmado = true;
                    
                    // TEMA: Listas dinámicas - Contains y Add
                    if (!doctor.PacientesAsignados.Contains(paciente.Id))
                    {
                        doctor.PacientesAsignados.Add(paciente.Id);
                    }
                    
                    Console.WriteLine("\nDiagnostico confirmado.");
                    break;
                    
                case "2":
                    Console.Write("Nuevo diagnostico: ");
                    registro.Diagnostico = Console.ReadLine();
                    registro.Confirmado = true;
                    
                    if (!doctor.PacientesAsignados.Contains(paciente.Id))
                    {
                        doctor.PacientesAsignados.Add(paciente.Id);
                    }
                    
                    Console.WriteLine("\nDiagnostico actualizado y confirmado.");
                    break;
                    
                case "3":
                    Console.Write("Observaciones: ");
                    registro.ObservacionDoctor = Console.ReadLine();
                    Console.WriteLine("\nObservaciones agregadas.");
                    break;
                    
                case "0":
                    // TEMA: Colas - Volver a encolar
                    colaPacientes.Enqueue(claveConsulta);
                    Console.WriteLine("\nPaciente devuelto a la cola.");
                    break;
                    
                default:
                    Console.WriteLine("\nOpcion no valida.");
                    break;
            }
        }

        // TEMA: Métodos - Gestionar registros médicos
        // Permite al doctor buscar y gestionar registros de pacientes
        public void GestionarRegistrosMedicos()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("   GESTIONAR REGISTROS MEDICOS");
            Console.WriteLine("========================================");
            Console.Write("ID del paciente: ");
            string idPaciente = Console.ReadLine();
            
            // TEMA: Listas dinámicas - Buscar elemento
            Paciente paciente = null;
            
            // TEMA: Bucles - foreach
            foreach (Paciente p in pacientes)
            {
                if (p.Id == idPaciente)
                {
                    paciente = p;
                    break;
                }
            }
            
            // TEMA: Condicionales - Verificar si existe
            if (paciente == null)
            {
                Console.WriteLine("\nPaciente no encontrado.");
                return;
            }
            
            paciente.MostrarInformacion();
            
            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("\nEste paciente no tiene registros medicos.");
                return;
            }
            
            Console.WriteLine("\n--- Historial completo ---");
            
            // TEMA: Bucles - for con índice
            for (int i = 0; i < paciente.Historial.Count; i++)
            {
                Console.WriteLine("\n" + (i + 1) + ".");
                paciente.Historial[i].MostrarRegistro();
            }
        }

        // TEMA: Métodos - Ver estadísticas
        // Muestra estadísticas y reportes del sistema
        public void VerEstadisticas()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("         ESTADISTICAS DEL SISTEMA");
            Console.WriteLine("========================================");
            
            // TEMA: Variables - Contadores
            int totalPacientes = pacientes.Count;
            int totalDoctores = doctores.Count;
            int totalRegistros = 0;
            int registrosConfirmados = 0;
            
            // TEMA: Bucles - foreach para contar registros
            foreach (Paciente p in pacientes)
            {
                totalRegistros += p.Historial.Count;
                
                // TEMA: Bucles anidados - foreach dentro de foreach
                foreach (RegistroMedico r in p.Historial)
                {
                    // TEMA: Condicionales - Contar confirmados
                    if (r.Confirmado)
                    {
                        registrosConfirmados++;
                    }
                }
            }
            
            Console.WriteLine("Total de pacientes: " + totalPacientes);
            Console.WriteLine("Total de doctores: " + totalDoctores);
            Console.WriteLine("Total de registros medicos: " + totalRegistros);
            Console.WriteLine("Registros confirmados: " + registrosConfirmados);
            Console.WriteLine("Pacientes en cola: " + colaPacientes.Count);
            Console.WriteLine("========================================");
            
            Console.WriteLine("\nVersión beta: esta funcionalidad se implementará en versiones posteriores.");
        }

        // TEMA: Métodos - Placeholder para funciones avanzadas
        // Funciones que se implementarán en versiones posteriores
        public void FuncionBeta(string nombreFuncion)
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("Versión beta: esta funcionalidad se implementará en versiones posteriores.");
            Console.WriteLine("==========================================");
        }
    }
}
