using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entidades;
using ExcepcionesPersonalizadas;
using Logica;

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
                LogicaPrestamo lp = new LogicaPrestamo();
                //Sucursal s = new Sucursal();
                List<Prestamo> prestamosAtrasados = lp.ListarPrestamosAtrasados(EMPLEADO.SUCURSAL);

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
