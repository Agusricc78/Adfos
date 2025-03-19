using System;
using System.Collections.Generic;

namespace Adfos.Entities
{
    public class RegistroDevolucion : AuditBase
    {
        public string ClaveRendicion { get; set; }
        public string Rnos { get; set; }
        public string TipoArchivo { get; set; }
        public DateTime PeriodoPresentacion { get; set; }
        public DateTime PeriodoPrestacion { get; set; }
        public string Cuil { get; set; }
        public int CodigoPractica { get; set; }
        public Decimal ImporteSubsidiado { get; set; }
        public Decimal ImporteSolicitado { get; set; }
        public int NroEnvioAfip { get; set; }
        public string CuitDelCbu { get; set; }
        public string Cbu { get; set; }
        public DateTime FechaTransferenciaI { get; set; }
        public Decimal ImporteAplicadoSSS { get; set; }
        public Decimal FondosPropiosOtraCuenta { get; set; }
        public int Procesado { get; set; }
        public string Estado { get; set; }
        public int CodigoErrorSSS { get; set; }
        //public int RegistroDevolucionId { get; set; }

        public string Practica { get; set; }
        public string Prestador { get; set; }
        public string NombreApellido { get; set; }

        public string Export { get; set; }
    }

    
}
