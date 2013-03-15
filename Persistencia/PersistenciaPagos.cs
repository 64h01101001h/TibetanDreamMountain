using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaPagos : IPersistenciaPagos
    {
        private static PersistenciaPagos _instancia = null;

        private PersistenciaPagos() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaPagos GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaPagos());
        }
        public Pago ObtenerUltimoPagoPrestamo(Prestamo p)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarUltimoPagoPrestamo", conexion, CommandType.StoredProcedure);
            SqlParameter _numSucursal = new SqlParameter("@IdPrestamo", p.IDPRESTAMO);
            cmd.Parameters.Add(_numSucursal);
            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                _Reader = cmd.ExecuteReader();
                int _idEmpleado, _idPrestamo, _idRecibo, _numCuota;
                decimal _montoPago;
                DateTime _fechaPago;
                Pago pago = null;
                while (_Reader.Read())
                {
                    _fechaPago = Convert.ToDateTime(_Reader["Fecha"]);
                    _idPrestamo = Convert.ToInt32(_Reader["IdPrestamo"]);
                    _numCuota = Convert.ToInt32(_Reader["NumeroCuota"]);
                    _montoPago = Convert.ToDecimal(_Reader["Monto"]);
                    _idEmpleado = Convert.ToInt32(_Reader["IdEmpleado"]);
                    _idRecibo = Convert.ToInt32(_Reader["IdRecibo"]);
                    pago = new Pago
                    {
                        EMPLEADO = new Empleado { CI = _idEmpleado },
                        FECHAPAGO = _fechaPago,
                        IDRECIBO = _idRecibo,
                        MONTO = _montoPago,
                        NUMEROCUOTA = _numCuota,
                        PRESTAMO = p
                    };

                }
                _Reader.Close();

                return pago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }


        /// <summary>
        /// LISTA EL ULTIMO PAGO DE LOS PRESTAMOS NO CANCELADOS AUN
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Pago> ListarUltimoPagoPrestamos(Sucursal s)
        {
            try
            {
                IPersistenciaPrestamo persPrestamo = FabricaPersistencia.getPersistenciaPrestamo();
                //Busco los prestamos que aun no han sido cancelados
                List<Prestamo> prestamosCancelados = persPrestamo.ListarPrestamos(s, false);

                List<Pago> ultimosPagos = new List<Pago>();
                foreach (Prestamo prestamo in prestamosCancelados)
                {
                    Pago p = ObtenerUltimoPagoPrestamo(prestamo);
                    if (p != null)
                    {
                        ultimosPagos.Add(p);
                    }
                }

                return ultimosPagos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pago> ListarTodosPagosPrestamo(Prestamo p)
        {
            try
            {
                List<Pago> pagos = new List<Pago>();
                return pagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
