using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.SSIA.Entities;

namespace ePortafolio.ViewModel
{
    public class MostrarPortafolioEvaluacionProfesorViewModel
    {
        public List<EvaluacionesOutcomeProfesorBE> EvaluacionesOutcomeProfesor { get; set; }
        public List<OutcomesBE> Outcomes { get; set; }
        public ProfesoresBE Profesor { get; set; }
        public String PeriodoId { get; set; }

        public MostrarPortafolioEvaluacionProfesorViewModel(String PeriodoId, String ProfesorId)
        {
            EvaluacionesOutcomeProfesor = ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().GetWhere(x => x.ProfesorId == ProfesorId && x.PeriodoId == PeriodoId);
            var EvaluacionesOutcomeProfesorId = EvaluacionesOutcomeProfesor.Select(x=>x.OutcomeId); 
            Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(ProfesorId);
            Outcomes = SSIARepositoryFactory.GetOutcomesRepository().GetWhere(x=>EvaluacionesOutcomeProfesorId.Contains(x.OutcomeId));
            this.PeriodoId = PeriodoId;
        }
    }
}
