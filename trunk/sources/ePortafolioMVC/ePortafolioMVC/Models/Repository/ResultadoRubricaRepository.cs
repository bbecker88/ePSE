using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class ResultadoRubricaRepository : ePortafolioMVC.Models.Repository.IResultadoRubricaRepository 
    {
        public void SaveResultadoRubrica(BEResultadoRubrica ResultadoRubrica)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var OldResultadoRubrica = ePortafolioDAO.ResultadosRubricaGrupos.SingleOrDefault(r => r.GrupoId == ResultadoRubrica.GrupoId && r.RubricaId == ResultadoRubrica.RubricaId);

            if (OldResultadoRubrica == null)
            {
                var ResultadosRubricaGrupo = new ResultadosRubricaGrupo();

                ResultadosRubricaGrupo.CriterioId = ResultadoRubrica.CriterioId;
                ResultadosRubricaGrupo.GrupoId = ResultadoRubrica.GrupoId;
                ResultadosRubricaGrupo.RubricaId = ResultadoRubrica.RubricaId;

                ePortafolioDAO.ResultadosRubricaGrupos.InsertOnSubmit(ResultadosRubricaGrupo);
            }
            else
            {
                OldResultadoRubrica.CriterioId = ResultadoRubrica.CriterioId;
            }

            ePortafolioDAO.SubmitChanges();
        }

        public List<BEResultadoRubrica> GetResultadosRubricaGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var ResultadosRubricaGrupo = from r in ePortafolioDAO.ResultadosRubricaGrupos
                                         where r.GrupoId == GrupoId
                                         select new BEResultadoRubrica
                                            {
                                                CriterioId = r.CriterioId,
                                                GrupoId = r.GrupoId,
                                                RubricaId = r.RubricaId
                                            };

            return ResultadosRubricaGrupo.ToList();
        }
    }
}
