using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    public interface ILogicaUsuarios
    {
         void AltaUsuario(Usuario u);
         void ActualizarUsuario(Usuario u);
         void EliminarUsuario(Usuario u);
         List<Cliente> ListarClientes();
         List<Empleado> ListarEmpleados();
         Usuario BuscarUsuarioPorCi(Usuario u);
         Usuario getLoginUsuario(string NombreUsuario, string Pass);
         void ModificarPassword(Cliente c, string newPass);
    }
}
