using System;

namespace Adfos.Entities
{
    public class AppProfile: AuditBase
    {
        public int Id { get; set; }
        public string Profile { get; set; }
        public DateTime? UnsuscribeDate { get; set; }
        
    }
}
