using System;

namespace Entidades
{
    [Serializable]
    public class Movimiento

    {
        private int _idMovimiento;
        public int IDMOVIMIENTO { get; set; }

        private int _tipoMovimiento;
        public int TIPOMOVIMIENTO { get; set; }

        private DateTime _fecha;
        public DateTime FECHA { get; set; }

        private Cotizacion _Cotizacion;
        public Cotizacion COTIZACION { get; set; }

        private Cuenta _cuenta;
        public Cuenta CUENTA { get; set; }

        private Usuario _usuario;
        public Usuario USUARIO { get; set; }

        private Sucursal _sucursal;
        public Sucursal SUCURSAL { get; set; }

        private string _moneda;
        public string MONEDA { get; set; }

        private bool _viaWeb;
        public bool VIAWEB { get; set; }

        private decimal _monto;
        public decimal MONTO { get; set; }



    }
}
