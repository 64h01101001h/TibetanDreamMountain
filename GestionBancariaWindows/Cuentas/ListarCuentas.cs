using System.Windows.Forms;

using System.Collections.Generic;
using GestionBancariaWindows.GestionBancariaWS;

using System;

namespace GestionBancariaWindows
{
    public partial class ListarCuentas : Form
    {
        public ListarCuentas()
        {
            InitializeComponent();
        }


        private void ListarCuentas_Load(object sender, System.EventArgs e)
        {
            try
            {
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Cuenta[] cuentas = serv.ListarCuentas();

                CuentasbindingSource.DataSource = cuentas;

                lvCuentas.DataSource = CuentasbindingSource;
                bindingNavigator1.BindingSource = CuentasbindingSource;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


        private void btnNueva_Click(object sender, System.EventArgs e)
        {
            NuevaCuenta nc = new NuevaCuenta();
            nc.Show();
        }


        private void lvCuentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    int idCuenta;
                    if (Int32.TryParse(Convert.ToString(lvCuentas.Rows[e.RowIndex].Cells[0].Value), out idCuenta))
                    {
                        ServiceGestionBancaria serv = new ServiceGestionBancaria();
                        Cuenta c = new Cuenta { IDCUENTA= idCuenta };

                        c = (Cuenta)serv.BuscarCuenta(c);
                        NuevaCuenta nu = new NuevaCuenta { CUENTA = c };

                        nu.Show();
                    }
                }
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
                lvCuentas.Rows.Clear();

                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Cuenta[] cuentas = serv.ListarCuentas();

                CuentasbindingSource.DataSource = cuentas;
                lvCuentas.DataSource = CuentasbindingSource;
                bindingNavigator1.BindingSource = CuentasbindingSource;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

      


    }
}
