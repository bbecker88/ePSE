using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IResultadoRubricaRepository
    {
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEResultadoRubrica> GetResultadosRubricaGrupo(int GrupoId);
        void SaveResultadoRubrica(ePortafolioMVC.Models.Entities.BEResultadoRubrica ResultadoRubrica);
    }
}
