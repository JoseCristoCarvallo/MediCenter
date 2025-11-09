using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Herencia
    // TEMA: Listas dinámicas
    // Clase Paciente que hereda de Usuario
    public class Paciente : Usuario
    {
        // TEMA: Variables y tipos de datos (int, string)
        public int Edad;
        public string ContactoEmergencia;
        
        // TEMA: Listas dinámicas - List<T> para almacenar historial médico
        public List<RegistroMedico> Historial;

        // TEMA: Métodos - Constructor
        // Constructor de Paciente
        public Paciente(string pacienteId, string pacienteNombre, string pacienteEmail, string pacientePassword, int pacienteEdad, string pacienteContactoEmergencia)
            : base(pacienteId, pacienteNombre, pacienteEmail, pacientePassword)
        {
            Edad = pacienteEdad;
            ContactoEmergencia = pacienteContactoEmergencia;
            
            // TEMA: Listas dinámicas - Inicialización
            Historial = new List<RegistroMedico>();
        }

        // TEMA: Métodos - Constructor vacío
        public Paciente() : base()
        {
            // TEMA: Listas dinámicas - Inicialización
            Historial = new List<RegistroMedico>();
        }

        // TEMA: Métodos - Método para mostrar información del paciente
        // Muestra la información básica del paciente
        public void MostrarInformacion()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("         INFORMACION DEL PACIENTE        ");
            Console.WriteLine("==========================================");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Edad: " + Edad);
            Console.WriteLine("Contacto Emergencia: " + ContactoEmergencia);
            Console.WriteLine("Registros medicos: " + Historial.Count);
            Console.WriteLine("==========================================");
        }
    }
}
