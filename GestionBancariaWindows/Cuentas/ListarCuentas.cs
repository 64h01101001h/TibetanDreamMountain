using System.Windows.Forms;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Collections.Generic;
using Logica;
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
                LogicaCuentas lc = new LogicaCuentas();
                List<Cuenta> cuentas = lc.ListarCuentas();

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
                        LogicaCuentas lC = new LogicaCuentas();
                        Cuenta c = new Cuenta { IDCUENTA= idCuenta };

                        c = (Cuenta)lC.BuscarCuenta(c);
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

                LogicaCuentas lu = new LogicaCuentas();
                List<Cuenta> cuentas = lu.ListarCuentas();

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
