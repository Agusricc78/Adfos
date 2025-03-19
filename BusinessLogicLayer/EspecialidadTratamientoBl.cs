using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class EspecialidadTratamientoBl
    {
        readonly DataAccess _objDa = new DataAccess();

        public EspecialidadTratamiento Get(int Id)
        {
            return _objDa.Get("EspecialidadTratamientoGetbyId", new EspecialidadTratamiento() { Id = Id });
        }

        public List<EspecialidadTratamiento> GetList()
        {
            return _objDa.GetList("EspecialidadTratamientoGet", new EspecialidadTratamiento() { });
        }


    }
}
