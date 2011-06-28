using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities;
using RubricOn.Models.RubricOn;

namespace RubricOn.ViewModel
{
    public class EvaluarRubricaExposeViewModel
    {
        public VersionesRubricasBE Rubrica { get; set; }
        public List<CategoriasRubricasBE> Categorias { get; set; }
        public List<AspectosRubricaBE> Aspectos { get; set; }
        public List<CriterioRubricaBE> Criterios { get; set; }
        public List<RespuestasRubricaBE> RespuestasPlantilla { get; set; }
        
        public EvaluacionesBE Evaluacion { get; set; }

        public String RutaCancelado { get; set; }
        public String GUID { get; set; }

        public EvaluarRubricaExposeViewModel(String RubricaId, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId, String GUID, Int32? CodigoEvaluacionPlantilla, String RutaCancelado)
        {
            this.GUID = GUID;
            this.RutaCancelado = RutaCancelado;

            Rubrica = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x=>x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.EsActual == true).First();

            Categorias = RubricOnRepositoryFactory.GetCategoriasRubricasRepository().GetWhere(x=>x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.Version == Rubrica.Version,x=>x.Orden);
            var CategoriasId = Categorias.Select(x => x.CategoriaRubricaId);
            
            Aspectos = RubricOnRepositoryFactory.GetAspectosRubricaRepository().GetWhere(x => CategoriasId.Contains(x.CategoriaRubricaId), x => x.Orden);
            var AspectosId = Aspectos.Select(x => x.AspectoRubricaId);
            
            Criterios = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetWhere(x => AspectosId.Contains(x.AspectoRubricaId), x => x.Orden);
            Evaluacion = new EvaluacionesBE()
                    {
                        CodigoEvaluadoId = CodigoEvaluadoId,
                        CodigoEvaluadorId = CodigoEvaluadorId,
                    };

            RespuestasPlantilla = new List<RespuestasRubricaBE>();
            
            if (CodigoEvaluacionPlantilla.HasValue)
            {
                RespuestasPlantilla = RubricOnRepositoryFactory.GetRespuestasRubricaRepository().GetWhere(x => x.EvaluacionId == CodigoEvaluacionPlantilla.Value);
            }
        }
    }
}

