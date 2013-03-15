using System;

namespace Entidades
{

    [Serializable]
    public class Prestamo
    {
        private int _idPrestamo;
        public int IDPRESTAMO { get; set; }

        private Sucursal _sucursal;
        public Sucursal SUCURSAL { get; set; }

        private DateTime _fechaEmitido;
        public DateTime FECHAEMITIDO { get; set; }

        private int _totalCuotas;
        public int TOTALCUOTAS { get; set; }

        private bool _cancelado;
        public bool CANCELADO { get; set; }

        private string _moneda;
        public string MONEDA { get; set; }

        private decimal _monto;
        public decimal MONTO { get; set; }

        private Cliente _cliente;
        public Cliente CLIENTE { get; set; }
    }
}
