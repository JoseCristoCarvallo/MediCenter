using System;

namespace MEDICENTER
{
    // TEMA: Programación Orientada a Objetos - Clase principal
    // TEMA: Métodos - Punto de entrada Main
    // Clase principal del programa MEDICENTER
    // Contiene el punto de entrada Main() y el menú principal
    class Program
    {
        // TEMA: Métodos - Main como punto de entrada del programa
        // Método principal que inicia la ejecución del programa
        static void Main(string[] args)
        {
            // TEMA: Programación Orientada a Objetos - Crear instancia del menú
            // TEMA: Bucles - do-while, Condicionales - switch
            Menu menuPrincipal = new Menu();
            
            // TEMA: Métodos - Llamar al menú principal
            menuPrincipal.MostrarMenuPrincipal();
        }
    }
}
