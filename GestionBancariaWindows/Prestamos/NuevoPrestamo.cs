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
    public partial class NuevoPrestamo : Form
    {
        public NuevoPrestamo()
        {
            InitializeComponent();
        }

        //public Prestamo PRESTAMO { get; set; }



        private void NuevoPrestamo_Load(object sender, EventArgs e)
        {
            try
            {
                //LISTAMOS LOS CLIENTES
                //---------------------
                LogicaUsuarios lu = new LogicaUsuarios();
                List<Cliente> clientes = lu.ListarClientes();
                foreach (Cliente c in clientes)
                {
                    ClientesbindingSource.Add(c);
                }


                //if (PRESTAMO != null)
                //{
                //    cmbCliente.SelectedValue = PRESTAMO.CLIENTE.CI;
                //    txtMonto.Text = Convert.ToString(PRESTAMO.MONTO);
                //    cmbMonedaRetiro.Text = PRESTAMO.MONEDA;
                //    dtpFecha.Value = PRESTAMO.FECHAEMITIDO;
                //    numericCuotas.Value = PRESTAMO.TOTALCUOTAS;
                //    chkCancelado.Checked = PRESTAMO.CANCELADO;

                //    btnGuardar.Text = "Actualizar";

                //}
                //else
                //{
                //    //btnEliminar.Visible = false;
                //}
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
                    //bool editar = false;
                    //if (PRESTAMO == null)
                    //{
                    //    PRESTAMO = new Prestamo();
                    //    PRESTAMO.CLIENTE = new Cliente();
                    //    PRESTAMO.SUCURSAL = new Sucursal();
                    //}
                    //else
                    //{
                    //    editar = true;
                    //}

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
                    PRESTAMO.SUCURSAL.IDSUCURSAL = 1; //** AQUI VA LA SUCURSAL DEL USUARIO LOGUEADO.
                    PRESTAMO.TOTALCUOTAS = Convert.ToInt32(numericCuotas.Value);

                    //** HACER EL LOGIN DE USUARIO ANTES DE ENTREGAR Y CORREGIR ESTO



                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    //if (editar)
                    //{
                    //    lu.ActualizarPrestamo(PRESTAMO);
                    //    lblInfo.Text = "Prestamo actualizado correctamente";
                    //}
                    //else
                    //{
                    LogicaPrestamo lu = new LogicaPrestamo();

                        lu.AltaPrestamo(PRESTAMO);

                        lblInfo.Text = "Prestamo ingresado correctamente";
                        //LIMPIAMOS EL FORMULARIO
                        LimpiarFormulario();
                    //}
                }
            }
            catch (ErrorUsuarioYaExiste uex)
            {
                lblInfo.Text = uex.Message;
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
