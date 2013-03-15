using GestionBancariaWindows.GestionBancariaWS;
using System.Windows.Forms;
using System.Collections.Generic;

using System;

namespace GestionBancariaWindows
{
    public partial class ListarEmpleados : Form
    {
        public ListarEmpleados()
        {
            InitializeComponent();
        }

        private void ListarEmpleados_Load(object sender, System.EventArgs e)
        {
            try
            {
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Empleado[] empleados = serv.ListarEmpleados();

                bindingSource1.DataSource = empleados;
                lvEmpleados.DataSource = bindingSource1;
                bindingNavigator1.BindingSource = bindingSource1;

            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

       

        private void btnCancelar_Click(object sender, System.EventArgs e)
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

        private void lvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    int ci;
                    if (Int32.TryParse(Convert.ToString(lvEmpleados.Rows[e.RowIndex].Cells[0].Value), out ci))
                    {
                        ServiceGestionBancaria serv = new ServiceGestionBancaria();
                        Empleado emp = new Empleado { CI = ci };
                        emp = (Empleado)serv.BuscarUsuarioPorCi(emp);
                        NuevoEmpleado nu = new NuevoEmpleado { EMPLEADO = emp };
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
                lvEmpleados.Rows.Clear();

                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Empleado[] empleados = serv.ListarEmpleados();

                bindingSource1.DataSource = empleados;
                lvEmpleados.DataSource = bindingSource1;
                bindingNavigator1.BindingSource = bindingSource1;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

       

       
    }
}
