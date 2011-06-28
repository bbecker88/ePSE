using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class PeriodosRepository 
    {
        public List<PeriodosBE> PeriodosMatriculados(String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
            var PeriodosId = (from x in DataContextObject.AlumnosCurso
                              where x.AlumnoId == AlumnoId
                              select x.PeriodoId).Distinct();

            var Periodos = from x in DataContextObject.Periodos
                              where PeriodosId.Contains(x.PeriodoId)
                              select GetLinq(x);

            return Periodos.ToList().OrderByDescending(x=>x.PeriodoId).ToList();
        }

        public List<PeriodosBE> PeriodosDictados(String ProfesorId)
        {
            var DataContextObject = GetDataContextObject();
            var PeriodosId = (from x in DataContextObject.SeccionesCurso
                              where x.ProfesorId == ProfesorId
                              select x.PeriodoId).Distinct();

            var Periodos = from x in DataContextObject.Periodos
                           where PeriodosId.Contains(x.PeriodoId)
                           select GetLinq(x);

            return Periodos.ToList().OrderByDescending(x => x.PeriodoId).ToList();
        }
    }
}
