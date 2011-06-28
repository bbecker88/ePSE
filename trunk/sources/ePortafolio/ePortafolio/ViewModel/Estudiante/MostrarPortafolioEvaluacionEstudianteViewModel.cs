using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Logic;

namespace ePortafolio.ViewModel
{
    public class MostrarPortafolioEvaluacionEstudianteViewModel
    {
        public List<OutcomesAlumnoBE> OutcomesAlumno { get; set; }
        public AlumnosBE Alumno { get; set; }
        

        public MostrarPortafolioEvaluacionEstudianteViewModel(String AlumnoId)
        {
            Alumno = SSIARepositoryFactory.GetAlumnosRepository().GetOne(AlumnoId);
            OutcomesAlumno = SSIARepositoryFactory.GetOutcomesAlumnoRepository().GetWhere(x=>x.AlumnoId == AlumnoId);
        }
    }
}
