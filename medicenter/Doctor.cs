using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Herencia
    // TEMA: Listas dinámicas
    // Clase Doctor que hereda de Usuario
    public class Doctor : Usuario
    {
        // TEMA: Variables y tipos de datos (string)
        public string Especialidad;
        
        // TEMA: Listas dinámicas - List<string> para IDs de pacientes asignados
        public List<string> PacientesAsignados;

        // TEMA: Métodos - Constructor
        // Constructor de Doctor
        public Doctor(string doctorId, string doctorNombre, string doctorEmail, string doctorPassword, string doctorEspecialidad)
            : base(doctorId, doctorNombre, doctorEmail, doctorPassword)
        {
            Especialidad = doctorEspecialidad;
            
            // TEMA: Listas dinámicas - Inicialización
            PacientesAsignados = new List<string>();
        }

        // TEMA: Métodos - Constructor vacío
        public Doctor() : base()
        {
            // TEMA: Listas dinámicas - Inicialización
            PacientesAsignados = new List<string>();
        }

        // TEMA: Métodos - Método para mostrar información del doctor
        // Muestra la información básica del doctor
        public void MostrarInformacion()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("         INFORMACION DEL DOCTOR          ");
            Console.WriteLine("==========================================");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Especialidad: " + Especialidad);
            Console.WriteLine("Pacientes asignados: " + PacientesAsignados.Count);
            Console.WriteLine("==========================================");
        }
    }
}
