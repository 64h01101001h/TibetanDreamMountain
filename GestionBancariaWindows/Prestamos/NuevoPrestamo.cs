using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionBancariaWindows.GestionBancariaWS;
using System.Web.Services.Protocols;


namespace GestionBancariaWindows
{
    public partial class NuevoPrestamo : Form
    {
        public NuevoPrestamo()
        {
            InitializeComponent();
        }

        //public Prestamo PRESTAMO { get; set; }
        public Empleado EMPLEADO { get; set; }


        private void NuevoPrestamo_Load(object sender, EventArgs e)
        {
            try
            {
                //LISTAMOS LOS CLIENTES
                //---------------------
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Cliente[] clientes = serv.ListarClientes();
                foreach (Cliente c in clientes)
                {
                    ClientesbindingSource.Add(c);
                }


            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }

        public bool ValidateForm()
        {
            try
            {
                bool validate = true;

                decimal monto;
                if (String.IsNullOrEmpty(txtMonto.Text))
                {
                    errorProviderMonto.SetError(txtMonto, "Debe ingresar un monto valido.");
                    validate = false;
                }
                else if (!Decimal.TryParse(txtMonto.Text, out monto))
                {
                    errorProviderMonto.SetError(txtMonto, "Debe ingresar un monto valido.");
                    validate = false;
                }
                else
                {
                    errorProviderMonto.SetError(txtMonto, string.Empty);
                }

                if (String.IsNullOrEmpty(Convert.ToString(cmbCliente.SelectedValue)))
                {
                    errorProvider.SetError(cmbCliente, "Debe seleccionar un cliente.");
                    validate = false;
                }
                else
                {
                    errorProvider.SetError(cmbCliente, string.Empty);
                }

                if (String.IsNullOrEmpty(cmbMonedaRetiro.Text))
                {
                    errorProviderMoneda.SetError(cmbMonedaRetiro, "Debe seleccionar una moneda.");
                    validate = false;
                }
                else
                {
                    errorProviderMoneda.SetError(cmbMonedaRetiro, string.Empty);
                }


                return validate;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
                return false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled) && ValidateForm())
                {
                   

                    //CARGAMOS INFORMACION DEL PRESTAMO
                    //---------------------------------
                    Prestamo PRESTAMO = new Prestamo();

                    PRESTAMO.CLIENTE = new Cliente();
                    PRESTAMO.SUCURSAL = new Sucursal();

                    PRESTAMO.CANCELADO = chkCancelado.Checked;
                    PRESTAMO.CLIENTE.CI = Convert.ToInt32(cmbCliente.SelectedValue);
                    PRESTAMO.FECHAEMITIDO = dtpFecha.Value;
                    PRESTAMO.MONEDA = cmbMonedaRetiro.Text;
                    PRESTAMO.MONTO = Convert.ToDecimal(txtMonto.Text);
                    PRESTAMO.SUCURSAL.IDSUCURSAL = EMPLEADO.SUCURSAL.IDSUCURSAL; //** AQUI VA LA SUCURSAL DEL USUARIO LOGUEADO.
                    PRESTAMO.TOTALCUOTAS = Convert.ToInt32(numericCuotas.Value);


                    ServiceGestionBancaria serv = new ServiceGestionBancaria();

                       serv.AltaPrestamo(PRESTAMO);

                        lblInfo.Text = "Prestamo ingresado correctamente";
                        //LIMPIAMOS EL FORMULARIO
                        LimpiarFormulario();
                    
                }
            }
            //catch (ErrorUsuarioYaExiste uex)
            //{
            //    lblInfo.Text = uex.Message;
            //}
            catch (SoapException exsoap)
            {
                lblInfo.Text = !string.IsNullOrEmpty(exsoap.Actor) ? exsoap.Actor : exsoap.Message;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void LimpiarFormulario()
        {
            try
            {
                cmbCliente.SelectedValue = "";
                txtMonto.Text = "";
                cmbMonedaRetiro.SelectedValue = "";
                dtpFecha.Value = DateTime.Now;
                numericCuotas.Value = numericCuotas.Minimum;
                chkCancelado.Checked = false;

            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }



    }
}
