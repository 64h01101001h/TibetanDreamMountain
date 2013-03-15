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
    public partial class NuevoEmpleado : Form
    {
        private Empleado EmpleadoId;
        public Empleado EMPLEADO { get; set; }



        public NuevoEmpleado()
        {
            InitializeComponent();
        }



        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            try
            {
                //CARGA LAS SUCURSALES
                //--------------------
                LogicaSucursal lu = new LogicaSucursal();
                List<Sucursal> sucursales = lu.ListarSucursales();
                foreach (Sucursal s in sucursales)
                {
                    bindingSource1.Add(s);
                }

                if (EMPLEADO != null)
                {
                    lblHeader.Text = "Editar Empleado";
                    txtApellido.Text = EMPLEADO.APELLIDO;
                    txtCedula.Text = Convert.ToString(EMPLEADO.CI);
                    txtNombre.Text = EMPLEADO.NOMBRE;
                    txtPassword.Text = EMPLEADO.PASS;
                    txtReiterarPass.Text = EMPLEADO.PASS;
                    txtUsuario.Text = EMPLEADO.NOMBREUSUARIO;
                    ddlSucursales.SelectedValue = EMPLEADO.SUCURSAL.IDSUCURSAL;

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled))
                {
                    bool editar = false;
                    if (EMPLEADO == null)
                    {
                        EMPLEADO = new Empleado();
                    }
                    else
                    {
                        editar = true;
                    }

                    //CARGAMOS INFORMACION DEL CLIENTE
                    //--------------------------------
                    EMPLEADO.CI = Convert.ToInt32(txtCedula.Text);
                    EMPLEADO.APELLIDO = txtApellido.Text;
                    EMPLEADO.NOMBRE = txtNombre.Text;
                    EMPLEADO.NOMBREUSUARIO = txtUsuario.Text;
                    EMPLEADO.PASS = txtPassword.Text;

                    if (editar)
                    {
                        EMPLEADO.SUCURSAL.IDSUCURSAL = Convert.ToInt32(ddlSucursales.SelectedValue);
                    }
                    else
                    {
                        EMPLEADO.SUCURSAL = new Sucursal {IDSUCURSAL = Convert.ToInt32(ddlSucursales.SelectedValue) };

                    }

                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    LogicaUsuarios lu = new LogicaUsuarios();
                    if (editar)
                    {
                        lu.ActualizarUsuario(EMPLEADO);
                        lblInfo.Text = "Empleado actualizado correctamente";
                    }
                    else
                    {
                        lu.AltaUsuario(EMPLEADO);

                        lblInfo.Text = "Empleado ingresado correctamente";
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
                txtApellido.Text = "";
                txtCedula.Text = "";
                txtNombre.Text = "";
                txtPassword.Text = "";
                txtReiterarPass.Text = "";
                txtUsuario.Text = "";

            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


        #region VALIDACIONES
        private void txtCedula_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCedula.Text))
            {
                errorProvider.SetError(txtCedula, "Debe ingresar un numero de cedula.");
                e.Cancel = true;
            }
        }

        private void txtCedula_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtCedula, string.Empty);
        }



        private void txtNombre_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "Debe ingresar un nombre.");
                e.Cancel = true;
            }

        }

        private void txtApellido_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtApellido.Text))
            {
                errorProvider.SetError(txtApellido, "Debe ingresar un apellido.");
                e.Cancel = true;
            }

        }

        private void txtUsuario_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsuario.Text))
            {
                errorProvider.SetError(txtUsuario, "Debe ingresar un nombre de usuario.");
                e.Cancel = true;
            }

        }

        private void txtReiterarPass_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Debe ingresar un password.");
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(txtReiterarPass.Text))
            {
                errorProvider.SetError(txtReiterarPass, "Debe reiterar el password previamente ingresado.");
                e.Cancel = true;

            }
            else if (txtReiterarPass.Text != txtPassword.Text)
            {
                errorProvider.SetError(txtReiterarPass, "Los passwords ingresados no coinciden. Por favor verifique");
                e.Cancel = true;
            }

        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtNombre, string.Empty);

        }

        private void txtApellido_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtApellido, string.Empty);

        }

        private void txtUsuario_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtUsuario, string.Empty);

        }

        private void txtReiterarPass_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtReiterarPass, string.Empty);

        }
        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Esta seguro de eliminar el empleado " + EMPLEADO.NOMBRE + " " + EMPLEADO.APELLIDO, "Eliminar Empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    LogicaUsuarios lu = new LogicaUsuarios();
                    lu.EliminarUsuario(EMPLEADO);
                    lblInfo.Text = "Empleado eliminado correctamente";
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

        

    }
}
