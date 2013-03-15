using System.Windows.Forms;
using System;
using GestionBancariaWindows.GestionBancariaWS;

using System.Collections.Generic;

namespace GestionBancariaWindows
{
    public partial class PagoCuota : Form
    {
        public PagoCuota()
        {
            InitializeComponent();
        }

        public Prestamo PRESTAMO { get; set; }
        public Empleado EMPLEADO { get; set; }

        private void PagoCuota_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblInfo.Text = "";


                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                Sucursal[] sucursales = serv.ListarSucursales();

                foreach (Sucursal s in sucursales)
                {
                    SucursalbindingSource.Add(s);
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                int numPrestamo;
                if (Int32.TryParse(txtNumPrestamo.Text, out numPrestamo))
                {
                    ServiceGestionBancaria serv = new ServiceGestionBancaria();
                    Prestamo p = new Prestamo { IDPRESTAMO = numPrestamo };

                    p = (Prestamo)serv.BuscarPrestamo(p);
                    if (p != null)
                    {
                        txtCliente.Text = p.CLIENTE.NOMBRE + " " + p.CLIENTE.APELLIDO;

                        //Calculamos el monto a pagar.
                        //---------------------------
                        decimal montoCuotaPagar = p.MONTO / p.TOTALCUOTAS;
                        txtMontoaPagar.Text = montoCuotaPagar.ToString("c");

                        PRESTAMO = p;
                    }
                    else
                    {
                        PRESTAMO = null;
                        lblInfo.Text = "No se encontro ningun prestamo.";
                        LimpiarFormulario();
                    }

                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


        private void LimpiarFormulario()
        {
            txtMontoaPagar.Text = "";
            txtNumPrestamo.Text = "";
            txtCliente.Text = "";
            cmbSucursal.SelectedValue = "";

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled))
                {
                    ServiceGestionBancaria serv = new ServiceGestionBancaria();

                    serv.PagarCuota(PRESTAMO,EMPLEADO);

                    lblInfo.Text = "Cuota pagada correctamente";
                    LimpiarFormulario();

                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


    }
}
