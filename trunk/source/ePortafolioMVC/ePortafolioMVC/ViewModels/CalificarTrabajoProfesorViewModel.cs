using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;

namespace ePortafolioMVC.ViewModels
{
    public class CalificarTrabajoProfesorViewModel
    {
        public Trabajo Trabajo { get; set; }
        public Grupo Grupo { get; set; }
        public List<Rubrica> ListRubricas { get; set; }
        public List<ResultadosRubricaGrupo> ListResultados { get; set; }
        public String Origen {get;set;}
        public bool Editable { get; set; }
    }
}
