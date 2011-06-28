using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class AlumnosGrupoRepository 
    {
       
        public List<AlumnosGrupoBE> GetAlumnosGrupoTrabajo(int TrabajoId)
        {
            var DataContextObject = GetDataContextObject();
            var AlumnosGrupo = from x in DataContextObject.AlumnosGrupo
                               where x.Grupos.TrabajoId == TrabajoId
                               select GetLinqFK(x);
            return AlumnosGrupo.ToList();
        }
    }
}
