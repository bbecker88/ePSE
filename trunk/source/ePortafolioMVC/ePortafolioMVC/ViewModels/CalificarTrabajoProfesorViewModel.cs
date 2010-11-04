using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;

namespace ePortafolioMVC.ViewModels
{
    public class CalificarTrabajoProfesorViewModel
    {
        public Grupo Grupo { get; set; }
        public List<Rubrica> ListRubricas { get; set; }
        public List<ResultadosRubricaGrupo> ListResultados { get; set; }
    }
}
