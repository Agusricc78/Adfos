using System.Net;
using System.Net.Http;
using System.Web.Http;
using Adfos.BusinessLogicLayer;
using IntegracionApi.Filters;

namespace IntegracionApi.Controllers
{
    //[HttpsRequired]
    [AuthenticationRequired]
    [AuthorizationRequired]
    public class EmpresaController : ApiController
    {
        internal EmpresaBl BusinessLogic = new EmpresaBl();
        
        [HttpGet]
        public HttpResponseMessage GetList(string cuit, string razonsocial)
        {
            var empresa = BusinessLogic.GetList(cuit, razonsocial);
            return Request.CreateResponse(HttpStatusCode.OK, empresa);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var empresa = BusinessLogic.GetbyId(id);
            return Request.CreateResponse(HttpStatusCode.OK, empresa);
        }
    }
}
