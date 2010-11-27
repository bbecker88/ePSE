using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BEPeriodo
    {
        public String PeriodoId { get; set; }
        public String Nombre { get; set; }
        public Boolean EsActual { get; set; }
    }
}
