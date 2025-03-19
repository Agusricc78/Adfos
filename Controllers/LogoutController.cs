using System;
using System.Diagnostics;
using System.Web.Http;
using Adfos.BusinessLogicLayer;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;
using IntegracionApi.Filters;

namespace IntegracionApi.Controllers
{
    //[HttpsRequired]
    [AuthorizationRequired]
    [AuthenticationRequired]
    public class LogoutController : ApiController
    {
        readonly Log _log = new Log();
        readonly TokenBl _tokenServices = new TokenBl();
        
        
        /// <summary>
        /// Kill token for logout
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void LogOut([FromBody]string token)
        {
            
            try
            {
                _tokenServices.Kill(token);
                
            }
            catch (Exception ex)
            {
                _log.Database(new LogEntry()
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -1,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    userId = "Integracion.ReintegrosDiscapacidad",
                    Ip = General.GetIp()
                });
                throw;
            }
        }

       
    }
}