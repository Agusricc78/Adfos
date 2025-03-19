using System;
using System.Collections.Generic;

namespace Adfos.Entities
{
    public class MovimientoTransferencia : AuditBase
    {
        public DateTime FechaTransferenciaI { get; set; }
        public Decimal ImporteAplicadoSSS { get; set; }
        public string CuitDelCbu { get; set; }
        public int NroEnvioAfip { get; set; }
    }

    
}
