namespace Adfos.Entities
{
    /// <summary>
    /// properties are intentionally set to lower case
    /// </summary>
    public class SmsQuery
    {
        public string email { get; set; }
        public string password { get; set; }
        public string number { get; set; }
        public string message { get; set; }
        public string device { get; set; }

    }
}
