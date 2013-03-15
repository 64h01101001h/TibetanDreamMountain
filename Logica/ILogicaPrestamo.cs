using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    public interface ILogicaPrestamo
    {
         decimal CalcularMontoCuotaPrestamo(Prestamo p);
         List<Prestamo> ListarPrestamosAtrasados(Sucursal s);
         List<Prestamo> ListarPrestamo(Sucursal s, bool Cancelado);
         void AltaPrestamo(Prestamo s);
         void CancelarPrestamo(Prestamo s);
         Prestamo BuscarPrestamo(Prestamo s);
         List<Pago> IsPrestamoCancelado(ref Prestamo p);
    }
}
