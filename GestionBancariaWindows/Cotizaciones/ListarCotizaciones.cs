using System.Windows.Forms;
using Logica;
using Entidades;
using System.Collections.Generic;
using System;

namespace GestionBancariaWindows
{
    public partial class ListarCotizaciones : Form
    {
        public ListarCotizaciones()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ListarCotizaciones_Load(object sender, System.EventArgs e)
        {
            try
            {
                LogicaCotizacion lu = new LogicaCotizacion();
                List<Cotizacion> cotizaciones = lu.ListarCotizaciones();

                CotizacionbindingSource.DataSource = cotizaciones;
                lvCotizaciones.DataSource = CotizacionbindingSource;
                bindingNavigator1.BindingSource = CotizacionbindingSource;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            try
            {
                NuevaCotizacion nc = new NuevaCotizacion();
                nc.Show();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                lvCotizaciones.Rows.Clear();

                LogicaCotizacion lu = new LogicaCotizacion();
                List<Cotizacion> cotizaciones = lu.ListarCotizaciones();

                CotizacionbindingSource.DataSource = cotizaciones;
                lvCotizaciones.DataSource = CotizacionbindingSource;
                bindingNavigator1.BindingSource = CotizacionbindingSource;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }



        private void lvCotizaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(lvCotizaciones.Rows[e.RowIndex].Cells[0].Value)))
                    {
                        DateTime dt = Convert.ToDateTime(lvCotizaciones.Rows[e.RowIndex].Cells[0].Value);
                        LogicaCotizacion lu = new LogicaCotizacion();

                        Cotizacion cot = new Cotizacion{ FECHA =  dt};

                        cot = (Cotizacion)lu.BuscarCotizacion(cot);
                        NuevaCotizacion nu = new NuevaCotizacion { COTIZACION = cot };

                        nu.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }
    }
}
