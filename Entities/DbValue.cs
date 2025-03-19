using System;

namespace Adfos.Entities
{
    public class DbValue : Attribute
    {
        public DbValue(string name)
        {
            fieldName = name;
        }

        protected string fieldName;

        public string FieldName
        {
            get { return fieldName; }
        }
    }
}
