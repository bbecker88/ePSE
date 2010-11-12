using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;

namespace ePortafolioMVC.ViewModels
{
    public class ReporteNotasTrabajoProfesorViewModel
    {
        public Trabajo Trabajo { get; set; }
        public List<AlumnoNota> ListAlumnoNotas { get; set; }
        public String Origen { get; set; }
    }

    public class AlumnoNota
    { 
        public Alumno Alumno  { get; set; }
        public String Nota { get; set; }
    }
}
