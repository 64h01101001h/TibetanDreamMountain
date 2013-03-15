using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    public interface ILogicaPagos
    {
         void PagarCuota(Prestamo p);
         List<Pago> ListarPagos(Prestamo p);
         //void EliminarPago(Pago p);
         //Pago BuscarPago(Pago p);


    }
}
