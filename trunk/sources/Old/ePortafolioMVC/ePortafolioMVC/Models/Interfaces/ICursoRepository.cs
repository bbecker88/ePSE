using System;
namespace ePortafolioMVC.Models.Repository
{
    interface ICursoRepository
    {
        ePortafolioMVC.Models.Entities.BECurso GetCurso(int CursoId, string PeriodoId);
        ePortafolioMVC.Models.Entities.BECurso GetCursoNoFK(int CursoId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BECurso> GetCursosDictados(string ProfesorId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BECurso> GetCursosMatriculados(string AlumnoId, string PeriodoId);
    }
}
