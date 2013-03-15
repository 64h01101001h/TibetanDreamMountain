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

namespace GestionBancariaWindows
{
    public partial class NuevaSucursal : Form
    {
        public NuevaSucursal()
        {
            InitializeComponent();
        }

        public Sucursal SUCURSAL { get; set; }


        private void NuevaSucursal_Load(object sender, EventArgs e)
        {
            try
            {
                if (SUCURSAL != null)
                {
                    txtNombreSucursal.Text = SUCURSAL.NOMBRE;
                    txtDireccion.Text = SUCURSAL.DIRECCION;

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
                    if (SUCURSAL == null)
                    {
                        SUCURSAL = new Sucursal();
                    }
                    else
                    {
                        editar = true;
                    }

                    //CARGAMOS INFORMACION DEL CLIENTE
                    //--------------------------------

                    SUCURSAL.NOMBRE = txtNombreSucursal.Text;
                    SUCURSAL.DIRECCION = txtDireccion.Text;


                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    LogicaSucursal lu = new LogicaSucursal();
                    if (editar)
                    {
                        lu.ActualizarSucursal(SUCURSAL);
                        lblInfo.Text = "Sucursal actualizada correctamente";
                    }
                    else
                    {
                        lu.AltaSucursal(SUCURSAL);

                        lblInfo.Text = "Sucursal ingresada correctamente";
                        //LIMPIAMOS EL FORMULARIO
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
            try
            {
                txtNombreSucursal.Text = "";
                txtDireccion.Text = "";
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
                if (MessageBox.Show("Esta seguro de eliminar esta sucursal " + SUCURSAL.NOMBRE, "Eliminar Sucursal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    LogicaSucursal lu = new LogicaSucursal();
                    lu.EliminarSucursal(SUCURSAL);
                    lblInfo.Text = "Sucursal eliminada correctamente";
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

        #region VALIDACIONES
        private void txtNombreSucursal_Validating_1(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombreSucursal.Text))
            {
                errorProvider.SetError(txtNombreSucursal, "Debe ingresar un nombre para la sucursal.");
                e.Cancel = true;
            }
        }

        private void txtNombreSucursal_Validated_1(object sender, EventArgs e)
        {
            errorProvider.SetError(txtNombreSucursal, string.Empty);

        }
        #endregion

       


    }
}
