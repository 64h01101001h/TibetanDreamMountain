using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcepcionesPersonalizadas;
using GestionBancariaWS;


public partial class Datos : System.Web.UI.Page
{
    public Cliente USUARIO_LOGUEADO
    {
        get
        {
            if (Session["Usuario"] == null)
                return null;
            return (Cliente)Session["Usuario"];
        }
        set
        {
            if (Session["Usuario"] == null)
                Session.Add("Usuario", value);
            else
                Session["Usuario"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblInfo.Text = "";

    }
    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceGestionBancaria sbancaria = new ServiceGestionBancaria();
            USUARIO_LOGUEADO.PASS = txtPassActual.Text;
            if (txtPassNueva1.Text == txtPassNueva2.Text)
            {
                sbancaria.ModificarPassword(USUARIO_LOGUEADO, txtPassNueva1.Text);
                lblInfo.Text = "Password actualizada correctamente.";

            }
            else
            {
                lblInfo.Text = "La nueva contraseña y su confirmacion no coinciden.";
            }
        }
        catch (ErrorPasswordActualNoValido ex)
        {
            lblInfo.Text = ex.Message;

        }  
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }

    private void LimpiarForm ()
    {
        txtPassNueva2.Text = "";
        txtPassNueva1.Text = "";
        txtPassActual.Text = "";
    }
}