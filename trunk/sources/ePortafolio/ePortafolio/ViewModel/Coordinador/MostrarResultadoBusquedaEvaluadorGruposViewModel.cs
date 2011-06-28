using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.SSIA.Entities;

namespace ePortafolio.ViewModel
{
    public class MostrarResultadoBusquedaEvaluadorGruposViewModel
    {
        public List<CursosPeriodosBE> Cursos { get; set; }

        public MostrarResultadoBusquedaEvaluadorGruposViewModel(String PeriodoId, String Filtro)
        {
            Cursos = SSIARepositoryFactory.GetCursosPeriodosRepository().GetWhere(x => x.PeriodoId == PeriodoId &&(x.CodigoCurso.Contains(Filtro) || x.NombreCurso.Contains(Filtro)) );
        }
    }
}
