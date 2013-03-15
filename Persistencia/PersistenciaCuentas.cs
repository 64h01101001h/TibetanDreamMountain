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


    internal class PersistenciaCuentas : MarshalByRefObject, IPersistenciaCuentas
    {

        private static PersistenciaCuentas _instancia = null;

        private PersistenciaCuentas() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaCuentas GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaCuentas());
        }


        /// <summary>
        /// Ingresa un nuevo Cuenta en el sistema
        /// </summary>
        /// <param name="u"></param>
        public void AltaCuenta(Cuenta c)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("spAltaCuenta", conexion, CommandType.StoredProcedure);

                SqlParameter _ci = new SqlParameter("@IdCliente", c.CLIENTE.CI);
                SqlParameter _moneda = new SqlParameter("@Moneda", c.MONEDA);
                SqlParameter _idSucursal = new SqlParameter("@IdSucursal", c.SUCURSAL.IDSUCURSAL);
                SqlParameter _saldo = new SqlParameter("@Saldo", c.SALDO);
                SqlParameter _fechaApertura = new SqlParameter("@FechaApertura", DateTime.Now);

                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_ci);
                cmd.Parameters.Add(_moneda);
                cmd.Parameters.Add(_idSucursal);
                cmd.Parameters.Add(_saldo);
                cmd.Parameters.Add(_fechaApertura);
                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    // throw new ErrorSucursalNoExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) < 0)
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

        public List<Cuenta> ListarCuentas()
        {
            List<Cuenta> _listaCuentas = new List<Cuenta>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarCuenta", conexion, CommandType.StoredProcedure);
            //SqlParameter _paramActivo = new SqlParameter("@Activo", true);
            //cmd.Parameters.Add(_paramActivo);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _idCuenta, _idSucursal, _idCliente;
                decimal _saldo;
                string _moneda, _nombreUsuario, _nombre, _apellido;
                bool _activo;

                while (_Reader.Read())
                {
                    _idCuenta = (int)_Reader["IdCuenta"];
                    _idCliente = (int)_Reader["IdCliente"];
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _saldo = Convert.ToDecimal(_Reader["Saldo"]);
                    _moneda = (string)_Reader["Moneda"];
                    _activo = (bool)_Reader["Activo"];
                    _nombreUsuario = (string)_Reader["NombreUsuario"];
                    _nombre = (string)_Reader["Nombre"];
                    _apellido = (string)_Reader["Apellido"];

                    Cuenta c = new Cuenta
                    {
                        IDCUENTA = _idCuenta,
                        CLIENTE = new Cliente { CI = _idCliente, ACTIVO = _activo, APELLIDO = _apellido, NOMBRE = _nombre, NOMBREUSUARIO = _nombreUsuario },
                        SUCURSAL = new Sucursal { IDSUCURSAL = _idSucursal },
                        SALDO = _saldo,
                        MONEDA = _moneda,
                    };

                    _listaCuentas.Add(c);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return _listaCuentas;
        }

        public Cuenta BuscarCuenta(Cuenta Cuenta)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarCuenta", conexion, CommandType.StoredProcedure);
                SqlParameter _idCuentaParam = new SqlParameter("@IdCuenta", Cuenta.IDCUENTA);
                cmd.Parameters.Add(_idCuentaParam);

                SqlDataReader _Reader;
                Cuenta c = null;
                int _idCuenta, _idSucursal, _idCliente;
                decimal _saldo;
                string _moneda,_nombreCliente, _apellidoCliente;


                conexion.Open();
                _Reader = cmd.ExecuteReader();
                if (_Reader.Read())
                {
                    _idCuenta = (int)_Reader["IdCuenta"];
                    _idCliente = (int)_Reader["IdCliente"];
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _saldo = Convert.ToDecimal(_Reader["Saldo"]);
                    _moneda = (string)_Reader["Moneda"];
                    _nombreCliente = (string)_Reader["Nombre"];
                    _apellidoCliente = (string)_Reader["Apellido"];
                    c = new Cuenta
                    {
                        IDCUENTA = _idCuenta,
                        CLIENTE = new Cliente { CI = _idCliente, NOMBRE =_nombreCliente, APELLIDO =_apellidoCliente },
                        SUCURSAL = new Sucursal { IDSUCURSAL = _idSucursal },
                        SALDO = _saldo,
                        MONEDA = _moneda,
                    };
                }
                _Reader.Close();

                return c;
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

        public void ModificarCuenta(Cuenta c)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand cmd = Conexion.GetCommand("spModificarCuenta", conexion, CommandType.StoredProcedure);
                    SqlParameter _IdCuenta = new SqlParameter("@IdCuenta", c.IDCUENTA);
                    SqlParameter _saldo = new SqlParameter("@Saldo", c.SALDO);
                    //SqlParameter _idCliente = new SqlParameter("@IdCliente", c.CLIENTE.CI);
                    SqlParameter _moneda = new SqlParameter("@Moneda", c.MONEDA);
                    SqlParameter _idSucursal = new SqlParameter("@IdSucursal", c.SUCURSAL.IDSUCURSAL);
                    SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);

                    _retorno.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(_IdCuenta);
                    cmd.Parameters.Add(_saldo);
                    //cmd.Parameters.Add(_idCliente);
                    cmd.Parameters.Add(_moneda);
                    cmd.Parameters.Add(_idSucursal);
                    cmd.Parameters.Add(_retorno);

                    //ACTUALIZAMOS LA CUENTA
                    //-----------------------
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void EliminarCuenta(Cuenta c)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spEliminarCuenta", conexion, CommandType.StoredProcedure);
            SqlParameter _idCuenta = new SqlParameter("@IdCuenta", c.IDCUENTA);

            cmd.Parameters.Add(_idCuenta);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(_Retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_Retorno.Value) == -1)
                {
                    throw new ErrorVaciarCuenta();
                }
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

        public List<Cuenta> BuscarCuentasCliente(Cliente cliente)
        {
            List<Cuenta> _listaCuentas = new List<Cuenta>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spBuscarCuentaPorCi", conexion, CommandType.StoredProcedure);
            SqlParameter _ci = new SqlParameter("@Ci", cliente.CI);
            cmd.Parameters.Add(_ci);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _idCuenta, _idSucursal, _idCliente;
                decimal _saldo;
                string _moneda, _nombreUsuario, _nombre, _apellido, _nombreSucursal;
                bool _activo;

                while (_Reader.Read())
                {
                    _idCuenta = (int)_Reader["IdCuenta"];
                    _idCliente = (int)_Reader["IdCliente"];
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _saldo = Convert.ToDecimal(_Reader["Saldo"]);
                    _moneda = (string)_Reader["Moneda"];
                    _activo = (bool)_Reader["Activo"];
                    _nombreUsuario = (string)_Reader["NombreUsuario"];
                    _nombre = (string)_Reader["Nombre"];
                    _apellido = (string)_Reader["Apellido"];
                    _nombreSucursal = (string)_Reader["Nombre"];

                    Cuenta c = new Cuenta
                    {
                        IDCUENTA = _idCuenta,
                        CLIENTE = new Cliente { CI = _idCliente, ACTIVO = _activo, APELLIDO = _apellido, NOMBRE = _nombre, NOMBREUSUARIO = _nombreUsuario },
                        SUCURSAL = new Sucursal { IDSUCURSAL = _idSucursal, NOMBRE= _nombreSucursal },
                        SALDO = _saldo,
                        MONEDA = _moneda,
                    };

                    _listaCuentas.Add(c);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return _listaCuentas;
        }
    }
}
