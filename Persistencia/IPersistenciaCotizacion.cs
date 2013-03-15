using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaCotizacion
    {
        List<Cotizacion> ListarCotizaciones();
        void AltaCotizacion(Cotizacion s);
        void EliminarCotizacion(Cotizacion s);
        Cotizacion BuscarCotizacion(Cotizacion s);
        void ModificarCotizacion(Cotizacion s, Empleado e);
    }
}
