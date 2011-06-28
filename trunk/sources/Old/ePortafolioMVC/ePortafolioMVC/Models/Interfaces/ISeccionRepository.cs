using System;
namespace ePortafolioMVC.Models.Repository
{
    interface ISeccionRepository
    {
        ePortafolioMVC.Models.Entities.BESeccion GetSeccion(string SeccionId);
        ePortafolioMVC.Models.Entities.BESeccion GetSeccionCursoAlumno(string AlumnoId, int CursoId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BESeccion> GetSeccionesCurso(int CursoId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BESeccion> GetSeccionesDictado(int CursoId, string ProfesorId, string PeriodoId);
        ePortafolioMVC.Models.Entities.BESeccion GetSeccionNoFK(string SeccionId);
    }
}
