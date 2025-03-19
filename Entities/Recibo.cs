using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class Recibo
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public int EmpresaId { get; set; }
        public int AfiliadoId { get; set; }
        public string Cuit { get; set; }
        public string Cuil { get; set; }
        public string CaeCai { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Procesado { get; set; }
        public DateTime Periodo { get; set; }
        public double Importe { get; set; }
        public Guid Token { get; set; }
    }
}
