using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Adfos.Common
{
    public static class General
    {
        public static string GetIp()
        {
            var retvalue = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
                retvalue = ip.ToString();

            if (string.IsNullOrEmpty(retvalue))
            {
                var ip = host.AddressList.FirstOrDefault();
                retvalue = ip == null ? "" : ip.ToString();
            }
            return retvalue;
        }

    }
}
