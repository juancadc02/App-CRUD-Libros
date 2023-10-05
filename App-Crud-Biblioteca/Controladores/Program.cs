using App_Crud_Biblioteca.Dtos;
using App_Crud_Biblioteca.Servicios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca
{
    /// <summary>
    /// Clase principal de la aplicacion.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Metodo main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            conexionPostgres conexionPostgresInterfaz = new conexionPostgresImpl();
            servicioConsultas consultasPostgresInterfaz = new servicioConsultasImpl();
            NpgsqlConnection conexion = null;
            conexion = conexionPostgresInterfaz.generarConexionPostgresql();
            int opcion;
            try
            {
                


                if (conexion != null)
                {
                    do
                    {
                        opcion = Util.Util.Menu();

                        switch (opcion)
                        {
                            case 1: //Registrar empleado 
                                consultasPostgresInterfaz.añadirLibro(conexion);
                                break;

                            case 2:
                                //Cabezera

                                List<LibrosDto> listaDeLibros = new List<LibrosDto>();
                                listaDeLibros = consultasPostgresInterfaz.listarTodoLosLibros(conexion);
                                Console.WriteLine("\n\tId    Titulo    Autor   Isbn   Edicion");
                                consultasPostgresInterfaz.mostrarListado(listaDeLibros);
                               

                                break;

                            case 3://Eliminar libros

                                listaDeLibros = consultasPostgresInterfaz.listarTodoLosLibros(conexion);
                                Console.WriteLine("\n\tId    Titulo    Autor   Isbn   Edicion");
                                consultasPostgresInterfaz.mostrarListado(listaDeLibros);
                                consultasPostgresInterfaz.eliminarLibro(conexion);
                                break;
                            case 4:
                                //Modificar libros
                                do
                                {
                                    opcion = Util.Util.MenuUpdate();

                                    switch (opcion)
                                    {

                                        case 1:

                                            do
                                            {
                                                //Mostramos todos los libros
                                                listaDeLibros = consultasPostgresInterfaz.listarTodoLosLibros(conexion);
                                                Console.WriteLine("\n\tId    Titulo    Autor   Isbn   Edicion");
                                                consultasPostgresInterfaz.mostrarListado(listaDeLibros);
                                                List<long> listaId = new List<long>();
                                                for (int i = 0; i < listaDeLibros.Count; i++)
                                                {
                                                    listaId.Add(listaDeLibros[i].Id_libro);
                                                }
                                                //Modificamos el que elegimos
                                                Console.Write("\n\n\t¿Elige el id del libro que quieres modificar?");
                                                string id = Console.ReadLine();
                                                for (int i = 0; i < listaId.Count; i++)
                                                {

                                                }
                                                Console.Write("\n\n\tIntroduce el nuevo titulo: ");
                                                string nuevoTitulo = Console.ReadLine();
                                                consultasPostgresInterfaz.modificarTitulo(Convert.ToInt32(id), nuevoTitulo, conexion);



                                            } while (Util.Util.PreguntaSiNo("¿Quieres modificar otro titulo? (S=Si - N=No)"));

                                            break;

                                        case 2:


                                            do
                                            {
                                                //Mostramos todos los libros
                                                listaDeLibros = consultasPostgresInterfaz.listarTodoLosLibros(conexion);
                                                Console.WriteLine("\n\tId    Titulo    Autor   Isbn   Edicion");
                                                consultasPostgresInterfaz.mostrarListado(listaDeLibros);

                                                //Modificamos el que elegimos
                                                Console.Write("\n\n\t¿Elige el id del libro que quieres modificar?");
                                                string id = Console.ReadLine();
                                                Console.Write("\n\n\tIntroduce el nuevo autor: ");
                                                string nuevoAutor = Console.ReadLine();
                                                consultasPostgresInterfaz.modificarAutor(Convert.ToInt32(id), nuevoAutor, conexion);



                                            } while (Util.Util.PreguntaSiNo("¿Quieres modificar otro autor? (S=Si - N=No)"));
                                            break;

                                        case 3:


                                            do
                                            {
                                                //Mostramos todos los libros
                                                listaDeLibros = consultasPostgresInterfaz.listarTodoLosLibros(conexion);
                                                Console.WriteLine("\n\tId    Titulo    Autor   Isbn   Edicion");
                                                consultasPostgresInterfaz.mostrarListado(listaDeLibros);

                                                //Modificamos el que elegimos
                                                Console.Write("\n\n\t¿Elige el id del libro que quieres modificar?");
                                                string id = Console.ReadLine();
                                                Console.Write("\n\n\tIntroduce el nuevo ISBN: ");
                                                string nuevoIsbn = Console.ReadLine();
                                                consultasPostgresInterfaz.modificarIsbn(Convert.ToInt32(id), nuevoIsbn, conexion);



                                            } while (Util.Util.PreguntaSiNo("¿Quieres modificar otro ISBN? (S=Si - N=No)"));
                                            break;

                                        case 4:


                                            do
                                            {
                                                //Mostramos todos los libros
                                                listaDeLibros = consultasPostgresInterfaz.listarTodoLosLibros(conexion);
                                                Console.WriteLine("\n\tId    Titulo    Autor   Isbn   Edicion");
                                                consultasPostgresInterfaz.mostrarListado(listaDeLibros);

                                                //Modificamos el que elegimos
                                                Console.Write("\n\n\t¿Elige el id del libro que quieres modificar?");
                                                string id = Console.ReadLine();
                                                Console.Write("\n\n\tIntroduce la nueva edicion: ");
                                                string nuevaEdicion = Console.ReadLine();
                                                consultasPostgresInterfaz.modificarEdicion(Convert.ToInt32(id), nuevaEdicion, conexion);



                                            } while (Util.Util.PreguntaSiNo("¿Quieres modificar otra edición? (S=Si - N=No)"));
                                            break;

                                        case 0:
                                            Console.WriteLine("Has salido del Menu");
                                            Console.ReadLine();

                                            break;
                                    }
                                } while (opcion != 0);

                                break;

                            case 0:
                                Console.WriteLine("\n\n\tHas salido de la apliación.");
                                Console.ReadKey(true);
                                break;

                        }
                        if (opcion != 0)
                            Util.Util.Pausa("para salir");

                    } while (opcion != 0);


                }
                else
                {
                    conexion = null;
                    Console.WriteLine("No hay conexion");
                }

            }catch(Exception ex)
            {
                Console.WriteLine("\n\n\t Se ha producido un error desconocido: "+ ex);
                conexion = null;
            }




        }
    }
}
