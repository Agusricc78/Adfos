using System;
using System.Collections.Generic;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class AfiliadoBl
    {
        readonly DataAccess _objDa = new DataAccess();

        public IList<Afiliado> GetList(string cuil, string nombre, string apellido,string nroBeneficiario)
        {
            return _objDa.GetList("AfiliadoGet", new Afiliado() { Nombre = nombre, Apellido = apellido, Cuil = cuil ,NroBeneficiario = nroBeneficiario });
        }

        public Afiliado GetbyId(int id)
        {
            return _objDa.Get("AfiliadoGetbyId", new Afiliado() { Id = id });
        }
    }
}
