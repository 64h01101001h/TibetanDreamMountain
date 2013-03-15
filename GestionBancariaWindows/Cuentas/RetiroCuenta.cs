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
    public partial class RetiroCuenta : Form
    {
        public RetiroCuenta()
        {
            InitializeComponent();
        }

        public Cliente CLIENTE { get; set; }
        public Empleado EMPLEADO { get; set; }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
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
                    m.CUENTA = new Cuenta { IDCUENTA = Convert.ToInt32(cmbCuentas.SelectedValue)};
                    m.FECHA = DateTime.Now;
                    m.MONEDA = cmbMonedaRetiro.Text;
                    m.MONTO = monto;
                    m.SUCURSAL = EMPLEADO.SUCURSAL;
                    m.TIPOMOVIMIENTO = 1; //TIPO RETIRO
                    m.USUARIO = EMPLEADO;
                    m.VIAWEB = false;

                    LogicaCuentas lc = new LogicaCuentas();
                    lc.RealizarMovimiento(m);

                    lblInfo.Text = "Retiro realizado correctamente.";
                    LimpiarFormulario();
                }
                else
                {
                    lblInfo.Text = "El formato del monto ingresado no es valido";
                }
            }
            catch (ErrorSaldoInsuficienteParaRetiro ex)
            {
                lblInfo.Text = ex.Message;
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

        public void LimpiarFormulario()
        {
            txtCedula.Text = "";
            txtMonto.Text = "";
            txtNombre.Text = "";
            cmbMonedaRetiro.Text = "";
            cmbCuentas.DataSource = null ;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                    int ci;
                    if (Int32.TryParse(txtCedula.Text, out ci))
                    {
                        LogicaUsuarios lu = new LogicaUsuarios();
                        Cliente c = new Cliente { CI = ci };

                        c = (Cliente)lu.BuscarUsuarioPorCi(c);
                        if (c != null)
                        {
                            CLIENTE = c;
                            txtNombre.Text = c.NOMBRE + " " + c.APELLIDO;
                            LogicaCuentas lc = new LogicaCuentas();
                            List<Cuenta> cuentasCliente = lc.ListarCuentasCliente(c);
                            if (cuentasCliente != null && cuentasCliente.Count > 0)
                            {
                                CuentasbindingSource.DataSource = cuentasCliente;
                                cmbCuentas.DataSource = CuentasbindingSource;
                            }
                            else
                            {
                                lblInfo.Text = "El cliente seleccionado no posee ninguna cuenta.";
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
