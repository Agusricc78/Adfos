using System;
using System.Collections.Generic;

namespace Adfos.Entities
{
    public class Rendicion : AuditBase
    {
        public string Clave { get; set; }
        public string Rnos { get; set; }
        public string TipoArchivo { get; set; }

        public DateTime PeriodoPresentacion { get; set; }
        public DateTime PeriodoPrestacion { get; set; }
        public string Cuit { get; set; }
        public string Prestador { get; set; }
        public string RazonSocial { get; set; }
        public string Cuil { get; set; }
        public int CodigoPractica { get; set; }
        public string Practica { get; set; }
        public int TipoComprobanteId { get; set; }
        public string Comprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroComprobante { get; set; }
        public Decimal ImporteSolicitado { get; set; }
        public Decimal ImporteLiquidado { get; set; }
        public Decimal ImporteAliquidar { get; set; }
        public Decimal ImporteCuentaPropia
        {
            get { return ImporteSolicitado - ImporteAliquidar; }
        }
        public int NumeroEnvioAfip { get; set; }
        public string Cbu { get; set; }
        public string NombreApellido { get; set; }
        public int RegistroDiscapacidadId { get; set; }
        public string Especialidad { get; set; }
    }

    
}
