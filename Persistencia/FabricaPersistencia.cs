
using System;

namespace Persistencia
{
    public class FabricaPersistencia : MarshalByRefObject
    {

       
        public IPersistenciaClientes getPersistenciaClientes()
        {
            return (PersistenciaClientes.GetInstancia());
        }

        public IPersistenciaCotizacion getPersistenciaCotizacion()
        {
            return (PersistenciaCotizacion.GetInstancia());
        }

        public IPersistenciaCuentas getPersistenciaCuentas()
        {
            return (PersistenciaCuentas.GetInstancia());
        }

        public IPersistenciaEmpleados getPersistenciaEmpleados()
        {
            return (PersistenciaEmpleados.GetInstancia());
        }

        public IPersistenciaMovimientos getPersistenciaMovimientos()
        {
            return (PersistenciaMovimientos.GetInstancia());
        }

        public IPersistenciaPagos getPersistenciaPagos()
        {
            return (PersistenciaPagos.GetInstancia());
        }

        public IPersistenciaPrestamo getPersistenciaPrestamo()
        {
            return (PersistenciaPrestamo.GetInstancia());
        }

        public IPersistenciaSucursal getPersistenciaSucursal()
        {
            return (PersistenciaSucursal.GetInstancia());
        }
    }

}
