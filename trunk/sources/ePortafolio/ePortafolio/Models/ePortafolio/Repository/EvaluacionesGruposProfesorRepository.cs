using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class EvaluacionesGruposProfesorRepository 
    {
        public List<EvaluacionesGruposProfesorBE> GetEvaluacionesGruposProfesorPorTrabajo(int TrabajoId,String ProfesorId)
        {
            var DataContextObject = GetDataContextObject();
            var EvaluacionesGruposProfesor = from x in DataContextObject.EvaluacionesGruposProfesor
                           where x.Grupos.TrabajoId == TrabajoId && x.ProfesorId == ProfesorId
                           select GetLinq(x);
            return EvaluacionesGruposProfesor.ToList();
        }
    }
}
