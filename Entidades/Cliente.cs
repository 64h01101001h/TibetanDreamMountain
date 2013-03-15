using System.Collections.Generic;
using System;

namespace Entidades
{
    [Serializable]
    public class Cliente :Usuario
    {
      
        private string _direccion;
        public string DIRECCION { get; set; }

        private List<string> _telefonos;
        public List<string> TELEFONOS { get; set; }



    }
}
