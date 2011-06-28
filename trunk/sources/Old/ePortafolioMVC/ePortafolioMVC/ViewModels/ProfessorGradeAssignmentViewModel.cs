using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class ProfessorGradeAssignmentViewModel
    {
        public BECurso Curso { get; set; }
        public BETrabajo Trabajo { get; set; }
        public BEGrupo Grupo { get; set; }
        public Dictionary<BERubrica,List<BECriterio>> CriteriosRubrica { get; set; }
        public List<BEResultadoRubrica> ResultadosRubricas { get; set; }
        public List<BEArchivo> Archivos { get; set; }
        public String Origen {get;set;}
        public bool EsEditable { get; set; }

        public ProfessorGradeAssignmentViewModel()
        {
            CriteriosRubrica = new Dictionary<BERubrica, List<BECriterio>>();
        }   
    }
}
