using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;
using System.Web.Mvc;
using ePortafolio.Helpers;
using ePortafolio.Logic;

namespace ePortafolio.ViewModel
{
    public class MostrarDetalleOutcomeEstudianteViewModel
    {
        public List<TrabajosOutcomeAlumnoBE> TrabajosOutcome { get; set; }
        public List<TrabajosBE> TrabajosDisponibles { get; set; }
        public List<TrabajosBE> TrabajosEntregados { get; set; }
        public List<CursosBE> CursosTrabajos { get; set; }
        public List<GroupDropListItem> GroupDropListItemList { get; set; }
        public OutcomesBE Outcome { get; set; }

        public EvaluacionesOutcomeProfesorBE EvaluacionesOutcomeProfesor { get; set; }

        public MostrarDetalleOutcomeEstudianteViewModel(int OutcomeId, String AlumnoId)
        {
            TrabajosEntregados = ePortafolioRepositoryFactory.GetTrabajosRepository().GetTrabajosEntregados(AlumnoId);
            var TrabajosEntregadosId = TrabajosEntregados.Select(x => x.TrabajoId);
            var CursosTrabajosEntregadosId = TrabajosEntregados.Select(x => x.CursoId);

            TrabajosOutcome = ePortafolioRepositoryFactory.GetTrabajosOutcomeAlumnoRepository().GetWhere(x => x.AlumnoId == AlumnoId && x.OutcomeId == OutcomeId && TrabajosEntregadosId.Contains(x.TrabajoId));
            Outcome = SSIARepositoryFactory.GetOutcomesRepository().GetOne(OutcomeId);

            var TrabajosOutcomeId = TrabajosOutcome.Select(x => x.TrabajoId);

            CursosTrabajos = SSIARepositoryFactory.GetCursosRepository().GetWhere(x => CursosTrabajosEntregadosId.Contains(x.CursoId));
            CursosTrabajos.Add(new CursosBE() { Codigo = "TRABAJOS INDEPENDIENTES", CursoId = 0, Nombre = "" });

            EvaluacionesOutcomeProfesor = ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().GetWhere(x => x.AlumnoId == AlumnoId && x.OutcomeId == OutcomeId)
                                                                                                                 .OrderByDescending(x => x.PeriodoId)
                                                                                                                 .FirstOrDefault();

            TrabajosDisponibles = TrabajosEntregados.Where(x => !TrabajosOutcomeId.Contains(x.TrabajoId)).ToList();

            GroupDropListItemList = new List<GroupDropListItem>();

            foreach (var curso in CursosTrabajos)
            {
                var TrabajosDisponiblesCurso = TrabajosDisponibles.Where(x => x.CursoId == curso.CursoId).ToList();

                if (TrabajosDisponiblesCurso.Count == 0)
                    continue;

                var GroupDropListItem = new GroupDropListItem();
                GroupDropListItem.Name = curso.Codigo + " " + curso.Nombre;
                GroupDropListItem.Items = TrabajosDisponiblesCurso.Select(x => new OptionItem() { Text = x.Nombre, Value = x.TrabajoId.ToString() }).ToList();

                GroupDropListItemList.Add(GroupDropListItem);
            }

        }

    }
}

