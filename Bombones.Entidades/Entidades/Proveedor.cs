using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Entidades.Entidades
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; } = null!;

        public string Telefono { get; set; }

        public string Email { get; set; }
    }
}
