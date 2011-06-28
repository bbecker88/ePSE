using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn;
using RubricOn.Models.RubricOn.Entities;

namespace RubricOn.ViewModel
{
    public class ListarRubricasViewModel
    {
        public List<TiposArtefactoBE> TiposArtefacto { get; set; }
        public String TipoArtefacto { get; set; }

        public ListarRubricasViewModel()
        {
            TiposArtefacto = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetAll();
            TipoArtefacto = "";
        }
    }
}
