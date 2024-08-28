using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using Bombones.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Bombones.Windows.Formularios
{
    public partial class frmEnvios : Form
    {
        private readonly IServiceProvider? _serviceProvider;
        private readonly IServiciosEnvios? _servicio;
        private List<Envio>? lista;

        public frmEnvios(IServiceProvider? serviceProvider)
        {
            InitializeComponent();
            if (serviceProvider == null)
            {
                throw new ApplicationException("Dependencias no cargadas");
            }
            _serviceProvider = serviceProvider;
            _servicio = serviceProvider?.GetService<IServiciosEnvios>()
                ?? throw new ApplicationException("Dependencias no cargadas!!!"); ;
        }

        private void frmEnvios_Load(object sender, EventArgs e)
        {
            try
            {
                lista = _servicio!.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            if (lista is not null)
            {
                foreach (var item in lista)
                {
                    var r = GridHelper.ConstruirFila(dgvDatos);
                    GridHelper.SetearFila(r, item);
                    GridHelper.AgregarFila(r, dgvDatos);
                }

            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {

        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {

        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {

        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
