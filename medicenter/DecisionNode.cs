using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    // TEMA: Árboles binarios - Estructura de nodo para árbol de decisión
    // TEMA: Listas dinámicas
    // Clase que representa un nodo en el árbol de decisión para diagnósticos
    public class DecisionNode
    {
        // TEMA: Variables y tipos de datos
        public string Id;
        public string Pregunta;
        public string Diagnostico; // Solo si es nodo hoja
        
        // TEMA: Listas dinámicas - Lista de nodos hijos (para árbol n-ario)
        public List<DecisionNode> Hijos;
        
        // TEMA: Variables - Variable para identificar respuestas (si/no)
        public string RespuestaEsperada; // "si" o "no"

        // TEMA: Métodos - Constructor
        // Constructor de DecisionNode
        public DecisionNode(string nodoId, string nodoPregunta)
        {
            Id = nodoId;
            Pregunta = nodoPregunta;
            
            // TEMA: Listas dinámicas - Inicialización
            Hijos = new List<DecisionNode>();
        }

        // TEMA: Métodos - Constructor para nodo hoja (con diagnóstico)
        public DecisionNode(string nodoId, string nodoDiagnostico, bool esHoja)
        {
            Id = nodoId;
            Diagnostico = nodoDiagnostico;
            
            // TEMA: Listas dinámicas - Inicialización
            Hijos = new List<DecisionNode>();
        }

        // TEMA: Métodos - Método para verificar si es nodo hoja
        // Verifica si el nodo es una hoja (tiene diagnóstico y no tiene hijos)
        public bool EsHoja()
        {
            // TEMA: Condicionales - Operadores lógicos
            return !string.IsNullOrEmpty(Diagnostico) && Hijos.Count == 0;
        }

        // TEMA: Métodos - Agregar hijo al nodo
        // Agrega un nodo hijo al nodo actual
        public void AgregarHijo(DecisionNode hijo)
        {
            // TEMA: Listas dinámicas - Método Add
            Hijos.Add(hijo);
        }
    }
}
