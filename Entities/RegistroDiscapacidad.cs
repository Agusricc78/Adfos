using Newtonsoft.Json;
using System;
using System.Text;

namespace Adfos.Entities
{
    public class RegistroDiscapacidad
    {
        public int Id { get; set; }
        public int AfiliadoId { get; set; }
        public DateTime Periodo { get; set; }
        public string Cuit { get; set; }
        public string Cue { get; set; }
        public string Cuil { get; set; }
        public int TipoComprobanteId { get; set; }
        public string TipoComprobante { get; set; }
        public int TipoEmisionId { get; set; }
        public string TipoEmision { get; set; }
        public DateTime FechaEmision { get; set; }
        public string CaeCai { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroComprobante { get; set; }
        public Decimal Importe { get; set; }
        public int Cantidad { get; set; }
        public int CodigoPractica { get; set; }
        public string Practica { get; set; }
        public int ProvinciaId { get; set; }
        public int EspecialidadId { get; set; }
        public string Provincia { get; set; }
        public bool EsDependiente { get; set; }
        public int Procesado { get; set; }
        public string Export { get; set; }
        public int CodigoErrorSSS { get; set; }
        public string Estado { get; set; }
        public string Error { get; set; }
        public string Cbu { get; set; }
        public string RazonSocial { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public string CodigoCertDisc { get; set; }
        public DateTime? VencimientoCertDisc { get; set; }
        public bool CertificadoPermanente { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public Guid Token { get; set; }
        public int NumeroEnvioAfip { get; set; }
        public string RendicionId { get; set; }
        public DateTime FechaAlta { get; set; }
        public string ApellidoAfiliado { get; set; }
        public string NombreAfiliado { get; set; }
        public string CuilAfiliado { get; set; }
        public int ErrorCodigo { get; set; }
        public string ErrorDescripcion { get; set; }
        public string ErrorVerificacion { get; set; }
    }

}
