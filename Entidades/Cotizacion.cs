
using System;

namespace Entidades
{
    [Serializable]
    public class Cotizacion
    {
        private DateTime _fecha;
        public DateTime FECHA { get; set; }

        private decimal _precioCompra;
        public decimal PRECIOCOMPRA { get; set; }

        private decimal _precioVenta;
        public decimal PRECIOVENTA { get; set; }


        public override string ToString()
        {
            return "Cotizacion del dolar para la fecha: " + FECHA.ToShortDateString()  + " Compra: $" + PRECIOCOMPRA + " " + " Venta: $"  + PRECIOVENTA;
        }
    }
}
