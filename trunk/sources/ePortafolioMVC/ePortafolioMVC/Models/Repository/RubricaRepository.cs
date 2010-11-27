using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class RubricaRepository : ePortafolioMVC.Models.Repository.IRubricaRepository
    {
        public BERubrica GetRubrica(int RubricaId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Rubrica = ePortafolioDAO.Rubricas.SingleOrDefault(r => r.RubricaId == RubricaId);

            if (Rubrica != null)
            {
                return new BERubrica
                    {
                        RubricaId = Rubrica.RubricaId,
                        Nombre = Rubrica.Nombre
                    };
            }
            return null;
        }

        public List<BERubrica> GetRubricasTrabajo(int TrabajoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var RubricasTrabajo = from r in ePortafolioDAO.RubricasTrabajos
                                  where r.TrabajoId==TrabajoId
                                  select new BERubrica
                                    {
                                        RubricaId = r.Rubrica.RubricaId,
                                        Nombre = r.Rubrica.Nombre
                                    };

            return RubricasTrabajo.ToList();
        }

    }
}
