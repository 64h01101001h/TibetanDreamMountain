using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcepcionesPersonalizadas;
using System.Web.Services.Protocols;
using GestionBancariaWS;

public partial class Masters_Site : System.Web.UI.MasterPage
{
    public Usuario USUARIO_LOGUEADO
    {
        get
        {
            if (Session["Usuario"] == null)
                return null;
            return (Usuario)Session["Usuario"];
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

    }



    protected void LoginUser_LoggingIn(object sender, LoginCancelEventArgs e)
    {
        try
        {
            ServiceGestionBancaria sm = new ServiceGestionBancaria();
            Usuario NuevoUsuario = sm.getLoginUsuario(LoginUser.UserName, LoginUser.Password);

            USUARIO_LOGUEADO = NuevoUsuario;
            if (NuevoUsuario != null)
            {
                if (NuevoUsuario is Cliente)
                {
                  
                    Response.Redirect("~/Home.aspx",false);
                }
            }
            else
            {
                lblError.Text = "El usuario o contraseña ingresados no son validos. Media pila! ...";
            }
        }
        catch (SoapException exsoap)
        {
            lblError.Text = !string.IsNullOrEmpty(exsoap.Actor) ? exsoap.Actor : exsoap.Message;
        }
        catch (Exception ex)
        {

            lblError.Text = ex.Message;
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }
}
