using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class Prestadores
    {
        public int Id { get; set; }
        public string Cuit { get; set; }
        public string Cue { get; set; }
        public string RazonSocial { get; set; }
        public string Cbu { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
    }
}
