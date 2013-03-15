
using System.Collections.Generic;
using System;

namespace Entidades
{
    [Serializable]
    public class Empleado: Usuario
    {

        private Sucursal _sucursal;
        public Sucursal SUCURSAL { get; set; }

       
    }
}
