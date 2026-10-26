using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASO_SEMANA11_T6_
{
    internal class Program
    {
        static int max_estudiantes = 40;
        static int max_cursos = 4;
        static string[] nombreCursos = {"Matemáticas", "Programación", "Física", "Base de datos"};
        static string[] estudiante = new string[max_estudiantes];
        static double[,] matrizNotas = new double[max_estudiantes,max_cursos];
        static int total_estudiantes = 0;
        static string rutaArchivo = "notas_cursos.txt";

        public static void RegistrarEstudiante()
        {
            Console.WriteLine("REGISTRO DE ESTUDIANTE");
            if (total_estudiantes > max_estudiantes)
            {
                Console.WriteLine("Se alcanzo la capacidad maxima");
                return;
            }
            string nombre = Validador.LeerTexto("Ingresar nombres y apellidos:");
            estudiante[total_estudiantes] = nombre;
            Console.WriteLine("Ingresar las notas [0-20]: ");
            for (int i= 0; i <max_cursos; i++)
            {
                matrizNotas[total_estudiantes, i] = Validador.LeerDecimales($"Ingresar nota de {nombreCursos[i]}:");
            }
            total_estudiantes++;
            Console.WriteLine("Estudiante registrado..!");
        }
        public static void guardarArchivo()
        {
            Console.WriteLine("Guardar registros e archivo");
            if (total_estudiantes == 0)
            {
                Console.WriteLine("Sin Registron a guardar");
                return;
            }
            using (StreamWriter escritor = new StreamWriter(rutaArchivo))
            {
                for(int i =0; i<total_estudiantes; i++)
                {
                    string linea = estudiante[i];
                    for(int j=0; j < max_cursos; j++)
                    {
                        linea += "," + matrizNotas[i, j];
                        //linea += $"{matrizNotas[i, j]}";
                    }
                    escritor.WriteLine(linea);
                }
            }
            Console.WriteLine("Registros guardado!!");
        }
        static void Main(string[] args)
        {
        }
    }
}
