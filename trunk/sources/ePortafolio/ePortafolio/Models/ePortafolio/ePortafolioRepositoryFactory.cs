using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Repository;
using  ePortafolio.Models.ePortafolio;
using System.Configuration;

namespace ePortafolio.Models.ePortafolio
{
    public static class ePortafolioRepositoryFactory
    {
        
        
        private static String ePortafolioConnectionString = ConfigurationManager.ConnectionStrings["ePortafolio"].ConnectionString;//"Data Source=localhost;Initial Catalog=ePortafolio;Integrated Security=True";

        public static bool SubmitChanges(bool ThrowException)
         {
             return DataContextFactory.SubmitChanges(ThrowException);
         }	

        private static TrabajosOutcomeAlumnoRepository TrabajosOutcomeAlumnoRepository = null;
        public static TrabajosOutcomeAlumnoRepository GetTrabajosOutcomeAlumnoRepository()
        {
            if(TrabajosOutcomeAlumnoRepository==null)
                TrabajosOutcomeAlumnoRepository = new TrabajosOutcomeAlumnoRepository(ePortafolioConnectionString);
            return TrabajosOutcomeAlumnoRepository;
        }	

        private static TrabajosRepository TrabajosRepository = null;
        public static TrabajosRepository GetTrabajosRepository()
        {
            if(TrabajosRepository==null)
                TrabajosRepository = new TrabajosRepository(ePortafolioConnectionString);
            return TrabajosRepository;
        }	

        private static GruposRepository GruposRepository = null;
        public static GruposRepository GetGruposRepository()
        {
            if(GruposRepository==null)
                GruposRepository = new GruposRepository(ePortafolioConnectionString);
            return GruposRepository;
        }	

        private static EvaluacionesOutcomeProfesorRepository EvaluacionesOutcomeProfesorRepository = null;
        public static EvaluacionesOutcomeProfesorRepository GetEvaluacionesOutcomeProfesorRepository()
        {
            if(EvaluacionesOutcomeProfesorRepository==null)
                EvaluacionesOutcomeProfesorRepository = new EvaluacionesOutcomeProfesorRepository(ePortafolioConnectionString);
            return EvaluacionesOutcomeProfesorRepository;
        }	

        private static EvaluacionesGruposProfesorRepository EvaluacionesGruposProfesorRepository = null;
        public static EvaluacionesGruposProfesorRepository GetEvaluacionesGruposProfesorRepository()
        {
            if(EvaluacionesGruposProfesorRepository==null)
                EvaluacionesGruposProfesorRepository = new EvaluacionesGruposProfesorRepository(ePortafolioConnectionString);
            return EvaluacionesGruposProfesorRepository;
        }	

        private static ArchivosGrupoRepository ArchivosGrupoRepository = null;
        public static ArchivosGrupoRepository GetArchivosGrupoRepository()
        {
            if(ArchivosGrupoRepository==null)
                ArchivosGrupoRepository = new ArchivosGrupoRepository(ePortafolioConnectionString);
            return ArchivosGrupoRepository;
        }	

        private static ArchivosRepository ArchivosRepository = null;
        public static ArchivosRepository GetArchivosRepository()
        {
            if(ArchivosRepository==null)
                ArchivosRepository = new ArchivosRepository(ePortafolioConnectionString);
            return ArchivosRepository;
        }	

        private static AlumnosGrupoRepository AlumnosGrupoRepository = null;
        public static AlumnosGrupoRepository GetAlumnosGrupoRepository()
        {
            if(AlumnosGrupoRepository==null)
                AlumnosGrupoRepository = new AlumnosGrupoRepository(ePortafolioConnectionString);
            return AlumnosGrupoRepository;
        }	
    }
}
