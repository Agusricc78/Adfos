using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class ReciboDetalle
    {
        public int Id { get; set; }
        public int ReciboId { get; set; }
        public string Concepto { get; set; }
        public int ConceptoId { get; set; }
        public string Detalle { get; set; }
        public DateTime Periodo { get; set; }
        public double Importe { get; set; }
        public int Procesado { get; set; }
        public string Tipo { get; set; }
        public Guid Token { get; set; }
    }
}
