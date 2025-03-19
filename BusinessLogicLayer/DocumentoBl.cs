using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class DocumentoBl
    {
        readonly DataAccess _objDa = new DataAccess();

        public Documento Get(int id)
        {
            return _objDa.Get("DocumentoGetbyId", new Documento() { Id = id });
        }

        public List<Documento> GetList()
        {
            return _objDa.GetList("DocumentoGet", new Documento(){Nombre = ""});
        }

        public Documento Get(string docName, TipoDocumento tipodocumento)
        {
            return _objDa.Get("DocumentoGet", new Documento() { Nombre= docName, Tipo = tipodocumento});
        }

        public int Insert(Documento documento)
        {
           return _objDa.ExecuteInteger("DocumentoIns", documento); 
        }

        public bool Delete(Documento doc)
        {
            return _objDa.Execute("DocumentoDel", doc);
        }

    }
}
