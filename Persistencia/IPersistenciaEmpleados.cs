using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    public interface IPersistenciaEmpleados
    {
        Empleado LoginEmpleado(string NombreUsuario, string Pass);
        void AltaEmpleado(Empleado e);
        Empleado BuscarEmpleadoPorCi(Empleado e);
        List<Empleado> ListarEmpleados();
        void ModificarEmpleado(Empleado e);
        void EliminarEmpleado(Empleado e);
    }
}
