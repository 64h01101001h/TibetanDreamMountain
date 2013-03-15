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
    public partial class NuevaCuenta : Form
    {
        public NuevaCuenta()
        {
            InitializeComponent();
        }

        public Cuenta CUENTA { get; set; }

        private void NuevaCuenta_Load(object sender, EventArgs e)
        {
            try
            {
                //CARGAMOS LA INFORMACION DE CLEINTES EXISTENTES
                //---------------------------------------------
                LogicaUsuarios lc = new LogicaUsuarios();
                List<Cliente> clientes = lc.ListarClientes();

                foreach (Cliente c in clientes)
                {
                    ClientesbindingSource.Add(c);
                }

                if (CUENTA != null)
                {
                    lblHeader.Text = "Editar Cuenta";

                    txtNumeroCuenta.Text = Convert.ToString(CUENTA.IDCUENTA);
                    txtSaldo.Text = Convert.ToString(CUENTA.SALDO);
                    cmbClientes.SelectedValue = CUENTA.CLIENTE.CI;
                    cmbMoneda.Text = CUENTA.MONEDA;

                    btnGuardar.Text = "Actualizar";
                    cmbClientes.Enabled = false;
                }
                else
                {
                    btnEliminar.Visible = false;
                    txtSaldo.Text = Convert.ToString(Decimal.Zero);//LAS CUENTAS SE ABREN CON SALDO 0
                    txtSaldo.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Esta seguro de eliminar la cuenta " + CUENTA.IDCUENTA, "Eliminar Cuenta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    LogicaCuentas lc = new LogicaCuentas();
                    lc.EliminarCuenta(CUENTA);

                    lblInfo.Text = "Cuenta eliminada correctamente";
                    LimpiarFormulario();
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
                    if (CUENTA == null)
                    {
                        CUENTA = new Cuenta();
                        CUENTA.CLIENTE = new Cliente();
                        CUENTA.SUCURSAL = new Sucursal { IDSUCURSAL = 1 };//** AQUI SE DEBE COLOCAR LA SUCURSAL DONDE TRABAJA EL EMPLEADO
                    }
                    else
                    {
                        editar = true;
                    }

                    //CARGAMOS INFORMACION DE CUENTA
                    //------------------------------
                    CUENTA.MONEDA = Convert.ToString(cmbMoneda.Text);
                    CUENTA.SALDO = Convert.ToDecimal(txtSaldo.Text);
                    CUENTA.CLIENTE.CI = Convert.ToInt32(cmbClientes.SelectedValue);


                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    LogicaCuentas lc = new LogicaCuentas();
                    if (editar)
                    {
                        lc.ActualizarCuenta(CUENTA);
                        lblInfo.Text = "Cuenta actualizada correctamente";
                    }
                    else
                    {
                        lc.AltaCuenta(CUENTA);
                        lblInfo.Text = "Cuenta ingresada correctamente";

                        //LIMPIAMOS EL FORMULARIO
                        LimpiarFormulario();
                    }


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
                txtNumeroCuenta.Text = "";
                txtSaldo.Text = "0.00";
                cmbClientes.SelectedValue = "";
                cmbMoneda.SelectedIndex = 0;
                CUENTA = null;
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }
    }
}
