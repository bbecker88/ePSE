using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class StudentIndexViewModel
    {
        public BEAlumno ActualAlumno { get; set; }
        public BEPeriodo Periodo { get; set; }
        public Dictionary<BECurso,List<BETrabajo>> TrabajosCurso { get; set; }
        public List<BETrabajo> TrabajosIndependientes { get; set; }
        public List<BEGrupo> GruposTrabajosIndependientes { get; set; }
        public List<BEGrupo> GruposTrabajosEntregados { get; set; }
        public List<BEGrupo> GruposTrabajosPendientes { get; set; }

        public StudentIndexViewModel()
        {
            TrabajosCurso = new Dictionary<BECurso, List<BETrabajo>>();
        }
    }
}
