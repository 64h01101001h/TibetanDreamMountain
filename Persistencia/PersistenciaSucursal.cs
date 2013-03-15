using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using ExcepcionesPersonalizadas;

namespace Persistencia
{
    internal class PersistenciaSucursal: IPersistenciaSucursal
    {
        private static PersistenciaSucursal _instancia = null;

        private PersistenciaSucursal() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaSucursal GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaSucursal());
        }

        public void AltaSucursal(Sucursal L)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("AltaSucursal", conexion, CommandType.StoredProcedure);


            SqlParameter _nombre = new SqlParameter("@Nombre", L.NOMBRE);
            SqlParameter _direccion = new SqlParameter("@Direccion", L.DIRECCION);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };


            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_direccion);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) < 0)
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

        public decimal ArqueoCaja(DateTime Fecha, string Moneda, Empleado E)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spArqueoCaja", conexion, CommandType.StoredProcedure);


            SqlParameter _Empleado = new SqlParameter("@IdEmpleado", E.CI);
            SqlParameter _Fecha = new SqlParameter("@Fecha", Fecha);
            SqlParameter _Moneda = new SqlParameter("@Moneda", Moneda);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };

            cmd.Parameters.Add(_Fecha);
            cmd.Parameters.Add(_Moneda);
            cmd.Parameters.Add(_Empleado);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return Convert.ToInt32(_retorno.Value);
        }

        public void ModificarSucursal(Sucursal L)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spModificarSucursal", conexion, CommandType.StoredProcedure);

            SqlParameter _IdSucursal = new SqlParameter("@IdSucursal", L.NOMBRE);
            SqlParameter _nombre = new SqlParameter("@Nombre", L.NOMBRE);
            SqlParameter _direccion = new SqlParameter("@Direccion", L.DIRECCION);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };


            cmd.Parameters.Add(_IdSucursal);
            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_direccion);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) < 0)
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

        /// <summary>
        /// LISTA LAS SUCURSALES DEL BANCO
        /// </summary>
        /// <returns></returns>
        public List<Sucursal> ListarSucursales()
        {
            List<Sucursal> _listaSucursales = new List<Sucursal>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarSucursal", conexion, CommandType.StoredProcedure);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _idSucursal;
                string _nombre, _direccion;
                bool _activa;

                while (_Reader.Read())
                {
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _nombre = (string)_Reader["Nombre"];
                    _direccion = (string)_Reader["Direccion"];
                    _activa = (bool)_Reader["Activa"]; 

                    Sucursal s = new Sucursal
                    {
                        IDSUCURSAL = _idSucursal,
                        NOMBRE = _nombre,
                        DIRECCION = _direccion,
                        ACTIVA = _activa
                    };

                    _listaSucursales.Add(s);
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

            return _listaSucursales;
        }

        public List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Sucursal> _listaSucursales = new List<Sucursal>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListadoProductividadComparativo", conexion, CommandType.StoredProcedure);
            SqlParameter _fechaInicio = new SqlParameter("@FechaInicio", fechaInicio);
            SqlParameter _fechaFin = new SqlParameter("@FechaFin", fechaFin);
            cmd.Parameters.Add(_fechaInicio);
            cmd.Parameters.Add(_fechaFin);


            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                string _nombre, _direccion;
                int _cantidadCuentas = 0, _cantidadPrestamos = 0;

                while (_Reader.Read())
                {
                    _cantidadCuentas = 0;
                    _cantidadPrestamos = 0;

                    _nombre = (string)_Reader["Nombre"];
                    _direccion = (string)_Reader["Direccion"];
                    if (_Reader["CantCuentasAbiertas"] != DBNull.Value)
                    _cantidadCuentas = Convert.ToInt32(_Reader["CantCuentasAbiertas"]);
                    if (_Reader["CantPrestamosOtorgados"] != DBNull.Value)
                    _cantidadPrestamos = Convert.ToInt32(_Reader["CantPrestamosOtorgados"]);


                    Sucursal s = new Sucursal
                    {
                        NOMBRE = _nombre,
                        DIRECCION = _direccion,
                        CANTIDADCUENTASABIERTAS = _cantidadCuentas,
                        CANTIDADPRESTAMOS = _cantidadPrestamos
                    };

                    _listaSucursales.Add(s);
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

            return _listaSucursales;
        }


        public decimal TotalesArqueoCaja(Empleado E, ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spTotalesArqueoCaja", conexion, CommandType.StoredProcedure);

            SqlParameter _Empleado = new SqlParameter("@IdEmpleado", E.CI);
            SqlParameter _IdSucursal = new SqlParameter("@IdSucursal", E.SUCURSAL.IDSUCURSAL);
            SqlParameter _CantTotalDepositos = new SqlParameter("@CantTotalDepositos",ParameterDirection.Output);
            SqlParameter _CantTotalRetiros = new SqlParameter("@CantTotalRetiros", ParameterDirection.Output);
            SqlParameter _CantTotalPagos = new SqlParameter("@CantTotalPagos", ParameterDirection.Output);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(_IdSucursal);
            cmd.Parameters.Add(_Empleado);
            cmd.Parameters.Add(_CantTotalDepositos);
            cmd.Parameters.Add(_CantTotalRetiros);
            cmd.Parameters.Add(_CantTotalPagos);

            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                cantTotalDepositos = Convert.ToInt32(_CantTotalDepositos.Value);
                cantTotalPagos = Convert.ToInt32(_CantTotalPagos.Value);
                cantTotalRetiros = Convert.ToInt32(_CantTotalRetiros.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return Convert.ToInt32(_retorno.Value);
        }

        public Sucursal BuscarSucursal(Sucursal sucursal)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarSucursal", conexion, CommandType.StoredProcedure);
                SqlParameter _idsucursalParam = new SqlParameter("@IdSucursal", sucursal.IDSUCURSAL);
                cmd.Parameters.Add(_idsucursalParam);

                SqlDataReader _Reader;
                Sucursal s = null;
                int _idSucursal;
                string _direccion;


                conexion.Open();
                _Reader = cmd.ExecuteReader();
                if (_Reader.Read())
                {
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _direccion = (string)_Reader["Direccion"];

                    s = new Sucursal
                    {
                        IDSUCURSAL = _idSucursal,
                        DIRECCION = _direccion
                    };
                }
                _Reader.Close();

                return s;
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
