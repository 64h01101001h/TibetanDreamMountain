using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace CommServicioRemoting
{
   public  class ServicioSucursal {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaSucursal unaPersistencia;
        
         //constructor del servicio
        public ServicioSucursal()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaSucursal();
        }

        public void AltaSucursal(Sucursal L) {
            unaPersistencia.AltaSucursal(L);
        }
        public decimal ArqueoCaja(DateTime Fecha, string Moneda, Empleado E) {
            return unaPersistencia.ArqueoCaja(Fecha, Moneda, E);
        }
        public void ModificarSucursal(Sucursal L) {

            unaPersistencia.ModificarSucursal(L);
        }
        public List<Sucursal> ListarSucursales() {

            return unaPersistencia.ListarSucursales();
        }
        public List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin) {

            return unaPersistencia.ListadoProductividadComparativo(fechaInicio, fechaFin);
        }
        public decimal TotalesArqueoCaja(Empleado E, ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos) {
            return unaPersistencia.TotalesArqueoCaja(E,ref cantTotalDepositos, ref cantTotalRetiros, ref cantTotalPagos);
        }
        public Sucursal BuscarSucursal(Sucursal sucursal) {
            return unaPersistencia.BuscarSucursal(sucursal);
        }
    }
}
