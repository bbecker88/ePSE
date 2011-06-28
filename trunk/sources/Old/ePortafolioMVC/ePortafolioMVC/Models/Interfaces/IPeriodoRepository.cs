using System;
using ePortafolioMVC.Models.Entities;
using System.Collections.Generic;
namespace ePortafolioMVC.Models.Repository
{
    interface IPeriodoRepository
    {
        ePortafolioMVC.Models.Entities.BEPeriodo GetGetPeriodoActual();
        ePortafolioMVC.Models.Entities.BEPeriodo GetGetPeriodoNoFK(string PeriodoId);
        ePortafolioMVC.Models.Entities.BEPeriodo GetPeriodo(string PeriodoId);
        List<BEPeriodo> GetPeriodosEvaluados(String ProfesorId);
        List<BEPeriodo> GetPeriodosEstudiados(String AlumnoId);
    }
}
