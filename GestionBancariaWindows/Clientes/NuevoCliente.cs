using System;
using System.Linq;
using System.Windows.Forms;
using GestionBancariaWindows.GestionBancariaWS;


namespace GestionBancariaWindows
{
    public partial class NuevoCliente : Form
    {
        private Cliente ClienteId;
        public Cliente CLIENTE { get; set; }


        public NuevoCliente()
        {
            InitializeComponent();

        }


        private void NuevoCliente_Load(object sender, EventArgs e)
        {
            try
            {

                toolTip1.SetToolTip(txtTelefonos, "Ingrese los telefonos separados por una coma ','");
                if (CLIENTE != null)
                {
                    txtApellido.Text = CLIENTE.APELLIDO;
                    txtCedula.Text = Convert.ToString(CLIENTE.CI);
                    txtDireccion.Text = CLIENTE.DIRECCION;
                    txtNombre.Text = CLIENTE.NOMBRE;
                    txtPassword.Text = CLIENTE.PASS;
                    txtReiterarPass.Text = CLIENTE.PASS;
                    txtUsuario.Text = CLIENTE.NOMBREUSUARIO;

                    string telefonos = "";
                    if (CLIENTE.TELEFONOS != null)
                    {
                        foreach (string s in CLIENTE.TELEFONOS)
                        {
                            telefonos = telefonos + s + ",";
                        }
                    }
                    txtTelefonos.Text = telefonos;

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
                    if (CLIENTE == null)
                    {
                        CLIENTE = new Cliente();
                    }
                    else
                    {
                        editar = true;
                    }

                    //CARGAMOS INFORMACION DEL CLIENTE
                    //--------------------------------

                    CLIENTE.CI = Convert.ToInt32(txtCedula.Text);
                    CLIENTE.DIRECCION = txtDireccion.Text;
                    CLIENTE.APELLIDO = txtApellido.Text;
                    CLIENTE.NOMBRE = txtNombre.Text;
                    CLIENTE.NOMBREUSUARIO = txtUsuario.Text;
                    CLIENTE.PASS = txtPassword.Text;


                    //TELEFONOS
                    //---------
                    if (!String.IsNullOrEmpty(txtTelefonos.Text))
                    {
                        CLIENTE.TELEFONOS = txtTelefonos.Text.Split(',').ToArray();
                    }

                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    ServiceGestionBancaria serv = new ServiceGestionBancaria();
                    if (editar)
                    {
                        serv.ActualizarUsuario(CLIENTE);
                        lblInfo.Text = "Cliente actualizado correctamente";
                    }
                    else
                    {
                        serv.AltaUsuario(CLIENTE);

                        lblInfo.Text = "Cliente ingresado correctamente";
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LimpiarFormulario()
        {
            try
            {
                txtApellido.Text = "";
                txtCedula.Text = "";
                txtDireccion.Text = "";
                txtNombre.Text = "";
                txtPassword.Text = "";
                txtReiterarPass.Text = "";
                txtTelefonos.Text = "";
                txtUsuario.Text = "";
                CLIENTE = null;
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
                if (MessageBox.Show("Esta seguro de eliminar el cliente " + CLIENTE.NOMBRE + " " + CLIENTE.APELLIDO, "Eliminar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    ServiceGestionBancaria serv = new ServiceGestionBancaria();
                    serv.EliminarUsuario(CLIENTE);
                    lblInfo.Text = "Cliente eliminado correctamente";
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
