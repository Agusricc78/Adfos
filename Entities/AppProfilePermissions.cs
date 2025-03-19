using System;

namespace Adfos.Entities
{
    public class AppProfilePermissions
    {
        public int ProfileId { get; set; }
        public int AppObjectId { get; set; }
        public int AppActionId { get; set; }
        public string Object { get; set; }
        public string Action { get; set; }
        public string ActionDescription { get; set; }
        public bool Active { get; set; }
    }
}
