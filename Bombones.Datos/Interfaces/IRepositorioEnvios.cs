using Bombones.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Datos.Interfaces
{
    public interface IRepositorioEnvios
    {
        List<Envio> GetLista(SqlConnection conn, SqlTransaction? tran = null);
    }
}
