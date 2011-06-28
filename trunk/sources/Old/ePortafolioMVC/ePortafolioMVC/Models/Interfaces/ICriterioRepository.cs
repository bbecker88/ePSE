using System;
namespace ePortafolioMVC.Models.Repository
{
    interface ICriterioRepository
    {
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BECriterio> GetCiteriosRubrica(int RubricaId);
        ePortafolioMVC.Models.Entities.BECriterio GetCriterio(int CriterioId);
        ePortafolioMVC.Models.Entities.BECriterio GetCriterioNoFK(int CriterioId);
    }
}
