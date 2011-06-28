using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BEArchivo
    {
        public int ArchivoId { get; set; }
        public String Ruta { get; set; }
        public String Nombre { get; set; }
        public BEAlumno Alumno { get; set; }
        public DateTime? FechaSubido { get; set; }
    }
}
