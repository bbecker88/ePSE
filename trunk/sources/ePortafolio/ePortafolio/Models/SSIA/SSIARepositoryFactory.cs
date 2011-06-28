using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Repository;
using  ePortafolio.Models.SSIA;
using System.Configuration;

namespace ePortafolio.Models.SSIA
{
    public static class SSIARepositoryFactory
    {
        private static String SSIAConnectionString = ConfigurationManager.ConnectionStrings["SSIA"].ConnectionString;//"Data Source=localhost;Initial Catalog=SSIA;Integrated Security=True";

        public static bool SubmitChanges(bool ThrowException)
         {
             return DataContextFactory.SubmitChanges(ThrowException);
         }	

        private static ProfesoresRepository ProfesoresRepository = null;
        public static ProfesoresRepository GetProfesoresRepository()
        {
            if(ProfesoresRepository==null)
                ProfesoresRepository = new ProfesoresRepository(SSIAConnectionString);
            return ProfesoresRepository;
        }	

        private static PeriodosRepository PeriodosRepository = null;
        public static PeriodosRepository GetPeriodosRepository()
        {
            if(PeriodosRepository==null)
                PeriodosRepository = new PeriodosRepository(SSIAConnectionString);
            return PeriodosRepository;
        }	

        private static OutcomesAlumnoRepository OutcomesAlumnoRepository = null;
        public static OutcomesAlumnoRepository GetOutcomesAlumnoRepository()
        {
            if(OutcomesAlumnoRepository==null)
                OutcomesAlumnoRepository = new OutcomesAlumnoRepository(SSIAConnectionString);
            return OutcomesAlumnoRepository;
        }	

        private static OutcomesRepository OutcomesRepository = null;
        public static OutcomesRepository GetOutcomesRepository()
        {
            if(OutcomesRepository==null)
                OutcomesRepository = new OutcomesRepository(SSIAConnectionString);
            return OutcomesRepository;
        }	

        private static CursosPeriodosRepository CursosPeriodosRepository = null;
        public static CursosPeriodosRepository GetCursosPeriodosRepository()
        {
            if(CursosPeriodosRepository==null)
                CursosPeriodosRepository = new CursosPeriodosRepository(SSIAConnectionString);
            return CursosPeriodosRepository;
        }	

        private static CursosRepository CursosRepository = null;
        public static CursosRepository GetCursosRepository()
        {
            if(CursosRepository==null)
                CursosRepository = new CursosRepository(SSIAConnectionString);
            return CursosRepository;
        }	

        private static CoordinadoresRepository CoordinadoresRepository = null;
        public static CoordinadoresRepository GetCoordinadoresRepository()
        {
            if(CoordinadoresRepository==null)
                CoordinadoresRepository = new CoordinadoresRepository(SSIAConnectionString);
            return CoordinadoresRepository;
        }	

        private static AlumnosCursoRepository AlumnosCursoRepository = null;
        public static AlumnosCursoRepository GetAlumnosCursoRepository()
        {
            if(AlumnosCursoRepository==null)
                AlumnosCursoRepository = new AlumnosCursoRepository(SSIAConnectionString);
            return AlumnosCursoRepository;
        }	

        private static AlumnosRepository AlumnosRepository = null;
        public static AlumnosRepository GetAlumnosRepository()
        {
            if(AlumnosRepository==null)
                AlumnosRepository = new AlumnosRepository(SSIAConnectionString);
            return AlumnosRepository;
        }	

        private static SeccionesCursoRepository SeccionesCursoRepository = null;
        public static SeccionesCursoRepository GetSeccionesCursoRepository()
        {
            if(SeccionesCursoRepository==null)
                SeccionesCursoRepository = new SeccionesCursoRepository(SSIAConnectionString);
            return SeccionesCursoRepository;
        }	
    }
}
