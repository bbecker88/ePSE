using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models.Entities
{
    public class BEGrupo
    {
        public int GrupoId { get; set; }
        public BETrabajo Trabajo { get; set; }
        public String Nota { get; set; }
        public BESeccion Seccion { get; set; }
        public BEAlumno Lider { get; set; }
    }
}
