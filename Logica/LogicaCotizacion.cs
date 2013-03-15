using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ExcepcionesPersonalizadas;
namespace Logica
{
    internal class LogicaCotizacion : ILogicaCotizacion
    {
        //singleton
        //------------------------------------------------
        private static LogicaCotizacion _instancia = null;
        private LogicaCotizacion() { }

        public static LogicaCotizacion GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCotizacion();

            return _instancia;
        }


        public List<Cotizacion> ListarCotizaciones()
        {
            try
            {
                CommServicioRemoting.ServicioCotizacion remoteCotizacion = new CommServicioRemoting.ServicioCotizacion();
                return remoteCotizacion.ListarCotizaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaCotizacion(Cotizacion s)
        {
            try
            {
                CommServicioRemoting.ServicioCotizacion remoteCotizacion = new CommServicioRemoting.ServicioCotizacion();



                remoteCotizacion.AltaCotizacion(s);
            }
            catch (ErrorCotizacionYaExiste ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCotizacion(Cotizacion s)
        {
            try
            {
                CommServicioRemoting.ServicioCotizacion remoteCotizacion = new CommServicioRemoting.ServicioCotizacion();


                remoteCotizacion.EliminarCotizacion(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cotizacion BuscarCotizacion(Cotizacion s)
        {
            try
            {
                CommServicioRemoting.ServicioCotizacion remoteCotizacion = new CommServicioRemoting.ServicioCotizacion();


                return remoteCotizacion.BuscarCotizacion(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCotizacion(Cotizacion s, Empleado e)
        {
            try
            {
                CommServicioRemoting.ServicioCotizacion remoteCotizacion = new CommServicioRemoting.ServicioCotizacion();


                remoteCotizacion.ModificarCotizacion(s, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
