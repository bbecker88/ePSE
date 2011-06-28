using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.SSIA.Entities;

namespace ePortafolio.ViewModel
{
    public class MostrarTrabajosCursoEvaluadorGrupos
    {
        public List<TrabajosBE> Trabajos { get; set; }
        public int TrabajoId { get; set; }
        public int CursoId { get; set; }

        public MostrarTrabajosCursoEvaluadorGrupos(String PeriodoId, int CursoId)
        {
            Trabajos = ePortafolioRepositoryFactory.GetTrabajosRepository().GetWhere(x => x.CursoId == CursoId && x.PeriodoId == PeriodoId);
            this.CursoId = CursoId;
        }
    }
}
