using App_Crud_Biblioteca.Dtos;
using Npgsql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca.Servicios
{
    /// <summary>
    /// Interfaz que define los metodos de consulta con la base de datos.
    /// </summary>
    internal interface servicioConsultas
    {
        /// <summary>
        /// Metodo que devuelve una lista con todos los datos de la base de datos.
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibrosDto> listarTodoLosLibros(NpgsqlConnection conexion);
        /// <summary>
        /// Metodo que inserta los nuevos libros en la base de datos.
        /// </summary>
        /// <param name="conexion"></param>
        void añadirLibro( NpgsqlConnection conexion);
        /// <summary>
        /// Metodo que muestra la lista de todos los datos de la base de datos 
        /// </summary>
        /// <param name="listaLibros"></param>
        void mostrarListado(List<LibrosDto> listaLibros);
        /// <summary>
        /// Metodo que eliminar un libro de la base de datos, por el id del libro.
        /// </summary>
        /// <param name="conexion"></param>
        void eliminarLibro(NpgsqlConnection conexion);
        List<long> listarTodosLosId(NpgsqlConnection conexion);

        void MostrarId(List<long> listaId);
        #region Metodos UPDATE

        /// <summary>
        /// Metodo que modifica el titulo de un libro segun el id dado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="conexion"></param>
        void modificarTitulo(int id,string nombre,NpgsqlConnection conexion);

        /// <summary>
        /// Metodo que modifica el autor de un libro segun el id dado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autor"></param>
        /// <param name="conexion"></param>
        void modificarAutor(int id, string autor, NpgsqlConnection conexion);
        /// <summary>
        /// Metodo que modifica el isbn de un libro segun el id dado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isbn"></param>
        /// <param name="conexion"></param>
        void modificarIsbn(int id, string isbn, NpgsqlConnection conexion);
        /// <summary>
        /// Metodo que modifica la edicion de un libro segun el id dado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edicion"></param>
        /// <param name="conexion"></param>
        void modificarEdicion(int id, string edicion, NpgsqlConnection conexion);
        #endregion


    }
}
