using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entidades;
using Logica;
using ExcepcionesPersonalizadas;

namespace GestionBancariaWindows
{
    public partial class NuevaCotizacion : Form
    {
        public Cotizacion COTIZACION { get; set; }
        public NuevaCotizacion()
        {
            InitializeComponent();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Esta seguro de eliminar esta cotizacion de fecha" + COTIZACION.FECHA, "Eliminar Cotizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    LogicaCotizacion lu = new LogicaCotizacion();
                    lu.EliminarCotizacion(COTIZACION);
                    lblInfo.Text = "Cotizacion eliminada correctamente";
                    LimpiarFormulario();
                    btnGuardar.Text = "Guardar";

                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled))
                {
                    bool editar = false;
                    if (COTIZACION == null)
                    {
                        COTIZACION = new Cotizacion();
                    }
                    else
                    {
                        editar = true;
                    }

                    //CARGAMOS INFORMACION DEL CLIENTE
                    //--------------------------------
                    COTIZACION.FECHA = dtpFecha.Value;
                    COTIZACION.PRECIOVENTA = Convert.ToDecimal(txtVenta.Text);
                    COTIZACION.PRECIOCOMPRA = Convert.ToDecimal(txtCompra.Text);

                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    LogicaCotizacion lu = new LogicaCotizacion();
                    if (editar)
                    {

                        lu.ActualizarCotizacion(COTIZACION, new Empleado()); //***** ACA HAY QUE PASAR EL EMPLEADO ACTUALMENTE LOGUEADO
                        lblInfo.Text = "Cotizacion actualizada correctamente";
                    }
                    else
                    {
                        lu.AltaCotizacion(COTIZACION);

                        lblInfo.Text = "Cotizacion ingresada correctamente";
                        //LIMPIAMOS EL FORMULARIO
                        LimpiarFormulario();
                    }


                }
            }
            catch (ErrorCotizacionYaExiste ex)
            {
                lblInfo.Text = ex.Message;
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
                dtpFecha.Value = DateTime.Now;
                txtVenta.Text = "";
                txtCompra.Text = "";
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


        private void NuevaCotizacion_Load(object sender, EventArgs e)
        {
            try
            {

                //toolTip1.SetToolTip(txtTelefonos, "Ingrese los telefonos separados por una coma ','");
                if (COTIZACION != null)
                {
                    dtpFecha.Value = COTIZACION.FECHA;
                    txtCompra.Text = Convert.ToString(COTIZACION.PRECIOCOMPRA);
                    txtVenta.Text = Convert.ToString(COTIZACION.PRECIOVENTA);

                    btnGuardar.Text = "Actualizar";

                }
                else
                {
                    btnEliminar.Visible = false;
                }
            }
           
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }

        #region Validaciones
        private void txtCompra_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtCompra, string.Empty);

        }

        private void txtCompra_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCompra.Text))
            {
                errorProvider.SetError(txtCompra, "Debe ingresar un precio de compra.");
                e.Cancel = true;
            }
        }

        private void txtVenta_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtVenta, string.Empty);

        }

        private void txtVenta_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtVenta.Text))
            {
                errorProvider.SetError(txtVenta, "Debe ingresar un precio de venta.");
                e.Cancel = true;
            }
        }
        #endregion


    }
}
