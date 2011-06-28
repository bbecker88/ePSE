using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn;
using RubricOn.Models.RubricOn.Entities;

namespace RubricOn.ViewModel
{
    public class ListarVersionesRubricaViewModel
    {
        public List<VersionesRubricasBE> Rubricas { get; set; }
        public TiposArtefactoBE TipoArtefacto { get; set; }
        public List<String> VersionesConEvaluaciones { get; set; }


        public ListarVersionesRubricaViewModel(String RubricaId,String TipoArtefacto)
        {
            Rubricas = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto);
            Rubricas = Rubricas.OrderByDescending(x => x.FechaCreacion).ToList();
            this.TipoArtefacto = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetOne(TipoArtefacto);
            VersionesConEvaluaciones = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto).Select(x=>x.Version).Distinct().ToList();
        }
    }
}
