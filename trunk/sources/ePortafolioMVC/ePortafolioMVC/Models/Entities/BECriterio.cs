using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BECriterio
    {
        public int CriterioId { get; set; }
        public String Nombre { get; set; }
        public BERubrica Rubrica { get; set; }
        public Decimal Valor { get; set; }
    }
}
