using System;

namespace Adfos.Entities
{
    public class Token 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
