using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;
using System.Xml;
using ExcepcionesPersonalizadas;
using GestionBancariaWS;

public partial class ConsultaMovimientos : System.Web.UI.Page
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
        try
        {
            //VER MOVIMIENTOS DE UN MES A ESTA PARTE
            //--------------------------------------
            txtFecha.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            ServiceGestionBancaria sbancaria = new ServiceGestionBancaria();
            CuentasListRepeater.DataSource = sbancaria.ListarCuentasCliente(USUARIO_LOGUEADO);
            CuentasListRepeater.DataBind();
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }


    protected void CuentasListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "SELECCIONAR")
            {
                ServiceGestionBancaria sm = new ServiceGestionBancaria();
                Cuenta c = new Cuenta();
                c.IDCUENTA = Convert.ToInt32(e.CommandArgument);

                XmlNode nodos = sm.ConsultaMovimientos(c, Convert.ToDateTime(txtFecha.Text));

                XmlDocument myXmlDocumentObject = new XmlDocument();

                myXmlDocumentObject.AppendChild(myXmlDocumentObject.ImportNode(nodos, true));

                //XmlDocument _DocumentoXML = new XmlDocument();

                //myXmlDocumentObject.Load(_camino); //camino tiene el path al document xml

                XPathNavigator _Navegador = myXmlDocumentObject.CreateNavigator();
                XmlMovimientos.XPathNavigator = _Navegador;

                XmlMovimientos.TransformSource = Server.MapPath("~/Movimientos.xslt");
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }

    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            //ejecuto la consulta
            XPathNodeIterator _Resultado = XmlMovimientos.XPathNavigator.Select("/Cuenta/Movimiento[Fecha = '" + txtFechaEspecifica.Text + "']");

            //si hay resultado lo muestro;
            //El iterador tiene una propiedad count que me va a determinar la cantidad de nodos que puedo navegar
            if (_Resultado.Count > 0)
            {
                XmlMovimientos.XPathNavigator = _Resultado.Current;
                while (_Resultado.MoveNext())
                {
                    //string nombre = _Resultado.Current.SelectSingleNode("NombreUsuario").ToString();
                    //string enviados = _Resultado.Current.SelectSingleNode("MailsEnviados").ToString();
                    //string recibidos = _Resultado.Current.SelectSingleNode("MailsRecibidos").ToString();

                    //object[] row0 = { nombre, enviados, recibidos };
                    //gridEstadistica.Rows.Add(row0);
                }
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }
}