using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Repository;
using  RubricOn.Models.RubricOn;
using System.Configuration;

namespace RubricOn.Models.RubricOn
{
    public static class RubricOnRepositoryFactory
    {
        private static String RubricOnConnectionString = ConfigurationManager.ConnectionStrings["RubricOn"].ConnectionString;

        public static bool SubmitChanges(bool ThrowException)
         {
             return DataContextFactory.SubmitChanges(ThrowException);
         }

        private static OutcomesRepository OutcomesRepository = null;
        public static OutcomesRepository GetOutcomesRepository()
        {
            if (OutcomesRepository == null)
                OutcomesRepository = new OutcomesRepository(RubricOnConnectionString);
            return OutcomesRepository;
        }	

        private static VersionesRubricasRepository VersionesRubricasRepository = null;
        public static VersionesRubricasRepository GetVersionesRubricasRepository()
        {
            if(VersionesRubricasRepository==null)
                VersionesRubricasRepository = new VersionesRubricasRepository(RubricOnConnectionString);
            return VersionesRubricasRepository;
        }	

        private static TiposArtefactoRepository TiposArtefactoRepository = null;
        public static TiposArtefactoRepository GetTiposArtefactoRepository()
        {
            if(TiposArtefactoRepository==null)
                TiposArtefactoRepository = new TiposArtefactoRepository(RubricOnConnectionString);
            return TiposArtefactoRepository;
        }	

        private static RubricasRepository RubricasRepository = null;
        public static RubricasRepository GetRubricasRepository()
        {
            if(RubricasRepository==null)
                RubricasRepository = new RubricasRepository(RubricOnConnectionString);
            return RubricasRepository;
        }	

        private static ResultadosRubricasRepository ResultadosRubricasRepository = null;
        public static ResultadosRubricasRepository GetResultadosRubricasRepository()
        {
            if(ResultadosRubricasRepository==null)
                ResultadosRubricasRepository = new ResultadosRubricasRepository(RubricOnConnectionString);
            return ResultadosRubricasRepository;
        }	

        private static RespuestasRubricaRepository RespuestasRubricaRepository = null;
        public static RespuestasRubricaRepository GetRespuestasRubricaRepository()
        {
            if(RespuestasRubricaRepository==null)
                RespuestasRubricaRepository = new RespuestasRubricaRepository(RubricOnConnectionString);
            return RespuestasRubricaRepository;
        }	

        private static EvaluacionesRepository EvaluacionesRepository = null;
        public static EvaluacionesRepository GetEvaluacionesRepository()
        {
            if(EvaluacionesRepository==null)
                EvaluacionesRepository = new EvaluacionesRepository(RubricOnConnectionString);
            return EvaluacionesRepository;
        }	

        private static CriterioRubricaRepository CriterioRubricaRepository = null;
        public static CriterioRubricaRepository GetCriterioRubricaRepository()
        {
            if(CriterioRubricaRepository==null)
                CriterioRubricaRepository = new CriterioRubricaRepository(RubricOnConnectionString);
            return CriterioRubricaRepository;
        }	

        private static CategoriasRubricasRepository CategoriasRubricasRepository = null;
        public static CategoriasRubricasRepository GetCategoriasRubricasRepository()
        {
            if(CategoriasRubricasRepository==null)
                CategoriasRubricasRepository = new CategoriasRubricasRepository(RubricOnConnectionString);
            return CategoriasRubricasRepository;
        }	

        private static AspectosRubricaRepository AspectosRubricaRepository = null;
        public static AspectosRubricaRepository GetAspectosRubricaRepository()
        {
            if(AspectosRubricaRepository==null)
                AspectosRubricaRepository = new AspectosRubricaRepository(RubricOnConnectionString);
            return AspectosRubricaRepository;
        }	
    }
}
