using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class SeccionRepository : ePortafolioMVC.Models.Repository.ISeccionRepository
    {
        public BESeccion GetSeccion(String SeccionId)
        {
            return new BESeccion { SeccionId = SeccionId,Nombre = SeccionId };
            /*
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();
            
            var Seccion = ePortafolioDAO.Secciones.SingleOrDefault(s => s.SeccionId == SeccionId);

            if (Seccion != null)
            {
                return new BESeccion
                    {
                        SeccionId = Seccion.SeccionId,
                        Nombre = Seccion.Nombre,
                        Profesor = RepositoryFactory.GetProfesorRepository().GetProfesorNoFK(Seccion.ProfesorId),
                        Curso = RepositoryFactory.GetCursoRepository().GetCursoNoFK(Seccion.CursoId)
                    };
            }

            return null;
             */
        }

        public BESeccion GetSeccionNoFK(String SeccionId)
        {
            return new BESeccion { SeccionId = SeccionId,Nombre = SeccionId };
            /*
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Seccion = ePortafolioDAO.Secciones.SingleOrDefault(s => s.SeccionId == SeccionId);

            if (Seccion != null)
            {
                return new BESeccion
                {
                    SeccionId = Seccion.SeccionId,
                    Nombre = Seccion.Nombre,
                    Profesor = new BEProfesor { ProfesorId = Seccion.ProfesorId },
                    Curso = new BECurso { CursoId = Seccion.CursoId }
                };
            }

            return null;*/
        }

        public BESeccion GetSeccionCursoAlumno(String AlumnoId,int CursoId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var SeccionCursoAlumno = pyrIntegradoDBDataContext.ePSE_AlumnosCursos.SingleOrDefault(ac => ac.CursoId == CursoId && ac.AlumnoId == AlumnoId && ac.PeriodoId == PeriodoId);

            if (SeccionCursoAlumno != null)
            {
                return RepositoryFactory.GetSeccionRepository().GetSeccionNoFK(SeccionCursoAlumno.SeccionId);
            }
            return null;
        }

        public List<BESeccion> GetSeccionesCurso(int CursoId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var SeccionesCurso = from s in pyrIntegradoDBDataContext.ePSE_SeccionesCursos
                                 where s.CursoId == CursoId && s.PeriodoId == PeriodoId
                                 select RepositoryFactory.GetSeccionRepository().GetSeccionNoFK(s.SeccionId);

            return SeccionesCurso.ToList();
        }

        public List<BESeccion> GetSeccionesDictado(int CursoId, String ProfesorId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var SeccionesCurso = from s in pyrIntegradoDBDataContext.ePSE_SeccionesCursos
                                 where s.CursoId == CursoId && s.PeriodoId == PeriodoId && s.ProfesorId == ProfesorId
                                 select RepositoryFactory.GetSeccionRepository().GetSeccionNoFK(s.SeccionId);

            return SeccionesCurso.ToList();
        }
    }
}
