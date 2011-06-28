using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorHistoryViewModel
    {
        public BEProfesor ActualProfesor { get; set; }
        public List<BEPeriodo> Periodos { get; set; }
        public List<BECurso> Cursos { get; set; }
        public List<BETrabajo> Trabajos { get; set; }
    }
}
