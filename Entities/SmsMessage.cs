using System;

namespace Adfos.Entities
{
    public class SmsMessage
    {
        public int Id { get; set; }
        public int AfiliadoId { get; set; }
        public int SmsMessageStateId { get; set; }
        public string MessageState { get; set; }
        public string Telephone { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }
        public int Retry { get; set; }
        public bool Sent { get; set; }
        public bool Inbox { get; set; }
    } 
}
