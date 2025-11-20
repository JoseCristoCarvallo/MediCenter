using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Herencia
    // TEMA: Listas dinámicas
    // Clase Paciente que hereda de Usuario
    public class Paciente : Usuario
    {
        // Datos básicos del paciente
        public string Cedula;
        public int Edad;
        public string ContactoEmergencia;
        public bool TieneSeguro;
        
        // Historial de consultas del paciente
        public List<RegistroMedico> Historial;

        // Constructor de Paciente con todos los datos
        public Paciente(string pacienteId, string pacienteNombre, string pacienteEmail, string pacientePassword, string pacienteCedula, int pacienteEdad, string pacienteContactoEmergencia, bool pacienteTieneSeguro)
            : base(pacienteId, pacienteNombre, pacienteEmail, pacientePassword)
        {
            Cedula = pacienteCedula;
            Edad = pacienteEdad;
            ContactoEmergencia = pacienteContactoEmergencia;
            TieneSeguro = pacienteTieneSeguro;
            Historial = new List<RegistroMedico>();
        }

        // Constructor vacío de Paciente
        public Paciente() : base()
        {
            Cedula = "";
            Edad = 0;
            ContactoEmergencia = "";
            TieneSeguro = false;
            Historial = new List<RegistroMedico>();
        }

        // Muestra la información básica del paciente
        public void MostrarInformacion()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("         INFORMACION DEL PACIENTE        ");
            Console.WriteLine("==========================================");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Cedula: " + Cedula);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Edad: " + Edad);
            Console.WriteLine("Tiene seguro medico: " + (TieneSeguro ? "Si" : "No"));
            Console.WriteLine("Contacto Emergencia: " + ContactoEmergencia);
            Console.WriteLine("Registros medicos: " + Historial.Count);
            Console.WriteLine("==========================================");
        }
    }
}
