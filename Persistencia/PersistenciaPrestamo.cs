using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Persistencia
{
    internal class PersistenciaPrestamo : IPersistenciaPrestamo
    {
        private static PersistenciaPrestamo _instancia = null;

        private PersistenciaPrestamo() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaPrestamo GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaPrestamo());
        }
        public void AltaPrestamo(Prestamo P)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaPrestamo", conexion, CommandType.StoredProcedure);

                SqlParameter _IdCliente = new SqlParameter("@IdCliente", P.CLIENTE.CI);
                SqlParameter _IdSucursal = new SqlParameter("@NumeroSucursal", P.SUCURSAL.IDSUCURSAL);
                SqlParameter _Fecha = new SqlParameter("@Fecha", P.FECHAEMITIDO);
                SqlParameter _Cuotas = new SqlParameter("@Cuotas", P.TOTALCUOTAS);
                SqlParameter _Moneda = new SqlParameter("@Moneda", P.MONEDA);
                SqlParameter _Monto = new SqlParameter("@Monto", P.MONTO);
                SqlParameter _retorno = new SqlParameter("@Mont", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_IdCliente);
                cmd.Parameters.Add(_IdSucursal);
                cmd.Parameters.Add(_Fecha);
                cmd.Parameters.Add(_Cuotas);
                cmd.Parameters.Add(_Moneda);
                cmd.Parameters.Add(_Monto);
                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    throw new ErrorSucursalNoExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) <= -4)
                    throw new ErrorBaseDeDatos();

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

        /*CancelarPrestamo*/
        public void CancelarPrestamo(Prestamo P)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("CancelarPrestamo", conexion, CommandType.StoredProcedure);

                SqlParameter _IdSucursal = new SqlParameter("@IdSucursal", P.SUCURSAL.IDSUCURSAL);
                SqlParameter _IdPrestamo = new SqlParameter("@NumeroPrestamo", P.IDPRESTAMO);
                SqlParameter _retorno = new SqlParameter("@Mont", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_IdSucursal);
                cmd.Parameters.Add(_IdPrestamo);

                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    throw new ErrorSucursalNoExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) <= -4)
                    throw new ErrorBaseDeDatos();

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

        public void PagarPrestamo(Prestamo P, Empleado E)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaPago", conexion, CommandType.StoredProcedure);
                SqlParameter _IdSucursal = new SqlParameter("@NumeroSucursal", P.SUCURSAL.IDSUCURSAL);
                SqlParameter _IdEmpleado = new SqlParameter("@IdEmpleado", E.CI);
                SqlParameter _IdPrestamo = new SqlParameter("@IdPrestamo", P.IDPRESTAMO);
                SqlParameter _Monto = new SqlParameter("@Monto",P.MONTO);
                SqlParameter _Fecha = new SqlParameter("@Fecha",DateTime.Now);
                SqlParameter _retorno = new SqlParameter("@Mont", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_IdSucursal);
                cmd.Parameters.Add(_IdEmpleado);
                cmd.Parameters.Add(_IdPrestamo);
                cmd.Parameters.Add(_Monto);
                cmd.Parameters.Add(_Fecha);

                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    throw new ErrorSucursalNoExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorBaseDeDatos();
      

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

        public List<Pago> ListarPagos(Sucursal s, Prestamo p)
        {

            List<Pago> listaPagos = new List<Pago>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarPagosPorPrestamo", conexion, CommandType.StoredProcedure);
            SqlParameter _numSucursal = new SqlParameter("@IdSucursal", s.IDSUCURSAL);
            SqlParameter _idPrestamo = new SqlParameter("@IdPrestamo", p.IDPRESTAMO);
            cmd.Parameters.Add(_numSucursal);
            cmd.Parameters.Add(_idPrestamo);
            SqlDataReader _Reader;
            try
            {
                /*IdRecibo int identity primary key not null,
				   IdEmpleado int,
				   IdPrestamo int,
				   NumeroSucursal int,
				   Monto float not null,
				   Fecha datetime not null,
				   NumeroCuota int not null,*/
                conexion.Open();
                _Reader = cmd.ExecuteReader();
                int idRecibo, idPrestamo, idSucursal, idEmpleado, NumeroCuota;
                decimal monto;
                DateTime Fecha;

                while (_Reader.Read())
                {
                    idRecibo = Convert.ToInt32(_Reader["IdRecibo"]);
                    idPrestamo = Convert.ToInt32(_Reader["IdPrestamo"]);
                    idSucursal = Convert.ToInt32(_Reader["Cuotas"]);
                    idEmpleado = Convert.ToInt32(_Reader["IdCliente"]);
                    NumeroCuota = Convert.ToInt32(_Reader["NumeroCuota"]);
                    monto = Convert.ToDecimal(_Reader["Monto"]);
                    Fecha = Convert.ToDateTime(_Reader["Fecha"]);
                    
                    Pago pago = new Pago
                    {
                        PRESTAMO = new Prestamo { IDPRESTAMO = idPrestamo},
                        EMPLEADO = new Empleado { CI = idEmpleado },
                        FECHAPAGO = Fecha,
                        IDRECIBO = idRecibo,
                        NUMEROCUOTA = NumeroCuota,
                        MONTO = monto
                    };

                    listaPagos.Add(pago);
                }
                _Reader.Close();

                return listaPagos;
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

        public List<Prestamo> ListarPrestamos(Sucursal s, bool cancelado)
        {

            List<Prestamo> listaPrestamos = new List<Prestamo>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarPrestamos", conexion, CommandType.StoredProcedure);
            SqlParameter _numSucursal = new SqlParameter("@IdSucursal", s.IDSUCURSAL);
            SqlParameter _cancelado = new SqlParameter("@Cancelado", cancelado);
            cmd.Parameters.Add(_numSucursal);
            cmd.Parameters.Add(_cancelado);
            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                _Reader = cmd.ExecuteReader();
                int _idEmpleado, _idPrestamo, _cuotasPrestamo, _idCliente;
                string _moneda;
                decimal _montoTotalPrestamo;
                DateTime _fechaEmitidoPrestamo;

                while (_Reader.Read())
                {
                    _fechaEmitidoPrestamo = Convert.ToDateTime(_Reader["Fecha"]);
                    _idPrestamo = Convert.ToInt32(_Reader["IdPrestamo"]);
                    _cuotasPrestamo = Convert.ToInt32(_Reader["Cuotas"]);
                    _idCliente = Convert.ToInt32(_Reader["IdCliente"]);
                    _moneda = Convert.ToString(_Reader["Moneda"]);
                    _montoTotalPrestamo = Convert.ToDecimal(_Reader["Monto"]);
                    ///_idEmpleado = Convert.ToInt32(_Reader["IdEmpleado"]);

                    Prestamo p = new Prestamo
                    {
                        CANCELADO = cancelado,
                        CLIENTE = new Cliente { CI = _idCliente },
                        FECHAEMITIDO = _fechaEmitidoPrestamo,
                        IDPRESTAMO = _idPrestamo,
                        MONEDA = _moneda,
                        MONTO = _montoTotalPrestamo,
                        SUCURSAL = s,
                        TOTALCUOTAS = _cuotasPrestamo
                    };

                    listaPrestamos.Add(p);
                }
                _Reader.Close();

                return listaPrestamos;
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


        public Prestamo BuscarPrestamo(Prestamo P)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarPrestamo", conexion, CommandType.StoredProcedure);
                SqlParameter _IdPrestamo = new SqlParameter("@IdPrestamo", P.IDPRESTAMO);
                cmd.Parameters.Add(_IdPrestamo);

                SqlDataReader _Reader;
                Prestamo PNuevo = null;
                int _idCuenta, _idSucursal, _idCliente,  _Cuotas, _Cancelado = 0;
                decimal _Monto;
                string _moneda, _nombreCliente, _apellidoCliente;
                DateTime _Fecha;


                conexion.Open();
                _Reader = cmd.ExecuteReader();
                if (_Reader.Read())
                {
                    //_idCuenta = (int)_Reader["IdCuenta"];
                    _idCliente = (int)_Reader["IdCliente"];
                    _idSucursal = (int)_Reader["NumeroSucursal"];
                    _Monto = Convert.ToDecimal(_Reader["Monto"]);
                    _moneda = (string)_Reader["Moneda"];
                    _Fecha = Convert.ToDateTime(_Reader["Fecha"]);
                    _Cuotas = (int)_Reader["Cuotas"];
                    _nombreCliente = (string)_Reader["Nombre"];
                    _apellidoCliente = (string)_Reader["Apellido"];

                    PNuevo = new Prestamo
                    {

                        FECHAEMITIDO = _Fecha,
                        MONTO = _Monto,
                        IDPRESTAMO = P.IDPRESTAMO,
                        TOTALCUOTAS = _Cuotas,
                        CLIENTE = new Cliente{CI=_idCliente, NOMBRE = _nombreCliente, APELLIDO = _apellidoCliente},
                        MONEDA = _moneda,
                        SUCURSAL = new Sucursal{IDSUCURSAL = _idSucursal}
                    };
                }
                _Reader.Close();


                if (_Cancelado == 1)
                {
                    PNuevo.CANCELADO = true;
                }
                return PNuevo;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }


        }


        








































      

    }
}