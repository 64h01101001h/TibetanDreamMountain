using System.Collections.Generic;
using System;

namespace Entidades
{
    [Serializable]
    public class Cliente :Usuario
    {
        //private int _idCliente;
        //public int IDCLIENTE { get; set; }

        private string _direccion;
        public string DIRECCION { get; set; }

        private List<string> _telefonos;
        public List<string> TELEFONOS { get; set; }



    }
}
