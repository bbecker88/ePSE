using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IRubricaRepository
    {
        ePortafolioMVC.Models.Entities.BERubrica GetRubrica(int RubricaId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BERubrica> GetRubricasTrabajo(int TrabajoId);
    }
}
