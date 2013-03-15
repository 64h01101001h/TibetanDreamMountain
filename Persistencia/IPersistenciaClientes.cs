using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaClientes
    {
        void AltaCliente(Cliente c);
        Cliente BuscarClientePorCi(Cliente cliente);
        List<Cliente> ListarClientes();
        void ModificarCliente(Cliente u);
        void EliminarCliente(Cliente u);
        Cliente LoginCliente(string NombreUsuario, string Pass);
        void ModificarPassword(Cliente u, string newPassword);
    }
}
