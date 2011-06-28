using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorAssignmentListViewModel
    {
        public BECurso Curso { get; set; }
        public BEProfesor ActualProfesor { get; set; }
        public List<BETrabajo> Trabajos { get; set; }
    }
}
