using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class AlumnoRepository : ePortafolioMVC.Models.Repository.IAlumnoRepository 
    {
        public BEAlumno GetAlumno(String AlumnoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var Alumno = pyrIntegradoDBDataContext.ePSE_Alumnos.SingleOrDefault(a => a.AlumnoId == AlumnoId);

            if (Alumno != null)
            {
                return new BEAlumno
                {
                    AlumnoId = Alumno.AlumnoId,
                    Nombre = Alumno.Nombre,
                    CorreoElectronico = Alumno.CorreoElectronico
                };
            }

            return null;
        }

        public BEAlumno GetAlumnoNoFK(String AlumnoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var Alumno = pyrIntegradoDBDataContext.ePSE_Alumnos.SingleOrDefault(a => a.AlumnoId == AlumnoId);

            if (Alumno != null)
            {
                return new BEAlumno
                {
                    AlumnoId = Alumno.AlumnoId,
                    Nombre = Alumno.Nombre,
                    CorreoElectronico = Alumno.CorreoElectronico
                };
            }

            return null;
        }

        public BEAlumno GetLiderGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var LiderAlumnoGrupo = ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId == GrupoId && ag.EsLider == true);

            if (LiderAlumnoGrupo == null)
                throw new Exception("El grupo no tiene lider");

            return RepositoryFactory.GetAlumnoRepository().GetAlumnoNoFK(LiderAlumnoGrupo.AlumnoId);
        }

        public List<BEAlumno> GetAlumnosGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var AlumnosGrupo = from ag in ePortafolioDAO.AlumnosGrupos
                               where ag.GrupoId == GrupoId
                               select RepositoryFactory.GetAlumnoRepository().GetAlumnoNoFK(ag.AlumnoId);

            return AlumnosGrupo.ToList();
        }

        public List<BEAlumno> GetAlumnosSeccionCurso(String SeccionId, int CursoId, String PeriodoId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var AlumnosCurso = from a in pyrIntegradoDBDataContext.ePSE_AlumnosCursos
                               where a.CursoId == CursoId && a.SeccionId == SeccionId && a.PeriodoId == PeriodoId
                               select RepositoryFactory.GetAlumnoRepository().GetAlumnoNoFK(a.AlumnoId);

            return AlumnosCurso.ToList();
        }

        public List<BEAlumno> GetAlumnosSinGrupoSeccionTrabajo(int TrabajoId, String SeccionId)
        {
            List<BEAlumno> AlumnosSinGrupo = new List<BEAlumno>();

            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajoNoFK(TrabajoId);

            List<BEAlumno> AlumnosSeccionCurso = RepositoryFactory.GetAlumnoRepository().GetAlumnosSeccionCurso(SeccionId,Trabajo.Curso.CursoId,Trabajo.Periodo.PeriodoId);

            foreach (BEAlumno Alumno in AlumnosSeccionCurso)
            {
                BEGrupo Grupo = RepositoryFactory.GetGrupoRepository().GetGrupoAlumno(TrabajoId, Alumno.AlumnoId);
                if (Grupo == null)
                    AlumnosSinGrupo.Add(Alumno);
            }

            return AlumnosSinGrupo;
        }
    }
}
