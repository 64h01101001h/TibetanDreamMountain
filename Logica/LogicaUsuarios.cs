using System;
using System.Collections.Generic;
using Entidades;
using ExcepcionesPersonalizadas;

namespace Logica
{
    internal class LogicaUsuarios : ILogicaUsuarios
    {

        //singleton
        //------------------------------------------------
        private static LogicaUsuarios _instancia = null;
        private LogicaUsuarios() { }

        public static LogicaUsuarios GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuarios();

            return _instancia;
        }


        public void AltaUsuario(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();
                    //PersistenciaClientes pclientes = new PersistenciaClientes();
                    remotingClientes.AltaCliente((Cliente)u);
                }
                else
                {
                    CommServicioRemoting.ServicioEmpleados remotingEmpleados = new CommServicioRemoting.ServicioEmpleados();
                    remotingEmpleados.AltaEmpleado((Empleado)u);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ActualizarUsuario(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    //PersistenciaClientes pclientes = new PersistenciaClientes();
                    CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();


                    remotingClientes.ModificarCliente((Cliente)u);
                }
                else
                {
                    CommServicioRemoting.ServicioEmpleados remotingEmpleados = new CommServicioRemoting.ServicioEmpleados();

                    remotingEmpleados.ModificarEmpleado((Empleado)u);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUsuario(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    //PersistenciaClientes pclientes = new PersistenciaClientes();
                    CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();


                    remotingClientes.EliminarCliente((Cliente)u);
                }
                else
                {
                    CommServicioRemoting.ServicioEmpleados remotingEmpleados = new CommServicioRemoting.ServicioEmpleados();

                    remotingEmpleados.EliminarEmpleado((Empleado)u);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Cliente> ListarClientes()
        {
            try
            {
                CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();


                return remotingClientes.ListarClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empleado> ListarEmpleados()
        {
            try
            {
                CommServicioRemoting.ServicioEmpleados remotingEmpleados = new CommServicioRemoting.ServicioEmpleados();

                return remotingEmpleados.ListarEmpleados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario BuscarUsuarioPorCi(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();


                    return remotingClientes.BuscarClientePorCi((Cliente)u);
                }
                else
                {
                    CommServicioRemoting.ServicioEmpleados remotingEmpleados = new CommServicioRemoting.ServicioEmpleados();

                    return remotingEmpleados.BuscarEmpleadoPorCi((Empleado)u);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public Usuario getLoginUsuario(string NombreUsuario, string Pass)
        {
            try
            {
                //ServicioRemoting.ServicioAlumno _objServicioA = new ServicioRemoting.ServicioAlumno();
                //PersistenciaClientes pc = new PersistenciaClientes();
                CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();


                Cliente a = remotingClientes.LoginCliente(NombreUsuario, Pass);
                if (a != null)
                {
                    return a;
                }
                else
                {
                    CommServicioRemoting.ServicioEmpleados remotingEmpleados = new CommServicioRemoting.ServicioEmpleados();
                    Empleado e = remotingEmpleados.LoginEmpleado(NombreUsuario, Pass);

                    return e;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPassword(Cliente c, string newPass)
        {
            try
            {
                //PersistenciaClientes pc = new PersistenciaClientes();
                CommServicioRemoting.ServicioClientes remotingClientes = new CommServicioRemoting.ServicioClientes();


                remotingClientes.ModificarPassword(c, newPass);
            }
            catch (ErrorPasswordActualNoValido ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
