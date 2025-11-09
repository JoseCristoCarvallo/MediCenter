using System;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Clase Base
    // TEMA: Variables y tipos de datos
    // Clase base Usuario que representa a cualquier usuario del sistema
    public class Usuario
    {
        // TEMA: Variables - Atributos de instancia (string)
        public string Id;
        public string Nombre;
        public string Email;
        public string Password;

        // TEMA: Métodos - Constructor
        // Constructor de la clase Usuario
        public Usuario(string usuarioId, string usuarioNombre, string usuarioEmail, string usuarioPassword)
        {
            Id = usuarioId;
            Nombre = usuarioNombre;
            Email = usuarioEmail;
            Password = usuarioPassword;
        }

        // TEMA: Métodos - Constructor vacío
        public Usuario()
        {
        }
    }
}
