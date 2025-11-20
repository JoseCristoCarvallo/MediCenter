using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Clase
    // TEMA: Variables y tipos de datos
    // TEMA: Listas dinámicas
    // Clase que representa un registro médico de un paciente
    public class RegistroMedico
    {
        // Datos básicos del registro médico
        public string IdRegistro;
        public DateTime Fecha;

        // Lista de síntomas ingresados por el paciente
        public List<string> Sintomas;

        // Diagnóstico sugerido o confirmado
        public string Diagnostico;
        public bool Confirmado;

        // Mensaje que el paciente escribe al solicitar consulta
        public string MensajePaciente;

        // Observaciones que el doctor agrega al revisar el caso
        public string ObservacionDoctor;

        // Constructor de RegistroMedico
        public RegistroMedico()
        {
            Sintomas = new List<string>();
            Fecha = DateTime.Now;
            Confirmado = false;
            MensajePaciente = "";
            ObservacionDoctor = "";
        }

        // Muestra la información del registro médico
        public void MostrarRegistro()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("ID Registro: " + IdRegistro);
            Console.WriteLine("Fecha: " + Fecha.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Sintomas: " + string.Join(", ", Sintomas));
            Console.WriteLine("Diagnostico: " + Diagnostico);
            Console.WriteLine("Confirmado por doctor: " + (Confirmado ? "Si" : "No"));
            if (!string.IsNullOrEmpty(MensajePaciente))
            {
                Console.WriteLine("Mensaje del paciente: " + MensajePaciente);
            }
            if (!string.IsNullOrEmpty(ObservacionDoctor))
            {
                Console.WriteLine("Observaciones del doctor: " + ObservacionDoctor);
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
