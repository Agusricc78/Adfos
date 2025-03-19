using System.Collections.Generic;

namespace Adfos.Entities
{
    public class SmsResult
    {
        public List<SmsSuccess> success { get; set; }
        public List<SmsFails> fails { get; set; }
    }
}