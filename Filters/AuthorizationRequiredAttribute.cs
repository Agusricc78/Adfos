using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Adfos.BusinessLogicLayer;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;

namespace IntegracionApi.Filters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";
        internal Log Log = new Log();
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var provider = new TokenBl();

            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                if (string.IsNullOrEmpty(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Token Invalido"
                    };
                    filterContext.Response = responseMessage;
                }
                if (!provider.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Token Invalido o Expirado"
                    };
                    filterContext.Response = responseMessage;
                }
                else
                {
                    var controllerName = filterContext.ControllerContext.ControllerDescriptor.ControllerName;
                    var actionName = filterContext.ActionDescriptor.ActionName;
                    var arguments = filterContext.ActionArguments;
                    string details = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(arguments);

                    if (!provider.ValidatePermission(controllerName, actionName, tokenValue, details))
                    {
                        var responseMessage = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
                        {
                            ReasonPhrase = string.Format("No HOLA JUAN CARLOS esta autorizado a acceder a este recurso: {0}-{1}", controllerName, actionName)
                        };
                        Log.Database(new LogEntry()
                        {
                            Source = "autorizar",
                            Type = EventLogEntryType.Error,
                            Number = -2,
                            Code = -4,
                            Message = "Controller:" + controllerName + "-Accion:" + actionName + "-Detalles:" + details,
                            userId = "Integracion.AutorizacionDenegada",
                            Ip = General.GetIp()
                        });
                        filterContext.Response = responseMessage;
                    }
                }
            }
            else
            {
                
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}