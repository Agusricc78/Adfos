﻿using System;
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
    public class EspecialidadTratamientoController : ApiController
    {
        internal EspecialidadTratamientoBl BusinessLogic = new EspecialidadTratamientoBl();
        internal Log Log = new Log();

        [HttpGet]
        public HttpResponseMessage GetById(int Id)
        {
            try
            {
                var espeialidadTratamiento = BusinessLogic.Get(Id);
                return Request.CreateResponse(HttpStatusCode.OK, espeialidadTratamiento);
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

        [HttpGet]
        public HttpResponseMessage GetList()
        {
            try
            {
                var especialidadTratamiento = BusinessLogic.GetList();
                return Request.CreateResponse(HttpStatusCode.OK, especialidadTratamiento);
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