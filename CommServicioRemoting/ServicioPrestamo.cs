using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace CommServicioRemoting
{
    public class ServicioPrestamo
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaPrestamo unaPersistencia;

        //constructor del servicio
        public ServicioPrestamo()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaPrestamo();
        }

        public void AltaPrestamo(Prestamo P)
        {
            unaPersistencia.AltaPrestamo(P);
        }

        public void CancelarPrestamo(Prestamo P)
        {
            unaPersistencia.CancelarPrestamo(P);
        }


        public List<Prestamo> ListarPrestamos(Sucursal s, bool cancelado)
        {
            return unaPersistencia.ListarPrestamos(s, cancelado);
        }
        public Prestamo BuscarPrestamo(Prestamo P)
        {
            return unaPersistencia.BuscarPrestamo(P);
        }
        public void PagarPrestamo(Prestamo P, Empleado E)
        {
            unaPersistencia.PagarPrestamo(P, E);
        }
    }
}
