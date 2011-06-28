using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BETrabajo
    {
        public int TrabajoId { get; set; }
        public BECurso Curso { get; set; }
        public BEPeriodo Periodo { get; set; }
        public bool EsGrupal { get; set; }
        public String Nombre { get; set; }
        public String Instrucciones { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public String Iniciativa { get; set; }
    }
}
