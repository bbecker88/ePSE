using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class CursoRepository : ePortafolioMVC.Models.Repository.ICursoRepository
    {
        public BECurso GetCurso(int CursoId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var Curso = pyrIntegradoDBDataContext.ePSE_Cursos.SingleOrDefault(c => c.CursoId == CursoId);
            var Coordinador = pyrIntegradoDBDataContext.ePSE_CursosPeriodos.SingleOrDefault(c => c.CursoId == CursoId && c.PeriodoId == PeriodoId);

            if (Curso != null)
            {
                return new BECurso
                {
                    CursoId = Curso.CursoId,
                    Nombre = Curso.Nombre,
                    Codigo = Curso.Codigo,
                    Coordinador = RepositoryFactory.GetProfesorRepository().GetProfesorNoFK(Coordinador!=null?Coordinador.CoordinadorId:null)
                };
            }
            return null;
        }

        public BECurso GetCursoNoFK(int CursoId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var Curso = pyrIntegradoDBDataContext.ePSE_Cursos.SingleOrDefault(c => c.CursoId == CursoId);
            var Coordinador = pyrIntegradoDBDataContext.ePSE_CursosPeriodos.SingleOrDefault(c => c.CursoId == CursoId && c.PeriodoId == PeriodoId);

            if (Curso != null)
            {
                return new BECurso
                {
                    CursoId = Curso.CursoId,
                    Nombre = Curso.Nombre,
                    Codigo = Curso.Codigo,
                    Coordinador = Coordinador != null ? new BEProfesor() { ProfesorId = Coordinador.CoordinadorId } : null
                };
            }
            return null;
        }

        public List<BECurso> GetCursosEvaluados(String ProfesorId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var CursosDictadosId = (from sc in pyrIntegradoDBDataContext.ePSE_SeccionesCursos
                                    where sc.PeriodoId == PeriodoId && sc.ProfesorId == ProfesorId
                                    select sc.CursoId).Distinct();

            var CursosDictados = from cd in CursosDictadosId
                                 select RepositoryFactory.GetCursoRepository().GetCursoNoFK(cd, PeriodoId);

            return CursosDictados.ToList();
        }
        
        public List<BECurso> GetCursosDictados(String ProfesorId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var CursosDictadosId = (from sc in pyrIntegradoDBDataContext.ePSE_SeccionesCursos
                                    where sc.PeriodoId == PeriodoId && sc.ProfesorId == ProfesorId
                                    select sc.CursoId
                                    ).Union(
                                    from sc in pyrIntegradoDBDataContext.ePSE_CursosPeriodos
                                    where sc.PeriodoId == PeriodoId && sc.CoordinadorId == ProfesorId
                                    select sc.CursoId).Distinct();
            
            var CursosDictados = from cd in CursosDictadosId
                                 select RepositoryFactory.GetCursoRepository().GetCursoNoFK(cd,PeriodoId);

            return CursosDictados.ToList();
        }

        public List<BECurso> GetCursosMatriculados(String AlumnoId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            //Falta agregar los cursos en los que es coordinador

            var CursosMatriculadosId = (from ac in pyrIntegradoDBDataContext.ePSE_AlumnosCursos
                                        where ac.PeriodoId == PeriodoId && ac.AlumnoId == AlumnoId
                                        select ac.CursoId).Distinct();

            var CursosMatriculados = from cm in CursosMatriculadosId
                                     select RepositoryFactory.GetCursoRepository().GetCursoNoFK(cm,PeriodoId);

            return CursosMatriculados.ToList();
        }

    }
}
