using App_Crud_Biblioteca.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Crud_Biblioteca.Util
{
    internal class ADto
    {
        public List<LibrosDto> readerALibroDto(NpgsqlDataReader resultadoConsulta)
        {
            List<LibrosDto> listaLibros = new List<LibrosDto>();
            while (resultadoConsulta.Read())
            {
                listaLibros.Add(new LibrosDto(
                    long.Parse(resultadoConsulta[0].ToString()),
                    resultadoConsulta[1].ToString(),
                    resultadoConsulta[2].ToString(),
                    resultadoConsulta[3].ToString(),
                    (int)Int64.Parse(resultadoConsulta[4].ToString())
                    )
                    );

            }
            return listaLibros;
        }
    }
}
