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

    internal class PersistenciaMovimientos : IPersistenciaMovimientos
    {
        private static PersistenciaMovimientos _instancia = null;

        private PersistenciaMovimientos() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaMovimientos GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaMovimientos());
        }
        public void RealizarMovimiento(Movimiento m)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaMovimiento", conexion, CommandType.StoredProcedure);

                SqlParameter _idSucursal = new SqlParameter("@IdSucursal", m.SUCURSAL.IDSUCURSAL);
                SqlParameter _tipo = new SqlParameter("@Tipo", m.TIPOMOVIMIENTO);
                SqlParameter _fecha = new SqlParameter("@Fecha", m.FECHA);
                SqlParameter _moneda = new SqlParameter("@Moneda", m.MONEDA);
                SqlParameter _viaWeb = new SqlParameter("@ViaWeb", m.VIAWEB);
                SqlParameter _monto = new SqlParameter("@Monto", m.MONTO);
                SqlParameter _ciUsuario = new SqlParameter("@CiUsuario", m.USUARIO.CI);
                SqlParameter _idCuenta = new SqlParameter("@IdCuenta", m.CUENTA.IDCUENTA);

                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_idSucursal);
                cmd.Parameters.Add(_tipo);
                cmd.Parameters.Add(_fecha);
                cmd.Parameters.Add(_moneda);
                cmd.Parameters.Add(_viaWeb);
                cmd.Parameters.Add(_monto);
                cmd.Parameters.Add(_ciUsuario);
                cmd.Parameters.Add(_idCuenta);

                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                    throw new ErrorSucursalNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -3)
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


        public void RealizarTransferencia(Movimiento morigen, Movimiento mdestino)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("spRealizarTransferencia", conexion, CommandType.StoredProcedure);

                SqlParameter _idSucursalOrigen = new SqlParameter("@IdSucursalOrigen", morigen.SUCURSAL.IDSUCURSAL);
                SqlParameter _idSucursalDestino = new SqlParameter("@IdSucursalDestino", mdestino.SUCURSAL.IDSUCURSAL);

                SqlParameter _moneda = new SqlParameter("@Moneda", morigen.MONEDA);
                SqlParameter _monto = new SqlParameter("@Monto", morigen.MONTO);
                SqlParameter _ciUsuario = new SqlParameter("@CiUsuario", morigen.USUARIO.CI);
                SqlParameter _idCuentaOrigen = new SqlParameter("@IdCuentaOrigen", morigen.CUENTA.IDCUENTA);
                SqlParameter _idCuentaDestino = new SqlParameter("@IdCuentaDestino", mdestino.CUENTA.IDCUENTA);


                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_idSucursalOrigen);
                cmd.Parameters.Add(_idSucursalDestino);

                cmd.Parameters.Add(_moneda);
                cmd.Parameters.Add(_monto);
                cmd.Parameters.Add(_ciUsuario);
                cmd.Parameters.Add(_idCuentaOrigen);
                cmd.Parameters.Add(_idCuentaDestino);


                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                    throw new ErrorSucursalNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -3)
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



        public List<Movimiento> ConsultaMovimientos(Cuenta Cuenta, DateTime d)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spConstultaMovimientos", conexion, CommandType.StoredProcedure);
                SqlParameter _idCuentaParam = new SqlParameter("@IdCuenta", Cuenta.IDCUENTA);
                SqlParameter _fecha = new SqlParameter("@FechaInicio", d);

                cmd.Parameters.Add(_idCuentaParam);
                cmd.Parameters.Add(_fecha);

                SqlDataReader _Reader;
                List<Movimiento> movimientos = new List<Movimiento>();
                int _idCuenta, _idSucursal, _idCliente, _NumMovimiento, _tipo;
                decimal _monto;
                string _moneda;
                DateTime _fechaMovimiento;
                bool _viaWeb;


                conexion.Open();
                _Reader = cmd.ExecuteReader();
                if (_Reader.Read())
                {
                    _idCuenta = (int)_Reader["IdCuenta"];
                    _idCliente = (int)_Reader["CiUsuario"];
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _monto = Convert.ToDecimal(_Reader["Monto"]);
                    _moneda = (string)_Reader["Moneda"];
                    _fechaMovimiento = Convert.ToDateTime(_Reader["Fecha"]);
                    _NumMovimiento = (int)_Reader["NumeroMovimiento"];
                    _viaWeb = Convert.ToBoolean(_Reader["ViaWeb"]);
                    _tipo = (int)_Reader["Tipo"];

                    Movimiento m = new Movimiento
                    {
                        CUENTA = new Cuenta { IDCUENTA = _idCuenta },
                        FECHA = _fechaMovimiento,
                        IDMOVIMIENTO = _NumMovimiento,
                        MONEDA = _moneda,
                        MONTO = _monto,
                        SUCURSAL = new Sucursal { IDSUCURSAL = _idSucursal },
                        TIPOMOVIMIENTO = _tipo,
                        USUARIO = new Usuario { CI = _idCliente },
                        VIAWEB = _viaWeb
                    };

                    movimientos.Add(m);

                }
                _Reader.Close();

                return movimientos;
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
