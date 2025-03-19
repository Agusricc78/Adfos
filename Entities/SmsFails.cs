namespace Adfos.Entities
{
    public class SmsFails
    {
        public string number { get; set; }
        public string message { get; set; }
        public int device { get; set; }
        public SmsErrors errors { get; set; }
    }
}