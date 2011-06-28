using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.SSIA;

namespace ePortafolio.ViewModel.Coordinador
{
    public class AgregarEvaluacionesOutcomesViewModel
    {
        public List<PeriodosBE> Periodos { get; set; }
        public List<OutcomesBE> Outcomes { get; set; }
        public String ProfesorId { get; set; }
        public String AlumnosId  { get; set; }
        public String PeriodoId { get; set; }
        public int OutcomeId { get; set; }

        public AgregarEvaluacionesOutcomesViewModel(String PeriodoId)
        {
            this.PeriodoId = PeriodoId;
            Periodos = SSIARepositoryFactory.GetPeriodosRepository().GetAll().OrderByDescending(x => x.PeriodoId).ToList();
            Outcomes = SSIARepositoryFactory.GetOutcomesRepository().GetAll().OrderBy(x => x.Outcome).ToList();
        }

        public AgregarEvaluacionesOutcomesViewModel()
        {
            Periodos = SSIARepositoryFactory.GetPeriodosRepository().GetAll().OrderByDescending(x => x.PeriodoId).ToList();
            Outcomes = SSIARepositoryFactory.GetOutcomesRepository().GetAll().OrderBy(x => x.Outcome).ToList();
        }
    }
}
