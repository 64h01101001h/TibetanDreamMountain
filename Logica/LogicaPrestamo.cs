using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

namespace Logica
{
    internal class LogicaPrestamo : ILogicaPrestamo
    {
        //singleton
        //------------------------------------------------
        private static LogicaPrestamo _instancia = null;
        private LogicaPrestamo() { }

        public static LogicaPrestamo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaPrestamo();

            return _instancia;
        }



        public decimal CalcularMontoCuotaPrestamo(Prestamo p)
        {
            try
            {
                Prestamo prestamo = BuscarPrestamo(p);
                if (prestamo != null)
                {
                    decimal monto = Decimal.Zero;
                    monto = prestamo.MONTO / prestamo.TOTALCUOTAS;
                    return monto;
                }

                return Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// LISTA LOS PRESTAMOS QUE ESTAN ATRASADOS EN SU PAGO
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Prestamo> ListarPrestamosAtrasados(Sucursal s)
        {
            try
            {
                //PersistenciaPagos pp = new PersistenciaPagos();
                IPersistenciaPagos pp = FabricaPersistencia.getPersistenciaPagos();
                List<Pago> ultimosPagos = pp.ListarUltimoPagoPrestamos(s);

                List<Prestamo> prestamosAtrasados = new List<Prestamo>();

                foreach (Pago pago in ultimosPagos)
                {
                    DateTime fechaUltimoPagoPrestamo = pago.FECHAPAGO;
                    DateTime fechaPagoPrestamoMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, pago.PRESTAMO.FECHAEMITIDO.Day);

                    if (fechaPagoPrestamoMesActual > fechaUltimoPagoPrestamo)
                    {
                        if (DateTime.Today > fechaPagoPrestamoMesActual)
                        {
                            prestamosAtrasados.Add(pago.PRESTAMO);
                        }
                    }
                }

                return prestamosAtrasados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Prestamo> ListarPrestamo(Sucursal s, bool Cancelado)
        {
            try
            {
                //PersistenciaPrestamo ps = new PersistenciaPrestamo();
                IPersistenciaPrestamo pp = FabricaPersistencia.getPersistenciaPrestamo();

                return pp.ListarPrestamos(s, Cancelado);
                //return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaPrestamo(Prestamo p)
        {
            try
            {
                //PersistenciaPrestamo pc = new PersistenciaPrestamo();
                IPersistenciaPrestamo pp = FabricaPersistencia.getPersistenciaPrestamo();

                pp.AltaPrestamo(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelarPrestamo(Prestamo s) //Cancelar prestamo, no elimina solo lo marca como cancelado en la base de datos.
        {
            try
            {
                //PersistenciaPrestamo pp = new PersistenciaPrestamo();
                IPersistenciaPrestamo pp = FabricaPersistencia.getPersistenciaPrestamo();

                pp.CancelarPrestamo(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Prestamo BuscarPrestamo(Prestamo s)
        {
            try
            {
                //PersistenciaPrestamo pp = new PersistenciaPrestamo();
                IPersistenciaPrestamo pp = FabricaPersistencia.getPersistenciaPrestamo();

                return pp.BuscarPrestamo(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Pago> IsPrestamoCancelado(ref Prestamo p)
        {
            try
            {
                p = BuscarPrestamo(p);
                if (p != null)
                {
                    ILogicaPagos lp = FabricaLogica.getLogicaPagos();
                    List<Pago> pagos = lp.ListarPagos(p);

                    if (pagos.Count >= p.TOTALCUOTAS)
                    {
                        p.CANCELADO = true;
                    }
                    else
                    {
                        p.CANCELADO = false;
                    }

                    return pagos;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
