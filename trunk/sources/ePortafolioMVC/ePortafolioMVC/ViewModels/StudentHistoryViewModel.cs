using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class StudentHistoryViewModel
    {
        public BEAlumno ActualAlumno { get; set; }
        public List<BEPeriodo> Periodos { get; set; }
        public List<BECurso> Cursos { get; set; }
        public List<BETrabajo> Trabajos { get; set; }
        public List<BEGrupo> Grupos { get; set; }
        public Dictionary<BEGrupo, List<BEAlumno>> AlumnosGrupo { get; set; }

        public StudentHistoryViewModel()
        {
            AlumnosGrupo = new Dictionary<BEGrupo, List<BEAlumno>>();
        }  
    }
}
