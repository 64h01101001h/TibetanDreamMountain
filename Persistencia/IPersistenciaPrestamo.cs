using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaPrestamo
    {

        void AltaPrestamo(Prestamo P);
        void CancelarPrestamo(Prestamo P);
        List<Prestamo> ListarPrestamos(Sucursal s, bool cancelado);
        Prestamo BuscarPrestamo(Prestamo P);

    }
}
