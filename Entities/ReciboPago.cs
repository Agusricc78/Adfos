using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class ReciboPago
    {
        public int Id { get; set; }
        public int ReciboId { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public string Banco { get; set; }
        public double Importe { get; set; }
    }
}
