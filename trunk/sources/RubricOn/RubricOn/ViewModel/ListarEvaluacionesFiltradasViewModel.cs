using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn;
using RubricOn.Models.RubricOn.Entities;

namespace RubricOn.ViewModel
{
    public class ListarEvaluacionesFiltradasViewModel
    {
        public List<EvaluacionesBE> Evaluaciones { get; set; }
        public List<ResultadosRubricasBE> Resultados { get; set; }

        public ListarEvaluacionesFiltradasViewModel(String RubricaId, String Version, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId, String FechaInicio, String FechaFin)
        {
            var FechaInicioDateTime = DateTime.Today;
            var FechaFinDateTime = DateTime.Today;

            var IsFechaInicioDateTime = false;
            var IsFechaFinDateTime = false;

            IsFechaInicioDateTime = DateTime.TryParse(FechaInicio, out FechaInicioDateTime);
            IsFechaFinDateTime = DateTime.TryParse(FechaFin, out FechaFinDateTime);

            Evaluaciones = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetWhere(x =>
                    (CodigoEvaluadoId == String.Empty || x.CodigoEvaluadoId.Contains(CodigoEvaluadoId)) &&
                    (CodigoEvaluadorId == String.Empty || x.CodigoEvaluadorId.Contains(CodigoEvaluadorId)) &&
                    (RubricaId == String.Empty || x.RubricaId.Contains(RubricaId)) &&
                    (Version == String.Empty || x.Version.Contains(Version)) &&
                    (TipoArtefacto == String.Empty || x.TipoArtefacto.Contains(TipoArtefacto)) &&
                    (IsFechaInicioDateTime == false || x.FechaEvaluacion > FechaInicioDateTime) &&
                    (IsFechaFinDateTime == false || x.FechaEvaluacion < FechaFinDateTime));

            Evaluaciones = Evaluaciones.OrderByDescending(x => x.FechaEvaluacion).ToList();
            
            var EvaluacionesId = Evaluaciones.Select(x => x.EvaluacionId);
            Resultados = RubricOnRepositoryFactory.GetResultadosRubricasRepository().GetWhere(x => EvaluacionesId.Contains(x.EvaluacionId));
        }
    }
}
