using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionBancariaWS;

public partial class Transferencia : System.Web.UI.Page
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

    public Cuenta CUENTA_DESTINO
    {
        get
        {
            if (Session["CUENTA_DESTINO"] == null)
                return null;
            return (Cuenta)Session["CUENTA_DESTINO"];
        }
        set
        {
            if (Session["CUENTA_DESTINO"] == null)
                Session.Add("CUENTA_DESTINO", value);
            else
                Session["CUENTA_DESTINO"] = value;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        lblInfo.Text = "";
        try
        {
            ServiceGestionBancaria sbancaria = new ServiceGestionBancaria();

            Cuenta[] cuentas = sbancaria.ListarCuentasCliente(USUARIO_LOGUEADO);

            if (cuentas != null)
            {
                foreach (Cuenta c in cuentas)
                {
                    ListItem l = new ListItem("Cuenta:" + " " + c.IDCUENTA + " - " + "Moneda: " + c.MONEDA + " - " + "Saldo: " + c.SALDO, Convert.ToString(c.IDCUENTA));
                    ddlCuentas.Items.Add(l);
                }
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }


    protected void btnBuscaar_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceGestionBancaria sbancaria = new ServiceGestionBancaria();

            int cuentaDest;
            if (Int32.TryParse(txtCuentaDestino.Text, out cuentaDest))
            {
                Cuenta c = new Cuenta { IDCUENTA = cuentaDest };
                c = sbancaria.BuscarCuenta(c);

                if (c != null)
                {
                    CUENTA_DESTINO = c;
                    lblTitular.Text = c.CLIENTE.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }


    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceGestionBancaria sbancaria = new ServiceGestionBancaria();

            Cuenta cuentaOrigen = new Cuenta { IDCUENTA = Convert.ToInt32(ddlCuentas.SelectedValue) };

            Movimiento mOrigen = new Movimiento
            {
                CUENTA = cuentaOrigen,
                MONEDA = ddlMoneda.SelectedValue,
                MONTO = Convert.ToDecimal(txtMonto.Text),
                SUCURSAL = cuentaOrigen.SUCURSAL,
                USUARIO = USUARIO_LOGUEADO
            };

            Movimiento mDestino = new Movimiento
            {
                CUENTA = CUENTA_DESTINO,
                MONEDA = ddlMoneda.SelectedValue,
                MONTO = Convert.ToDecimal(txtMonto.Text),
                SUCURSAL = CUENTA_DESTINO.SUCURSAL,
                USUARIO = USUARIO_LOGUEADO
            };

            sbancaria.RealizarTransferencia(mOrigen, mDestino);
            lblInfo.Text = "Transferencia realizada con exito";

        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }


}