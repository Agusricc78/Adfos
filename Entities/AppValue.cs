using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class AppValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal MinValue { get; set; }
        public Decimal MaxValue { get; set; }
        public bool Active { get; set; }
    }
}
