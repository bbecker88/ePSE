using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IResultadoProgramaRepository
    {
        ePortafolioMVC.Models.Entities.BEResultadoPrograma GetResultadoPrograma(int CursoId, string PeriodoId);
        ePortafolioMVC.Models.Entities.BEResultadoPrograma GetResultadoProgramaNoFK(int CursoId, string PeriodoId);
    }
}
