using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;

namespace ePortafolio.ViewModel.Coordinador
{
    public class MostrarEvaluacionesOutcomesFiltradasViewModel
    {
        public List<AlumnosBE> Alumnos { get; set; }
        public List<OutcomesBE> Outcomes { get; set; }
        public List<EvaluacionesOutcomeProfesorBE> Evaluaciones { get; set; }
        public String ProfesorId { get; set; }
        public String AlumnoId { get; set; }
        public String PeriodoId { get; set; }
        public Int32? OutcomeId { get; set; }

        public MostrarEvaluacionesOutcomesFiltradasViewModel(String AlumnoId, String ProfesorId, String PeriodoId, Int32? OutcomeId)
        {
            AlumnoId = AlumnoId ?? "";
            ProfesorId = ProfesorId ?? "";
            PeriodoId = PeriodoId ?? "";
            OutcomeId = OutcomeId ?? 0;

            this.AlumnoId = AlumnoId;
            this.ProfesorId = ProfesorId;
            this.PeriodoId = PeriodoId;
            this.OutcomeId = OutcomeId;

            Evaluaciones = ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().GetWhere(x =>
                        (AlumnoId == String.Empty || x.AlumnoId.Contains(AlumnoId)) &&
                        (ProfesorId == String.Empty || x.ProfesorId.Contains(ProfesorId)) &&
                        (PeriodoId == String.Empty || x.PeriodoId == PeriodoId) &&
                        (OutcomeId.GetValueOrDefault(0) == 0 || x.OutcomeId == OutcomeId.Value));

            var OutcomesId = Evaluaciones.Select(x => x.OutcomeId).Distinct();
            var AlumnosId = Evaluaciones.Select(x => x.AlumnoId).Distinct();

            Outcomes = SSIARepositoryFactory.GetOutcomesRepository().GetWhere(x => OutcomesId.Contains(x.OutcomeId)).ToList();
            Alumnos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosId.Contains(x.AlumnoId)).ToList();
        }
    }
}
