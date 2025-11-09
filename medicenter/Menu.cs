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
                Console.WriteLine("    BIENVENIDO A MEDICENTER v1.0");
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
            Console.WriteLine("Credenciales de prueba:");
            Console.WriteLine("  Paciente: P001 / pass123");
            Console.WriteLine("  Doctor: D001 / doc123");
            Console.WriteLine("========================================");

            // TEMA: Variables - Lectura de datos
            Console.Write("ID de usuario: ");
            string id = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // TEMA: Condicionales - Verificar tipo de usuario por ID
            // TEMA: Manejo de cadenas de texto - StartsWith
            if (id.StartsWith("P"))
            {
                // Buscar paciente
                Paciente paciente = sistema.BuscarPaciente(id, password);

                // TEMA: Condicionales - Verificar si existe
                if (paciente != null)
                {
                    Console.WriteLine("\nBienvenido, " + paciente.Nombre);
                    // TEMA: Métodos - Llamar menú de paciente
                    MenuPaciente(paciente);
                }
                else
                {
                    Console.WriteLine("\nCredenciales incorrectas.");
                }
            }
            else if (id.StartsWith("D"))
            {
                // Buscar doctor
                Doctor doctor = sistema.BuscarDoctor(id, password);

                if (doctor != null)
                {
                    Console.WriteLine("\nBienvenido, Dr. " + doctor.Nombre);
                    // TEMA: Métodos - Llamar menú de doctor
                    MenuDoctor(doctor);
                }
                else
                {
                    Console.WriteLine("\nCredenciales incorrectas.");
                }
            }
            else
            {
                Console.WriteLine("\nID no valido.");
            }
        }

        // TEMA: Métodos - Menú del paciente
        // TEMA: Bucles - do-while para mantener menú activo
        // TEMA: Condicionales - switch para manejar opciones
        // Muestra el menú de opciones para el paciente
        public void MenuPaciente(Paciente paciente)
        {
            // TEMA: Variables - Control de bucle
            bool salir = false;

            // TEMA: Bucles - do-while
            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("          MENU PACIENTE");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Ingresar sintomas");
                Console.WriteLine("2. Obtener diagnostico automatico");
                Console.WriteLine("3. Consultar historial medico");
                Console.WriteLine("4. Comparar clinicas");
                Console.WriteLine("5. Solicitar consulta con doctor");
                Console.WriteLine("6. Actualizar datos personales");
                Console.WriteLine("7. Ver informacion personal");
                Console.WriteLine("0. Cerrar sesion");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                // TEMA: Condicionales - switch
                switch (opcion)
                {
                    case "1":
                        // TEMA: Métodos - Llamar método del sistema
                        sistema.IngresarSintomas(paciente);
                        break;
                    case "2":
                        sistema.ObtenerDiagnosticoInteractivo(paciente);
                        break;
                    case "3":
                        sistema.MostrarHistorial(paciente);
                        break;
                    case "4":
                        sistema.CompararClinicas();
                        break;
                    case "5":
                        sistema.SolicitarConsultaDoctor(paciente);
                        break;
                    case "6":
                        sistema.ActualizarDatosPaciente(paciente);
                        break;
                    case "7":
                        paciente.MostrarInformacion();
                        break;
                    case "0":
                        Console.WriteLine("\nCerrando sesion...");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        break;
                }

            } while (!salir); // Continuar hasta que el usuario cierre sesión
        }

        // TEMA: Métodos - Menú del doctor
        // TEMA: Bucles - do-while para mantener menú activo
        // TEMA: Condicionales - switch para manejar opciones
        // Muestra el menú de opciones para el doctor
        public void MenuDoctor(Doctor doctor)
        {
            // TEMA: Variables - Control de bucle
            bool salir = false;

            // TEMA: Bucles - do-while
            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("           MENU DOCTOR");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Revisar cola de pacientes");
                Console.WriteLine("2. Validar diagnosticos");
                Console.WriteLine("3. Gestionar registros medicos");
                Console.WriteLine("4. Ver estadisticas");
                Console.WriteLine("5. Agregar nodo al arbol (beta)");
                Console.WriteLine("6. Entrenar modelo IA (beta)");
                Console.WriteLine("7. Ver informacion personal");
                Console.WriteLine("0. Cerrar sesion");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                // TEMA: Condicionales - switch
                switch (opcion)
                {
                    case "1":
                        // TEMA: Métodos - Llamar método del sistema
                        sistema.RevisarColaPacientes();
                        break;
                    case "2":
                        sistema.ValidarDiagnosticos(doctor);
                        break;
                    case "3":
                        sistema.GestionarRegistrosMedicos();
                        break;
                    case "4":
                        sistema.VerEstadisticas();
                        break;
                    case "5":
                        sistema.FuncionBeta("Agregar nodo al arbol de diagnostico");
                        break;
                    case "6":
                        sistema.FuncionBeta("Entrenar modelo IA");
                        break;
                    case "7":
                        doctor.MostrarInformacion();
                        break;
                    case "0":
                        Console.WriteLine("\nCerrando sesion...");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        break;
                }

            } while (!salir); // Continuar hasta que el usuario cierre sesión
        }
    }
}
