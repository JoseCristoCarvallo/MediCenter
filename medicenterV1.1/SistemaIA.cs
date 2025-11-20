using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class SistemaIA
    {
        // Tabla de frecuencias: enfermedad -> (sintoma -> conteo)
        private Dictionary<string, Dictionary<string, int>> baseEnfermedades;

        // Conteo total de casos por enfermedad
        private Dictionary<string, int> totalCasosPorEnfermedad;

        // Descripciones cortas por enfermedad
        private Dictionary<string, string> descripcionEnfermedad;

        // Recomendaciones simples por enfermedad
        private Dictionary<string, string> recomendacionEnfermedad;

        // Conteo total de entrenamientos
        private int totalEntrenamientos;

        // Constructor: inicializa la base de conocimientos fija
        public SistemaIA()
        {
            baseEnfermedades = new Dictionary<string, Dictionary<string, int>>();
            totalCasosPorEnfermedad = new Dictionary<string, int>();
            descripcionEnfermedad = new Dictionary<string, string>();
            recomendacionEnfermedad = new Dictionary<string, string>();
            totalEntrenamientos = 0;

            InicializarBaseConocimientos();
        }

        // Carga enfermedades, síntomas frecuentes y explicaciones
        private void InicializarBaseConocimientos()
        {
            // Cada enfermedad usa un total de 100 "casos" para que los porcentajes sean fáciles de entender

            AgregarEnfermedadBase(
                "gripe",
                new string[] { "fiebre", "tos", "dolor de cabeza", "fatiga", "dolor de garganta" },
                new int[]    {       80,    70,                60,       60,                 40 },
                "Infeccion viral de vias respiratorias con fiebre y malestar general.",
                "Reposo, hidratacion, analgésicos simples y observar evolucion."
            );

            AgregarEnfermedadBase(
                "covid-19",
                new string[] { "fiebre", "tos", "dificultad para respirar", "fatiga", "dolor muscular" },
                new int[]    {       85,    80,                        60,       70,               50 },
                "Enfermedad respiratoria viral que puede complicarse en algunos casos.",
                "Aislamiento, mascarilla, hidratacion y consulta medica si empeora."
            );

            AgregarEnfermedadBase(
                "alergia",
                new string[] { "estornudos", "picazon nasal", "lagrimeo", "congestion nasal" },
                new int[]    {          80,             70,         60,                65 },
                "Reaccion del cuerpo a polvo, polen u otras sustancias.",
                "Evitar el alergeno, limpiar ambiente y usar antihistaminicos simples.");

            AgregarEnfermedadBase(
                "migrana",
                new string[] { "dolor de cabeza", "nauseas", "sensibilidad a la luz" },
                new int[]    {                90,        60,                      70 },
                "Dolor de cabeza intenso que puede repetirse en crisis.",
                "Descanso en lugar oscuro, hidratacion y analgesicos adecuados."
            );

            AgregarEnfermedadBase(
                "faringitis",
                new string[] { "dolor de garganta", "fiebre", "dificultad para tragar" },
                new int[]    {                 90,       60,                    70 },
                "Inflamacion de la garganta que causa dolor al tragar.",
                "Gargaras con agua tibia, analgesicos simples y consulta si persiste."
            );

            AgregarEnfermedadBase(
                "bronquitis",
                new string[] { "tos", "flema", "dolor de pecho", "fatiga" },
                new int[]    {    90,     70,             60,       50 },
                "Inflamacion de los bronquios con tos y produccion de flema.",
                "Evitar humo, beber liquidos y consultar si hay dificultad para respirar."
            );

            AgregarEnfermedadBase(
                "neumonia",
                new string[] { "fiebre", "tos", "dificultad para respirar", "dolor de pecho" },
                new int[]    {       90,    80,                        80,              70 },
                "Infeccion pulmonar que puede ser grave.",
                "Consulta medica urgente, posible uso de antibioticos y reposo estricto."
            );

            AgregarEnfermedadBase(
                "asma",
                new string[] { "dificultad para respirar", "sibilancias", "opresion en el pecho", "tos" },
                new int[]    {                        90,           80,                     70,    60 },
                "Enfermedad cronica de las vias respiratorias con crisis de falta de aire.",
                "Uso de inhaladores recetados y evitar desencadenantes como polvo o humo."
            );

            // Se pueden agregar mas enfermedades simples aqui si es necesario
        }

        // Agrega una enfermedad a la base con sus frecuencias y textos
        private void AgregarEnfermedadBase(string nombre, string[] sintomas, int[] porcentajes, string descripcion, string recomendacion)
        {
            string clave = nombre.ToLower();

            Dictionary<string, int> tabla = new Dictionary<string, int>();
            int total = 0;

            for (int i = 0; i < sintomas.Length; i++)
            {
                string sintoma = sintomas[i].Trim().ToLower();
                int valor = porcentajes[i];
                tabla[sintoma] = valor;
                total = total + valor;
            }

            baseEnfermedades[clave] = tabla;
            totalCasosPorEnfermedad[clave] = total;
            descripcionEnfermedad[clave] = descripcion;
            recomendacionEnfermedad[clave] = recomendacion;

            totalEntrenamientos = totalEntrenamientos + total;
        }

        // ENTRENAR: aprende de un caso confirmado por el doctor
        public bool Entrenar(RegistroMedico registro)
        {
            if (!registro.Confirmado)
                return false;

            if (registro.Sintomas == null || registro.Sintomas.Count == 0)
                return false;

            if (string.IsNullOrWhiteSpace(registro.Diagnostico))
                return false;

            string enfermedad = registro.Diagnostico.Trim().ToLower();

            if (!baseEnfermedades.ContainsKey(enfermedad))
            {
                baseEnfermedades[enfermedad] = new Dictionary<string, int>();
                totalCasosPorEnfermedad[enfermedad] = 0;
                descripcionEnfermedad[enfermedad] = "Caso agregado por el doctor.";
                recomendacionEnfermedad[enfermedad] = "Seguir indicaciones del especialista.";
            }

            foreach (string sintoma in registro.Sintomas)
            {
                string sintomaLimpio = sintoma.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(sintomaLimpio))
                    continue;

                if (!baseEnfermedades[enfermedad].ContainsKey(sintomaLimpio))
                    baseEnfermedades[enfermedad][sintomaLimpio] = 0;

                baseEnfermedades[enfermedad][sintomaLimpio] = baseEnfermedades[enfermedad][sintomaLimpio] + 1;
                totalCasosPorEnfermedad[enfermedad] = totalCasosPorEnfermedad[enfermedad] + 1;
                totalEntrenamientos = totalEntrenamientos + 1;
            }

            return true;
        }

        // PREDECIR: calcula la enfermedad mas probable segun los sintomas
        public ResultadoIA Predecir(List<string> sintomasPaciente)
        {
            ResultadoIA resultado = new ResultadoIA();

            if (baseEnfermedades.Count == 0)
            {
                resultado.diagnostico = "Sistema sin entrenar";
                resultado.confianza = 0.0f;
                resultado.mensaje = "No hay casos confirmados.";
                return resultado;
            }

            if (sintomasPaciente == null || sintomasPaciente.Count == 0)
            {
                resultado.diagnostico = "Sin síntomas";
                resultado.confianza = 0.0f;
                resultado.mensaje = "Ingrese al menos un síntoma.";
                return resultado;
            }

            // Normalizar síntomas
            List<string> sintomasLimpios = new List<string>();
            foreach (string s in sintomasPaciente)
            {
                string sLimpio = s.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(sLimpio))
                    sintomasLimpios.Add(sLimpio);
            }

            if (sintomasLimpios.Count == 0)
            {
                resultado.diagnostico = "Síntomas inválidos";
                resultado.confianza = 0.0f;
                resultado.mensaje = "Los síntomas no son válidos.";
                return resultado;
            }

            // Calcular probabilidades por enfermedad
            Dictionary<string, double> probabilidades = new Dictionary<string, double>();
            foreach (string enfermedad in baseEnfermedades.Keys)
            {
                double prob = CalcularProbabilidad(enfermedad, sintomasLimpios);
                probabilidades[enfermedad] = prob;
            }

            // Encontrar mejor
            double maxProb = 0.0;
            string mejorEnf = "";
            foreach (string enf in probabilidades.Keys)
            {
                if (probabilidades[enf] > maxProb)
                {
                    maxProb = probabilidades[enf];
                    mejorEnf = enf;
                }
            }

            if (maxProb == 0.0)
            {
                resultado.diagnostico = "No concluyente";
                resultado.confianza = 0.0f;
                resultado.mensaje = "Síntomas no reconocidos.";
                return resultado;
            }

            // Calcular confianza relativa
            double sumaTotal = 0.0;
            foreach (string enf in probabilidades.Keys)
            {
                sumaTotal = sumaTotal + probabilidades[enf];
            }

            float confianzaFinal = 0.0f;
            if (sumaTotal > 0.0)
            {
                confianzaFinal = (float)(maxProb / sumaTotal);
            }

            resultado.diagnostico = mejorEnf;
            resultado.confianza = confianzaFinal;

            if (descripcionEnfermedad.ContainsKey(mejorEnf))
            {
                resultado.descripcion = descripcionEnfermedad[mejorEnf];
            }
            if (recomendacionEnfermedad.ContainsKey(mejorEnf))
            {
                resultado.recomendacion = recomendacionEnfermedad[mejorEnf];
            }

            resultado.mensaje = "Analisis basado en " + totalEntrenamientos + " unidades de frecuencia.";

            return resultado;
        }

        // Calcular P(sintomas | enfermedad) multiplicando probabilidades con suavizado simple
        private double CalcularProbabilidad(string enfermedad, List<string> sintomas)
        {
            int totalCasos = totalCasosPorEnfermedad[enfermedad];
            double probabilidad = 1.0;
            double suavizado = 0.1;

            foreach (string sintoma in sintomas)
            {
                string sintomaClave = sintoma.Trim().ToLower();
                double probSintoma = 0.0;

                if (baseEnfermedades[enfermedad].ContainsKey(sintomaClave))
                {
                    int frecuencia = baseEnfermedades[enfermedad][sintomaClave];
                    probSintoma = (double)(frecuencia + suavizado) / (totalCasos + suavizado);
                }
                else
                {
                    // Sintoma no visto para esta enfermedad: solo suavizado
                    probSintoma = suavizado / (totalCasos + suavizado);
                }

                probabilidad = probabilidad * probSintoma;
            }

            return probabilidad;
        }
        // Mostrar estadísticas simples de la IA en memoria
        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("ESTADISTICAS DEL SISTEMA IA");
            Console.WriteLine("========================================");
            Console.WriteLine("Unidades de entrenamiento: " + totalEntrenamientos);
            Console.WriteLine("Enfermedades conocidas: " + baseEnfermedades.Count);
            Console.WriteLine("========================================\n");

            foreach (string enfermedad in baseEnfermedades.Keys)
            {
                int totalCasos = totalCasosPorEnfermedad[enfermedad];
                Console.WriteLine("Enfermedad: " + enfermedad.ToUpper());
                Console.WriteLine("Total de frecuencia: " + totalCasos);

                Dictionary<string, int> sintomasEnf = baseEnfermedades[enfermedad];
                foreach (string s in sintomasEnf.Keys)
                {
                    float pct = 0.0f;
                    if (totalCasos > 0)
                    {
                        pct = (float)sintomasEnf[s] / totalCasos * 100;
                    }
                    Console.WriteLine("  - " + s + ": " + pct.ToString("F0") + "%");
                }
                Console.WriteLine();
            }
            Console.WriteLine("========================================");
        }

        // Verifica si hay sintomas que no se encuentran en la base de conocimientos
        public bool HaySintomasDesconocidos(List<string> sintomas)
        {
            bool hayDesconocidos = false;

            foreach (string sintoma in sintomas)
            {
                string claveSintoma = sintoma.Trim().ToLower();
                bool encontrado = false;

                foreach (string enfermedad in baseEnfermedades.Keys)
                {
                    if (baseEnfermedades[enfermedad].ContainsKey(claveSintoma))
                    {
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    hayDesconocidos = true;
                }
            }

            return hayDesconocidos;
        }

        public int ObtenerTotalEntrenamientos()
        {
            return totalEntrenamientos;
        }

        public int ObtenerNumeroEnfermedades()
        {
            return baseEnfermedades.Count;
        }
    }

    // Resultado de predicción
    public class ResultadoIA
    {
        public string diagnostico;
        public float confianza;
        public string mensaje;
        public string descripcion;
        public string recomendacion;

        public ResultadoIA()
        {
            diagnostico = "";
            confianza = 0.0f;
            mensaje = "";
            descripcion = "";
            recomendacion = "";
        }

        // Muestra el resultado de la prediccion de forma sencilla
        public void Mostrar()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("DIAGNOSTICO IA (NAIVE BAYES)");
            Console.WriteLine("========================================");
            Console.WriteLine("Diagnostico: " + diagnostico);
            Console.WriteLine("Confianza aproximada: " + (confianza * 100).ToString("F1") + "%");

            if (!string.IsNullOrEmpty(descripcion))
            {
                Console.WriteLine("Descripcion: " + descripcion);
            }
            if (!string.IsNullOrEmpty(recomendacion))
            {
                Console.WriteLine("Recomendacion: " + recomendacion);
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                Console.WriteLine(mensaje);
            }

            Console.WriteLine("========================================");
        }
    }
}
