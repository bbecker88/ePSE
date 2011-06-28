using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.RubricOnService;
using ePortafolio.Logic;

namespace ePortafolio.ViewModel
{
    public class MostrarTrabajosEstudianteViewModel
    {
        public AlumnosBE Alumno { get; set; }
        public PeriodosBE Periodo { get; set; }

        public List<AlumnosCursoBE> CursosAlumno { get; set; }
        public List<TrabajosBE> Trabajos { get; set; }
        public List<GruposBE> Grupos { get; set; }
        public List<GruposBE> GruposConArchivosEntregados { get; set; }
        public List<AlumnosGrupoBE> GruposAlumno { get; set; }

        public MostrarTrabajosEstudianteViewModel(String PeriodoId, String AlumnoId)
        {
            Periodo = SSIARepositoryFactory.GetPeriodosRepository().GetOne(PeriodoId);
            CursosAlumno = SSIARepositoryFactory.GetAlumnosCursoRepository().GetWhere(x => x.AlumnoId == AlumnoId && x.PeriodoId == PeriodoId);
            Alumno = SSIARepositoryFactory.GetAlumnosRepository().GetOne(AlumnoId);
            var CursosId = CursosAlumno.Select(x => x.CursoId).Distinct();
            Trabajos = ePortafolioRepositoryFactory.GetTrabajosRepository().GetWhere(x => x.PeriodoId == PeriodoId && CursosId.Contains(x.CursoId));
            Grupos = ePortafolioRepositoryFactory.GetGruposRepository().GetGruposPeriodo(PeriodoId, AlumnoId);
            var GruposId = Grupos.Select(x => x.GrupoId);
            GruposAlumno = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => GruposId.Contains(x.GrupoId) && x.AlumnoId == AlumnoId);
            GruposConArchivosEntregados = ePortafolioRepositoryFactory.GetGruposRepository().GetGruposPeriodoConArchivosEntregados(PeriodoId, AlumnoId);

            Trabajos = Trabajos.OrderBy(x => x.Nombre).ToList();
            CursosAlumno = CursosAlumno.OrderBy(x => x.NombreCurso).ToList();
        }
    }
}
