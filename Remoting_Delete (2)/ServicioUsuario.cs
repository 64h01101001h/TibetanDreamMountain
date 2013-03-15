using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicioRemoting
{
    public class ServicioUsuario
    {
        //atributo que representa un objeto de la fachadalogicaremota. 
        private Persistencia.IPersistenciaUsuarios unaPersistencia;

     //constructor del servicio
        public ServicioUsuario()
        {
            Servicio.CreoServicio();
            unaPersistencia = (new Persistencia.FabricaPersistencia()).getPersistenciaDocentes();
        }

        //operaciones
        public void Alta(Entidades.Usuario unUsuario)
        {
            unaPersistencia.NuevoUsuario(unUsuario);
        }
    }
}
