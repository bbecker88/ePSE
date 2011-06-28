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
            BERubrica Rubrica;
            List<BERubrica> Rubricas = new List<BERubrica>();

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 1;
            Rubrica.Nombre = "Satisface los requerimientos no funcionales";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 2;
            Rubrica.Nombre = "Satisface los requerimientos funcionales";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 3;
            Rubrica.Nombre = "Uso de patrones o estilos arquitecturales";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 4;
            Rubrica.Nombre = "Componentes Arquitecturales";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 5;
            Rubrica.Nombre = "Uso de estándares de la industria (UML)";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 6;
            Rubrica.Nombre = "C3.1.1 Uso de estándares de la industria";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            Rubrica = new BERubrica();
            Rubrica.RubricaId = 7;
            Rubrica.Nombre = "Uso de patrones de diseño (GoF u otros)";
            Rubricas.Add(Rubrica);
            //Fin Rubrica

            
            return Rubricas;
        }

    }
}
