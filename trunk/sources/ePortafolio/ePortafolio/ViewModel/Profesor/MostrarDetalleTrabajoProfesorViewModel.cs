using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Logic;

namespace ePortafolio.ViewModel
{
    public class MostrarDetalleTrabajoProfesorViewModel
    {
        public ProfesoresBE Profesor { get; set; }
        public TrabajosBE Trabajo { get; set; }
        public CursosBE Curso { get; set; }
        public List<SeccionesCursoBE> SeccionesDictado { get; set; }
        public List<GruposBE> GruposEntregados { get; set; }
        public List<AlumnosGrupoBE> AlumnosGrupos { get; set; }
        public List<AlumnosCursoBE> AlumnosCursoSeccionesDictado { get; set; }
        public List<EvaluacionesGruposProfesorBE> EvaluacionesGruposProfesor { get; set; }
        public List<Int32> GruposEntregadosExoneradosId { get; set; }


        public MostrarDetalleTrabajoProfesorViewModel(int TrabajoId, String ProfesorId)
        {
            EvaluacionesGruposProfesor = ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().GetEvaluacionesGruposProfesorPorTrabajo(TrabajoId,ProfesorId);
            
            Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(ProfesorId);
            Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);
            Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Trabajo.CursoId);
 
            GruposEntregados = ePortafolioRepositoryFactory.GetGruposRepository().GetGruposTrabajoConArchivosEntregados(TrabajoId);
            
            SeccionesDictado = SSIARepositoryFactory.GetSeccionesCursoRepository().GetWhere(x => x.PeriodoId == Trabajo.PeriodoId && x.CursoId == Trabajo.CursoId && ProfesorId == x.ProfesorId);

            GruposEntregadosExoneradosId = ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().GetWhere(x => GruposEntregados.Select(ge=>ge.GrupoId).Contains(x.GrupoId) && x.ProfesorId != ProfesorId).Select(x=>x.GrupoId).ToList();

            var SeccionesDictadoId = SeccionesDictado.Select(x=>x.SeccionId);
            var AlumnosGruposTrabajoId = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetAlumnosGrupoTrabajo(TrabajoId).Select(x => x.AlumnoId.Trim().ToUpper());

            AlumnosGrupos = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => GruposEntregados.Select(ge => ge.GrupoId).Contains(x.GrupoId));
            var AlumnosGruposId = AlumnosGrupos.Select(x => x.AlumnoId);

            var AlumnosGruposExtraordinario = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => EvaluacionesGruposProfesor.Select(egp => egp.GrupoId).Contains(x.GrupoId));
            var AlumnosGruposExtraordinarioId = AlumnosGruposExtraordinario.Select(x => x.AlumnoId);

            AlumnosCursoSeccionesDictado = SSIARepositoryFactory.GetAlumnosCursoRepository().GetWhere(x => x.CursoId == Trabajo.CursoId && x.PeriodoId == Trabajo.PeriodoId && (SeccionesDictadoId.Contains(x.SeccionId) || AlumnosGruposExtraordinarioId.Contains(x.AlumnoId)));

            if (EvaluacionesGruposProfesor.Count > 0)
            {
                SeccionesDictado.Add(new SeccionesCursoBE() { SeccionId="EXTRA"});
             
                foreach(var Grupo in EvaluacionesGruposProfesor)
                {
                    var GrupoEntregado = GruposEntregados.SingleOrDefault(x => x.GrupoId == Grupo.GrupoId);
                    if (GrupoEntregado != null)
                        GrupoEntregado.SeccionId = "EXTRA";

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
