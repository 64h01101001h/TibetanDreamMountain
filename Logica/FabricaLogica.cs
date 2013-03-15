using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaCotizacion getLogicaCotizacion()
        {
            return LogicaCotizacion.GetInstancia();
        }

        public static ILogicaCuentas getLogicaCuentas()
        {
            return LogicaCuentas.GetInstancia();
        }

        public static ILogicaUsuarios getLogicaUsuario()
        {
            return LogicaUsuarios.GetInstancia();
        }

        public static ILogicaPagos getLogicaPagos()
        {
            return LogicaPagos.GetInstancia();
        }

        public static ILogicaPrestamo getLogicaPrestamo()
        {
            return LogicaPrestamo.GetInstancia();
        }

        public static ILogicaSucursal getLogicaSucursal()
        {
            return LogicaSucursal.GetInstancia();
        }
    }
}
