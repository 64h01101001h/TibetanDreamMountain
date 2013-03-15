using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;

using Logica;
using System.Xml;
using System;
using ExcepcionesPersonalizadas;

namespace ServicioGestionBancaria
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://WebMailWS/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioGestionBancaria : WebService
    {
       
    }
}