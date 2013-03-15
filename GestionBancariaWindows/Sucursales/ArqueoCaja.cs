using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logica;
using ExcepcionesPersonalizadas;
using Entidades;

namespace GestionBancariaWindows
{
    public partial class ArqueoCaja : Form
    {
        public ArqueoCaja()
        {
            InitializeComponent();
        }

        public Empleado EMPLEADO { get; set; }




        private void NuevaSucursal_Load(object sender, EventArgs e)
        {
            try
            {
                LogicaSucursal ls = new LogicaSucursal();
                decimal saldoCajaDolares = Decimal.Zero, saldoCajaPesos = Decimal.Zero;
                int cantTotalDepositos = 0, cantTotalPagos = 0, cantTotalRetiros = 0;
                ls.ArqueoCaja(EMPLEADO, ref saldoCajaDolares, ref saldoCajaPesos, ref cantTotalDepositos, ref cantTotalRetiros, ref cantTotalPagos);

                txtCantidadCuotasPrestamos.Text = Convert.ToString(cantTotalPagos);
                txtCantidadDepositos.Text = Convert.ToString(cantTotalDepositos);
                txtCantidadRetiros.Text = Convert.ToString(cantTotalRetiros);
                txtSaldoDolares.Text = Convert.ToString(saldoCajaDolares);
                txtSaldoPesos.Text = Convert.ToString(saldoCajaPesos);

            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }

}



       




       


    

