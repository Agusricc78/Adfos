using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Adfos.Entities
{
    public class AdfosPrincipal : GenericPrincipal
    {
        public AppUser AppUser { get; set; }
        public AdfosPrincipal(IIdentity identity, string[] roles,AppUser user)
            : base(identity, roles)
        {
            AppUser = user;            
        }
       
    }
}
