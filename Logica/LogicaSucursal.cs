using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    public class LogicaSucursal : ILogicaSucursal
    {
         //singleton
        //------------------------------------------------
        private static LogicaSucursal _instancia = null;
        private LogicaSucursal() { }

        public static LogicaSucursal GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaSucursal();

            return _instancia;
        }


        public List<Sucursal> ListarSucursales()
        {
            try
            {
                //PersistenciaSucursal ps = new PersistenciaSucursal();
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();
                return remotingSucursal.ListarSucursales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaSucursal(Sucursal s)
        {
            try
            {
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();

                remotingSucursal.AltaSucursal(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarSucursal(Sucursal s)
        {
            try
            {
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();

               // remotingSucursal.eli
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sucursal BuscarSucursal(Sucursal s)
        {
            try
            {
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();

                return remotingSucursal.BuscarSucursal(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSucursal(Sucursal c)
        {
            try
            {
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();


                remotingSucursal.ModificarSucursal(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Sucursal> ListadoProductividadComparativo (DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();


                return remotingSucursal.ListadoProductividadComparativo(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public void ArqueoCaja (Empleado e, ref decimal saldoCajaDolares, ref decimal saldoCajaPesos,
            ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos)
        {
            try
            {
                CommServicioRemoting.ServicioSucursal remotingSucursal = new CommServicioRemoting.ServicioSucursal();


                saldoCajaDolares = remotingSucursal.ArqueoCaja(DateTime.Now, "USD", e);
                saldoCajaPesos = remotingSucursal.ArqueoCaja(DateTime.Now, "UYU", e);

                remotingSucursal.TotalesArqueoCaja(e, ref cantTotalDepositos, ref cantTotalRetiros, ref cantTotalPagos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
