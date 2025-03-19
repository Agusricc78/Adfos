using System;

namespace Adfos.Entities
{
    public class AuditBase
    {
        public Guid Token { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
    }
}
