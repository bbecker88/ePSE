using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BESeccion
    {
        public String SeccionId { get; set; }
        public BEProfesor Profesor { get; set; }
        public BECurso Curso { get; set; }
        public String Nombre { get; set; }
    }
}
