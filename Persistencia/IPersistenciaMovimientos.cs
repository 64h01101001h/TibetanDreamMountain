using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaMovimientos
    {
        void RealizarMovimiento(Movimiento m);
        void RealizarTransferencia(Movimiento morigen, Movimiento mdestino);
        List<Movimiento> ConsultaMovimientos(Cuenta Cuenta, DateTime d);
    }
}
