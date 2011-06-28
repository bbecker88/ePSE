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
                    Rubrica = new BERubrica { RubricaId = Criterio.RubricaId }
                };
            }

            return null;
        }

        public List<BECriterio> GetCiteriosRubrica(int RubricaId)
        {
            List<BECriterio> Criterios = new List<BECriterio>();

            if (RubricaId == 1)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 1,
                    Rubrica = new BERubrica() { RubricaId = 1 },
                    Nombre = "Insuficiente: Los requerimientos no funcionales no son abordados o sustentados en la arquitectura propuesta.",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 2,
                    Rubrica = new BERubrica() { RubricaId = 1 },
                    Nombre = "Necesita Mejorar: Existen requerimientos no funcionales no cubiertos por la arquitectura o no se sustenta su cobertura.",
                    Valor = (Decimal)1.5
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 3,
                    Rubrica = new BERubrica() { RubricaId = 1 },
                    Nombre = "Esperado: Existe evidencia directa de las decisiones arquitecturales y su correspondencia con los requerimientos no funcionales establecidos.",
                    Valor = 3
                });
            }
            //Fin Rubrica

            if (RubricaId == 2)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 4,
                    Rubrica = new BERubrica() { RubricaId = 2 },
                    Nombre = "Insuficiente: Los requerimientos funcionales no son abordados o sustentados en la arquitectura propuesta.",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 5,
                    Rubrica = new BERubrica() { RubricaId = 2 },
                    Nombre = "Necesita Mejorar: Existen requerimientos funcionales no cubiertos por la arquitectura o no se sustenta su cobertura. Los requerimientos funcionales no se ven reflejados en la organización de la solución en subsistemas funcionales.",
                    Valor = 1
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 6,
                    Rubrica = new BERubrica() { RubricaId = 2 },
                    Nombre = "Esperado: Existe evidencia directa de la relación entre los requerimientos funcionales y la organización de los componentes arquitecturales en subsistemas funcionales (clara definición de servicios de apoyo a los requerimientos funcionales).",
                    Valor = 2
                });
            }
            //Fin Rubrica


            if (RubricaId == 3)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 7,
                    Rubrica = new BERubrica() { RubricaId = 3 },
                    Nombre = "Insuficiente: No hay estilo arquitectural aplicado a la solución o la solución es ad-hoc cuando se pudo aprovechar estilos existentes para resolver problemas similares (de acuerdo con los requerimientos del producto).",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 8,
                    Rubrica = new BERubrica() { RubricaId = 3 },
                    Nombre = "Necesita Mejorar: Se aplica un estilo arquitectural pero la decisión de su aplicación no es explícita.",
                    Valor = 2
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 9,
                    Rubrica = new BERubrica() { RubricaId = 3 },
                    Nombre = "Esperado: Al menos es aplicado un estilo arquitectural (de preferencia el estilo en capas) y este es sustentado respecto de los requerimientos del producto.",
                    Valor = 4
                });
            }
            //Fin Rubrica


            if (RubricaId == 4)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 10,
                    Rubrica = new BERubrica() { RubricaId = 4 },
                    Nombre = "Insuficiente: No se evidencia una relación clara de los componentes arquitecturales propuestos o simplemente no se definen componentes arquitecturales.",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 11,
                    Rubrica = new BERubrica() { RubricaId = 4 },
                    Nombre = "Necesita Mejorar: Aunque se definen los componentes de la arquitectura no es clara su relación respecto de las categorías que aparecen en el/los estilo(s) arquitectural(es) seleccionado(s).",
                    Valor = 1
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 12,
                    Rubrica = new BERubrica() { RubricaId = 4 },
                    Nombre = "Esperado: La definición de los componentes arquitecturales es apropiada y se corresponde con los requerimientos del producto: componentes de presentación, lógica de negocio, base de datos, etc. Estos componentes están perfectamente identificados dentro de las categorías establecidas en el/los estilo(s) arquitectural(es) seleccionado(s). Los componentes satisfacen los requerimientos definidos.",
                    Valor = 2
                });
            }
            //Fin Rubrica


            if (RubricaId == 5)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 13,
                    Rubrica = new BERubrica() { RubricaId = 5 },
                    Nombre = "Insuficiente: Los diagramas empleados no satisfacen estándares de la industria.",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 14,
                    Rubrica = new BERubrica() { RubricaId = 5 },
                    Nombre = "Necesita Mejorar: No es sistemático el empleo de estándares de la industria tales como UML e IDEF1x, entre otros.",
                    Valor = (Decimal)0.5
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 15,
                    Rubrica = new BERubrica() { RubricaId = 5 },
                    Nombre = "Esperado: Se emplean estándares de la industria aplicables al nivel arquitectural (UML, EIP, entre otros).",
                    Valor = 1
                });
            }

            if (RubricaId == 6)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 16,
                    Rubrica = new BERubrica() { RubricaId = 6 },
                    Nombre = "Insuficiente: Los diagramas empleados no satisfacen estándares de la industria.",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 17,
                    Rubrica = new BERubrica() { RubricaId = 6 },
                    Nombre = "Necesita Mejorar: No es sistemático el empleo de estándares de la industria tales como UML e IDEF1x, entre otros.",
                    Valor = (Decimal)1.5
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 18,
                    Rubrica = new BERubrica() { RubricaId = 6 },
                    Nombre = "Esperado: Los diagramas presentados sobre el diseño de los componentes se ajustan completamente a la notación UML e IDEF1x, entre otros.",
                    Valor = 3
                });
            }

            if (RubricaId == 7)
            {
                Criterios.Add(new BECriterio
                {
                    CriterioId = 19,
                    Rubrica = new BERubrica() { RubricaId = 7 },
                    Nombre = "Insuficiente: No se emplean patrones de diseño en la solución.",
                    Valor = 0
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 20,
                    Rubrica = new BERubrica() { RubricaId = 7 },
                    Nombre = "Necesita Mejorar: Se aplican patrones de diseño aunque es evidente que ellos no tienen un sólido sustento en los requerimientos definidos (se han identificado al menos 3 patrones de diseño diferentes)",
                    Valor = (Decimal)2.5
                });
                Criterios.Add(new BECriterio
                {
                    CriterioId = 21,
                    Rubrica = new BERubrica() { RubricaId = 7 },
                    Nombre = "Esperado: Se evidencian varios patrones de diseño GoF aplicados al diseño detallado y son los apropiados de acuerdo con los requerimientos (se han identificado al menos 5 patrones de diseño diferentes)",
                    Valor = 5
                });
            }

            return Criterios;
        }
    }
}
