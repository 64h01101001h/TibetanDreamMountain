using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    public interface ILogicaSucursal
    {
         List<Sucursal> ListarSucursales();
         void AltaSucursal(Sucursal s);
         void EliminarSucursal(Sucursal s);
         Sucursal BuscarSucursal(Sucursal s);
         void ActualizarSucursal(Sucursal c);
         List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin);
         void ArqueoCaja(Empleado e, ref decimal saldoCajaDolares, ref decimal saldoCajaPesos,
            ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos);
    }
}
