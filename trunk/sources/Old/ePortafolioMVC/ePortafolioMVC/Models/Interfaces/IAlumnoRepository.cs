using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IAlumnoRepository
    {
        ePortafolioMVC.Models.Entities.BEAlumno GetAlumno(string AlumnoId);
        ePortafolioMVC.Models.Entities.BEAlumno GetAlumnoNoFK(string AlumnoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEAlumno> GetAlumnosGrupo(int GrupoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEAlumno> GetAlumnosSeccionCurso(string SeccionId, int CursoId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEAlumno> GetAlumnosSinGrupoSeccionTrabajo(int TrabajoId, string SeccionId);
        ePortafolioMVC.Models.Entities.BEAlumno GetLiderGrupo(int GrupoId);
    }
}
