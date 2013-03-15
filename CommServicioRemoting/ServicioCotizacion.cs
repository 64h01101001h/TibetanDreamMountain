using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
namespace CommServicioRemoting
{
    public class ServicioCotizacion
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaCotizacion unaPersistencia;

        //constructor del servicio
        public ServicioCotizacion()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaCotizacion();
        }


        public List<Cotizacion> ListarCotizaciones()
        {

            return unaPersistencia.ListarCotizaciones();
        }
        public void AltaCotizacion(Cotizacion s)
        {
            unaPersistencia.AltaCotizacion(s);
        }
        public void EliminarCotizacion(Cotizacion s)
        {

            unaPersistencia.EliminarCotizacion(s);
        }
        public Cotizacion BuscarCotizacion(Cotizacion s)
        {
            return unaPersistencia.BuscarCotizacion(s);
        }
        public void ModificarCotizacion(Cotizacion s, Empleado e)
        {
            unaPersistencia.ModificarCotizacion(s, e);
        }
    }
}
