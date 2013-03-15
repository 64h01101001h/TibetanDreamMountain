using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionBancariaWindows.GestionBancariaWS;

namespace GestionBancariaWindows
{
    public partial class ListarSucursales : Form
    {
        public ListarSucursales()
        {
            InitializeComponent();
        }

        private void ListarSucursales_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Sucursal[] clientes = serv.ListarSucursales();

                SucursalesbindingSource.DataSource = clientes;
                lvSucursales.DataSource = SucursalesbindingSource;
                bindingNavigator1.BindingSource = SucursalesbindingSource;


            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void lvSucursales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    int idSucursal;
                    if (Int32.TryParse(Convert.ToString(lvSucursales.Rows[e.RowIndex].Cells[0].Value), out idSucursal))
                    {
                        ServiceGestionBancaria serv = new ServiceGestionBancaria();

                        Sucursal c = new Sucursal { IDSUCURSAL = idSucursal };

                        c = (Sucursal)serv.BuscarSucursal(c);
                        NuevaSucursal nu = new NuevaSucursal { SUCURSAL = c };

                        nu.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                lvSucursales.Rows.Clear();

                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Sucursal[] clientes = serv.ListarSucursales();

                SucursalesbindingSource.DataSource = clientes;
                lvSucursales.DataSource = SucursalesbindingSource;
                bindingNavigator1.BindingSource = SucursalesbindingSource;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }


    }
}
