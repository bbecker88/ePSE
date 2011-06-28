using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class StudentIndependentAssignmentListViewModel
    {
        public BEAlumno ActualAlumno { get; set; }
        public List<BETrabajo> Trabajos { get; set; }
        public List<BEGrupo> GruposTrabajosIndependientes { get; set; }
    }
}
