using System;
using System.Collections.Generic;
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
    public class ProfileController : ApiController
    {
        internal ProfileBl BusinessLogic = new ProfileBl();
        internal Log Log = new Log();

        [HttpGet]
        public IEnumerable<AppProfile> Get()
        {
            return BusinessLogic.GetList();
        }

        [HttpPost]
        public HttpResponseMessage Post(AppProfile profile)
        {
            try
            {
                profile.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                profile.Id = BusinessLogic.Insert(profile);
                return Request.CreateResponse(HttpStatusCode.OK, profile);
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

        [HttpPut]
        public HttpResponseMessage Put(AppProfile profile)
        {
            try
            {
                profile.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                BusinessLogic.Update(profile);
                return Request.CreateResponse(HttpStatusCode.OK, profile);
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
        public HttpResponseMessage Delete(AppProfile profile)
        {
            try
            {
                profile.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                BusinessLogic.Delete(profile);
                return Request.CreateResponse(HttpStatusCode.OK, profile);
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
