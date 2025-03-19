using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class NomencladorPracticas
    {
        public int Codigo { get; set; }
        public string Modulo { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public bool CueFlag { get; set; }
        public bool Dependencia { get; set; }
        public bool Especialidad { get; set; }
    }
}
