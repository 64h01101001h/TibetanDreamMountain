using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
namespace CommServicioRemoting
{
   public  class ServicioClientes
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaClientes unaPersistencia;
        
         //constructor del servicio
        public ServicioClientes()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaClientes();
        }


        public void AltaCliente(Cliente c) {
            unaPersistencia.AltaCliente(c);
        }
        public Cliente BuscarClientePorCi(Cliente cliente) {
            return unaPersistencia.BuscarClientePorCi(cliente);
        }
        public List<Cliente> ListarClientes() {
            return unaPersistencia.ListarClientes();
        }
        public void ModificarCliente(Cliente u) {
            unaPersistencia.ModificarCliente(u);
        }
        public void EliminarCliente(Cliente u) {

            unaPersistencia.EliminarCliente(u);
        }
        public Cliente LoginCliente(string NombreUsuario, string Pass) {

            return unaPersistencia.LoginCliente(NombreUsuario, Pass);
        }
        public void ModificarPassword(Cliente u, string newPassword) {

            unaPersistencia.ModificarPassword(u, newPassword);
        }
    }
}
