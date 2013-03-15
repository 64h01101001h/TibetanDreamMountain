using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using Entidades;
using Logica;
using System.Xml;
using System;
using ExcepcionesPersonalizadas;

[WebService(Namespace = "http://ServiceGestionBancaria.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class ServiceGestionBancaria : System.Web.Services.WebService
{
    public ServiceGestionBancaria()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region procedimientos Usuarios
    [WebMethod]
    public void AltaUsuario(Usuario u)
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            ls.AltaUsuario(u);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ActualizarUsuario(Usuario u)
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            ls.ActualizarUsuario(u);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void EliminarUsuario(Usuario u)
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            ls.EliminarUsuario(u);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<Cliente> ListarClientes()
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            return ls.ListarClientes();
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<Empleado> ListarEmpleados()
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            return ls.ListarEmpleados();
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public Usuario BuscarUsuarioPorCi(Usuario u)
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            return ls.BuscarUsuarioPorCi(u);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public Usuario getLoginUsuario(string userName, string pass)
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            return ls.getLoginUsuario(userName, pass);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ModificarPassword(Cliente c, string newPass)
    {
        try
        {
            ILogicaUsuarios ls = FabricaLogica.getLogicaUsuario();
            ls.ModificarPassword(c, newPass);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region Procedimientos de Cuentas
    [WebMethod]
    public void RealizarTransferencia(Movimiento movOrigen, Movimiento movDestino)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();
            lc.RealizarTransferencia(movOrigen, movDestino);
        }
        catch (ErrorNoExisteCotizacion exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (ErrorSaldoInsuficienteParaRetiro exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (ErrorSucursalNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public XmlDocument ConsultaMovimientos(Cuenta c, DateTime d)
    {
        #region this must go to the web service class
        //ILogicaUsuario le = FabricaLogica.getLogicaUsuario();
        //LogicaCuentas lc = new LogicaCuentas();
        ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();


        List<Movimiento> movs = lc.ConsultaMovimientosCuenta(c, d);
        XmlDocument ArchivoRetornoXml = new XmlDocument();

        XmlNode raiz = ArchivoRetornoXml.CreateNode(XmlNodeType.Element, "raiz", null);

        foreach (Movimiento m in movs)
        {
            XmlNode NuevoPadre = ArchivoRetornoXml.CreateNode(XmlNodeType.Element, "Cuenta", null);

            //numero movimiento
            XmlNode NumMovimiento = ArchivoRetornoXml.CreateNode(XmlNodeType.Element, "NumeroMovimiento", null);
            NumMovimiento.InnerText = Convert.ToString(m.IDMOVIMIENTO);
            NuevoPadre.AppendChild(NumMovimiento);

            //fecha
            XmlNode Fecha = ArchivoRetornoXml.CreateNode(XmlNodeType.Element, "Fecha", null);
            Fecha.InnerText = Convert.ToString(m.FECHA);
            NuevoPadre.AppendChild(Fecha);

            //Moneda
            XmlNode Moneda = ArchivoRetornoXml.CreateNode(XmlNodeType.Element, "Moneda", null);
            Moneda.InnerText = m.MONEDA;
            NuevoPadre.AppendChild(Moneda);

            //Monto
            XmlNode Monto = ArchivoRetornoXml.CreateNode(XmlNodeType.Element, "Monto", null);
            Monto.InnerText = Convert.ToString(m.MONTO);
            NuevoPadre.AppendChild(Monto);

            raiz.AppendChild(NuevoPadre);
        }
        ArchivoRetornoXml.AppendChild(raiz);
        return ArchivoRetornoXml;
        #endregion
    }


    [WebMethod]
    public List<Cuenta> ListarCuentasCliente(Usuario u)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();
            return lc.ListarCuentasCliente((Cliente)u);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void AltaCuenta(Cuenta c)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();
            lc.AltaCuenta(c);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<Cuenta> ListarCuentas()
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();
            return lc.ListarCuentas();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void EliminarCuenta(Cuenta c)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();
            lc.EliminarCuenta(c);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public Cuenta BuscarCuenta(Cuenta c)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();
            return lc.BuscarCuenta(c);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ActualizarCuenta(Cuenta c)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();

            lc.ActualizarCuenta(c);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void RealizarMovimiento(Movimiento m)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();

            lc.RealizarMovimiento(m);
        }
        catch (ErrorSaldoInsuficienteParaRetiro exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (ErrorSucursalNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (ErrorUsuarioNoExiste exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<Movimiento> ConsultaMovimientosCuenta(Cuenta c, DateTime d)
    {
        try
        {
            ILogicaCuentas lc = FabricaLogica.getLogicaCuentas();

            return lc.ConsultaMovimientosCuenta(c, d);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Procedimientos Cotizacion
    [WebMethod]
    public List<Cotizacion> ListarCotizaciones()
    {
        try
        {
            ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();

            return lc.ListarCotizaciones();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void AltaCotizacion(Cotizacion s)
    {
        try
        {
            ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();

            lc.AltaCotizacion(s);
        }
        catch (ErrorSaldoInsuficienteParaRetiro exal)
        {
            throw new SoapException(exal.Message, SoapException.ClientFaultCode, exal.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void EliminarCotizacion(Cotizacion s)
    {
        try
        {
            ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();

            lc.EliminarCotizacion(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public Cotizacion BuscarCotizacion(Cotizacion s)
    {
        try
        {
            ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();

            return lc.BuscarCotizacion(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ActualizarCotizacion(Cotizacion s, Empleado e)
    {
        try
        {
            ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();

            lc.ActualizarCotizacion(s, e);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Procedimientos Pagos
    [WebMethod]
    public void PagarCuota(Prestamo p)
    {
        try
        {
            ILogicaPagos lc = FabricaLogica.getLogicaPagos();

            lc.PagarCuota(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<Pago> ListarPagos(Prestamo p)
    {
        try
        {
            ILogicaPagos lc = FabricaLogica.getLogicaPagos();

            return lc.ListarPagos(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region procedimientos Prestamo
    [WebMethod]
    public decimal CalcularMontoCuotaPrestamo(Prestamo p)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();

            return lc.CalcularMontoCuotaPrestamo(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<Prestamo> ListarPrestamosAtrasados(Sucursal s)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();

            return lc.ListarPrestamosAtrasados(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<Prestamo> ListarPrestamo(Sucursal s, bool Cancelado)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();

            return lc.ListarPrestamo(s, Cancelado);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void AltaPrestamo(Prestamo s)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();

            lc.AltaPrestamo(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void CancelarPrestamo(Prestamo s)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();

            lc.CancelarPrestamo(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public Prestamo BuscarPrestamo(Prestamo s)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();

            return lc.BuscarPrestamo(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public List<Pago> IsPrestamoCancelado(ref Prestamo p)
    {
        try
        {
            ILogicaPrestamo lc = FabricaLogica.getLogicaPrestamo();
            return lc.IsPrestamoCancelado(ref p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion


    #region procedimientos Sucursal

    [WebMethod]
    public List<Sucursal> ListarSucursales()
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            return lc.ListarSucursales();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void AltaSucursal(Sucursal s)
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            lc.AltaSucursal(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void EliminarSucursal(Sucursal s)
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            lc.EliminarSucursal(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public Sucursal BuscarSucursal(Sucursal s)
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            return lc.BuscarSucursal(s);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public void ActualizarSucursal(Sucursal c)
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            lc.ActualizarSucursal(c);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin)
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            return lc.ListadoProductividadComparativo(fechaInicio, fechaFin);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public void ArqueoCaja(Empleado e, ref decimal saldoCajaDolares, ref decimal saldoCajaPesos,
       ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos)
    {
        try
        {
            ILogicaSucursal lc = FabricaLogica.getLogicaSucursal();
            lc.ArqueoCaja(e, ref  saldoCajaDolares, ref  saldoCajaPesos,
      ref  cantTotalDepositos, ref  cantTotalRetiros, ref  cantTotalPagos);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion


}