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
    public class MostrarGruposCursoEvaluadorGruposViewModel
    {
        public List<GruposBE> Grupos { get; set; }
        public List<AlumnosGrupoBE> AlumnosGrupos { get; set; }
        public List<AlumnosBE> Alumnos { get; set; }
        public List<EvaluacionesGruposProfesorBE> EvaluacionesGruposProfesor { get; set; }
        public List<ProfesoresBE> Profesores { get; set; }
        public int TrabajoId { get; set; }

        public MostrarGruposCursoEvaluadorGruposViewModel()
        {
            List<GruposBE> Grupos = new List<GruposBE>();
            AlumnosGrupos = new List<AlumnosGrupoBE>();
            Alumnos = new List<AlumnosBE>();
            EvaluacionesGruposProfesor = new List<EvaluacionesGruposProfesorBE>();
            Profesores = new List<ProfesoresBE>();
            TrabajoId = 0;
        }

        public MostrarGruposCursoEvaluadorGruposViewModel(int TrabajoId)
        {
            Grupos = ePortafolioRepositoryFactory.GetGruposRepository().GetWhere(x => x.TrabajoId == TrabajoId);
            var GruposId = Grupos.Select(x => x.GrupoId);

            AlumnosGrupos = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => GruposId.Contains(x.GrupoId));
            EvaluacionesGruposProfesor = ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().GetWhere(x => GruposId.Contains(x.GrupoId));

            var AlumnosGruposId = AlumnosGrupos.Select(x => x.AlumnoId);
            Alumnos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosGruposId.Contains(x.AlumnoId));

            var ProfesoresId = EvaluacionesGruposProfesor.Select(x => x.ProfesorId);
            Profesores = SSIARepositoryFactory.GetProfesoresRepository().GetWhere(x => ProfesoresId.Contains(x.ProfesorId));

            this.TrabajoId = TrabajoId;
        }
    }
}
