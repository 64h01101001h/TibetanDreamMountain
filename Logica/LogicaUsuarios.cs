using System;
using System.Collections.Generic;
using Entidades;
using Persistencia;
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
                    IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();
                    //PersistenciaClientes pclientes = new PersistenciaClientes();
                    pclientes.AltaCliente((Cliente)u);
                }
                else
                {
                    //PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    IPersistenciaEmpleados pempleados = FabricaPersistencia.getPersistenciaEmpleados();
                    pempleados.AltaEmpleado((Empleado)u);
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
                    IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();

                    pclientes.ModificarCliente((Cliente)u);
                }
                else
                {
                    //PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    IPersistenciaEmpleados pempleados = FabricaPersistencia.getPersistenciaEmpleados();

                    pempleados.ModificarEmpleado((Empleado)u);
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
                    IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();

                    pclientes.EliminarCliente((Cliente)u);
                }
                else
                {
                    //PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    IPersistenciaEmpleados pempleados = FabricaPersistencia.getPersistenciaEmpleados();

                    pempleados.EliminarEmpleado((Empleado)u);
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
                //PersistenciaClientes pclientes = new PersistenciaClientes();
                IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();

                return pclientes.ListarClientes();
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
                //PersistenciaEmpleados pemp = new PersistenciaEmpleados();
                IPersistenciaEmpleados pempleados = FabricaPersistencia.getPersistenciaEmpleados();

                return pempleados.ListarEmpleados();
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
                    //PersistenciaClientes pclientes = new PersistenciaClientes();
                    IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();

                    return pclientes.BuscarClientePorCi((Cliente)u);
                }
                else
                {
                    //PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    IPersistenciaEmpleados pempleados = FabricaPersistencia.getPersistenciaEmpleados();

                    return pempleados.BuscarEmpleadoPorCi((Empleado)u);
                }
                return null;
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
                IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();

                Cliente a = pclientes.LoginCliente(NombreUsuario, Pass);
                if (a != null)
                {
                    return a;
                }
                else
                {
                    //PersistenciaEmpleados pe = new PersistenciaEmpleados();
                    IPersistenciaEmpleados pempleados = FabricaPersistencia.getPersistenciaEmpleados();


                    //ServicioRemoting.ServicioDocente _objServicioD = new ServicioRemoting.ServicioDocente();
                    pempleados.LoginEmpleado(NombreUsuario, Pass);
                    Empleado e = pempleados.LoginEmpleado(NombreUsuario, Pass);

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
                IPersistenciaClientes pclientes = FabricaPersistencia.getPersistenciaClientes();

                pclientes.ModificarPassword(c, newPass);
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
