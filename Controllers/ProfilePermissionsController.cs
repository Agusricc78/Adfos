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
    public class ProfilePermissionsController : ApiController
    {
        internal ProfilePermissionsBl BusinessLogic = new ProfilePermissionsBl();
        internal Log Log = new Log();

        [HttpGet]
        public IEnumerable<AppProfilePermissions> Get(int profileId)
        {
            return BusinessLogic.GetList(profileId);
        }

        [HttpPost]
        public HttpResponseMessage Post(IList<AppProfilePermissions> profile)
        {
            try
            {
                 BusinessLogic.Insert(profile);
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
        public HttpResponseMessage Delete(AppProfilePermissions profile)
        {
            try
            {
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
