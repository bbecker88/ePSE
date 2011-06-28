using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BECurso
    {
        public int CursoId { get; set; }
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public BEProfesor Coordinador { get; set; }
    }
}
