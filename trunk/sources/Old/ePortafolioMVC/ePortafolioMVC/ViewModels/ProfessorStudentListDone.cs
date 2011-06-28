using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorStudentListDone
    {
        public Dictionary<BEGrupo, List<BEAlumno>> AlumnosGrupos { get; set; }     
    }
}
