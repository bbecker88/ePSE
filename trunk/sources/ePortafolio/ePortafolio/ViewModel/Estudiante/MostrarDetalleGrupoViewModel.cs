using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;

namespace ePortafolio.ViewModel
{
    public class MostrarDetalleGrupoViewModel
    {
        public Boolean Habilitado { get; set; }

        public AlumnosBE Alumno { get; set; }
        public GruposBE Grupo { get; set; }

        public List<AlumnosBE> AlumnosGrupo  { get; set; }
        public List<AlumnosBE> AlumnosSinGrupo  { get; set; }
        public Boolean EsIndependiente { get; set; }     

        public MostrarDetalleGrupoViewModel(int TrabajoId, String AlumnoId)
        {
            Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetGrupoTrabajo(TrabajoId, AlumnoId);
            
            var AlumnosId = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x=>x.GrupoId == Grupo.GrupoId).Select(x=>x.AlumnoId);
            AlumnosGrupo = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x=>AlumnosId.Contains(x.AlumnoId));

            Alumno = AlumnosGrupo.SingleOrDefault(x => x.AlumnoId == AlumnoId);

            var Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);

            var AlumnosSeccionId = SSIARepositoryFactory.GetAlumnosCursoRepository().GetWhere(x => x.SeccionId == Grupo.SeccionId && x.PeriodoId == Trabajo.PeriodoId && x.CursoId == Trabajo.CursoId).Select(x => x.AlumnoId.Trim().ToUpper());
            var AlumnosGruposTrabajoId = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetAlumnosGrupoTrabajo(TrabajoId).Select(x => x.AlumnoId.Trim().ToUpper());

            var AlumnosSinGrupoId = AlumnosSeccionId.Where(x => !(AlumnosGruposTrabajoId.Contains(x)));
            AlumnosSinGrupo = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosSinGrupoId.Contains(x.AlumnoId));

            AlumnosGrupo = AlumnosGrupo.OrderBy(x => x.Nombre).ToList();
            AlumnosSinGrupo = AlumnosSinGrupo.OrderBy(x => x.Nombre).ToList();

            Habilitado = Grupo.EvaluacionId == null;

            EsIndependiente = Trabajo.Iniciativa != "UPC";
        }

    }
}
