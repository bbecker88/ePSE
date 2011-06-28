using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class ArchivosRepository 
    {     
        public List<ArchivosBE> GetArchivosGrupo(int GrupoId)
        {
		    var DataContextObject = GetDataContextObject();
            var Archivos = from x in DataContextObject.ArchivosGrupo
                           where x.GrupoId == GrupoId
                           select GetLinq(x.Archivos);
            return Archivos.ToList();
        }
    }
}
