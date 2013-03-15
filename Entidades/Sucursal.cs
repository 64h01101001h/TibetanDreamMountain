using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{

    [Serializable]
   public class Sucursal

   {
       private int _idSucursal;
       public int IDSUCURSAL { get; set; }

       private string _nombre;
       public string NOMBRE { get; set; }

       private string _direccion;
       public string DIRECCION { get; set; }

       private bool _activa;
       public bool ACTIVA { get; set; }

       private List<Cuenta> _cuentasSucursal;
       public List<Cuenta> CUENTASSUCURSAL { get; set; }

       private List<Empleado> _empleadosSucursal;
       public List<Empleado> EMPLEADOSUCURSAL { get; set; }

       private List<Prestamo> _prestamosSucursal;
       public List<Prestamo> PRESTAMOSSUCURSAL { get; set; }

       private int _cantidadPrestamos;
       private int _cantidadCuentasAbiertas;
       public int CANTIDADPRESTAMOS { get; set; }
       public int CANTIDADCUENTASABIERTAS { get; set; }

       public string TOSTRING
       {
           get
           {
               return NOMBRE;
           }
           set
           {

           }
       }
   }
}
