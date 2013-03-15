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
    public partial class ListadoProductividad : Form
    {
        public ListadoProductividad()
        {
            InitializeComponent();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //lvSucursales.Rows.Clear();

                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Sucursal[] listadoComparativo = serv.ListadoProductividadComparativo(dtpFechaInicio.Value,dtpFechaFin.Value);

                SucursalbindingSource.DataSource = listadoComparativo;
                lvSucursales.DataSource = SucursalbindingSource;
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

        private void ListadoProductividad_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
        }

    }
}
