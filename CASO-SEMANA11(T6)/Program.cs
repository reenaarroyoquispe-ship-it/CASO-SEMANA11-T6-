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
        private static int i;

        public static void RegistrarEstudiante()
        {
            Console.WriteLine("REGISTRO DE ESTUDIANTE");
            if (total_estudiantes > max_estudiantes)
            {
                Console.WriteLine("Se alcanzo la capacidad maxima");
                return;
            }
            string nombre = Validador.LeerTexto("Ingresar nombres y apellidos:").ToUpper();
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
                    }
                    escritor.WriteLine(linea);
                }
            }
            Console.WriteLine("Registros guardado!!");
        }
        public static void CargarRegistro()
        {
            Console.WriteLine("Cargar registroa desde archivo");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No existe el archivo");
                return;
            }
            total_estudiantes = 0;
            using(StreamReader lector= new StreamReader(rutaArchivo))
            {
                string linea;
                while((linea=lector.ReadLine()) !=null && total_estudiantes < max_estudiantes)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == max_cursos + 1)
                    {
                        estudiante[total_estudiantes] = partes[0];
                        for (int i = 0;  i< max_cursos; i++)
                        {
                            matrizNotas[total_estudiantes, i] = Convert.ToDouble(partes[i + 1]);
                        }
                        total_estudiantes++;
                    }
                }
            }
            Console.WriteLine("Se cargaron los registros del archivo...!!");
        }
        public static void MostrarRegistros()
        {
            Console.WriteLine(new string('=', 100));
            Console.WriteLine("\t\tREGISTRO GENERAL DE NOTAS POR CURSO");
            Console.WriteLine(new string('=', 100));
            if (total_estudiantes == 0)
            {
                Console.WriteLine("No hay registros para mostrar");
                return;
            }
            Console.Write($"{"Estudiante",-25}|");
            for (int i = 0; i < max_cursos; i++)
            {
                Console.Write($"{nombreCursos[i],-12}|");
            }
            Console.WriteLine("PROMEDIO");
            Console.WriteLine(new string('-', 100));
            for (int i = 0; i < total_estudiantes; i++)
            {
                Console.Write($"{estudiante[i],-25}|");
                double suma = 0;
                for (int j = 0; j < max_cursos; j++)
                {
                    Console.Write($"{matrizNotas[i, j],-12:F1}|");
                    suma += matrizNotas[i, j];
                }
                double promedio = suma / max_cursos;
                string estado = promedio >= 12 ? "Aprobado" : "Reprobado";
                Console.WriteLine($"{promedio,-12:F2}({estado})");

            }
        }
        static void Main(string[] args)
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.Clear();
                Console.WriteLine("[1] Registrar estudiante");
                Console.WriteLine("[2] Guardar en archivo");
                Console.WriteLine("[3] Cargar registros desde archivo");
                Console.WriteLine("[4] Mostrar registros");
                Console.WriteLine("[5] Salir");
                opc = Validador.LeerEntero("Seleccionar opciom[1-5]: ");
                switch (opc)
                {
                    case 1:
                        RegistrarEstudiante();
                        break;
                    case 2:
                        guardarArchivo();
                        break;
                    case 3:
                        CargarRegistro();
                        break;
                    case 4:
                        MostrarRegistros();
                        break;
                    case 5:
                        Console.WriteLine("MUCHOS EXITOS PARA EL NUEVO SEMESTRE...");
                        break;
                }
                Console.ReadKey();

            }
        }
    }
}
