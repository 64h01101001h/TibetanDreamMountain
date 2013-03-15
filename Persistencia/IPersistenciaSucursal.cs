using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaSucursal
    {
        void AltaSucursal(Sucursal L);
        decimal ArqueoCaja(DateTime Fecha, string Moneda, Empleado E);
        void ModificarSucursal(Sucursal L);
        List<Sucursal> ListarSucursales();
        List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin);
        decimal TotalesArqueoCaja(Empleado E, ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos);
        Sucursal BuscarSucursal(Sucursal sucursal);
    }
}
