using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
    public class RespuestaController : ApiController
    {
        internal RespuestaBl BusinessLogic;
        internal Log Log = new Log();

        
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            try
            {
                var root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);
                var respuesta = new Respuesta();
                
                await LoadRespuesta(provider, respuesta);
                string sp;
                respuesta.DicRespuesta.TryGetValue(respuesta.Tipo.ToString(),out sp);
                respuesta.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
                var t = Type.GetType("Adfos.BusinessLogicLayer." + sp +",BusinessLogicLayer");
                
                BusinessLogic = new RespuestaBl((ITipoRespuestaStrategy)Activator.CreateInstance(t));
                
                respuesta.Id = BusinessLogic.Insert(respuesta);
                BusinessLogic.Proccess(provider.FileData[0].LocalFileName);

                File.Delete(provider.FileData[0].LocalFileName);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
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

        private async Task LoadRespuesta(MultipartFormDataStreamProvider provider, Respuesta respuesta)
        {
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var fieldKey in provider.FormData.AllKeys)
            {
                var dato = string.Empty;
                var strings = provider.FormData.GetValues(fieldKey);
                if (strings != null)
                {
                    dato = strings[0];
                }
                switch (fieldKey.ToUpper())
                {
                    case "NOMBRE":
                        respuesta.Nombre = dato;
                        break;
                    case "TIPO":
                        respuesta.Tipo = (TipoRespuesta) Enum.Parse(typeof (TipoRespuesta), dato.ToUpper());
                        respuesta.TipoRespuesta = (int) respuesta.Tipo;
                        break;
                }
            }

            using (var stream = new FileStream(provider.FileData[0].LocalFileName, FileMode.Open))
            {
                respuesta.Datos = new byte[stream.Length - 1];
                stream.Read(respuesta.Datos, 0, respuesta.Datos.Length);
            }
        }
    }
}
