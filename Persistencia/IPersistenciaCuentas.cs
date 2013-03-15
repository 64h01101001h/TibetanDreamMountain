using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaCuentas
    {
        void AltaCuenta(Cuenta c);
        List<Cuenta> ListarCuentas();
        Cuenta BuscarCuenta(Cuenta Cuenta);
        void ModificarCuenta(Cuenta c);
        void EliminarCuenta(Cuenta c);
        List<Cuenta> BuscarCuentasCliente(Cliente cliente);
    }
}
