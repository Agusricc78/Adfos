using System;
using System.Collections.Generic;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class ConceptoReciboBl
    {
        readonly DataAccess _objDa = new DataAccess();

        public ConceptoRecibo Get(int codigo)
        {
            return _objDa.Get("ConceptoGetbyId", new ConceptoRecibo() { Concepto_codigo = codigo });
        }

        public List<ConceptoRecibo> GetList()
        {
            return _objDa.GetList("ConceptoGet", new ConceptoRecibo() { });
        }

        public List<Estados> GetTotales(int? afiliadoId, DateTime? periodoIni, DateTime? periodoFin, int? estado, int? numeroEnvioAfip)
        {
            return _objDa.GetList<Estados>("EstadoGetTotales", afiliadoId, periodoIni, periodoFin, estado, numeroEnvioAfip);
        }
    }
}
