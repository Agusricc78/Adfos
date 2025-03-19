namespace Adfos.Entities
{
    public class Documento : AuditBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] Datos { get; set; }

        public TipoDocumento Tipo
        {
            get { return (TipoDocumento) TipoDocumento; }
            set
            {
                TipoDocumento = (int) value; 
            }
        }

        public int TipoDocumento { get; set; }
    }

    public enum TipoDocumento
    {
        Pdf = 1,
        Word,
        Jpg
    };
    
}
