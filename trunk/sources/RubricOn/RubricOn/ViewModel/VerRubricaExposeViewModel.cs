using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities;
using RubricOn.Models.RubricOn;

namespace RubricOn.ViewModel
{
    public class VerRubricaExposeViewModel
    {
        public VersionesRubricasBE Rubrica { get; set; }
        public String RutaRetorno { get; set; }

        public List<CategoriasRubricasBE> Categorias { get; set; }
        public List<AspectosRubricaBE> Aspectos { get; set; }
        public List<CriterioRubricaBE> Criterios { get; set; }
        
        public EvaluacionesBE Evaluacion { get; set; }
        public ResultadosRubricasBE Resultado { get; set; }
        public List<RespuestasRubricaBE> Respuestas { get; set; }        

        public Boolean MostrarDetallesEvaluacion { get; set; }

        public VerRubricaExposeViewModel(String RubricaId, String Version, String TipoArtefacto, int EvaluacionId, String RutaRetorno)
        {
            this.RutaRetorno = RutaRetorno;

            Rubrica = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetOne(RubricaId, TipoArtefacto, Version);

            MostrarDetallesEvaluacion = false;

            Categorias = RubricOnRepositoryFactory.GetCategoriasRubricasRepository().GetWhere(x=>x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.Version == Version,x=>x.Orden);
            var CategoriasId = Categorias.Select(x => x.CategoriaRubricaId);
            
            Aspectos = RubricOnRepositoryFactory.GetAspectosRubricaRepository().GetWhere(x => CategoriasId.Contains(x.CategoriaRubricaId), x => x.Orden);
            var AspectosId = Aspectos.Select(x => x.AspectoRubricaId);
            
            Criterios = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetWhere(x => AspectosId.Contains(x.AspectoRubricaId), x => x.Orden);

            Evaluacion = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetOne(EvaluacionId);

            if (Evaluacion != null)
            {
                Resultado = RubricOnRepositoryFactory.GetResultadosRubricasRepository().GetOne(EvaluacionId);
                Respuestas = RubricOnRepositoryFactory.GetRespuestasRubricaRepository().GetWhere(x=>x.EvaluacionId == EvaluacionId);
                MostrarDetallesEvaluacion = true;
            }
        }
    }
}

