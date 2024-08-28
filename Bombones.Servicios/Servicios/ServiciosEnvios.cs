using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using System.Data.SqlClient;

namespace Bombones.Servicios.Servicios
{
    public class ServiciosEnvios : IServiciosEnvios
    {
        private readonly IRepositorioEnvios? _repositorio;
        private readonly string? _cadena;
        public ServiciosEnvios(IRepositorioEnvios? repositorio, string? cadena)
        {
            _repositorio = repositorio ?? throw new ApplicationException("Dependencias no cargadas!!!"); ;
            _cadena = cadena;
        }

        public List<Envio> GetLista()
        {
            using (var conn = new SqlConnection(_cadena))
            {
                return _repositorio!.GetLista(conn);

            }
        }
    }

}
