using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class PeriodoRepository : ePortafolioMVC.Models.Repository.IPeriodoRepository 
    {
        public BEPeriodo GetPeriodo(String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();
            var Periodo = pyrIntegradoDBDataContext.ePSE_Periodos.SingleOrDefault(p => p.PeriodoId == PeriodoId);

            if (Periodo != null)
            {
                return new BEPeriodo
                    {
                        PeriodoId = Periodo.PeriodoId,
                        Nombre = (Convert.ToInt32(Periodo.PeriodoId) / 10).ToString() + "-" + (Convert.ToInt32(Periodo.PeriodoId) % 10).ToString(),
                        EsActual = Periodo.EsActual
                    };
            }
            return null;
        }

        public BEPeriodo GetGetPeriodoNoFK(String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();
            var Periodo = pyrIntegradoDBDataContext.ePSE_Periodos.SingleOrDefault(p => p.PeriodoId == PeriodoId);

            if (Periodo != null)
            {
                return new BEPeriodo
                {
                    PeriodoId = Periodo.PeriodoId,
                    Nombre = (Convert.ToInt32(Periodo.PeriodoId) / 10).ToString() + "-" + (Convert.ToInt32(Periodo.PeriodoId) % 10).ToString(),
                    EsActual = Periodo.EsActual
                };
            }
            return null;
        }

        public BEPeriodo GetGetPeriodoActual()
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();
            var Periodo = pyrIntegradoDBDataContext.ePSE_Periodos.SingleOrDefault(p => p.EsActual == true);

            if (Periodo != null)
            {
                return RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(Periodo.PeriodoId);
            }
            return null;
        }

        public List<BEPeriodo> GetPeriodosEvaluados(String ProfesorId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var PeriodosEvaluadosId = (from sc in pyrIntegradoDBDataContext.ePSE_SeccionesCursos
                                    where sc.ProfesorId == ProfesorId
                                    select sc.PeriodoId).Distinct();

            var PeriodosEvaluados = from pe in PeriodosEvaluadosId
                                    select RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(pe);

            return PeriodosEvaluados.ToList();
        }

        public List<BEPeriodo> GetPeriodosEstudiados(String AlumnoId)
        {
            ePortafolioDBDataContext ePortafolioDBDataContext = new ePortafolioDBDataContext();

            var PeriodosEstudiadosId = (from ac in ePortafolioDBDataContext.AlumnosGrupos
                                       where ac.AlumnoId == AlumnoId
                                       select ac.Grupo.Trabajo.PeriodoId).Distinct();

            var PeriodosEstudiados = from pe in PeriodosEstudiadosId
                                    select RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(pe);

            return PeriodosEstudiados.ToList();
        }

    }
}
