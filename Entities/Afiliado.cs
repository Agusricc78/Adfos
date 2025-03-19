using System;

namespace Adfos.Entities
{
    public class Afiliado
    {
        public int Id { get; set; }

        public string NroBeneficiario { get; set; }

        public int NroOrden { get; set; }

        public int NroLegajo { get; set; }

        public string Cuil { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Sexo { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime FechaNacimiento { get; set; }
        
        public string Observaciones { get; set; }

        public string NombreCompleto
        {
            get { return string.Format("{0} {1}", Nombre, Apellido); }
        }

        public string CodigoCertDisc { get; set; }

        public DateTime? VencimientoCertDisc { get; set; }

        public bool CertificadoPermanente { get; set; }
    }
}

