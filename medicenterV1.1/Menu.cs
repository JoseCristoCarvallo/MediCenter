using System;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Clase de control de menús
    // TEMA: Bucles do-while, Condicionales switch
    // Clase Menu que controla el flujo del programa con menús para Paciente y Doctor
    public class Menu
    {
        // TEMA: Variables - Instancia del sistema
        public Sistema sistema;

        // TEMA: Métodos - Constructor
        // Constructor que inicializa el sistema
        public Menu()
        {
            sistema = new Sistema();
        }

        // TEMA: Métodos - Menú principal
        // TEMA: Bucles - do-while para mantener el programa ejecutándose
        // TEMA: Condicionales - switch para manejar opciones
        // Muestra el menú principal y controla el flujo del programa
        public void MostrarMenuPrincipal()
        {
            // TEMA: Variables - Variable de control de bucle
            bool salir = false;

            // TEMA: Bucles - do-while ejecuta al menos una vez
            do
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("    BIENVENIDO A MEDICENTER v1.1");
                Console.WriteLine("    Sistema de Gestion Medica");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Iniciar sesion");
                Console.WriteLine("2. Registrar usuario (Paciente o Doctor)");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=========================================");
                Console.Write("Seleccione una opcion: ");

                // TEMA: Variables - Lectura de entrada
                // TEMA: Manejo de cadenas de texto
                string opcion = Console.ReadLine();

                // TEMA: Condicionales - switch para manejar opciones del menú
                switch (opcion)
                {
                    case "1":
                        // TEMA: Métodos - Llamada a método de inicio de sesión
                        IniciarSesion();
                        break;
                    case "2":
                        // TEMA: Métodos - Llamada a menú de registro
                        MenuRegistro();
                        break;
                    case "0":
                        Console.WriteLine("\nGracias por usar MEDICENTER!");
                        Console.WriteLine("Hasta pronto.");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida. Intente de nuevo.");
                        break;
                }

            } while (!salir); // TEMA: Bucles - Condición de salida del do-while
        }

        // TEMA: Métodos - Menú de registro
        // TEMA: Bucles - do-while, Condicionales - switch
        // Muestra opciones para registrar paciente o doctor
        public void MenuRegistro()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("        REGISTRO DE USUARIO");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Paciente");
            Console.WriteLine("2. Registrar Doctor");
            Console.WriteLine("0. Volver");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            // TEMA: Condicionales - switch
            switch (opcion)
            {
                case "1":
                    sistema.RegistrarPaciente();
                    break;
                case "2":
                    sistema.RegistrarDoctor();
                    break;
                case "0":
                    // Volver al menú principal
                    break;
                default:
                    Console.WriteLine("\nOpcion no valida.");
                    break;
            }
        }

        // TEMA: Métodos - Inicio de sesión
        // TEMA: Condicionales - if-else
        // Maneja el proceso de inicio de sesión
        public void IniciarSesion()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("          INICIAR SESION");
            Console.WriteLine("========================================");
            Console.WriteLine("Paciente de prueba: P001 / pass123");
            Console.WriteLine("Doctor de prueba:   D001 / doc123");
            Console.WriteLine("========================================");

            Console.Write("ID de usuario: ");
            string id = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool loginOk = sistema.Login(id, password);
            if (!loginOk)
            {
                Console.WriteLine("Credenciales incorrectas.");
                return;
            }

            if (id.StartsWith("P"))
            {
                Paciente paciente = sistema.BuscarPaciente(id, password);
                if (paciente != null)
                {
                    MenuPaciente(paciente);
                }
            }
            else if (id.StartsWith("D"))
            {
                Doctor doctor = sistema.BuscarDoctor(id, password);
                if (doctor != null)
                {
                    MenuDoctor(doctor);
                }
            }
        }

        // Menú de opciones para el paciente activo
        public void MenuPaciente(Paciente pacienteActivo)
        {
            bool salir = false;

            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("          MENU PACIENTE");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Ver / actualizar perfil");
                Console.WriteLine("2. Ingresar sintomas");
                Console.WriteLine("3. Obtener diagnostico IA");
                Console.WriteLine("4. Ver historial");
                Console.WriteLine("5. Solicitar consulta al doctor");
                Console.WriteLine("6. (beta) Comparar clinicas");
                Console.WriteLine("0. Cerrar sesion");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        pacienteActivo.MostrarInformacion();
                        Console.WriteLine("¿Desea actualizar datos? 1=Si 2=No");
                        string r = Console.ReadLine();
                        if (r == "1")
                        {
                            sistema.ActualizarDatosPaciente(pacienteActivo);
                        }
                        break;
                    case "2":
                        sistema.IngresarSintomas(pacienteActivo);
                        break;
                    case "3":
                        sistema.ObtenerDiagnosticoIA(pacienteActivo);
                        break;
                    case "4":
                        sistema.MostrarHistorial(pacienteActivo);
                        break;
                    case "5":
                        sistema.SolicitarConsultaDoctor(pacienteActivo);
                        break;
                    case "6":
                        sistema.CompararClinicas();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

            } while (!salir);
        }

        // Menú de opciones para el doctor activo
        public void MenuDoctor(Doctor doctorActivo)
        {
            bool salir = false;

            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("           MENU DOCTOR");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Revisar consultas pendientes");
                Console.WriteLine("2. Validar / corregir diagnosticos");
                Console.WriteLine("3. Gestionar historial medico del paciente");
                Console.WriteLine("4. Ver estadisticas IA");
                Console.WriteLine("5. Actualizar base de conocimientos IA");
                Console.WriteLine("6. (beta) Clinica");
                Console.WriteLine("7. Entrenar IA con casos confirmados (beta)");
                Console.WriteLine("8. Ver informacion personal");
                Console.WriteLine("0. Cerrar sesion");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        sistema.RevisarColaPacientes();
                        break;
                    case "2":
                        RegistroMedico reg = sistema.TomarSiguienteConsulta();
                        if (reg == null)
                        {
                            Console.WriteLine("No hay consultas pendientes.");
                        }
                        else
                        {
                            reg.MostrarRegistro();
                            Console.WriteLine("1. Confirmar diagnostico actual");
                            Console.WriteLine("2. Corregir diagnostico");
                            Console.WriteLine("0. Volver sin cambios");
                            Console.Write("Opcion: ");
                            string opVal = Console.ReadLine();
                            if (opVal == "1")
                            {
                                sistema.ValidarDiagnostico(reg.IdRegistro, doctorActivo.Id, true, "");
                            }
                            else if (opVal == "2")
                            {
                                Console.Write("Nuevo diagnostico: ");
                                string nuevo = Console.ReadLine();
                                sistema.ValidarDiagnostico(reg.IdRegistro, doctorActivo.Id, true, nuevo);
                            }
                        }
                        break;
                    case "3":
                        sistema.GestionarRegistrosMedicos();
                        break;
                    case "4":
                        sistema.MostrarEstadisticasIA();
                        break;
                    case "5":
                        Console.WriteLine("Versión beta: esta funcionalidad se implementará en versiones posteriores.");
                        break;
                    case "6":
                        sistema.FuncionBeta("Clinica");
                        break;
                    case "7":
                        Console.WriteLine("Versión beta: esta funcionalidad se implementará en versiones posteriores.");
                        break;
                    case "8":
                        doctorActivo.MostrarInformacion();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

            } while (!salir);
        }
    }
}
