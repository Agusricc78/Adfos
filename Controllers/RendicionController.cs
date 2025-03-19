using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Adfos.BusinessLogicLayer;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;
using IntegracionApi.Filters;

namespace IntegracionApi.Controllers
{
    //[HttpsRequired]
    [AuthenticationRequired]
    [AuthorizationRequired]
    public class RendicionController : ApiController
    {
        internal RendicionBl BusinessLogic = new RendicionBl();
        internal Log Log = new Log();

        [HttpGet]
        public HttpResponseMessage GetList(long? afiliadoCuil, DateTime? periodoIni, DateTime? periodoFin, string cuit, int? numeroEnvioAfip)
        {
            try
            {
                var registros = BusinessLogic.GetList(afiliadoCuil ?? 0, periodoIni, periodoFin,cuit, numeroEnvioAfip ?? 0);
                return Request.CreateResponse(HttpStatusCode.OK, registros);
            }
            catch (Exception ex)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("Afiliado:").Append(afiliadoCuil);
                sb.Append(" Inicio:").Append(periodoIni);
                sb.Append(" Fin:").Append(periodoFin);
                sb.Append(" Error:").Append(ex.GetExceptionMessages());
                Log.Database(new LogEntry()
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -2,
                    Code = ex.HResult,
                    Message = sb.ToString(),
                    userId = "Integracion.ReintegrosDiscapacidad",
                    Ip = General.GetIp()
                });
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
            }
        }

        [HttpGet]
        public HttpResponseMessage GetById(string clave)
        {
            var registros = BusinessLogic.GetbyId(clave);
            return Request.CreateResponse(HttpStatusCode.OK, registros);
        }
    }
}
