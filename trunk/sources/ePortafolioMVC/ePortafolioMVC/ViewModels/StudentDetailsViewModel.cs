using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;
using System.Web.Mvc;

namespace ePortafolioMVC.ViewModels
{
    public class StudentDetailsViewModel
    {
        public BEAlumno Alumno { get; set; }
        public BEGrupo Grupo { get; set; }
        public BETrabajo Trabajo { get; set; }
        public BECurso Curso { get; set; }
        public BESeccion Seccion { get; set; }
        public BEResultadoPrograma ResultadoPrograma { get; set; }

        public List<BEAlumno> AlumnosGrupo { get; set; }
        public List<BEArchivo> ArchivosGrupo { get; set; }
        public SelectList AlumnosSinGrupo { get; set; }
    }
}
