using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.SSIA;

namespace ePortafolio.ViewModel.Coordinador
{
    public class MostrarEvaluacionesOutcomesViewModel
    {
        public List<PeriodosBE> Periodos { get; set; }
        public List<OutcomesBE> Outcomes { get; set; }
        public String ProfesorId { get; set; }
        public String AlumnoId { get; set; }
        public String PeriodoId { get; set; }
        public Int32? OutcomeId { get; set; }

        public MostrarEvaluacionesOutcomesViewModel(String AlumnoId, String ProfesorId, String PeriodoId, Int32? OutcomeId)
        {
            this.AlumnoId = AlumnoId;
            this.ProfesorId = ProfesorId;
            this.PeriodoId = PeriodoId;
            this.OutcomeId = OutcomeId;

            Periodos = SSIARepositoryFactory.GetPeriodosRepository().GetAll().OrderByDescending(x => x.PeriodoId).ToList();
            Outcomes = SSIARepositoryFactory.GetOutcomesRepository().GetAll().OrderBy(x => x.Outcome).ToList();
        }

        public MostrarEvaluacionesOutcomesViewModel()
        {
            Periodos = SSIARepositoryFactory.GetPeriodosRepository().GetAll().OrderByDescending(x => x.PeriodoId).ToList();
            Outcomes = SSIARepositoryFactory.GetOutcomesRepository().GetAll().OrderBy(x => x.Outcome).ToList();
        }
    }
}
