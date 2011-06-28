using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn;
using RubricOn.Models.RubricOn.Entities;

namespace RubricOn.ViewModel
{
    public class ListarEvaluacionesViewModel
    {
        public List<TiposArtefactoBE> TiposArtefacto { get; set; }
        public String RubricaId { get; set; }
        public String Version { get; set; }
        public String TipoArtefacto { get; set; }
        public String CodigoEvaluadoId { get; set; }
        public String CodigoEvaluadorId { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }


        public ListarEvaluacionesViewModel(String RubricaId, String Version, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId, String FechaInicio, String FechaFin)
        {
            TiposArtefacto = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetAll();
            this.RubricaId = RubricaId;
            this.Version = Version;
            this.TipoArtefacto = TipoArtefacto;
            this.CodigoEvaluadoId = CodigoEvaluadoId;
            this.CodigoEvaluadorId = CodigoEvaluadorId;
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
        }
    }
}
