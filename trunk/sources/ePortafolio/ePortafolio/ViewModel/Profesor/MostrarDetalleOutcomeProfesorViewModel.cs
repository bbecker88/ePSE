using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;
using System.Web.Mvc;
using ePortafolio.Helpers;
using ePortafolio.Logic;

namespace ePortafolio.ViewModel
{
    public class MostrarDetalleOutcomeProfesorViewModel
    {
        public OutcomesBE Outcome { get; set; }
        public List<AlumnosBE> Alumnos { get; set; }
        public String PeriodoId { get; set; }
        public String RutaVerRubrica;
        public List<EvaluacionesOutcomeProfesorBE> EvaluacionesOutcomeProfesor { get; set; }

        public MostrarDetalleOutcomeProfesorViewModel(int OutcomeId, String PeriodoId, String ProfesorId, List<EvaluacionesOutcomeProfesorBE> EvaluacionesOutcomeProfesor)
        {
            this.EvaluacionesOutcomeProfesor = EvaluacionesOutcomeProfesor;
            var AlumnosId = EvaluacionesOutcomeProfesor.Select(x => x.AlumnoId);
            Alumnos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosId.Contains(x.AlumnoId));
            Outcome = SSIARepositoryFactory.GetOutcomesRepository().GetOne(OutcomeId);

        }
    }
}

