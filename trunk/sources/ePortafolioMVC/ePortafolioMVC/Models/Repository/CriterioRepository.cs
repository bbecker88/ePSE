using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class CriterioRepository : ePortafolioMVC.Models.Repository.ICriterioRepository 
    {
        public BECriterio GetCriterio(int CriterioId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Criterio = ePortafolioDAO.CriteriosRubricas.SingleOrDefault(c => c.CriterioId == CriterioId);

            if (Criterio != null)
            {
                return new BECriterio
                {
                    CriterioId = Criterio.CriterioId,
                    Nombre = Criterio.Nombre,
                    Valor = Criterio.Valor,
                    Rubrica = RepositoryFactory.GetRubricaRepository().GetRubrica(Criterio.Rubrica.RubricaId)
                };
            }

            return null;
        }

        public BECriterio GetCriterioNoFK(int CriterioId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Criterio = ePortafolioDAO.CriteriosRubricas.SingleOrDefault(c => c.CriterioId == CriterioId);

            if (Criterio != null)
            {
                return new BECriterio
                {
                    CriterioId = Criterio.CriterioId,
                    Nombre = Criterio.Nombre,
                    Valor = Criterio.Valor,
                    Rubrica = new BERubrica { RubricaId = Criterio.RubricaId}
                };
            }

            return null;
        }

        public List<BECriterio> GetCiteriosRubrica(int RubricaId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Rubrica = RepositoryFactory.GetRubricaRepository().GetRubrica(RubricaId);

            var CiteriosRubrica = from c in ePortafolioDAO.CriteriosRubricas
                                  where c.RubricaId == RubricaId
                                  select RepositoryFactory.GetCriterioRubricaRepository().GetCriterioNoFK(c.CriterioId);

            return CiteriosRubrica.ToList();
        }
    }
}
