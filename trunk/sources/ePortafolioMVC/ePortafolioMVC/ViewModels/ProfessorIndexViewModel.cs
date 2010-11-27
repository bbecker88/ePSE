using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorIndexViewModel
    {
        public BEProfesor ActualProfesor { get; set; }
        public Dictionary<BECurso, List<BETrabajo>> TrabajosCurso { get; set; }

        public ProfessorIndexViewModel()
        {
            TrabajosCurso = new Dictionary<BECurso,List<BETrabajo>>();
        }        
    }
}
