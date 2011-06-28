using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class SharedRubricaViewModel
    {
        public BERubrica Rubrica { get; set; }
        public List<BECriterio> Criterios { get; set; }
        public List<BEResultadoRubrica> ResultadoRubricas { get; set; }
        public Boolean Editable { get; set; }
    }
}
