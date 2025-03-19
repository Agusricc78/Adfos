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
    public class RegistroDevolucionController : ApiController
    {
        internal RegistroDevolucionBl BusinessLogic = new RegistroDevolucionBl();
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
        public HttpResponseMessage GetById(string id)
        {
            var registros = BusinessLogic.GetbyId(id);
            return Request.CreateResponse(HttpStatusCode.OK, registros);
        }

        [HttpGet]
        public HttpResponseMessage GetValidate(int numeroEnvioAfip)
        {
            var result = BusinessLogic.GetValidate(numeroEnvioAfip);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]RegistroDevolucion registro)
        {
            try
            {
                //switch (registro.Procesado)
                //{
                //    case 0: // Sin Presentar
                //        // No cambia de estado al Actualizar
                //        break;
                //    case 1: // Generado;
                //        // TODO: No deberia poder actualizarse un registro en este estado
                //        break;
                //    case 2: // Presentado
                //        // TODO: No deberia poder actualizarse un registro en este estado
                //        break;
                //    case 3: // Aceptado (anteriormente Ok)
                //        // TODO: No deberia poder actualizarse un registro en este estado
                //        break;
                //    case 4: // Error
                //        // Al actualizarse pasa a estado Corregido (5)
                //        registro.Procesado = 5;
                //        break;
                //    case 5: // Corregido
                //        // No cambia de estado al Actualizar
                //        break;
                //    case 6: // Cerrado
                //        // TODO: No deberia poder actualizarse un registro en este estado
                //        break;
                //    case 7: // Anulado
                //        // No cambia de estado al Actualizar
                //        break;
                //}
                if (registro.Procesado == 4)
                {
                    // El estado Error (4) es el unico que al actualizarse pasa al estado Corregido (5).
                    registro.Procesado = 5;
                }
                registro.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                BusinessLogic.Update(registro);
                return Request.CreateResponse(HttpStatusCode.OK, registro);
            }
            catch (Exception ex)
            {
                Log.Database(new LogEntry()
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -2,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    userId = "Integracion.ReintegrosDiscapacidad",
                    Ip = General.GetIp()
                });
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
            }
        }
    }
}
