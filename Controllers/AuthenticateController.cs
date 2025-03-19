using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;
using IntegracionApi.Filters;

namespace IntegracionApi.Controllers
{
    //[HttpsRequired]
    [AuthenticationRequired]
    public class AuthenticateController : ApiController
    {
        readonly Log _log = new Log();

        /// <summary>
        /// Authenticates user and returns token with expiry.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Authenticate()
        {
            try
            {
                var principal = (AdfosPrincipal) Thread.CurrentPrincipal;
                return Request.CreateResponse(HttpStatusCode.OK, principal.AppUser);
            }
            catch (Exception ex)
            {
                _log.Database(new LogEntry()
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -2,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    userId = "Integracion.Authenticate",
                    Ip = General.GetIp()
                });
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
            }
        }
    }
   
}