using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace App_Crud_Biblioteca.Servicios
{
    /// <summary>
    /// Implementación de la interfaz de conexión a postgresql
    /// 270923-rfg
    /// </summary>
    internal class conexionPostgresImpl : conexionPostgres
    {

        public NpgsqlConnection generarConexionPostgresql()
        {

            // Obtén la cadena de conexión del archivo app.config
            string connectionString = ConfigurationManager.ConnectionStrings["conexion-bbdd"].ConnectionString;
            Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-generarConexionPostgresql] Cadena conexión: " + connectionString);

            NpgsqlConnection conexion = null;
            string estado = "";

            //En este if comprobamos si el connectioString esta vacio o no.
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                try
                {
                    //Le pasamos la conexion y la abrimos.
                    conexion = new NpgsqlConnection(connectionString);
                    conexion.Open();
                    //Se obtiene el estado de conexión para informarlo por consola
                    estado = conexion.State.ToString();
                   //Comprobamos que la conexion esta abierta.
                    if (estado.Equals("Open"))
                    {

                        Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-generarConexionPostgresql] Estado conexión: " + estado);

                    }
                    //En el caso de que la conexion no este abierta la ponemos el null ya que es como controlamos nosotros la conexion.
                    else
                    {
                        conexion = null;
                    }
                }
                //Mostramos la excepcion producida y la volvemos a poner en null.
                catch (Exception e)
                {
                    Console.WriteLine("[ERROR-ConexionPostgresqlImplementacion-generarConexionPostgresql] Error al generar la conexión:" + e);
                    conexion = null;
                    return conexion;

                }
            }

            return conexion;
        }
    }
}
