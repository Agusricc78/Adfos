using System;
using System.Collections.Generic;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class AppValueBl
    {
        readonly DataAccess _objDa = new DataAccess();

        public AppValue Get(string name)
        {
            return _objDa.Get("AppValueGetByName", new AppValue() { Name = name });
        }
    }
}
