namespace Adfos.Entities
{
    public class Mail
    {
        public string To { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }

        public string FromAddress { get; set; }

        public string FromDisplay { get; set; }

        public bool ExchangeSmtp { get; set; }

        public bool IsBodyHtml { get; set; }

        public string Ip { get; set; }
        
        public bool IsReadReceiptRequested { get; set; }

        public MailAttachment[] Attachments { get; set; }
    }
}
