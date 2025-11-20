using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Clase controladora principal
    // TEMA: Listas dinámicas, Colas, Árboles, Grafos, Arreglos
    // Clase Sistema que controla toda la lógica del programa MEDICENTER
    public class Sistema
    {
        // Listas de usuarios registrados
        public List<Paciente> pacientes;
        public List<Doctor> doctores;

        // Cola de consultas pendientes del doctor (usa "pacienteId|registroId")
        public Queue<string> colaPacientes;

        // Arbol de decision para preguntas guiadas
        public DecisionNode arbolDiagnosticoRoot;

        // Arreglo de sintomas frecuentes para seleccion rapida
        public string[] sintomasFrecuentes;

        // Motor de IA Naive Bayes en memoria
        public SistemaIA sistemaIA;

        // Contadores para generar IDs simples
        public int contadorPacientes;
        public int contadorDoctores;
        public int contadorRegistros;

        // Constructor: inicializa listas, cola, IA y datos de prueba
        public Sistema()
        {
            pacientes = new List<Paciente>();
            doctores = new List<Doctor>();
            colaPacientes = new Queue<string>();

            sistemaIA = new SistemaIA();

            contadorPacientes = 1;
            contadorDoctores = 1;
            contadorRegistros = 1;

            sintomasFrecuentes = new string[]
            {
                "Fiebre",
                "Tos",
                "Dolor de cabeza",
                "Dolor de garganta",
                "Fatiga",
                "Nauseas",
                "Dificultad para respirar",
                "Dolor de pecho",
                "Congestion nasal",
                "Estornudos"
            };

            InicializarDatosPrueba();
            InicializarArbolDiagnostico();
        }

        // Crea usuarios de prueba: Paciente P001/pass123 y Doctor D001/doc123
        public void InicializarDatosPrueba()
        {
            Paciente pacienteTest = new Paciente(
                "P001",
                "Paciente Test",
                "paciente@test.com",
                "pass123",
                "001-000000-0000",
                30,
                "118 - Policia Nacional de Nicaragua",
                true
            );

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


        // Construye un arbol de decision sencillo para complementar la IA
        public void InicializarArbolDiagnostico()
        {
            arbolDiagnosticoRoot = new DecisionNode("root", "Tiene fiebre?");

            DecisionNode nodoFiebreSi = new DecisionNode("fiebre_si", "Tiene tos fuerte?");
            nodoFiebreSi.RespuestaEsperada = "si";

            DecisionNode nodoFiebreNo = new DecisionNode("fiebre_no", "Tiene estornudos frecuentes?");
            nodoFiebreNo.RespuestaEsperada = "no";

            DecisionNode diagAltoRiesgo = new DecisionNode("diag_neumonia", "Posible cuadro respiratorio serio. Vigilar dificultad para respirar.", true);
            diagAltoRiesgo.RespuestaEsperada = "si";

            DecisionNode diagLeve = new DecisionNode("diag_leve", "Sintomas leves. Se recomienda reposo y observacion.", true);
            diagLeve.RespuestaEsperada = "no";

            nodoFiebreSi.AgregarHijo(diagAltoRiesgo);
            nodoFiebreSi.AgregarHijo(diagLeve);

            DecisionNode diagAlergia = new DecisionNode("diag_alergia", "Cuadro compatible con alergia simple.", true);
            diagAlergia.RespuestaEsperada = "si";

            DecisionNode diagIndefinido = new DecisionNode("diag_indefinido", "Sintomas generales sin clara causa unica.", true);
            diagIndefinido.RespuestaEsperada = "no";

            nodoFiebreNo.AgregarHijo(diagAlergia);
            nodoFiebreNo.AgregarHijo(diagIndefinido);

            arbolDiagnosticoRoot.AgregarHijo(nodoFiebreSi);
            arbolDiagnosticoRoot.AgregarHijo(nodoFiebreNo);
        }

        // Registra un nuevo paciente en el sistema
        public void RegistrarPaciente()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      REGISTRO DE NUEVO PACIENTE");
            Console.WriteLine("========================================");

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();

            Console.Write("Cedula: ");
            string cedula = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Edad: ");
            int edad = 0;
            if (!int.TryParse(Console.ReadLine(), out edad))
            {
                Console.WriteLine("Edad invalida. Registro cancelado.");
                return;
            }

            Console.Write("Contacto de emergencia: ");
            string contacto = Console.ReadLine();

            Console.Write("¿Tiene seguro medico? (si/no): ");
            string respuestaSeguro = Console.ReadLine();
            bool tieneSeguro = respuestaSeguro.ToLower() == "si";

            string id = "P" + contadorPacientes.ToString().PadLeft(3, '0');
            contadorPacientes = contadorPacientes + 1;

            Paciente nuevoPaciente = new Paciente(id, nombre, email, password, cedula, edad, contacto, tieneSeguro);
            pacientes.Add(nuevoPaciente);

            Console.WriteLine("Paciente registrado. Su ID es: " + id);
        }

        // Registra un nuevo doctor en el sistema
        public void RegistrarDoctor()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("       REGISTRO DE NUEVO DOCTOR");
            Console.WriteLine("========================================");

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Especialidad: ");
            string especialidad = Console.ReadLine();

            string id = "D" + contadorDoctores.ToString().PadLeft(3, '0');
            contadorDoctores = contadorDoctores + 1;

            Doctor nuevoDoctor = new Doctor(id, nombre, email, password, especialidad);
            doctores.Add(nuevoDoctor);

            Console.WriteLine("Doctor registrado. Su ID es: " + id);
        }

        // Verifica si existe un usuario con ese ID y password
        public bool Login(string idParam, string passwordParam)
        {
            Paciente p = BuscarPaciente(idParam, passwordParam);
            if (p != null)
            {
                return true;
            }

            Doctor d = BuscarDoctor(idParam, passwordParam);
            if (d != null)
            {
                return true;
            }

            return false;
        }

        // Busca un paciente por ID y password
        public Paciente BuscarPaciente(string idBusqueda, string passwordBusqueda)
        {
            foreach (Paciente p in pacientes)
            {
                if (p.Id == idBusqueda && p.Password == passwordBusqueda)
                {
                    return p;
                }
            }
            return null;
        }

        // Busca un doctor por ID y password
        public Doctor BuscarDoctor(string idBusqueda, string passwordBusqueda)
        {
            foreach (Doctor d in doctores)
            {
                if (d.Id == idBusqueda && d.Password == passwordBusqueda)
                {
                    return d;
                }
            }
            return null;
        }

        // Permite al paciente ingresar sintomas con lista o texto libre
        public void IngresarSintomas(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("        INGRESAR SINTOMAS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Seleccionar de lista numerada");
            Console.WriteLine("2. Ingresar sintomas por texto libre");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            RegistroMedico nuevoRegistro = new RegistroMedico();
            nuevoRegistro.IdRegistro = "R" + contadorRegistros.ToString().PadLeft(4, '0');
            contadorRegistros = contadorRegistros + 1;

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("\nSintomas frecuentes:");
                    for (int i = 0; i < sintomasFrecuentes.Length; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + sintomasFrecuentes[i]);
                    }
                    Console.WriteLine("\nIngrese los numeros separados por coma (ej: 1,3,5):");
                    Console.Write("Sintomas: ");
                    string entrada = Console.ReadLine();
                    string[] numeros = entrada.Split(',');
                    foreach (string num in numeros)
                    {
                        int indice;
                        if (int.TryParse(num.Trim(), out indice))
                        {
                            if (indice >= 1 && indice <= sintomasFrecuentes.Length)
                            {
                                nuevoRegistro.Sintomas.Add(sintomasFrecuentes[indice - 1]);
                            }
                        }
                    }
                    break;

                case "2":
                    Console.WriteLine("\nEscriba sus sintomas separados por coma:");
                    Console.Write("Sintomas: ");
                    string sintomasTexto = Console.ReadLine();
                    string[] sintomasArray = sintomasTexto.Split(',');
                    foreach (string sintoma in sintomasArray)
                    {
                        string limpio = sintoma.Trim();
                        if (limpio.Length > 0)
                        {
                            nuevoRegistro.Sintomas.Add(limpio);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Opcion no valida. No se registraron sintomas.");
                    return;
            }

            if (nuevoRegistro.Sintomas.Count == 0)
            {
                Console.WriteLine("No se ingresaron sintomas.");
                return;
            }

            nuevoRegistro.Diagnostico = "Pendiente";
            AgregarRegistroMedico(paciente.Id, nuevoRegistro);

            bool hayDesconocidos = sistemaIA.HaySintomasDesconocidos(nuevoRegistro.Sintomas);
            if (hayDesconocidos)
            {
                Console.WriteLine("Alerta: Algunos síntomas no se encuentran en nuestra base. Se recomienda consultar directamente con un doctor.");
                Console.WriteLine("¿Desea solicitar consulta ahora? 1=Si 2=No");
                string respuesta = Console.ReadLine();
                if (respuesta == "1")
                {
                    Console.Write("Escriba un mensaje para el doctor: ");
                    string mensaje = Console.ReadLine();
                    EncolarConsulta(paciente.Id, nuevoRegistro.IdRegistro, mensaje);
                }
            }

            Console.WriteLine("Sintomas registrados. ID del registro: " + nuevoRegistro.IdRegistro);
        }

        // Usa el arbol de decision para hacer preguntas simples y complementar la IA
        public void ObtenerDiagnosticoInteractivo(Paciente paciente)
        {
            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No hay registros. Primero ingrese sintomas.");
                return;
            }

            RegistroMedico ultimo = paciente.Historial[paciente.Historial.Count - 1];

            Console.WriteLine("\nPequeño cuestionario para complementar el analisis:");

            DecisionNode nodoActual = arbolDiagnosticoRoot;
            while (!nodoActual.EsHoja())
            {
                Console.Write(nodoActual.Pregunta + " (si/no): ");
                string respuesta = Console.ReadLine();
                string respuestaLimpia = respuesta.ToLower().Trim();

                bool encontrado = false;
                foreach (DecisionNode hijo in nodoActual.Hijos)
                {
                    if (hijo.RespuestaEsperada == respuestaLimpia)
                    {
                        nodoActual = hijo;
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    Console.WriteLine("Respuesta invalida. Use 'si' o 'no'.");
                }
            }

            Console.WriteLine("Comentario del arbol de decision: " + nodoActual.Diagnostico);
            if (ultimo.Diagnostico == "Pendiente")
            {
                ultimo.Diagnostico = nodoActual.Diagnostico;
            }
        }

        // Agrega un registro medico al historial de un paciente por ID
        public void AgregarRegistroMedico(string pacienteId, RegistroMedico registro)
        {
            foreach (Paciente p in pacientes)
            {
                if (p.Id == pacienteId)
                {
                    p.Historial.Add(registro);
                    return;
                }
            }
        }

        // Muestra el historial medico completo del paciente
        public void MostrarHistorial(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("       HISTORIAL MEDICO");
            Console.WriteLine("========================================");

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No hay registros medicos.");
                return;
            }

            foreach (RegistroMedico registro in paciente.Historial)
            {
                registro.MostrarRegistro();
            }
        }

        // Opcion beta de comparacion de clinicas
        public void CompararClinicas()
        {
            Console.WriteLine("Versión beta: esta funcionalidad se implementará en versiones posteriores.");
        }

        // Encola una consulta con mensaje detallado del paciente
        public void EncolarConsulta(string pacienteId, string idRegistro, string mensaje)
        {
            string claveConsulta = pacienteId + "|" + idRegistro;
            colaPacientes.Enqueue(claveConsulta);

            foreach (Paciente p in pacientes)
            {
                if (p.Id == pacienteId)
                {
                    foreach (RegistroMedico r in p.Historial)
                    {
                        if (r.IdRegistro == idRegistro)
                        {
                            r.MensajePaciente = mensaje;
                            return;
                        }
                    }
                }
            }
        }

        // Permite al paciente seleccionar un registro y solicitar consulta al doctor
        public void SolicitarConsultaDoctor(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("    SOLICITAR CONSULTA CON DOCTOR");
            Console.WriteLine("========================================");

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No tiene registros medicos.");
                Console.WriteLine("Primero ingrese sintomas.");
                return;
            }

            Console.WriteLine("Seleccione el registro para consulta:\n");
            for (int i = 0; i < paciente.Historial.Count; i++)
            {
                RegistroMedico reg = paciente.Historial[i];
                Console.WriteLine((i + 1) + ". " + reg.IdRegistro + " - " + reg.Diagnostico);
            }

            Console.Write("Numero de registro: ");
            int seleccion;
            if (!int.TryParse(Console.ReadLine(), out seleccion) || seleccion < 1 || seleccion > paciente.Historial.Count)
            {
                Console.WriteLine("Seleccion invalida.");
                return;
            }

            RegistroMedico registroSeleccionado = paciente.Historial[seleccion - 1];
            Console.Write("Escriba un mensaje para el doctor: ");
            string mensaje = Console.ReadLine();

            EncolarConsulta(paciente.Id, registroSeleccionado.IdRegistro, mensaje);

            Console.WriteLine("Consulta enviada. Posicion en cola: " + colaPacientes.Count);
        }


        // Actualiza datos basicos del paciente (nombre, email, cedula, seguro)
        public void ActualizarDatosPaciente(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("    ACTUALIZAR DATOS PERSONALES");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Actualizar nombre");
            Console.WriteLine("2. Actualizar email");
            Console.WriteLine("3. Actualizar cedula");
            Console.WriteLine("4. Actualizar dato de seguro");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Nuevo nombre: ");
                    paciente.Nombre = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Nuevo email: ");
                    paciente.Email = Console.ReadLine();
                    break;
                case "3":
                    Console.Write("Nueva cedula: ");
                    paciente.Cedula = Console.ReadLine();
                    break;
                case "4":
                    Console.Write("¿Tiene seguro medico? (si/no): ");
                    string resp = Console.ReadLine();
                    paciente.TieneSeguro = resp.ToLower() == "si";
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }
        }

        // Muestra la cola de consultas pendientes
        public void RevisarColaPacientes()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      COLA DE PACIENTES");
            Console.WriteLine("========================================");

            if (colaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes en cola.");
                return;
            }

            string[] colaArray = colaPacientes.ToArray();
            for (int i = 0; i < colaArray.Length; i++)
            {
                string[] partes = colaArray[i].Split('|');
                string idPaciente = partes[0];
                string idRegistro = partes[1];
                Console.WriteLine((i + 1) + ". Paciente: " + idPaciente + " | Registro: " + idRegistro);
            }
        }

        // Toma el siguiente registro en la cola y lo devuelve al doctor
        public RegistroMedico TomarSiguienteConsulta()
        {
            if (colaPacientes.Count == 0)
            {
                return null;
            }

            string claveConsulta = colaPacientes.Dequeue();
            string[] partes = claveConsulta.Split('|');
            string idPaciente = partes[0];
            string idRegistro = partes[1];

            foreach (Paciente p in pacientes)
            {
                if (p.Id == idPaciente)
                {
                    foreach (RegistroMedico r in p.Historial)
                    {
                        if (r.IdRegistro == idRegistro)
                        {
                            return r;
                        }
                    }
                }
            }

            return null;
        }

        // Permite al doctor validar o corregir diagnósticos y actualizar la IA
        public void ValidarDiagnostico(string registroId, string doctorId, bool confirmar, string nuevoDiag)
        {
            Doctor doctorEncontrado = null;
            foreach (Doctor d in doctores)
            {
                if (d.Id == doctorId)
                {
                    doctorEncontrado = d;
                }
            }

            if (doctorEncontrado == null)
            {
                Console.WriteLine("Doctor no encontrado.");
                return;
            }

            Paciente pacienteEncontrado = null;
            RegistroMedico registroEncontrado = null;

            foreach (Paciente p in pacientes)
            {
                foreach (RegistroMedico r in p.Historial)
                {
                    if (r.IdRegistro == registroId)
                    {
                        pacienteEncontrado = p;
                        registroEncontrado = r;
                        break;
                    }
                }
            }

            if (registroEncontrado == null || pacienteEncontrado == null)
            {
                Console.WriteLine("Registro no encontrado.");
                return;
            }

            if (!string.IsNullOrEmpty(nuevoDiag))
            {
                registroEncontrado.Diagnostico = nuevoDiag;
            }

            if (confirmar)
            {
                registroEncontrado.Confirmado = true;
                ActualizarFrecuenciasConCaso(registroEncontrado);

                if (!doctorEncontrado.PacientesAsignados.Contains(pacienteEncontrado.Id))
                {
                    doctorEncontrado.PacientesAsignados.Add(pacienteEncontrado.Id);
                }
            }
        }

        // Gestion basica del historial de un paciente (ver y eliminar registros)
        public void GestionarRegistrosMedicos()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("   GESTIONAR HISTORIAL MEDICO");
            Console.WriteLine("========================================");
            Console.Write("ID del paciente o cedula: ");
            string texto = Console.ReadLine();

            Paciente paciente = null;
            foreach (Paciente p in pacientes)
            {
                if (p.Id == texto || p.Cedula == texto)
                {
                    paciente = p;
                }
            }

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("Este paciente no tiene registros.");
                return;
            }

            Console.WriteLine("Registros del paciente:");
            for (int i = 0; i < paciente.Historial.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + paciente.Historial[i].IdRegistro + " - " + paciente.Historial[i].Diagnostico);
            }

            Console.WriteLine("1. Ver registro completo");
            Console.WriteLine("2. Eliminar registro");
            Console.WriteLine("0. Volver");
            Console.Write("Opcion: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("Numero de registro: ");
                int num;
                if (int.TryParse(Console.ReadLine(), out num) && num >= 1 && num <= paciente.Historial.Count)
                {
                    paciente.Historial[num - 1].MostrarRegistro();
                }
            }
            else if (opcion == "2")
            {
                Console.Write("Numero de registro a eliminar: ");
                int num;
                if (int.TryParse(Console.ReadLine(), out num) && num >= 1 && num <= paciente.Historial.Count)
                {
                    paciente.Historial.RemoveAt(num - 1);
                    Console.WriteLine("Registro eliminado.");
                }
            }
        }

        // Muestra estadísticas generales simples del sistema
        public void VerEstadisticas()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("         ESTADISTICAS DEL SISTEMA");
            Console.WriteLine("========================================");

            int totalPacientes = pacientes.Count;
            int totalDoctores = doctores.Count;
            int totalRegistros = 0;

            foreach (Paciente p in pacientes)
            {
                totalRegistros = totalRegistros + p.Historial.Count;
            }

            Console.WriteLine("Pacientes registrados: " + totalPacientes);
            Console.WriteLine("Doctores registrados: " + totalDoctores);
            Console.WriteLine("Registros medicos totales: " + totalRegistros);
            Console.WriteLine("Consultas en cola: " + colaPacientes.Count);
            Console.WriteLine("========================================");
        }

        // Calcula un diagnostico Naive Bayes y devuelve solo el nombre
        public string ObtenerDiagnosticoNaiveBayes(List<string> sintomas)
        {
            ResultadoIA resultado = sistemaIA.Predecir(sintomas);
            resultado.Mostrar();
            return resultado.diagnostico;
        }

        // Usa Naive Bayes y luego el arbol de decision como complemento
        public void ObtenerDiagnosticoIA(Paciente paciente)
        {
            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No hay registros medicos. Primero ingrese sintomas.");
                return;
            }

            RegistroMedico ultimo = paciente.Historial[paciente.Historial.Count - 1];
            if (ultimo.Sintomas.Count == 0)
            {
                Console.WriteLine("El ultimo registro no tiene sintomas.");
                return;
            }

            Console.WriteLine("\nAnalizando sintomas con IA...");
            string diag = ObtenerDiagnosticoNaiveBayes(ultimo.Sintomas);
            if (!string.IsNullOrEmpty(diag))
            {
                ultimo.Diagnostico = diag;
            }

            Console.WriteLine("\nAhora se haran algunas preguntas simples para complementar.");
            ObtenerDiagnosticoInteractivo(paciente);
        }

        // Actualiza la base de frecuencias con un caso confirmado
        public void ActualizarFrecuenciasConCaso(RegistroMedico confirmado)
        {
            sistemaIA.Entrenar(confirmado);
        }

        // Muestra estadísticas de la IA en memoria
        public void MostrarEstadisticasIA()
        {
            sistemaIA.MostrarEstadisticas();
        }

        // Mensaje generico para opciones beta
        public void FuncionBeta(string nombreFuncion)
        {
            Console.WriteLine("Versión beta: esta funcionalidad se implementará en versiones posteriores.");
        }
    }
}
