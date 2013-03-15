using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
namespace CommServicioRemoting
{
    public class ServicioCuentas
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaCuentas unaPersistencia;

        //constructor del servicio
        public ServicioCuentas()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaCuentas();
        }


        public void AltaCuenta(Cuenta c)
        {
            unaPersistencia.AltaCuenta(c);
        }
        public List<Cuenta> ListarCuentas()
        {
            return unaPersistencia.ListarCuentas();
        }
        public Cuenta BuscarCuenta(Cuenta Cuenta)
        {
            return unaPersistencia.BuscarCuenta(Cuenta);
        }
        public void ModificarCuenta(Cuenta c)
        {
            unaPersistencia.ModificarCuenta(c);
        }
        public void EliminarCuenta(Cuenta c)
        {
            unaPersistencia.EliminarCuenta(c);
        }
        public List<Cuenta> BuscarCuentasCliente(Cliente cliente)
        {
            return unaPersistencia.BuscarCuentasCliente(cliente);
        }
    }
}
