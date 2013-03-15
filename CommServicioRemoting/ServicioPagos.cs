using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
namespace CommServicioRemoting
{
    public class ServicioPagos
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaPagos unaPersistencia;

        //constructor del servicio
        public ServicioPagos()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaPagos();
        }

        public Pago ObtenerUltimoPagoPrestamo(Prestamo p) {
            return unaPersistencia.ObtenerUltimoPagoPrestamo(p);
        }
        public List<Pago> ListarUltimoPagoPrestamos(Sucursal s) {
            return unaPersistencia.ListarUltimoPagoPrestamos(s);
        }
        public List<Pago> ListarTodosPagosPrestamo(Prestamo p) {
            return unaPersistencia.ListarTodosPagosPrestamo(p);
        }
    }
}
