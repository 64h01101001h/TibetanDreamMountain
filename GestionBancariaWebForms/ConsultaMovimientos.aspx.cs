using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;
using System.Xml;
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

    public XmlDocument MOVIMIENTOS
    {
        get
        {
            if (Session["MOVIMIENTOS"] == null)
                return null;
            return (XmlDocument)Session["MOVIMIENTOS"];
        }
        set
        {
            if (Session["MOVIMIENTOS"] == null)
                Session.Add("MOVIMIENTOS", value);
            else
                Session["MOVIMIENTOS"] = value;
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

                XPathNavigator _Navegador = myXmlDocumentObject.CreateNavigator();
                XmlMovimientos.XPathNavigator = _Navegador;
                XmlMovimientos.DataBind();

                XmlMovimientos.TransformSource = Server.MapPath("~/Movimientos.xslt");

                MOVIMIENTOS = myXmlDocumentObject;
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
            if (MOVIMIENTOS != null)
            {
                XPathNavigator _Navegador = MOVIMIENTOS.CreateNavigator();
                XmlMovimientos.XPathNavigator = _Navegador;
                XmlMovimientos.DataBind();
                XmlMovimientos.TransformSource = Server.MapPath("~/Movimientos.xslt");

                //ejecuto la consulta
                XPathNodeIterator _Resultado = XmlMovimientos.XPathNavigator.Select("raiz/Cuenta[Fecha = '" + txtFechaEspecifica.Text + "']");


                //si hay resultado lo muestro;
                //El iterador tiene una propiedad count que me va a determinar la cantidad de nodos que puedo navegar
                if (_Resultado != null && _Resultado.Count > 0)
                {
                    XmlDocument ResultadoXml = new XmlDocument();

                    XmlNode raiz = ResultadoXml.CreateNode(XmlNodeType.Element, "raiz", null);
                    while (_Resultado.MoveNext())
                    {

                        XmlNode NuevoPadre = ResultadoXml.CreateNode(XmlNodeType.Element, "Cuenta", null);

                        //numero movimiento
                        XmlNode NumMovimiento = ResultadoXml.CreateNode(XmlNodeType.Element, "NumeroMovimiento", null);
                        NumMovimiento.InnerText = Convert.ToString(_Resultado.Current.SelectSingleNode("NumeroMovimiento"));
                        NuevoPadre.AppendChild(NumMovimiento);

                        //fecha
                        XmlNode Fecha = ResultadoXml.CreateNode(XmlNodeType.Element, "Fecha", null);
                        Fecha.InnerText = Convert.ToString(_Resultado.Current.SelectSingleNode("Fecha"));
                        NuevoPadre.AppendChild(Fecha);

                        //Moneda
                        XmlNode Moneda = ResultadoXml.CreateNode(XmlNodeType.Element, "Moneda", null);
                        Moneda.InnerText = Convert.ToString(_Resultado.Current.SelectSingleNode("Moneda"));
                        NuevoPadre.AppendChild(Moneda);

                        //Monto
                        XmlNode Monto = ResultadoXml.CreateNode(XmlNodeType.Element, "Monto", null);
                        Monto.InnerText = Convert.ToString(_Resultado.Current.SelectSingleNode("Monto"));
                        NuevoPadre.AppendChild(Monto);

                        raiz.AppendChild(NuevoPadre);

                    }
                    ResultadoXml.AppendChild(raiz);


                    //bindeamos el control xml a los nuevos resultados filtrados
                    //------------------------------------------------------------
                    _Navegador = ResultadoXml.CreateNavigator();
                    XmlMovimientos.XPathNavigator = _Navegador;
                    XmlMovimientos.DataBind();
                    XmlMovimientos.TransformSource = Server.MapPath("~/Movimientos.xslt");
                }
            }

            else
            {
                lblInfo.Text = "No hay movimientos a filtrar";
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }
}