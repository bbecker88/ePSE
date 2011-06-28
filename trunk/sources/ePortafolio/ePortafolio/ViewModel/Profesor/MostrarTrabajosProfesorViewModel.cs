using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Logic;

namespace ePortafolio.ViewModel
{
    public class MostrarTrabajosProfesorViewModel
    {
        public PeriodosBE Periodo { get; set; }

        public ProfesoresBE Profesor { get; set; }

        public List<SeccionesCursoBE> CursosDictado { get; set; }
        public List<CursosPeriodosBE> CursosTutor { get; set; }
        public List<CursosPeriodosBE> CursosExtraordinarios { get; set; }
        public List<CursosBE> Cursos { get; set; }
        public List<TrabajosBE> Trabajos { get; set; }

        public MostrarTrabajosProfesorViewModel(String PeriodoId, String ProfesorId)
        {
            var CursosExtraordinariosId = GetCursosExtraordinarios(PeriodoId, ProfesorId);
            
            Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(ProfesorId);
            Periodo = SSIARepositoryFactory.GetPeriodosRepository().GetOne(PeriodoId);
            CursosDictado = SSIARepositoryFactory.GetSeccionesCursoRepository().GetWhere(x => x.ProfesorId == ProfesorId && x.PeriodoId == PeriodoId);
            CursosTutor = SSIARepositoryFactory.GetCursosPeriodosRepository().GetWhere(x => x.CoordinadorId == ProfesorId && x.PeriodoId == PeriodoId);
            CursosExtraordinarios = SSIARepositoryFactory.GetCursosPeriodosRepository().GetWhere(x => CursosExtraordinariosId.Contains(x.CursoId));

            var CursosId = CursosTutor.Select(x => x.CursoId).Union(CursosDictado.Select(x => x.CursoId)).Union(CursosExtraordinarios.Select(x=>x.CursoId)).Distinct();
            
            Cursos = SSIARepositoryFactory.GetCursosRepository().GetWhere(x => CursosId.Contains(x.CursoId));
            Trabajos = ePortafolioRepositoryFactory.GetTrabajosRepository().GetWhere(x => CursosId.Contains(x.CursoId) && x.PeriodoId == PeriodoId);

            Trabajos = Trabajos.OrderBy(x => x.Nombre).ToList();
            Cursos = Cursos.OrderBy(x => x.Nombre).ToList();
        }


        public List<Int32> GetCursosExtraordinarios(String PeriodoId, String ProfesorId)
        {
            var Grupos = ePortafolioRepositoryFactory.GetGruposRepository().GetGruposEvaluacionExtraordinarios(PeriodoId,ProfesorId);
            return Grupos.Select(x => x.ExtraTrabajo.CursoId).ToList();
       }

    }
}
