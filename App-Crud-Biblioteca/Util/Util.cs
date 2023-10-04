using App_Crud_Biblioteca.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca.Util
{
    internal class Util
    {
        public static int Menu()
        {

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔════════════════════════════════════╗");
                Console.WriteLine("\t\t║           Menú Múltiplos           ║");
                Console.WriteLine("\t\t╠════════════════════════════════════╣");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   1) Registrar Libro               ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   2) Listado de libros             ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   3) Eliminar libro                ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   4) Modificar libro               ║");
                Console.WriteLine("\t\t║____________________________________║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║           0) Salir                 ║");
                Console.WriteLine("\t\t╚════════════════════════════════════╝");

                Console.Write("\t\tIntroduce una opción: ");
                opcion = Console.ReadKey().KeyChar - '0';

                // Comprobamos que se ha pulsado una opción correcta
                while (opcion < 0 || opcion > 4)
                {
                    Console.WriteLine("\n\t\t\t*ERROR*");
                    Console.Write("\n\n\tIntroduce una opción: ");
                    opcion = Console.ReadKey().KeyChar - '0';
                }
                Console.Clear();

                return opcion;

            } while (opcion != 0);

        }

        public static int MenuUpdate()
        {

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔════════════════════════════════════╗");
                Console.WriteLine("\t\t║           Menú Múltiplos           ║");
                Console.WriteLine("\t\t╠════════════════════════════════════╣");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   1) Modificar titulo              ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   2) Modificar autor               ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   3) Modificar ISBN                ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   4) Modificar edicion             ║");
                Console.WriteLine("\t\t║____________________________________║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║           0) Salir                 ║");
                Console.WriteLine("\t\t╚════════════════════════════════════╝");

                Console.Write("\t\tIntroduce una opción: ");
                opcion = Console.ReadKey().KeyChar - '0';

                // Comprobamos que se ha pulsado una opción correcta
                while (opcion < 0 || opcion > 4)
                {
                    Console.WriteLine("\n\t\t\t*ERROR*");
                    Console.Write("\n\n\tIntroduce una opción: ");
                    opcion = Console.ReadKey().KeyChar - '0';
                }
                Console.Clear();

                return opcion;

            } while (opcion != 0);

        }
        public static void Pausa(string texto)
        {
            Console.WriteLine("\n\n\tPulsa una tecla para {0}", texto);
            Console.ReadKey();
        }

        //Metodo que genera un id aleatorio sin que se repitan ninguno.
        public static long IdAleatorio()
        {
            Random azar = new Random();
            List<int> numerosGenerados = new List<int>();

            int numeroAleatorio;
            do
            {
                numeroAleatorio = azar.Next(10, 101); // Genera un número entre 10 y 100
            } while (numerosGenerados.Contains(numeroAleatorio));

            numerosGenerados.Add(numeroAleatorio);
            return numeroAleatorio;

        }

        public static int CapturaEntero(string texto,int min,int max)
        {
            int respuesta;
            bool esCorrecto;

            do
            {
                Console.Write("\n\n\t{0}  [{1}--{2}]",texto,min,max);
                esCorrecto=Int32.TryParse(Console.ReadLine(),out respuesta);
                if (!esCorrecto)
                {
                    Console.WriteLine("\n\n\tError: El valor introducido no es un entero");
                }
                else if (respuesta < min || respuesta > max)
                {
                    Console.WriteLine("\n\n\tError:El valor introducido no esta en los limites");
                    esCorrecto=false;
                }

            }while(!esCorrecto);

            return respuesta;
        }
        public static bool PreguntaSiNo(string pregunta)
        {
            char tecla;

            do
            {
                // Hacemos la pregunta
                Console.Write("\n\n{0}", pregunta);
                // Capturamos la respuesta (una pulsación)
                tecla = (Console.ReadKey()).KeyChar;
                if (tecla == 's' || tecla == 'S')
                    return true;
                if (tecla == 'n' || tecla == 'N')
                    return false;
                // Si llega aquí es que no ha pulsado ninguna de las teclas correctas
                Console.Write("\n\n\t**ERROR** Por favor, responde S o N.");

            } while (true);

        }

    }





            


    }
    
