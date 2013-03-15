using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
namespace CommServicioRemoting
{
    public class ServicioMovimientos
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaMovimientos unaPersistencia;

        //constructor del servicio
        public ServicioMovimientos()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaMovimientos();
        }

        public void RealizarMovimiento(Movimiento m)
        {

            unaPersistencia.RealizarMovimiento(m);
        }
        public void RealizarTransferencia(Movimiento morigen, Movimiento mdestino)
        {
            unaPersistencia.RealizarTransferencia(morigen, mdestino);
        }
        public List<Movimiento> ConsultaMovimientos(Cuenta Cuenta, DateTime d)
        {
            return unaPersistencia.ConsultaMovimientos(Cuenta, d);
        }
    }
}
