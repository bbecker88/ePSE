using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorDetailsViewModel
    {
        public BECurso Curso { get; set; }
        public BETrabajo Trabajo { get; set; }
        public BEResultadoPrograma ResultadoPrograma { get; set; }

        public List<BESeccion> Secciones { get; set; }
        
        
        public Dictionary<BEGrupo, List<BEAlumno>> AlumnosGrupoEntregados { get; set; }
        public Dictionary<BEGrupo, List<BEAlumno>> AlumnosGrupoPendientes { get; set; }

        public ProfessorDetailsViewModel()
        {
            AlumnosGrupoEntregados = new Dictionary<BEGrupo, List<BEAlumno>>();
            AlumnosGrupoPendientes = new Dictionary<BEGrupo, List<BEAlumno>>();
        }        
    }
}
