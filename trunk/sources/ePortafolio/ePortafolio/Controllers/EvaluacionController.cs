using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.Logic;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA;
using ePortafolio.Helpers;

namespace ePortafolio.Controllers
{
    public class EvaluacionController : BaseController
    {
        //
        // GET: /Evaluacion/

        public ActionResult VerRubricaTrabajo(int TrabajoId)
        {
            var Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);
            var Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Trabajo.CursoId);

            var RubricOnLogic = new RubricOnLogic();

            try
            {
                var RubricaId = Curso.Codigo + "-" + Trabajo.Codigo;
                var TipoArtefacto = "TRABAJO";

                var Ruta = RubricOnLogic.GetVerRubricaUrl(RubricaId, TipoArtefacto,"", true);
                return Redirect(Ruta);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult VerRubricaOutcome(int OutcomeId)
        {
            var Outcome = SSIARepositoryFactory.GetOutcomesRepository().GetOne(OutcomeId);

            var RubricOnLogic = new RubricOnLogic();

            try
            {
                var RubricaId = Outcome.Outcome;
                var TipoArtefacto = "LOGRO";

                var Ruta = RubricOnLogic.GetVerRubricaUrl(RubricaId, TipoArtefacto, "", true);
                return Redirect(Ruta);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult VerEvaluacion(int EvaluacionId, int? GrupoRetornoId)
        {
            var RubricOnLogic = new RubricOnLogic();
            var RutaRetorno = "";

            if (GrupoRetornoId.HasValue)
                RutaRetorno = Url.Action("EditarEvaluacionGrupo", "Profesor", new { GrupoId = GrupoRetornoId.Value },"http");

            try
            {
                var Ruta = RubricOnLogic.GetRutaVerRubricaEvaluadaUrl(EvaluacionId, RutaRetorno, true);
                return Redirect(Ruta);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult EvaluarGrupo(int GrupoId, int? GrupoRetornoId)
        {
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
            var Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Grupo.ExtraTrabajo.CursoId);
            var ProfesorId = Session.Get(GlobalKey.UsuarioId);

            var RubricOnLogic = new RubricOnLogic();

            var RutaCancelado = "";

            if (GrupoRetornoId.HasValue)
                RutaCancelado = Url.Action("EditarEvaluacionGrupo", "Profesor", new { GrupoId = GrupoRetornoId.Value }, "http");

            try
            {
                var RubricaId = Curso.Codigo + "-" + Grupo.ExtraTrabajo.Codigo;
                var TipoArtefacto = "TRABAJO";
                var Evaluado = String.Format("Grupo {0}: {1}", Grupo.GrupoId, Grupo.NombreTrabajo);
                var Evaluador = ProfesorId.ToString();
                var GUID = Guid.NewGuid().ToString();

                Session["Tipo_Evaluacion_" + GUID] = "GRUPO";
                Session["Grupo_" + GUID] = Grupo.GrupoId;

                var Ruta = RubricOnLogic.GetRutaEvaluarRubricaUrl(RubricaId, TipoArtefacto, Evaluado, Evaluador, GUID,Grupo.EvaluacionId,RutaCancelado, true);
                return Redirect(Ruta);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult EvaluarOutcomeAlumno(String AlumnoId, int OutcomeId)
        {
            var Outcome = SSIARepositoryFactory.GetOutcomesRepository().GetOne(OutcomeId);
            var ProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
            var PeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();
            var EvaluacionesOutcomeProfesor = ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().GetOne(AlumnoId, OutcomeId, PeriodoId, ProfesorId);

            var RubricOnLogic = new RubricOnLogic();

            try
            {
                var RubricaId = Outcome.Outcome;
                var TipoArtefacto = "LOGRO";

                var Evaluado = AlumnoId;
                var Evaluador = ProfesorId.ToString();
                var GUID = Guid.NewGuid().ToString();

                Session["Tipo_Evaluacion_" + GUID] = "LOGRO";
                Session["Outcome_" + GUID] = OutcomeId;
                Session["Alumno_" + GUID] = AlumnoId;

                var Ruta = RubricOnLogic.GetRutaEvaluarRubricaUrl(RubricaId, TipoArtefacto, Evaluado, Evaluador, GUID,EvaluacionesOutcomeProfesor.EvaluacionId,"", true);
                return Redirect(Ruta);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult EvaluarAlumnoGrupo(String AlumnoId, int GrupoId, int? GrupoRetornoId)
        {
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
            var Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Grupo.ExtraTrabajo.CursoId);
            var AlumnoGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetOne(AlumnoId,GrupoId);
            var ProfesorId = Session.Get(GlobalKey.UsuarioId).ToInteger();

            var RubricOnLogic = new RubricOnLogic();

            var RutaCancelado = "";

            if (GrupoRetornoId.HasValue)
                RutaCancelado = Url.Action("EditarEvaluacionGrupo", "Profesor", new { GrupoId = GrupoRetornoId.Value }, "http");

            try
            {
                var RubricaId = Curso.Codigo + "-" + Grupo.ExtraTrabajo.Codigo;
                var TipoArtefacto = "TRABAJO";
                var Evaluado = AlumnoId;
                var Evaluador = ProfesorId.ToString();
                var GUID = Guid.NewGuid().ToString();

                Session["Tipo_Evaluacion_" + GUID] = "MIEMBRO_GRUPO";
                Session["Grupo_" + GUID] = GrupoId;
                Session["Alumno_" + GUID] = AlumnoId;

                var Ruta = RubricOnLogic.GetRutaEvaluarRubricaUrl(RubricaId, TipoArtefacto, Evaluado, Evaluador, GUID,AlumnoGrupo.EvaluacionId,RutaCancelado,true);
                return Redirect(Ruta);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
