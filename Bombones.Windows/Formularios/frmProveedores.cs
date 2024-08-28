using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using Bombones.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Bombones.Windows.Formularios
{
    public partial class frmProveedores : Form
    {
        private readonly IServiceProvider? _serviceProvider;
        private readonly IServiciosProveedores? _servicio;
        private List<Proveedor>? lista;
        public frmProveedores(IServiceProvider? serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _servicio = serviceProvider?.GetService<IServiciosProveedores>()
                ?? throw new ApplicationException("Dependencias no cargadas!!!"); ;
        }

        private void frmProveedores_Load(object sender, EventArgs e)
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
            frmProveedoresAE frm = new frmProveedoresAE() { Text = "Agregar proveedor" };
            DialogResult dr = frm.ShowDialog(this);
            try
            {
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                Proveedor? proveedor = frm.GetProveedor();
                if (proveedor is not null)
                {
                    if (!_servicio!.Existe(proveedor))
                    {
                        _servicio!.Guardar(proveedor);
                        DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                        GridHelper.SetearFila(r, proveedor);
                        GridHelper.AgregarFila(r, dgvDatos);
                        MessageBox.Show("Registro agregado",
                            "Mensaje",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Entrada duplicada!!!",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            if (r.Tag is null) return;

            var proveedor = (Proveedor)r.Tag;

            try
            {
                DialogResult dr = MessageBox.Show($@"¿Desea dar de baja el proveedor {proveedor.NombreProveedor}?",
                        "Confirmar Baja",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }
                _servicio!.Borrar(proveedor.ProveedorId);
                GridHelper.QuitarFila(r, dgvDatos);
                MessageBox.Show("Registro eliminado",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

            }


        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Proveedor? proveedor = (r.Tag as Proveedor) ?? new Proveedor();
            frmProveedoresAE frm = new frmProveedoresAE() { Text = "Editar Proveedor" };
            frm.SetProveedor(proveedor);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                proveedor = frm.GetProveedor();
                if (proveedor is null) return;
                if (!_servicio!.Existe(proveedor))
                {
                    _servicio!.Guardar(proveedor);

                    GridHelper.SetearFila(r, proveedor);
                    MessageBox.Show("Registro modificado",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Entrada duplicada!!!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

            }
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
