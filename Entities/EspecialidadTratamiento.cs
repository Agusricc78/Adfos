using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class EspecialidadTratamiento
    {
        public int Id { get; set; }
        public string Especialidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UserId { get; set; }
        public bool Activo { get; set; }
    }
}
