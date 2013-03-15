using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
     public interface ILogicaCuentas
    {
         void AltaCuenta(Cuenta c);
         List<Cuenta> ListarCuentas();
         List<Cuenta> ListarCuentasCliente(Cliente c);
         void EliminarCuenta(Cuenta c);
         Cuenta BuscarCuenta(Cuenta c);
         void ActualizarCuenta(Cuenta c);
         void RealizarMovimiento(Movimiento m);
         List<Movimiento> ConsultaMovimientosCuenta(Cuenta c, DateTime d);
         void RealizarTransferencia(Movimiento movOrigen, Movimiento movDestino);
    }
}
