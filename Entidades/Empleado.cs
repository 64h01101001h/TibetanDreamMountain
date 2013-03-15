
using System.Collections.Generic;
using System;

namespace Entidades
{
    [Serializable]
    public class Empleado : Usuario
    {

        private Sucursal _sucursal;
        public Sucursal SUCURSAL { get; set; }

        public string SUCURSALTOSTRING
        {
            get
            {
                if (SUCURSAL != null)
                    return SUCURSAL.TOSTRING;
                return null;
            }
            set
            {

            }
        }
    }
}
