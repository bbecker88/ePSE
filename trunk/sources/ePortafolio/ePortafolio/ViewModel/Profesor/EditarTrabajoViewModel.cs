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
    public class EditarTrabajoViewModel
    {
        public ProfesoresBE Profesor { get; set; }
        public TrabajosBE Trabajo { get; set; }
        public CursosBE Curso { get; set; }

        public EditarTrabajoViewModel(int TrabajoId, String ProfesorId)
        {
            Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(ProfesorId);
            Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);
            Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Trabajo.CursoId);
        }
    }
}
