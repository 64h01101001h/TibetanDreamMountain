using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Transactions;

namespace Persistencia
{
    internal class PersistenciaClientes : IPersistenciaClientes
    {


        private static PersistenciaClientes _instancia = null;

        private PersistenciaClientes() { }

        //ESTE METODO TIENE QUE SER DE CLASE Y PUBLICO!! (TAMBIEN PODRIA SER UNA PROPIEDAD QUE SOLO CONTENGA UN GET)
        public static PersistenciaClientes GetInstancia()
        {
            return _instancia ?? (_instancia = new PersistenciaClientes());
        }
        /// <summary>
        /// Ingresa un nuevo cliente en el sistema
        /// </summary>
        /// <param name="u"></param>
        public void AltaCliente(Cliente c)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlTransaction transaction;
            conexion.Open();
            transaction = conexion.BeginTransaction("AltaClienteTransaccion");

            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaCliente", conexion, CommandType.StoredProcedure);
                cmd.Transaction = transaction;

                SqlParameter _ci = new SqlParameter("@Ci", c.CI);
                SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", c.NOMBREUSUARIO);
                SqlParameter _nombre = new SqlParameter("@Nombre", c.NOMBRE);
                SqlParameter _apellido = new SqlParameter("@Apellido", c.APELLIDO);
                SqlParameter _pass = new SqlParameter("@Pass", c.PASS);
                SqlParameter _direccion = new SqlParameter("@Direccion", c.DIRECCION);
                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_ci);
                cmd.Parameters.Add(_nombreusuario);
                cmd.Parameters.Add(_nombre);
                cmd.Parameters.Add(_apellido);
                cmd.Parameters.Add(_pass);
                cmd.Parameters.Add(_direccion);
                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorUsuarioYaExiste();
                else if (Convert.ToInt32(_retorno.Value) < 0)
                    throw new ErrorBaseDeDatos();



