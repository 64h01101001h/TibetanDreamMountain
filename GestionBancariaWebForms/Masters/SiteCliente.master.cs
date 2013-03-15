using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionBancariaWS;

public partial class Masters_SiteCliente : System.Web.UI.MasterPage
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

    public void SessionEnded()
    {

        Session.Abandon();
        Response.Redirect("~/index.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                if (Session["Usuario"] == null) //si no existe el session["Login"], 
                {
                    SessionEnded();
                }
                else
                {
                    lblLogueadoComo.Text = ((Cliente)Session["Usuario"]).NOMBREUSUARIO;
                }
            }
        }

        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
        }


    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        SessionEnded();
        Response.Redirect("~/index.aspx");
    }

}
