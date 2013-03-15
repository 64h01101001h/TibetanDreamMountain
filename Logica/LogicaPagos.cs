using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

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


        public void PagarCuota(Prestamo p)
        {
            try
            {
               // PersistenciaPrestamo persPrestamo = new PersistenciaPrestamo();
                IPersistenciaPagos persPrestamo = FabricaPersistencia.getPersistenciaPagos();
                //persPrestamo.
                //persPrestamo.
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
                //PersistenciaPagos ps = new PersistenciaPagos();
                IPersistenciaPagos ps = FabricaPersistencia.getPersistenciaPagos();

                return ps.ListarTodosPagosPrestamo(p);
                //return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void EliminarPago(Pago p)
        //{
        //    try
        //    {
        //        PersistenciaPagos pc = new PersistenciaPagos();
        //        pc.EliminarPrestamo(s);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Pago BuscarPago(Pago p)
        //{
        //    try
        //    {
        //        PersistenciaPagos pc = new PersistenciaPagos();
        //        return pc.();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
