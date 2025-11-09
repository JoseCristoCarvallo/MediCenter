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
        // TEMA: Variables - Tipos de datos (string, DateTime, bool)
        public string IdRegistro;
        public DateTime Fecha;
        
        // TEMA: Listas dinámicas - List<string> para almacenar síntomas
        public List<string> Sintomas;
        
        public string Diagnostico;
        public bool Confirmado;
        public string ObservacionDoctor;

        // TEMA: Métodos - Constructor
        // Constructor de RegistroMedico
        public RegistroMedico()
        {
            // TEMA: Listas dinámicas - Inicialización de lista
            Sintomas = new List<string>();
            Fecha = DateTime.Now;
            Confirmado = false;
        }

        // TEMA: Métodos - Método para mostrar información
        // Método que muestra la información del registro médico
        public void MostrarRegistro()
        {
            // TEMA: Manejo de cadenas de texto
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("ID Registro: " + IdRegistro);
            Console.WriteLine("Fecha: " + Fecha.ToString("dd/MM/yyyy HH:mm"));
            
            // TEMA: Manejo de cadenas de texto - Join para unir elementos de lista
            Console.WriteLine("Sintomas: " + string.Join(", ", Sintomas));
            Console.WriteLine("Diagnostico: " + Diagnostico);
            Console.WriteLine("Confirmado por doctor: " + (Confirmado ? "Si" : "No"));
            
            // TEMA: Condicionales - if para verificar si hay observaciones
            if (!string.IsNullOrEmpty(ObservacionDoctor))
            {
                Console.WriteLine("Observaciones: " + ObservacionDoctor);
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
