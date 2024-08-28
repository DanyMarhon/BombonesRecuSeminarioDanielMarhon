using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Entidades.Entidades
{
    public class Envio
    {
        public int EnvioId { get; set; }
        public string Cobertura { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EnviosAereos { get; set; }
        public string Direccion { get; set; } = null!;
        public int CiudadId { get; set; }
        public int ProvinciaEstadoID { get; set; }
        public int PaisID { get; set; }

    }
}
