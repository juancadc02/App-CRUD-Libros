using App_Crud_Biblioteca.Dtos;
using App_Crud_Biblioteca.Util;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca.Servicios
{
    /// <summary>
    /// Implementación de la interfaz de consultas.
    /// </summary>
    internal class servicioConsultasImpl : servicioConsultas
    {
        public void añadirLibro( NpgsqlConnection conexion)
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            long idLibro = Util.Util.IdAleatorio();
            Console.Write("\n\n\tIntroduce el titulo del nuevo libro:");
            string tituloLibro = Console.ReadLine();
            Console.Write("\n\n\tIntroduce el autor del libro: ");
            string autor=Console.ReadLine();
            Console.Write("\n\n\tIntroduce el isbn del libro: ");
            string isbn=Console.ReadLine();
            int edicionLibro = Util.Util.CapturaEntero("Introduce la edicion del libro:", 1, 100);

            LibrosDto nuevoLibro = new LibrosDto(idLibro, tituloLibro, autor, isbn, Convert.ToInt32(edicionLibro));
           
                try
                {


                    string sqlQuery = "INSERT INTO gbp_alm_cat_libros (id_libros, titulo, autor, isbn, edicion) VALUES (@id, @titulo, @autor, @isbn, @edicion)";

                    using (NpgsqlCommand comando = new NpgsqlCommand(sqlQuery, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, nuevoLibro.Id_libro);
                        comando.Parameters.AddWithValue("@titulo", NpgsqlDbType.Text, nuevoLibro.Titulo);
                        comando.Parameters.AddWithValue("@autor", NpgsqlDbType.Text, nuevoLibro.Autor);
                        comando.Parameters.AddWithValue("@isbn", NpgsqlDbType.Text, nuevoLibro.Isbn);
                        comando.Parameters.AddWithValue("@edicion", NpgsqlDbType.Integer, nuevoLibro.Edicion);

                        comando.ExecuteNonQuery();

                    }
                    conexion.Close();

                Console.WriteLine("\n\n\tLibro insertado: Titulo:{0}  Autor:{1}  ISBN:{2}  Edicion:{3}",nuevoLibro.Titulo,nuevoLibro.Autor,nuevoLibro.Isbn,nuevoLibro.Edicion);

                }
                catch (NpgsqlException npsqlEx)
                {
                    Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                    conexion = null;
                }catch(FormatException formatEx)
                {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico "+ formatEx);
                conexion = null;
                }
                catch(Exception ex)
                {
                Console.WriteLine("\n\n\tSe ha producido un error desconocido:  " + ex);
                }
                finally
                {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                }
            
            

        }

        public void eliminarLibro(NpgsqlConnection conexion)
        {

            List<long> listaId;
            listaId = listarTodosLosId(conexion);
            string id;
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                while (true)
                {
                    Console.WriteLine("\n\n\t¿Qué libro quieres eliminar (idLibro)?");
                    id = Console.ReadLine();

                    
                    //Comprueba a ver si lo ingresado lo puede pasar a long.
                    //Si puede convertirlo a los lo almacena en idIngresado,si no se va al else.
                    if (long.TryParse(id, out long idIngresado))
                    {
                        // Comprueba si el idIngresado está en la lista.
                        //En el caso de que este ejecuta la consulta.
                        if (listaId.Contains(idIngresado))
                        {
                            string sqlQuery = "DELETE FROM gbp_alm_cat_libros WHERE id_libros = @id_libros";

                            using (NpgsqlCommand comando = new NpgsqlCommand(sqlQuery, conexion))
                            {
                                comando.Parameters.AddWithValue("@id_libros", NpgsqlDbType.Integer, int.Parse(id)); // Convierte el ID a entero

                                comando.ExecuteNonQuery();                             
                            }
                            break; // Sale del bucle
                        }
                        else
                        {
                            Console.WriteLine("El ID ingresado no está en la lista. Intente nuevamente.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada no válida. Por favor, ingrese un número válido.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);
            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public List<LibrosDto> listarTodoLosLibros(NpgsqlConnection conexion)
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            ADto aDto = new ADto();
            List<LibrosDto> listaDeLibros = new List<LibrosDto>();

            try
            {
                //Si dejo puesto el conexion.open() me da error al ejecutar.
                // conexion.Open();
                string tableName = "gbp_alm_cat_libros";
                string sqlQuery = $"SELECT * FROM {tableName}";

                using (NpgsqlCommand comando = new NpgsqlCommand(sqlQuery, conexion))
                {
                    NpgsqlDataReader resultadoConsulta = comando.ExecuteReader();

                    //Pasamos el DataReader a la lista 

                    listaDeLibros = aDto.readerALibroDto(resultadoConsulta);
                    Console.WriteLine("[INFORMACIÓN-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Cierre conexión y conjunto de datos");
                    conexion.Close();
                    resultadoConsulta.Close();
                }
            } catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);


            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return listaDeLibros;
        }

        public void mostrarListado(List<LibrosDto> listaLibros)
       
        {
            for(int i =0; i < listaLibros.Count;i++)
            {
                Console.WriteLine("\t{0}    {1}      {2}    {3}     {4}", listaLibros[i].Id_libro, listaLibros[i].Titulo, listaLibros[i].Autor, listaLibros[i].Isbn , listaLibros[i].Edicion);
            }
        }


        #region Metodos UPDATE 

        public void modificarTitulo(int idLibro, string nuevoTitulo, NpgsqlConnection conexion)
        {
            //Comprobamos si la conexion esta abierta o no, y en el caso de que no lo este la abrimos.
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            // Primero, obtén los datos actuales del libro
            try
            {
                LibrosDto libroModificar = new LibrosDto();
                string consulta = "SELECT * FROM gbp_alm_cat_libros WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    //Le pasamos el parametro del idLibro para que haga
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    //Obtenemos los resultado de la consulta y se lo accionamos a cada propiedad.
                    using (NpgsqlDataReader reader = comando.ExecuteReader())
                    {
                        //Verificamos si hemos encontrado el libro con ese id
                        if (reader.HasRows)
                        {
                            reader.Read();
                            //Asignamos los distintos valores a los campos de libro.
                            libroModificar.Id_libro = idLibro;
                            libroModificar.Titulo = reader["Titulo"].ToString();
                            libroModificar.Autor = reader["Autor"].ToString();
                            libroModificar.Isbn = reader["isbn"].ToString();

                            reader.Close();
                        }
                    }
                }


                // Ponemos el nuevo título en el objeto libroModificar
                libroModificar.Titulo = nuevoTitulo;

                // Consulta de actualización para modificar el título en la base de datos
                string consultaActualizacion = "UPDATE gbp_alm_cat_libros SET Titulo = @nuevoTitulo WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaActualizacion, conexion))
                {
                    comando.Parameters.AddWithValue("@nuevoTitulo", nuevoTitulo);
                    comando.Parameters.AddWithValue("@idLibro", idLibro);
                    // Ejecuta la consulta de actualización
                    comando.ExecuteNonQuery();

                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);
            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }



        }

        public void modificarAutor(int idLibro, string nuevoAutor, NpgsqlConnection conexion)
        {
            //Comprobamos si la conexion esta abierta o no, y en el caso de que no lo este la abrimos.
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            try
            {
                // Primero, obtén los datos actuales del libro
                LibrosDto libroModificar = new LibrosDto();
                string consulta = "SELECT * FROM gbp_alm_cat_libros WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    //Le pasamos el parametro del idLibro para que haga
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    //Obtenemos los resultado de la consulta y se lo accionamos a cada propiedad.
                    using (NpgsqlDataReader reader = comando.ExecuteReader())
                    {
                        //Verificamos si hemos encontrado el libro con ese id
                        if (reader.HasRows)
                        {
                            reader.Read();
                            //Asignamos los distintos valores a los campos de libro.
                            libroModificar.Id_libro = idLibro;
                            libroModificar.Titulo = reader["Titulo"].ToString();
                            libroModificar.Autor = reader["Autor"].ToString();
                            libroModificar.Isbn = reader["isbn"].ToString();

                            reader.Close();
                        }

                    }
                }


                // Ponemos el nuevo autor en el objeto libroModificar
                libroModificar.Autor = nuevoAutor;

                // Consulta de actualización para modificar el autor en la base de datos
                string consultaActualizacion = "UPDATE gbp_alm_cat_libros SET Autor = @nuevoAutor WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaActualizacion, conexion))
                {
                    comando.Parameters.AddWithValue("@nuevoAutor", nuevoAutor);
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    // Ejecuta la consulta de actualización
                    comando.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);
            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

           
        }

        public void modificarIsbn(int idLibro, string nuevoIsbn, NpgsqlConnection conexion)
        {
            //Comprobamos si la conexion esta abierta o no, y en el caso de que no lo este la abrimos.
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }

            try
            {
                // Primero, obtén los datos actuales del libro
                LibrosDto libroModificar = new LibrosDto();
                string consulta = "SELECT * FROM gbp_alm_cat_libros WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    //Le pasamos el parametro del idLibro para que haga
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    //Obtenemos los resultado de la consulta y se lo accionamos a cada propiedad.
                    using (NpgsqlDataReader reader = comando.ExecuteReader())
                    {
                        //Verificamos si hemos encontrado el libro con ese id
                        if (reader.HasRows)
                        {
                            reader.Read();
                            //Asignamos los distintos valores a los campos de libro.
                            libroModificar.Id_libro = idLibro;
                            libroModificar.Titulo = reader["Titulo"].ToString();
                            libroModificar.Autor = reader["Autor"].ToString();
                            libroModificar.Isbn = reader["isbn"].ToString();

                            reader.Close();
                        }
                    }
                }


                // Ponemos el nuevo autor en el objeto libroModificar
                libroModificar.Isbn = nuevoIsbn;

                // Consulta de actualización para modificar el autor en la base de datos
                string consultaActualizacion = "UPDATE gbp_alm_cat_libros SET isbn = @nuevoIsbn WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaActualizacion, conexion))
                {
                    comando.Parameters.AddWithValue("@nuevoIsbn", nuevoIsbn);
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    // Ejecuta la consulta de actualización
                    comando.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);
            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            
        }

        public void modificarEdicion(int idLibro, string nuevaEdicion, NpgsqlConnection conexion)
        {
            //Comprobamos si la conexion esta abierta o no, y en el caso de que no lo este la abrimos.
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            try
            {
                // Primero, obtén los datos actuales del libro
                LibrosDto libroModificar = new LibrosDto();
                string consulta = "SELECT * FROM gbp_alm_cat_libros WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                {
                    //Le pasamos el parametro del idLibro para que haga
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    //Obtenemos los resultado de la consulta y se lo accionamos a cada propiedad.
                    using (NpgsqlDataReader reader = comando.ExecuteReader())
                    {
                        //Verificamos si hemos encontrado el libro con ese id
                        if (reader.HasRows)
                        {
                            reader.Read();
                            //Asignamos los distintos valores a los campos de libro.
                            libroModificar.Id_libro = idLibro;
                            libroModificar.Titulo = reader["Titulo"].ToString();
                            libroModificar.Autor = reader["Autor"].ToString();
                            libroModificar.Isbn = reader["isbn"].ToString();

                            reader.Close();
                        }
                    }
                }
                // Ponemos el nuevo autor en el objeto libroModificar
                libroModificar.Autor = nuevaEdicion;

                // Consulta de actualización para modificar el autor en la base de datos
                string consultaActualizacion = "UPDATE gbp_alm_cat_libros SET edicion = @nuevaEdicion WHERE id_libros = @idLibro";

                using (NpgsqlCommand comando = new NpgsqlCommand(consultaActualizacion, conexion))
                {
                    comando.Parameters.AddWithValue("@nuevaEdicion", nuevaEdicion);
                    comando.Parameters.AddWithValue("@idLibro", idLibro);

                    // Ejecuta la consulta de actualización
                    comando.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);
            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            
        }

        public List<long> listarTodosLosId(NpgsqlConnection conexion)
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            
            List<long> listaId = new List<long>();

            try
            {
                //Si dejo puesto el conexion.open() me da error al ejecutar.
                // conexion.Open();
                string tableName = "gbp_alm_cat_libros";
                string sqlQuery = $"SELECT id_libros FROM {tableName}";

                using (NpgsqlCommand comando = new NpgsqlCommand(sqlQuery, conexion))
                {
                    NpgsqlDataReader resultadoConsulta = comando.ExecuteReader();

                    //Pasamos el DataReader a la lista 

                    while (resultadoConsulta.Read())
                    {
                        long idLibro = resultadoConsulta.GetInt64(0); // Suponiendo que la columna sea de tipo long en la base de datos
                        listaId.Add(idLibro);
                    }
                    Console.WriteLine("[INFORMACIÓN-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Cierre conexión y conjunto de datos");
                    conexion.Close();
                    resultadoConsulta.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error en la consulta SQL: " + sqlEx);
            }
            catch (NpgsqlException npsqlEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error relaccionado con la base de datos" + npsqlEx);
                conexion = null;
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine("\n\n\tSe ha producido un error al convertir un valor a un tipo de dato específico " + formatEx);
                conexion = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n\tSe ha producido un error: " + ex);


            }
            finally
            {
                // Cierra la conexión si es necesario
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return listaId;
        
    }

      


        #endregion
    }

}


