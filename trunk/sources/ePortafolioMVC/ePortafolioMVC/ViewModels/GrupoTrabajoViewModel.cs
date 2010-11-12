using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;
using System.Web.Mvc;

namespace ePortafolioMVC.ViewModels
{
    public class GrupoTrabajoViewModel
    {
        public Trabajo Trabajo { get; set; }
        public Grupo Grupo { get; set; }
        public List<AlumnosGrupo> AlumnosGrupo { get; set; }
        public SelectList AlumnosSinGrupo { get; set; }
        public List<Archivo> Archivos { get; set; }
    }
}
