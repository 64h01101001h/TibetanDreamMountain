using System;
using System.Windows.Forms;
//using BiosWebMailWindows.refServiceWebMailWin;
using Entidades;
using ExcepcionesPersonalizadas;
using Logica;

namespace GestionBancariaWindows
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            //******************************************************
            //SOLO A EFECTOS DE TESTING. ELIMINAR ESTAS LINEAS LUEGO
            //------------------------------------------------------
            txtNombreUsuario.Text = "ElGaucho";
            txtPassword.Text = "1234";
        }

        private void btnLoguearse_Click(object sender, EventArgs e)
        {
            try
            {
                //ILogicaUsuario LogicaUsuario = FabricaLogica.getLogicaUsuario();
                //ServiceWebMail sm = new ServiceWebMail();
                //Usuario NuevoUsuario = LogicaUsuario.getLoginUsuario(txtUsuario.Text, txtPass.Text);

               


                LogicaUsuarios lu = new LogicaUsuarios();
                Usuario employee = lu.getLoginUsuario(txtNombreUsuario.Text, txtPassword.Text);

                if (employee != null && employee.ACTIVO)
                {
                    if (employee.PASS == txtPassword.Text)
                    {
                        
                        if (employee is Empleado)
                        {
                            Empleado emp = (Empleado)employee;
                            if (emp.SUCURSAL != null && emp.SUCURSAL.ACTIVA)
                            {
                                this.Hide();
                                Main menuForm = new Main();
                                menuForm.EMPLEADO = emp;
                                menuForm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                lblInfo.Text = "El usuario no pertenece a una sucursal o la sucursal no se encuentra activa";
                            }
                        }
                    }
                    else
                    {
                        lblInfo.Text = "El password ingresado no es valido";
                    }
                }
                else
                {
                    lblInfo.Text = "El usuario ingresado no existe, o no se encuentra activo.";
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

    
    }
}
