
using System;
namespace Entidades
{

    [Serializable]
    public class Usuario
    {

        private int _ci;
        public int CI { get; set; }

        private string _nombreUsuario;
        public string NOMBREUSUARIO { get; set; }

        private string _nombre;
        public string NOMBRE { get; set; }

        private string _apellido;
        public string APELLIDO { get; set; }

        private string _pass;
        public string PASS { get; set; }

        private bool _activo;
        public bool ACTIVO { get; set; }


        public override string ToString()
        {
            return NOMBRE + " " + APELLIDO + " "  + "(" + CI + ")";
        }

    }
}
