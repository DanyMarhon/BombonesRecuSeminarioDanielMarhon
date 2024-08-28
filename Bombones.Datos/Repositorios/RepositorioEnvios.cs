using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Datos.Repositorios
{
    public class RepositorioEnvios : IRepositorioEnvios
    {
        public RepositorioEnvios()
        {
        }

        public List<Envio> GetLista(SqlConnection conn, SqlTransaction? tran = null)
        {
            var selectQuery = @"SELECT * FROM 
                        Envios ORDER BY Nombre";
            return conn.Query<Envio>(selectQuery).ToList();
        }
    }
}