using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn;
using RubricOn.Models.RubricOn.Entities;

namespace RubricOn.ViewModel
{
    public class ListarRubricasArtefactoViewModel
    {
        public List<VersionesRubricasBE> Rubricas { get; set; }
        public TiposArtefactoBE TipoArtefacto { get; set; }

        public ListarRubricasArtefactoViewModel(String TipoArtefacto)
        {
            Rubricas = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => (x.TipoArtefacto == TipoArtefacto || TipoArtefacto == String.Empty) && x.EsActual == true);
            this.TipoArtefacto = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetOne(TipoArtefacto);

            Rubricas = Rubricas.OrderByDescending(x => x.FechaCreacion).ToList();
        }
    }
}
