
using System;

namespace Persistencia
{
    public class FabricaPersistencia : MarshalByRefObject
    {

       
        public static IPersistenciaClientes getPersistenciaClientes()
        {
            return (PersistenciaClientes.GetInstancia());
        }

        public static IPersistenciaCotizacion getPersistenciaCotizacion()
        {
            return (PersistenciaCotizacion.GetInstancia());
        }

        public static IPersistenciaCuentas getPersistenciaCuentas()
        {
            return (PersistenciaCuentas.GetInstancia());
        }

        public static IPersistenciaEmpleados getPersistenciaEmpleados()
        {
            return (PersistenciaEmpleados.GetInstancia());
        }

        public static IPersistenciaMovimientos getPersistenciaMovimientos()
        {
            return (PersistenciaMovimientos.GetInstancia());
        }

        public static IPersistenciaPagos getPersistenciaPagos()
        {
            return (PersistenciaPagos.GetInstancia());
        }

        public static IPersistenciaPrestamo getPersistenciaPrestamo()
        {
            return (PersistenciaPrestamo.GetInstancia());
        }

        public static IPersistenciaSucursal getPersistenciaSucursal()
        {
            return (PersistenciaSucursal.GetInstancia());
        }
    }

}
