namespace Adfos.Entities
{
    public class SmsSuccess
    {
        public string id { get; set; }
        public string device_id { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public int send_at { get; set; }
        public int queued_at { get; set; }
        public int sent_at { get; set; }
        public int delivered_at { get; set; }
        public int expires_at { get; set; }
        public int canceled_at { get; set; }
        public int failed_at { get; set; }
        public int received_at { get; set; }
        public string error { get; set; }
        public int created_at { get; set; }
        public SmsContact contact { get; set; }
    }
}
