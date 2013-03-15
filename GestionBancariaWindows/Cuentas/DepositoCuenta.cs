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
    public partial class DepositoCuenta : Form
    {
        public DepositoCuenta()
        {
            InitializeComponent();
        }

        public Cuenta CUENTA { get; set; }

        public Empleado EMPLEADO { get; set; }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = "";

                int idCuenta;
                if (Int32.TryParse(Convert.ToString(txtNumCuenta.Text), out idCuenta))
                {
                    LogicaCuentas lC = new LogicaCuentas();
                    Cuenta c = new Cuenta { IDCUENTA = idCuenta };

                    c = (Cuenta)lC.BuscarCuenta(c);

                    if (c != null)
                    {
                        CUENTA = c;
                        txtNombre.Text = c.CLIENTE.ToString();
                        txtMoneda.Text = c.MONEDA;

                    }
                }
                else
                {
                    lblInfo.Text = "Formato de numero de prestamo incorrecto";
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        public void LimpiarFormulario()
        {
            txtMoneda.Text = "";
            txtMonto.Text = "";
            txtNombre.Text = "";
            txtNumCuenta.Text = "";
            cmbMonedaDeposito.Text = "";
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            try
            {
                decimal monto;
                if (Decimal.TryParse(txtMonto.Text, out monto))
                {
                    Movimiento m = new Movimiento();
                    m.CUENTA = CUENTA;
                    m.FECHA = DateTime.Now;
                    m.MONEDA = cmbMonedaDeposito.Text;
                    m.MONTO = monto;
                    m.SUCURSAL = EMPLEADO.SUCURSAL;
                    m.TIPOMOVIMIENTO = 2; //TIPO DEPOSITO
                    m.USUARIO = EMPLEADO;
                    m.VIAWEB = false;

                    LogicaCuentas lc = new LogicaCuentas();
                    lc.RealizarMovimiento(m);

                    lblInfo.Text = "Deposito realizado correctamente.";
                    LimpiarFormulario();
                }
                else
                {
                    lblInfo.Text = "El formato del monto ingresado no es valido";
                }
            }
            catch (ErrorSucursalNoExiste ex)
            {
                lblInfo.Text = ex.Message;
            }
            catch (ErrorUsuarioNoExiste ex)
            {
                lblInfo.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void cmbMonedaDeposito_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCotizacion.Text = "";

                //VERIFICAMOS QUE SI LA MONEDA DEL MOVIMIENTO ES DISTINTO A LA MONEDA DE DEPOSITO SE MUESTRA LA COTIZCION DEL DIA
                //------------------------------------------------------------------
                string monedaDeposito = cmbMonedaDeposito.Text;
                if (!String.IsNullOrEmpty(monedaDeposito))
                {
                    if (CUENTA != null && CUENTA.MONEDA != monedaDeposito)
                    {
                        //DESPLIEGA COTIZACION DEL DIA
                        //----------------------------
                        LogicaCotizacion lc = new LogicaCotizacion();
                        Cotizacion c = new Cotizacion();

                        c.FECHA = DateTime.Now;

                        c = lc.BuscarCotizacion(c);//BUSCAMOS LA COTIZACION DEL DIA

                        if (c != null)
                        {
                            lblCotizacion.Text = lblCotizacion.Text + " " + c.ToString();
                        }
                        else
                        {
                            lblInfo.Text = "No existe una cotizacion ingresada para el dia de hoy.";
                        }
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
