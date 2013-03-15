using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionBancariaWindows.GestionBancariaWS;


namespace GestionBancariaWindows
{
    public partial class PrestamosAtrasados : Form
    {
        public PrestamosAtrasados()
        {
            InitializeComponent();
        }
        public Empleado EMPLEADO { get; set; }

        private void PrestamosAtrasados_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Prestamo[] prestamosAtrasados = serv.ListarPrestamosAtrasados(EMPLEADO.SUCURSAL);

                PrestamosbindingSource.DataSource = prestamosAtrasados;
                lvPrestamosAtrasados.DataSource = PrestamosbindingSource;
                bindingNavigator1.BindingSource = PrestamosbindingSource;
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


    }
}
