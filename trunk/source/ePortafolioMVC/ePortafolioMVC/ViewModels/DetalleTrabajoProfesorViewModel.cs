using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;

namespace ePortafolioMVC.ViewModels
{
    public class DetalleTrabajoProfesorViewModel
    {
        public Trabajo Trabajo { get; set; }
        public List<Grupo> ListGrupos { get; set; }
        public List<Alumno> ListAlumnosSinGrupo { get; set; }
    }
}
