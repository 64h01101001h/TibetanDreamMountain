using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logica;
using Entidades;
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
                LogicaSucursal lu = new LogicaSucursal();
                List<Sucursal> clientes = lu.ListarSucursales();

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
                        LogicaSucursal lu = new LogicaSucursal();

                        Sucursal c = new Sucursal { IDSUCURSAL = idSucursal };

                        c = (Sucursal)lu.BuscarSucursal(c);
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

                LogicaSucursal lu = new LogicaSucursal();
                List<Sucursal> clientes = lu.ListarSucursales();

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
