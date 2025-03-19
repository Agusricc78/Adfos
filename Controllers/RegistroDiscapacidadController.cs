using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
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
    public class RegistroDiscapacidadController : ApiController
    {
        internal RegistroDiscapacidadBl BusinessLogic = new RegistroDiscapacidadBl();
        internal Log Log = new Log();

        [HttpGet]
        public HttpResponseMessage GetList(int? afiliadoId, string rnos, DateTime? periodoIni, DateTime? periodoFin, int? estado, int? numeroEnvioAfip, bool? filtra)
        {
            var registros = BusinessLogic.GetList(afiliadoId ?? 0, rnos ?? string.Empty, periodoIni, periodoFin, estado, numeroEnvioAfip, filtra ?? false);
            return Request.CreateResponse(HttpStatusCode.OK, registros);
        }

        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            var registros = BusinessLogic.GetbyId(id);
            return Request.CreateResponse(HttpStatusCode.OK, registros);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]RegistroDiscapacidad registro )
        {
            try
            {
                registro.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                registro.Id = BusinessLogic.Insert(registro);
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
                    userId = "Integracion.ReintegroDiscapacidadController",
                    Ip = General.GetIp()
                });
                if (ex.GetExceptionMessages().StartsWith("Nro. Factura Duplicado"))
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, ex.GetExceptionMessages());
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]RegistroDiscapacidad registro)
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

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                var result = BusinessLogic.Delete(id,token);
                return Request.CreateResponse(HttpStatusCode.OK, result);
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
