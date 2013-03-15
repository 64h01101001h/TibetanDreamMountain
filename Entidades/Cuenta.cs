using System.Collections.Generic;
using System;

namespace Entidades
{
    [Serializable]
    public class Cuenta
    {
        private int _idCuenta;
        public int IDCUENTA { get; set; }

        private Sucursal _sucursal;
        public Sucursal SUCURSAL { get; set; }

        private string _moneda;
        public string MONEDA { get; set; }

        private Cliente _cliente;
        public Cliente CLIENTE { get; set; }

        private decimal _saldo;
        public decimal SALDO { get; set; }

        private List<Movimiento> _movimientosCuenta;
        public List<Movimiento> MOVIMIENTOSCUENTA { get; set; }

        private DateTime _fechaApertura;
        public DateTime FECHAAPERTURA { get; set; }


        public override string ToString()
        {
            return "Cuenta:" + " " + IDCUENTA + " - " + "Moneda: " + MONEDA + " - " + "Saldo: " + SALDO;
        }

        public string TOSTRING
        {
            get
            {
                return ToString();
            }
        }

    }
}
