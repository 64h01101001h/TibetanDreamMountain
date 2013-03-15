using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
namespace CommServicioRemoting
{
    public class ServicioEmpleados
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaEmpleados unaPersistencia;

        //constructor del servicio
        public ServicioEmpleados()
        {
            //se crea el servicio para uso del remoting
            Servicio.CreoServicio();

            //unaPersistencia = Persistencia.FabricaPersistencia.getPersistenciaAlumnos();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaEmpleados();
        }


        public Empleado LoginEmpleado(string NombreUsuario, string Pass)
        {
            return unaPersistencia.LoginEmpleado(NombreUsuario, Pass);
        }
        public void AltaEmpleado(Empleado e)
        {

            unaPersistencia.AltaEmpleado(e);
        }
        public Empleado BuscarEmpleadoPorCi(Empleado e) {
            return unaPersistencia.BuscarEmpleadoPorCi(e);
        }
        public List<Empleado> ListarEmpleados() {
            return unaPersistencia.ListarEmpleados();
        
        }
        public void ModificarEmpleado(Empleado e) {
            unaPersistencia.ModificarEmpleado(e);
        }
        public void EliminarEmpleado(Empleado e) {
            unaPersistencia.EliminarEmpleado(e);
        }
    }
}
