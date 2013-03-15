using System;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Collections.Generic;
using System.Transactions;

namespace Persistencia
{
    internal class PersistenciaEmpleados: IPersistenciaEmpleados
    {
        private static PersistenciaEmpleados _instancia = null;

        private PersistenciaEmpleados() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaEmpleados GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaEmpleados());
        }

        public Empleado LoginEmpleado(string NombreUsuario, string Pass)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spLoginEmpleado", conexion, CommandType.StoredProcedure);
            SqlParameter _userName = new SqlParameter("@Usuario", NombreUsuario);
            cmd.Parameters.Add(_userName);
            //SqlParameter _passWord = new SqlParameter("@Pass", Pass);
            //cmd.Parameters.Add(_passWord);
            SqlDataReader reader;

            Empleado e = null;
            int _ci, _idSucursal;
            string _nombreusuario, _nombre, _nombreSucursal, _direccionSucursal, _apellido, _password;
            bool _activo, _activaSucursal;
            Sucursal s = new Sucursal();

            try
            {
                conexion.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    _ci = (int)reader["Ci"];
                    _nombre = (string)reader["Nombre"];
                    _nombreusuario = (string)reader["NombreUsuario"];
                    _apellido = (string)reader["Apellido"];
                    _password = (string)reader["Pass"];
                    _activo = (bool)reader["Activo"];
                    _nombreSucursal = (string)reader["NombreSucursal"];
                    _direccionSucursal = (string)reader["DireccionSucursal"];
                    _idSucursal = (int)reader["IdSucursal"];
                    _activaSucursal = (bool)reader["SucursalActiva"];
                    e = new Empleado
                    {
                        CI = _ci,
                        NOMBREUSUARIO = _nombreusuario,
                        NOMBRE = _nombre,
                        APELLIDO = _apellido,
                        PASS = _password,
                        ACTIVO = _activo,
                        SUCURSAL = new Sucursal
                        {
                            IDSUCURSAL = _idSucursal,
                            NOMBRE = _nombreSucursal,
                            ACTIVA = _activaSucursal,
                            DIRECCION = _direccionSucursal
                        }
                    };
                }
                reader.Close();
            }
            catch
            {
                throw new ErrorBaseDeDatos();
            }
            finally
            {
                conexion.Close();
            }

            return e;
        }

        /// <summary>
        /// Ingresa un nuevo Empleado en el sistema
        /// </summary>
        /// <param name="e"></param>
        public void AltaEmpleado(Empleado e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("AltaEmpleado", conexion, CommandType.StoredProcedure);

            SqlParameter _ci = new SqlParameter("@Ci", e.CI);
            SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", e.NOMBREUSUARIO);
            SqlParameter _nombre = new SqlParameter("@Nombre", e.NOMBRE);
            SqlParameter _apellido = new SqlParameter("@Apellido", e.APELLIDO);
            SqlParameter _pass = new SqlParameter("@Pass", e.PASS);
            SqlParameter _idSucursal = new SqlParameter("@IdSucursal", e.SUCURSAL.IDSUCURSAL);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };

            cmd.Parameters.Add(_ci);
            cmd.Parameters.Add(_nombreusuario);
            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_apellido);
            cmd.Parameters.Add(_pass);
            cmd.Parameters.Add(_idSucursal);
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


        public Empleado BuscarEmpleadoPorCi(Empleado e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarEmpleado", conexion, CommandType.StoredProcedure);
                SqlParameter _Ci = new SqlParameter("@Ci", e.CI);
                cmd.Parameters.Add(_Ci);

                SqlDataReader reader;
                Empleado emp = null;
                int _ci;
                string _nombreusuario, _nombre, _apellido, _password;
                int _idSucursal;

                conexion.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _ci = (int)reader["Ci"];
                    _nombre = (string)reader["Nombre"];
                    _nombreusuario = (string)reader["NombreUsuario"];
                    _apellido = (string)reader["Apellido"];
                    _password = (string)reader["Pass"];
                    _idSucursal = (int)reader["IdSucursal"];

                    emp = new Empleado
                    {
                        CI = _ci,
                        NOMBREUSUARIO = _nombreusuario,
                        NOMBRE = _nombre,
                        APELLIDO = _apellido,
                        PASS = _password,
                        SUCURSAL = new Sucursal { IDSUCURSAL = _idSucursal }
                    };
                }
                reader.Close();

                return emp;
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


        /// <summary>
        /// LISTA LOS EMPLEADOS
        /// </summary>
        /// <returns></returns>
        public List<Empleado> ListarEmpleados()
        {
            List<Empleado> _listaEmpleados = new List<Empleado>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarEmpleado", conexion, CommandType.StoredProcedure);
            SqlParameter _paramActivo = new SqlParameter("@Activo", true);
            cmd.Parameters.Add(_paramActivo);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _ci;
                string _nombreUsuario, _nombre, _apellido, _pass;
                bool _activo;

                while (_Reader.Read())
                {
                    _ci = (int)_Reader["Ci"];
                    _nombreUsuario = (string)_Reader["NombreUsuario"];
                    _nombre = (string)_Reader["Nombre"];
                    _apellido = (string)_Reader["Apellido"];
                    _pass = (string)_Reader["Pass"];
                    _activo = (bool)_Reader["Activo"];

                    Empleado e = new Empleado
                    {
                        CI = _ci,
                        NOMBREUSUARIO = _nombreUsuario,
                        PASS = _pass,
                        NOMBRE = _nombre,
                        APELLIDO = _apellido,
                        ACTIVO = _activo,

                    };

                    _listaEmpleados.Add(e);
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

            return _listaEmpleados;
        }


        public void ModificarEmpleado(Empleado e)
        {

            using (TransactionScope tran = new TransactionScope())
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
                {
                    try
                    {
                        SqlCommand cmd = Conexion.GetCommand("spModificarEmpleado", conexion, CommandType.StoredProcedure);
                        SqlParameter _ci = new SqlParameter("@Ci", e.CI);
                        SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", e.NOMBREUSUARIO);
                        SqlParameter _nombre = new SqlParameter("@Nombre", e.NOMBRE);
                        SqlParameter _apellido = new SqlParameter("@Apellido", e.APELLIDO);
                        SqlParameter _pass = new SqlParameter("@Pass", e.PASS);
                        SqlParameter _idSucursal = new SqlParameter("@IdSucursal", e.SUCURSAL.IDSUCURSAL);

                        SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);

                        _retorno.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(_ci);
                        cmd.Parameters.Add(_nombreusuario);
                        cmd.Parameters.Add(_nombre);
                        cmd.Parameters.Add(_apellido);
                        cmd.Parameters.Add(_pass);
                        cmd.Parameters.Add(_idSucursal);

                        cmd.Parameters.Add(_retorno);

                        //ACTUALIZAMOS EL EMPLEADO
                        //-----------------------
                        conexion.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                tran.Complete();
            }

        }


        public void EliminarEmpleado(Empleado e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spEliminarEmpleado", conexion, CommandType.StoredProcedure);
            SqlParameter _ci = new SqlParameter("@ci", e.CI);

            cmd.Parameters.Add(_ci);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
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
