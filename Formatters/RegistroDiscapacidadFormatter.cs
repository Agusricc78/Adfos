using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Adfos.BusinessLogicLayer;
using Adfos.Common;
using Adfos.Entities;
using Adfos.Logging;
using System.Linq;
namespace IntegracionApi.Formatters
{
    public class RegistroDiscapacidadFormatter : MediaTypeFormatter
    {
        Log _log = new Log();
        private static readonly Type SupportedType = typeof(IEnumerable<RegistroDiscapacidad>);
        internal RegistroDiscapacidadBl BusinessLogic = new RegistroDiscapacidadBl();
        public RegistroDiscapacidadFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("xtext/plain"));
            MediaTypeMappings.Add(new UriPathExtensionMapping("txt", "xtext/plain"));
            
        }

        public override bool CanReadType(Type type)
        {
            return SupportedType == type;
        }

        public override bool CanWriteType(Type type)
        {
            return SupportedType == type;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            
            var taskSource = new TaskCompletionSource<object>();
            try
            {
                var datos = (IEnumerable<RegistroDiscapacidad>)value;
                //var token =  new Guid(content.Headers.GetValues("Token").FirstOrDefault());
                var st = new StreamWriter(writeStream);
                foreach (var registro in datos)
                {
                    //registro.Token = token;
                    
                    // TODO: esto deberia estar en una transaction
                    registro.Procesado = 1; //Generado
                    BusinessLogic.Update(registro);
                    st.Write(Escape(registro.Export) + Environment.NewLine);
                    st.Flush();
                }

                taskSource.SetResult(st);
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
                _log.Database(new LogEntry
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -1,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    userId = "Integracion.ReintegrosDiscapacidad",
                    Ip = General.GetIp()
                });
            }
            return taskSource.Task;
        }

        static readonly char[] SpecialChars = { ',', '\n', '\r', '"' };

        private static string Escape(object o)
        {
            if (o == null)
            {
                o= @"";
            }
            var field = o.ToString();
            return field.IndexOfAny(SpecialChars) != -1 ? $"\"{field.Replace("\"", "\"\"")}\"" : field;
        }
    }
}