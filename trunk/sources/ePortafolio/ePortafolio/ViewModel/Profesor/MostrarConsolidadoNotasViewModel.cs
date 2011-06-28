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
    public class MostrarConsolidadoNotasViewModel
    {
        public ProfesoresBE Profesor { get; set; }
        public TrabajosBE Trabajo { get; set; }
        public List<SeccionesCursoBE> SeccionesDictado { get; set; }

        public List<GruposBE> GruposTrabajo { get; set; }
        public List<AlumnosGrupoBE> AlumnosGruposTrabajo { get; set; }
        public List<AlumnosCursoBE> AlumnosCursoSeccionesDictado { get; set; }
        public List<EvaluacionesGruposProfesorBE> EvaluacionesGruposProfesor { get; set; }
        
        public MostrarConsolidadoNotasViewModel(int TrabajoId, String ProfesorId)
        {
            EvaluacionesGruposProfesor = ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().GetEvaluacionesGruposProfesorPorTrabajo(TrabajoId, ProfesorId);
            
            Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(ProfesorId);
            Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);

            SeccionesDictado = SSIARepositoryFactory.GetSeccionesCursoRepository().GetWhere(x => x.PeriodoId == Trabajo.PeriodoId && x.CursoId == Trabajo.CursoId && ProfesorId == x.ProfesorId);

            var SeccionesDictadoId = SeccionesDictado.Select(x => x.SeccionId);
            
            AlumnosGruposTrabajo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetAlumnosGrupoTrabajo(TrabajoId);
            GruposTrabajo = ePortafolioRepositoryFactory.GetGruposRepository().GetWhere(x => x.TrabajoId == TrabajoId);

            var AlumnosGruposExtraordinario = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => EvaluacionesGruposProfesor.Select(egp => egp.GrupoId).Contains(x.GrupoId));
            var AlumnosGruposExtraordinarioId = AlumnosGruposExtraordinario.Select(x => x.AlumnoId);

            AlumnosCursoSeccionesDictado = SSIARepositoryFactory.GetAlumnosCursoRepository().GetWhere(x => x.CursoId == Trabajo.CursoId && x.PeriodoId == Trabajo.PeriodoId && (SeccionesDictadoId.Contains(x.SeccionId) || AlumnosGruposExtraordinarioId.Contains(x.AlumnoId)));

            if (EvaluacionesGruposProfesor.Count > 0)
            {
                SeccionesDictado.Add(new SeccionesCursoBE() { SeccionId = "EXTRA" });

                foreach (var Grupo in EvaluacionesGruposProfesor)
                {
                    var AlumnosGrupo = AlumnosGruposExtraordinario.Where(x => x.GrupoId == Grupo.GrupoId);

                    foreach (var AlumnoGrupo in AlumnosGrupo)
                    {
                        AlumnosCursoSeccionesDictado.Where(x => x.AlumnoId == AlumnoGrupo.AlumnoId).ToList()
                                                    .ForEach(delegate(AlumnosCursoBE AlumnoCurso)
                                                    {
                                                        AlumnoCurso.SeccionId = "EXTRA";
                                                    });
                    }
                }
            }
        }
    }
}
