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
    [AuthenticationRequired]
    [AuthorizationRequired]
    public class PrestadoresController : ApiController
    {
        internal PrestadoresBl BusinessLogic = new PrestadoresBl();
        internal Log Log = new Log();

        [HttpGet]
        public HttpResponseMessage GetByCuit(string cuit)
        {
            try
            {
                var prestadores = BusinessLogic.Get(cuit);
                return Request.CreateResponse(HttpStatusCode.OK, prestadores);
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

        [HttpGet]
        public HttpResponseMessage GetByRazonSocial(string x, string razonSocial)
        {
            try
            {
                var prestadores = BusinessLogic.GetByRazon(razonSocial);
                return Request.CreateResponse(HttpStatusCode.OK, prestadores);
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
        [HttpGet]
        public HttpResponseMessage GetList()
        {
            try
            {
                var prestadores = BusinessLogic.GetList();
                return Request.CreateResponse(HttpStatusCode.OK, prestadores);
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

        [HttpGet]
        public HttpResponseMessage GetList(string cuit, string razonSocial)
        {
            try
            {
                var prestadores = BusinessLogic.GetList(cuit, razonSocial);
                return Request.CreateResponse(HttpStatusCode.OK, prestadores);
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