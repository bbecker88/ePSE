using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class SharedGroupMemberListViewModel
    {
        public BEAlumno Alumno { get; set; }
        public BEGrupo Grupo { get; set; }
        public BETrabajo Trabajo { get; set; }
        public List<BEAlumno> AlumnosGrupo { get; set; }
    }
}
