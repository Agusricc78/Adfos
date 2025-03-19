using System.Collections.Generic;

namespace Adfos.Entities
{
    public class Respuesta : AuditBase
    {
        public Respuesta()
        {
            DicRespuesta = new Dictionary<string, string>()
            {
                {"ERR", "RespuestaErr"},
                {"OK", "RespuestaOk"},
                {"DEVOK", "RespuestaDevOk"},
                {"DEVERR", "RespuestaDevErr"},
                {"SUBSIDIO", "RespuestaSubsidio"},
                {"ENVIO", "RespuestaEnvio"},
                {"CONTROL", "RespuestaControl"},
                {"DEVOLUCION_OK", "RespuestaDevolucionOk"},
                {"DEVOLUCION_ERR", "RespuestaDevolucionErr"},
            };
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte[] Datos { get; set; }

        public TipoRespuesta Tipo
        {
            get { return (TipoRespuesta)TipoRespuesta; }
            set
            {
                TipoRespuesta = (int) value; 
            }
        }
        public int TipoRespuesta { get; set; }

        public Dictionary<string, string> DicRespuesta;
    }

    public enum TipoRespuesta
    {
        ERR = 1,
        OK,
        DEVOK,
        DEVERR,
        SUBSIDIO,
        ENVIO,
        CONTROL,
        DEVOLUCION_OK,
        DEVOLUCION_ERR,
    };
    
    
}
