using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class StudentAssignmentListViewModel
    {
        public BECurso Curso { get; set; }
        public BEAlumno ActualAlumno { get; set; }
        public List<BETrabajo> Trabajos { get; set; }
        public List<BEGrupo> GruposTrabajosEntregados { get; set; }
        public List<BEGrupo> GruposTrabajosPendientes { get; set; }
    }
}
