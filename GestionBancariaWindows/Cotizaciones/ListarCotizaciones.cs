using System.Windows.Forms;
using GestionBancariaWindows.GestionBancariaWS;

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
                //LogicaCotizacion lu = new LogicaCotizacion();
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Cotizacion[] cotizaciones = serv.ListarCotizaciones();

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

                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Cotizacion[] cotizaciones = serv.ListarCotizaciones();

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
                        ServiceGestionBancaria serv = new ServiceGestionBancaria();
                        Cotizacion cot = new Cotizacion{ FECHA =  dt};

                        cot = (Cotizacion)serv.BuscarCotizacion(cot);
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
