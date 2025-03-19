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
    public class RegistroDevolucionFormatter : MediaTypeFormatter
    {
        Log _log = new Log();
        private static readonly Type SupportedType = typeof(IEnumerable<RegistroDevolucion>);
        internal RegistroDevolucionBl BusinessLogic = new RegistroDevolucionBl();
        public RegistroDevolucionFormatter()
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
                var datos = (IEnumerable<RegistroDevolucion>)value;
                var st = new StreamWriter(writeStream);
                foreach (var registro in datos)
                {
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
                    userId = "Integracion.ReintegrosDevolucion",
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