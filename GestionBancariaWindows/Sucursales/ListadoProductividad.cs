using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logica;
using Entidades;
using ExcepcionesPersonalizadas;

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
                lvSucursales.Rows.Clear();

                LogicaSucursal ls = new LogicaSucursal();
                List<Sucursal> listadoComparativo = ls.ListadoProductividadComparativo(dtpFechaInicio.Value,dtpFechaFin.Value);

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

    }
}
