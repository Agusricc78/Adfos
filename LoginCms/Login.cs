using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Adfos.Entities;

namespace LoginCms
{
    // <summary>
    /// Clase para crear objetos Login Tickets
    /// </summary
    public class LoginAFIP
    {
        private static Int64 uId = 0; 
        /// <summary>
        /// Construye un Login Ticket obtenido del WSAA
        /// </summary>
        /// <param name="servicio">Servicio al que se desea acceder</param>
        /// <param name="urlWsaa">URL del WSAA</param>
        /// <param name="rutaCertX509Firmante">Ruta del certificado X509</param>
        /// <param name="password">Password del certificado X509</param>
        public LoginTicket GetTicket(string servicio, string urlWsaa, string rutaCertX509Firmante, SecureString password)
        {
            uId += 1;
            var retValue = new LoginTicket(uId, servicio);
            var cmsFirmadoBase64 = FirmaCertificado(rutaCertX509Firmante, password, retValue);
            var servicioWsaa = new Wsaa.LoginCMSService{Url = urlWsaa};
            string response = servicioWsaa.loginCms(cmsFirmadoBase64);
            retValue.XmlLoginTicketResponse.LoadXml(response);
            retValue.LoadResponse();
            return retValue;         
        }

        private static string FirmaCertificado(string rutaCertX509Firmante, SecureString password, LoginTicket retValue)
        {
            string cmsFirmadoBase64;
            X509Certificate2 certFirmante = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(rutaCertX509Firmante, password);
            Encoding EncodedMsg = Encoding.UTF8;
            byte[] msgBytes = EncodedMsg.GetBytes(retValue.XmlLoginTicketRequest.OuterXml);
            byte[] encodedSignedCms = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante);
            cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);
            return cmsFirmadoBase64;
        }
    }
}
