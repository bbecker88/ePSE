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
    public class MostrarDetalleTrabajoEstudianteViewModel
    {
        public AlumnosBE Alumno { get; set; }
        public TrabajosBE Trabajo { get; set; }
        public GruposBE Grupo { get; set; }
        public CursosBE Curso { get; set; }
        public AlumnosGrupoBE AlumnoGrupo { get; set; }

        public MostrarDetalleTrabajoEstudianteViewModel(int TrabajoId, String AlumnoId)
        {
            Alumno = SSIARepositoryFactory.GetAlumnosRepository().GetOne(AlumnoId);
            Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);
            Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetGrupoTrabajo(TrabajoId, AlumnoId);
            Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Trabajo.CursoId);
            if (Grupo != null)
                AlumnoGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetOne(AlumnoId, Grupo.GrupoId);
        }
    }
}
