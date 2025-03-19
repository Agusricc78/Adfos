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

    [AuthenticationRequired]
    [AuthorizationRequired]
    public class ReciboPagoController : ApiController
    {
        internal ReciboPagoBl BusinessLogic = new ReciboPagoBl();
        internal Log Log = new Log();

        [HttpGet]
        public HttpResponseMessage GetList(int reciboId)
        {
            try
            {
                var registros = BusinessLogic.GetList(reciboId);
                return Request.CreateResponse(HttpStatusCode.OK, registros);
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
                    userId = "Integracion.ReciboController",
                    Ip = General.GetIp()
                });
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
            }

        }

        [HttpGet]
        public HttpResponseMessage GetById(int Id)
        {
            var registros = BusinessLogic.GetbyId(Id);
            return Request.CreateResponse(HttpStatusCode.OK, registros);
        }

        //[HttpPost]
        //public HttpResponseMessage Post([FromBody]Recibo registro )
        //{
        //    try
        //    {
        //        registro.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
        //        //registro.Id = BusinessLogic.Insert(registro);
        //        return Request.CreateResponse(HttpStatusCode.OK, registro);
        //    }
        //    catch (Exception ex)
        //    {
                
        //        Log.Database(new LogEntry()
        //        {
        //            Source = ex.Source,
        //            Type = EventLogEntryType.Error,
        //            Number = -2,
        //            Code = ex.HResult,
        //            Message = ex.GetExceptionMessages(),
        //            userId = "Integracion.ReciboController",
        //            Ip = General.GetIp()
        //        });
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
        //    }
        //}

        //[HttpPut]
        //public HttpResponseMessage Put([FromBody]Recibo registro)
        //{
        //    try
        //    {
                
        //        registro.Token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
        //        //BusinessLogic.Update(registro);
        //        return Request.CreateResponse(HttpStatusCode.OK, registro);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Database(new LogEntry()
        //        {
        //            Source = ex.Source,
        //            Type = EventLogEntryType.Error,
        //            Number = -2,
        //            Code = ex.HResult,
        //            Message = ex.GetExceptionMessages(),
        //            userId = "Integracion.ReintegrosDiscapacidad",
        //            Ip = General.GetIp()
        //        });
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
        //    }
        //}

        //[HttpDelete]
        //public HttpResponseMessage Delete(int id)
        //{
        //    try
        //    {
        //        var token = new Guid(Request.GetFirstHeaderValueOrDefault<string>("Token"));
        //        var result = BusinessLogic.Delete(id,token);
        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Database(new LogEntry()
        //        {
        //            Source = ex.Source,
        //            Type = EventLogEntryType.Error,
        //            Number = -2,
        //            Code = ex.HResult,
        //            Message = ex.GetExceptionMessages(),
        //            userId = "Integracion.ReintegrosDiscapacidad",
        //            Ip = General.GetIp()
        //        });
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetExceptionMessages());
        //    }
        //}
    }
}
