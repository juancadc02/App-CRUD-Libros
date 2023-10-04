using App_Crud_Biblioteca.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca.Servicios
{
    internal interface servicioConsultas
    {
        List<LibrosDto> listarTodoLosLibros(NpgsqlConnection conexion);
        void añadirLibro( NpgsqlConnection conexion);
        void mostrarListado(List<LibrosDto> listaLibros);
        void eliminarLibro(NpgsqlConnection conexion);

        #region Metodos UPDATE
        void modificarTitulo(int id,string nombre,NpgsqlConnection conexion);
        void modificarAutor(int id, string autor, NpgsqlConnection conexion);
        void modificarIsbn(int id, string isbn, NpgsqlConnection conexion);
        void modificarEdicion(int id, string edicion, NpgsqlConnection conexion);
        #endregion


    }
}
