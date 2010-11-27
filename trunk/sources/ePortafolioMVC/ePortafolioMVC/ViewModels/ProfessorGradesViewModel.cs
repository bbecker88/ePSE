using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorGradesViewModel
    {
        public BECurso Curso { get; set; }
        public BETrabajo Trabajo { get; set; }
        public Dictionary<BEAlumno, String> AlumnosNotas { get; set; }
        public List<BESeccion> Secciones { get; set; }
        public String Origen { get; set; }

        public ProfessorGradesViewModel()
        {
            AlumnosNotas = new Dictionary<BEAlumno, String>();
        }   
    }
}
