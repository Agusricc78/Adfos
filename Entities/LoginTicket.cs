using System;
using System.Xml;

namespace Adfos.Entities
{

    // <summary>
    /// Clase para crear objetos Login Tickets
    /// </summary>
    public class LoginTicket
    {
        public Int64 UniqueId { get; set; }
        public DateTime GenerationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Sign { get; set; }
        public string Token { get; set; }
        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;

        protected XmlNode xmlNodoUniqueId = default(XmlNode);
        protected XmlNode xmlNodoGenerationTime = default(XmlNode);
        protected XmlNode xmlNodoExpirationTime = default(XmlNode);
        protected XmlNode xmlNodoService = default(XmlNode);
        public LoginTicket()
        {
        }
        public LoginTicket(Int64 id, string service)
        {
            XmlLoginTicketRequest = new XmlDocument();
            XmlLoginTicketResponse = new XmlDocument();
            XmlLoginTicketRequest.LoadXml("<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>");
            xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
            xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
            xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
            xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");
            xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
            xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
            xmlNodoUniqueId.InnerText = Convert.ToString(id);
            xmlNodoService.InnerText = service;
        }
        public void LoadResponse()
        {
            UniqueId = Int64.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
            GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
            ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
            Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
            Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;
        }

    }
}
