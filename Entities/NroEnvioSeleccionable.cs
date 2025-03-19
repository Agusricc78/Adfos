using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class NroEnvioSeleccionable
    {
        public NroEnvioSeleccionable() { }
        public NroEnvioSeleccionable(int pCodigo, String pNombre)
        {
            Codigo = pCodigo;
            Nombre = pNombre;
        }
        public int Codigo { get; set; }
        public string Nombre { get; set; }

    }
}