                //ingresamos nuevos telefonos
                //---------------------------
                if (c.TELEFONOS != null)
                {
                    foreach (string tel in c.TELEFONOS)
                    {
                        //Ingresamos telefonos de nuevo
                        //-----------------------------
                        SqlCommand cmdAltaTel = Conexion.GetCommand("spAltaTelefono", conexion, CommandType.StoredProcedure);
                        cmdAltaTel.Transaction = transaction;

                        SqlParameter _CiCliente = new SqlParameter("@IdCliente", c.CI);
                        SqlParameter _telefono = new SqlParameter("@Tel ", tel);
                        cmdAltaTel.Parameters.Add(_CiCliente);
                        cmdAltaTel.Parameters.Add(_telefono);
                        cmdAltaTel.ExecuteNonQuery();
                    }
                }


                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }


        public Cliente BuscarClientePorCi(Cliente cliente)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarClientePorCi", conexion, CommandType.StoredProcedure);
                SqlParameter _Ci = new SqlParameter("@Ci", cliente.CI);
                cmd.Parameters.Add(_Ci);

                SqlDataReader reader;
                Cliente c = null;
                int _ci;
                string _nombreusuario, _nombre, _apellido, _password, _direccion;


                conexion.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _ci = (int)reader["Ci"];
                    _nombre = (string)reader["Nombre"];
                    _nombreusuario = (string)reader["NombreUsuario"];
                    _apellido = (string)reader["Apellido"];
                    _password = (string)reader["Pass"];
                    _direccion = (string)reader["Direccion"];
                    c = new Cliente
                            {
                                CI = _ci,
                                NOMBREUSUARIO = _nombreusuario,
                                NOMBRE = _nombre,
                                APELLIDO = _apellido,
                                PASS = _password,
                                DIRECCION = _direccion
                            };
                }
                reader.Close();


                //LEEMOS LOS TELEFONOS DEL CLIENTE
                //--------------------------------
                SqlCommand cmdTelefonos = Conexion.GetCommand("spListarTelefonos", conexion, CommandType.StoredProcedure);
                SqlParameter _CiTelefonos = new SqlParameter("@IdCliente", cliente.CI);
                cmdTelefonos.Parameters.Add(_CiTelefonos);

                SqlDataReader readerTelefonos;
                List<string> telefonos = new List<string>();

                readerTelefonos = cmdTelefonos.ExecuteReader();
                while (readerTelefonos.Read())
                {
                    telefonos.Add(Convert.ToString(readerTelefonos["Tel"]));
                }
                readerTelefonos.Close();

                //ASIGNAMOS LOS TELEFONOS LEIDOS
                //------------------------------
                c.TELEFONOS = telefonos;

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


        /// <summary>
        /// LISTA LOS CLIENTES
        /// </summary>
        /// <returns></returns>
        public List<Cliente> ListarClientes()
        {
            List<Cliente> _listaClientess = new List<Cliente>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarClientes", conexion, CommandType.StoredProcedure);
            SqlParameter _paramActivo = new SqlParameter("@Activo", true);
            cmd.Parameters.Add(_paramActivo);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _ci;
                string _nombreUsuario, _nombre, _apellido, _pass, _direccion;
                bool _activo;

                while (_Reader.Read())
                {
                    _ci = (int)_Reader["Ci"];
                    _nombreUsuario = (string)_Reader["NombreUsuario"];
                    _nombre = (string)_Reader["Nombre"];
                    _apellido = (string)_Reader["Apellido"];
                    _pass = (string)_Reader["Pass"];
                    _activo = (bool)_Reader["Activo"];
                    _direccion = (string)_Reader["Direccion"];

                    Cliente c = new Cliente
                    {
                        CI = _ci,
                        NOMBREUSUARIO = _nombreUsuario,
                        PASS = _pass,
                        NOMBRE = _nombre,
                        APELLIDO = _apellido,
                        ACTIVO = _activo,
                        DIRECCION = _direccion

                    };

                    _listaClientess.Add(c);
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

            return _listaClientess;
        }


        public void ModificarCliente(Cliente u)
        {

            using (TransactionScope tran = new TransactionScope())
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
                {
                    try
                    {
                        SqlCommand cmd = Conexion.GetCommand("spModificarCliente", conexion, CommandType.StoredProcedure);
                        SqlParameter _ci = new SqlParameter("@Ci", u.CI);
                        SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", u.NOMBREUSUARIO);
                        SqlParameter _nombre = new SqlParameter("@Nombre", u.NOMBRE);
                        SqlParameter _apellido = new SqlParameter("@Apellido", u.APELLIDO);
                        SqlParameter _pass = new SqlParameter("@Pass", u.PASS);
                        SqlParameter _direccion = new SqlParameter("@Direccion", u.DIRECCION);
                        SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);

                        _retorno.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(_ci);
                        cmd.Parameters.Add(_nombreusuario);
                        cmd.Parameters.Add(_nombre);
                        cmd.Parameters.Add(_apellido);
                        cmd.Parameters.Add(_pass);
                        cmd.Parameters.Add(_direccion);
                        cmd.Parameters.Add(_retorno);

                        //ACTUALIZAMOS EL CLIENTE
                        //-----------------------
                        conexion.Open();
                        cmd.ExecuteNonQuery();

                        //Damos de baja a los telefonos los telefonos
                        //--------------------------
                        SqlCommand cmdBajaTel = Conexion.GetCommand("spBajaTelefono", conexion, CommandType.StoredProcedure);
                        SqlParameter _idCliente = new SqlParameter("@IdCliente", u.CI);
                        cmdBajaTel.Parameters.Add(_idCliente);
                        cmdBajaTel.ExecuteNonQuery();

                        //ingresamos nuevos telefonos
                        //---------------------------
                        foreach (string tel in u.TELEFONOS)
                        {
                            //Ingresamos telefonos de nuevo
                            //-----------------------------
                            SqlCommand cmdAltaTel = Conexion.GetCommand("spAltaTelefono", conexion, CommandType.StoredProcedure);
                            SqlParameter _CiCliente = new SqlParameter("@IdCliente", u.CI);
                            SqlParameter _telefono = new SqlParameter("@Tel ", tel);
                            cmdAltaTel.Parameters.Add(_CiCliente);
                            cmdAltaTel.Parameters.Add(_telefono);
                            cmdAltaTel.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                tran.Complete();
            }

        }


        public void EliminarCliente(Cliente u)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spEliminarCliente", conexion, CommandType.StoredProcedure);
            SqlParameter _ci = new SqlParameter("@ci", u.CI);

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

        public Cliente LoginCliente(string NombreUsuario, string Pass)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spLoginCliente", conexion, CommandType.StoredProcedure);
            SqlParameter _userName = new SqlParameter("@Usuario", NombreUsuario);
            cmd.Parameters.Add(_userName);
            SqlParameter _passWord = new SqlParameter("@Pass", Pass);
            cmd.Parameters.Add(_passWord);
            SqlDataReader reader;

            Cliente c = null;
            int _ci;
            string _nombreusuario, _nombre, _apellido, _password, _direccion;
            bool _activo;

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
                    c = new Cliente
                    {
                        CI = _ci,
                        NOMBREUSUARIO = _nombreusuario,
                        NOMBRE = _nombre,
                        APELLIDO = _apellido,
                        PASS = _password,
                        TELEFONOS = null,
                        ACTIVO = _activo,

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

            return c;
        }


        public void ModificarPassword(Cliente u, string newPassword)
        {

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand cmd = Conexion.GetCommand("spModificarContraseña", conexion, CommandType.StoredProcedure);
                    SqlParameter _ci = new SqlParameter("@CiUsuario", u.CI);
                    SqlParameter _passActual = new SqlParameter("@PasswordActual", u.PASS);
                    SqlParameter _passNuevo = new SqlParameter("@PasswordNuevo", newPassword);

                    SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);

                    _retorno.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(_ci);
                    cmd.Parameters.Add(_passActual);
                    cmd.Parameters.Add(_passNuevo);

                    cmd.Parameters.Add(_retorno);

                    if (Convert.ToInt32(_retorno.Value) == -1)
                        throw new ErrorPasswordActualNoValido();
                    
                    //ACTUALIZAMOS EL CLIENTE
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


    }
}
