using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
   public interface ILogicaCotizacion
    {
         List<Cotizacion> ListarCotizaciones();
         void AltaCotizacion(Cotizacion s);
         void EliminarCotizacion(Cotizacion s);
         Cotizacion BuscarCotizacion(Cotizacion s);
         void ActualizarCotizacion(Cotizacion s, Empleado e);


    }
}
