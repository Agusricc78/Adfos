using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Razon_Social { get; set; }
        public string Cuit { get; set; }
        public string Ingresos_Brutos{ get; set; }
        public string Estado { get; set; }
        public int Id_condicion_iva { get; set; }
    }
}
