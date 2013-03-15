using System;
using System.Runtime.Serialization;

namespace ExcepcionesPersonalizadas
{
    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class MisExcepciones : Exception
    {

    }


    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorGeneral : Exception
    {
        public ErrorGeneral()
        {

        }
        public ErrorGeneral(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "Hubo un error general, vuelva a intentarlo más tarde";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorBaseDeDatos : Exception
    {
        public ErrorBaseDeDatos()
        {

        }

        public ErrorBaseDeDatos(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "Hubo un error en la base de datos, vuelva a intentarlo más tarde";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorDocumentoInvalido : Exception
    {
        public ErrorDocumentoInvalido()
        {

        }

        public ErrorDocumentoInvalido(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "Escriba la CI sin puntos ni guiones";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorUsuarioYaExiste : Exception
    {
        public ErrorUsuarioYaExiste()
        {

        }

        public ErrorUsuarioYaExiste(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: Usuario ya existe en el sistema";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorUsuarioNoExiste : Exception
    {
        public ErrorUsuarioNoExiste()
        {

        }

        public ErrorUsuarioNoExiste(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: Usuario no existe en el sistema";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorNoHayUsuarios : Exception
    {
        public ErrorNoHayUsuarios()
        {

        }

        public ErrorNoHayUsuarios(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: No hay usuarios registrados en el sistema";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorVaciarCuenta : Exception
    {
        public ErrorVaciarCuenta()
        {

        }

        public ErrorVaciarCuenta(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: Se debe vaciar la cuenta antes de eliminarla.";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    /*ErrorCotizacionYaExiste*/

    //[Serializable]
    public class ErrorCotizacionYaExiste : Exception
    {
        public ErrorCotizacionYaExiste()
        {

        }

        public ErrorCotizacionYaExiste(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: Ya existe una cotización para la fecha seleccionada.";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    /*ErrorNoExisteCotizacion*/
    public class ErrorNoExisteCotizacion : Exception
    {
        public ErrorNoExisteCotizacion()
        {

        }

        public ErrorNoExisteCotizacion(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: No existe cotización para la fecha seleccionada.";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    public class ErrorSucursalNoExiste: Exception
    {
        public ErrorSucursalNoExiste()
        {

        }

        public ErrorSucursalNoExiste(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: No existe la sucursal o se encuentra inactiva.";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    public class ErrorSaldoInsuficienteParaRetiro : Exception
    {
        public ErrorSaldoInsuficienteParaRetiro()
        {

        }

        public ErrorSaldoInsuficienteParaRetiro(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: El saldo de su cuenta es inferior al monto que desea retirar.";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    public class ErrorPasswordActualNoValido : Exception
    {
        public ErrorPasswordActualNoValido()
        {

        }

        public ErrorPasswordActualNoValido(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: La contraseña actual ingresada no es correcta.";

        public override string Message
        {
            get { return Mensaje; }
        }

    }
}

