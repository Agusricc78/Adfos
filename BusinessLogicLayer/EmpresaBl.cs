using System;
using System.Collections.Generic;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class EmpresaBl
    {
        readonly DataAccess _objDa = new DataAccess();

        public IList<Empresa> GetList(string cuit, string razonsocial)
        {
            return _objDa.GetList("EmpresaGet", new Empresa() { Razon_Social = razonsocial, Cuit = cuit });
        }

        public Empresa GetbyId(int id)
        {
            return _objDa.Get("EmpresaGetbyId", new Empresa() { Id = id });
        }
    }
}
