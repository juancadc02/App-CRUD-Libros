using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca.Servicios
{
    /// <summary>
    /// Interfaz que define los métodos para generar conexiones a base de datos
    /// 270923-rfg
    /// </summary>
    internal interface conexionPostgres
    {
        /// <summary>
        /// Metodo para generar la conexion con la base de datos.
        /// </summary>
        /// <returns></returns>
         NpgsqlConnection generarConexionPostgresql();
    }
}
