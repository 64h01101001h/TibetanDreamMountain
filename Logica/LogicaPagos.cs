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


        public void PagarCuota(Prestamo p, Empleado e)
        {
            try
            {
                //ILogicaPrestamo logPres = FabricaLogica.getLogicaPrestamo();
                //Prestamo pe = logPres.IsPrestamoCancelado(p);

                IPersistenciaPrestamo persPrestamo = FabricaPersistencia.getPersistenciaPrestamo();
                persPrestamo.PagarPrestamo(p, e);

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
                IPersistenciaPagos ps = FabricaPersistencia.getPersistenciaPagos();

                return ps.ListarTodosPagosPrestamo(p);
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
