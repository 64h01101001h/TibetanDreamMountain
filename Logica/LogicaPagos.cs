using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    internal class LogicaPagos : ILogicaPagos
    {

        //singleton
        //------------------------------------------------
        private static LogicaPagos _instancia = null;
        private LogicaPagos() { }

        public static LogicaPagos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaPagos();

            return _instancia;
        }


        public void PagarCuota(Prestamo p, Empleado e)
        {
            try
            {
                CommServicioRemoting.ServicioPrestamo remPagos = new CommServicioRemoting.ServicioPrestamo();
                remPagos.PagarPrestamo(p, e);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pago> ListarPagos(Prestamo p)
        {
            try
            {
                CommServicioRemoting.ServicioPagos remPagos = new CommServicioRemoting.ServicioPagos();


                return remPagos.ListarTodosPagosPrestamo(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}
