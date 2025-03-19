using System.Collections.Generic;

namespace Adfos.Entities
{
    public class IngresarMovTransf : AuditBase
    {
        public IngresarMovTransf()
        {
            DicIngresarMovTransf = new Dictionary<string, string>()
            {
                //{"CSV", "MovimientosConformadosCSV"}
                {"CSV", "IngresarMovTransfCsv"}
            };
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte[] Datos { get; set; }

        public TipoIngresarMovTransf Tipo
        {
            get { return (TipoIngresarMovTransf)TipoIngresarMovTransf; }
            set
            {
                TipoIngresarMovTransf = (int) value; 
            }
        }
        public int TipoIngresarMovTransf { get; set; }

        public Dictionary<string, string> DicIngresarMovTransf;
    }

    public enum TipoIngresarMovTransf
    {
        CSV = 1
    };
    
    
}
