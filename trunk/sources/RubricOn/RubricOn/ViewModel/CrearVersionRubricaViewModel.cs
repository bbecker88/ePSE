using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities;
using RubricOn.Models.RubricOn;

namespace RubricOn.ViewModel
{
    public class CrearVersionRubricaViewModel
    {
        public VersionesRubricasBE Rubrica { get; set; }
        public List<TiposArtefactoBE> TiposArtefactos { get; set; }
        public bool EsNuevo { get; set; }

        public CrearVersionRubricaViewModel(String RubricaId, String TipoArtefacto)
        {
            TiposArtefactos = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetAll();
            Rubrica = new VersionesRubricasBE();
            var RubricaActual = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x=>x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.EsActual == true).FirstOrDefault();

            if (RubricaActual != null)
                Rubrica = RubricaActual;

            Rubrica.RubricaId = RubricaId;
            Rubrica.TipoArtefacto = TipoArtefacto;

            EsNuevo = RubricaActual == null;
        }

        public CrearVersionRubricaViewModel()
        {
            TiposArtefactos = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetAll();
        }
    }
}
