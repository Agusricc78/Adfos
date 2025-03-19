using System;

namespace Adfos.Entities
{
    public class AppUser: AuditBase
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public int PersonId { get; set; }
        public DateTime? UnsuscribeDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
