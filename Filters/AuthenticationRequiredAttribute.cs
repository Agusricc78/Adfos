using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Adfos.BusinessLogicLayer;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;

namespace IntegracionApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticationRequiredAttribute : AuthorizationFilterAttribute
    {

        readonly AppUserBl _provider = new AppUserBl();
        readonly TokenBl _tokenServices = new TokenBl();
        readonly Log _log = new Log();


        /// <summary>
        /// Public default Constructor
        /// </summary>
        public AuthenticationRequiredAttribute()
        {
        }

        private readonly bool _isActive = true;

        /// <summary>
        /// parameter isActive explicitly enables/disables this filetr.
        /// </summary>
        /// <param name="isActive"></param>
        public AuthenticationRequiredAttribute(bool isActive)
        {
            _isActive = isActive;
        }

        /// <summary>
        /// Checks basic authentication request
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (!_isActive) return;
            var identity = FetchAuthHeader(filterContext);
            if (identity == null)
            {
                filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            //Valida user en db
            try
            {
                var userId = _provider.Authenticate(identity.UserName, identity.Password);
                if (userId <= 0)
                {
                    _log.Database(new LogEntry
                    {
                        Source = "OnAutorization",
                        Type = EventLogEntryType.Warning,
                        Number = -1,
                        Message = userId ==-1 ?"Usuario dado de baja":"Usuario y contraseña incorrecto",
                        userId = "Integracion.ReintegrosDiscapacidad",
                        Ip = General.GetIp()
                    });

                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = userId ==-1 ?"Usuario dado de baja":"Usuario o contraseña inválido"
                    };
                    filterContext.Response = responseMessage;
                }
                else
                {
                    _provider.SaveLoginData(userId, General.GetIp());
                    var genericPrincipal = new AdfosPrincipal(identity, null, new AppUser { Id = userId, Token = GetAuthToken(userId).AuthToken, UserName = identity.UserName });
                    Thread.CurrentPrincipal = genericPrincipal;
                }
                base.OnAuthorization(filterContext);
            }

            catch (Exception ex)
            {
                _log.Database(new LogEntry
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -1,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    userId = "Integracion.Authenticate",
                    Ip = General.GetIp()
                });
                var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    ReasonPhrase = "Error validando usuario, comuniquese con administrador"
                };
                filterContext.Response = responseMessage;
                base.OnAuthorization(filterContext);
            }
        }
        /// <summary>
        /// Checks for autrhorization header in the request and parses it, creates user credentials and returns as BasicAuthenticationIdentity
        /// </summary>
        /// <param name="filterContext"></param>
        protected virtual AuthenticationIdentity FetchAuthHeader(HttpActionContext filterContext)
        {
            string authHeaderValue = null;
            var authRequest = filterContext.Request.Headers.Authorization;
            if (authRequest != null && !String.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
                authHeaderValue = authRequest.Parameter;
            if (string.IsNullOrEmpty(authHeaderValue))
                return null;
            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));
            var credentials = authHeaderValue.Split(':');

            return credentials.Length < 2 ? null : new AuthenticationIdentity(credentials[0], credentials[1]);
        }

        private Token GetAuthToken(int userId)
        {
            return _tokenServices.GenerateToken(userId);
        }

    }
}