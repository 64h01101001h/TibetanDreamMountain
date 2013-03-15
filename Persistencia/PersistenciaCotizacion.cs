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
    internal class PersistenciaCotizacion : IPersistenciaCotizacion
    {
        private static PersistenciaCotizacion _instancia = null;

        private PersistenciaCotizacion() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaCotizacion GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaCotizacion());
        }

        public List<Cotizacion> ListarCotizaciones()
        {
            List<Cotizacion> _listaCotizacion = new List<Cotizacion>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarCotizacion", conexion, CommandType.StoredProcedure);
            SqlParameter _fechaInicio = new SqlParameter("@FechaInicio", DateTime.Now.AddYears(-1));
            SqlParameter _fechaFin = new SqlParameter("@FechaFin", DateTime.Now.AddYears(1));
            cmd.Parameters.Add(_fechaFin);
            cmd.Parameters.Add(_fechaInicio);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                //cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                DateTime _fecha;
                decimal _precioCompra, _precioVenta;

                while (_Reader.Read())
                {
                    _fecha = Convert.ToDateTime(_Reader["Fecha"]);
                    _precioCompra = Convert.ToDecimal(_Reader["PrecioCompra"]);
                    _precioVenta = Convert.ToDecimal(_Reader["PrecioVenta"]);

                    Cotizacion s = new Cotizacion
                    {
                        FECHA = _fecha,
                        PRECIOCOMPRA = _precioCompra,
                        PRECIOVENTA = _precioVenta
                    };

                    _listaCotizacion.Add(s);
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

            return _listaCotizacion;
        }


        public void AltaCotizacion(Cotizacion s)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaCotizacion", conexion, CommandType.StoredProcedure);

                SqlParameter _fecha = new SqlParameter("@Fecha", s.FECHA);
                SqlParameter _precioCompra = new SqlParameter("@PrecioCompra", s.PRECIOCOMPRA);
                SqlParameter _precioVenta = new SqlParameter("@PrecioVenta", s.PRECIOVENTA);

                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_fecha);
                cmd.Parameters.Add(_precioCompra);
                cmd.Parameters.Add(_precioVenta);
                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    throw new ErrorCotizacionYaExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                {
                    throw new ErrorBaseDeDatos();
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

        /*EliminarCotizacion*/
        public void EliminarCotizacion(Cotizacion s)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spEliminarCotizacion", conexion, CommandType.StoredProcedure);
            SqlParameter _fecha = new SqlParameter("@Fecha", s.FECHA);

            cmd.Parameters.Add(_fecha);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(_Retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_Retorno.Value) == -1)
                {
                    throw new ErrorNoExisteCotizacion();
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


        public Cotizacion BuscarCotizacion(Cotizacion s)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarCotizacion", conexion, CommandType.StoredProcedure);
                SqlParameter _Ci = new SqlParameter("@Fecha", s.FECHA);
                cmd.Parameters.Add(_Ci);

                SqlDataReader _Reader;
                Cotizacion sNueva = null;
                int _precioCompra, _precioVenta;
                DateTime fecha;
                

                conexion.Open();
                _Reader = cmd.ExecuteReader();
                if (_Reader.Read())
                {
                    fecha = (DateTime)_Reader["Fecha"];
                    _precioCompra = Convert.ToInt32(_Reader["PrecioCompra"]);
                    _precioVenta = Convert.ToInt32(_Reader["PrecioVenta"]);


                    sNueva = new Cotizacion
                    {
                        FECHA = fecha,
                        PRECIOCOMPRA = _precioCompra,
                        PRECIOVENTA = _precioVenta,
                    };
                }
                _Reader.Close();

                return sNueva;


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

        public void ModificarCotizacion(Cotizacion s, Empleado e)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand cmd = Conexion.GetCommand("spModificarCotizacion", conexion, CommandType.StoredProcedure);
                    SqlParameter _IdUsuario = new SqlParameter("@IdUsuario", e.CI);
                    SqlParameter _Fecha = new SqlParameter("@Fecha", s.FECHA);
                    SqlParameter _PrecioCompra = new SqlParameter("@PrecioCompra", s.PRECIOCOMPRA);
                    SqlParameter _PrecioVenta = new SqlParameter("@PrecioVenta", s.PRECIOVENTA);
                    SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);

                    _retorno.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(_IdUsuario);
                    cmd.Parameters.Add(_Fecha);
                    cmd.Parameters.Add(_PrecioCompra);
                    cmd.Parameters.Add(_PrecioVenta);
                    cmd.Parameters.Add(_retorno);

                    //ACTUALIZAMOS LA CUENTA
                    //----------------------
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    if (Convert.ToInt32(_retorno.Value) == -1)
                    {
                        throw new ErrorUsuarioNoExiste();
                    }
                    else if (Convert.ToInt32(_retorno.Value) == -2)
                    {
                        throw new ErrorNoExisteCotizacion();
                    }
                    else if (Convert.ToInt32(_retorno.Value) == -3)
                    {
                        throw new ErrorBaseDeDatos();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
