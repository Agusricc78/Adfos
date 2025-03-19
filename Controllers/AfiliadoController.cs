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
    public class AfiliadoController : ApiController
    {
        internal AfiliadoBl BusinessLogic = new AfiliadoBl();
        
        [HttpGet]
        public HttpResponseMessage GetList(string cuil, string nombre, string apellido, string nroBeneficiario)
        {
            var afiliado = BusinessLogic.GetList(cuil, nombre, apellido, nroBeneficiario);
            return Request.CreateResponse(HttpStatusCode.OK,afiliado);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var afiliado = BusinessLogic.GetbyId(id);
            return Request.CreateResponse(HttpStatusCode.OK, afiliado);
        }
    }
}
