using Bombones.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombones.Windows.Formularios
{
        public partial class frmProveedoresAE : Form
        {
            private Proveedor? proveedor;
            public frmProveedoresAE()
            {
                InitializeComponent();
            }

            public Proveedor? GetProveedor()
            {
                return proveedor;
            }

            public void SetProveedor(Proveedor? proveedor)
            {
                this.proveedor = proveedor;
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                if (proveedor != null)
                {
                    txtProveedor.Text = proveedor.NombreProveedor;
                }
            }


            private void btnCancelar_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
            }

            private void btnOk_Click(object sender, EventArgs e)
            {
                if (ValidarDatos())
                {
                    if (proveedor == null)
                    {
                        proveedor = new Proveedor();
                    }
                    proveedor.NombreProveedor = txtProveedor.Text;

                    DialogResult = DialogResult.OK;
                }
            }

            private bool ValidarDatos()
            {
                bool valido = true;
                errorProvider1.Clear();


                if (string.IsNullOrEmpty(txtProveedor.Text))
                {
                    valido = false;
                    errorProvider1.SetError(txtProveedor, "El proveedor es requerido");

                }
                return valido;
            }
        }
    }
