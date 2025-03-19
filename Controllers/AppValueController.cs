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
    public class AppValueController : ApiController
    {
        internal AppValueBl BusinessLogic = new AppValueBl();
        internal Log Log = new Log();

        [HttpGet]
        public HttpResponseMessage GetByName(string name)
        {
            try
            {
                var estado = BusinessLogic.Get(name);
                return Request.CreateResponse(HttpStatusCode.OK, estado);
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