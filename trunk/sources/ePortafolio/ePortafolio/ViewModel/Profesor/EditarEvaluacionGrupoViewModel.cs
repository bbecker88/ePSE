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
    public class EditarEvaluacionGrupoViewModel
    {
        public List<AlumnosGrupoBE> AlumnosGrupo { get; set; }
        public List<AlumnosBE> Alumnos { get; set; }
        public GruposBE Grupo { get; set; }

        public EditarEvaluacionGrupoViewModel()
        {
        }

        public EditarEvaluacionGrupoViewModel(int GrupoId, String ProfesorId)
        {
            Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
            AlumnosGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => x.GrupoId == GrupoId);
            var AlumnosGrupoId = AlumnosGrupo.Select(x => x.AlumnoId);
            Alumnos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosGrupoId.Contains(x.AlumnoId),x=>x.Nombre);
        }
    }
}
