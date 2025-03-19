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
    public class AppUserController : ApiController
    {
        internal AppUserBl BusinessLogic = new AppUserBl();
        internal Log Log = new Log();

        public class UserInfo
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var usuario =  BusinessLogic.GetList();
            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        [HttpGet]
        public HttpResponseMessage GetbyId(int id)
        {
            var usuario = BusinessLogic.GetbyId(id);
            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        [HttpPost]
        public HttpResponseMessage Insert(UserInfo user)
        {
            var token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
            var usuario = BusinessLogic.Insert(user.UserName,user.Password,user.Name,user.Email,token);

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        [HttpPut]
        public HttpResponseMessage SelfUpdate(UserInfo user)
        {
            try
            {
                var token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                var afiliado = BusinessLogic.SelfUpdate(user.Password, user.Name, user.Email, token);
                return Request.CreateResponse(HttpStatusCode.OK, afiliado);

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

        [HttpPatch]
        public HttpResponseMessage Update(UserInfo user)
        {
            var token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
            var afiliado = BusinessLogic.Update(user.Id, user.Password, user.Name, user.Email, token);
            return Request.CreateResponse(HttpStatusCode.OK, afiliado);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(UserInfo user)
        {
            var token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
            var afiliado = BusinessLogic.Delete(user.Id, token);

            return Request.CreateResponse(HttpStatusCode.OK, afiliado);
        }
    }
}

