using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Adfos.Common
{

    public static class HttpRequestMessageExtensions
    {
        public static T GetFirstHeaderValueOrDefault<T>(this HttpRequestMessage request, string header)
        {
            var toReturn = default(T);

            IEnumerable<string> headerValues;

            if (request.Headers.TryGetValues(header, out headerValues))
            {
                var valueString = headerValues.FirstOrDefault();
                if (valueString != null)
                {
                    return (T)Convert.ChangeType(valueString, typeof(T));
                }
            }

            return toReturn;
        }
    }
}
