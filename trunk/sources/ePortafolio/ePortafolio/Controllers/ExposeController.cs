using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.Models;
using ePortafolio.Helpers;
using System.Transactions;
using ePortafolio.Models.ePortafolio;

namespace ePortafolio.Controllers
{
    public class ExposeController : BaseController
    {
        //
        // GET: /Expose/


        public ActionResult EditarEvaluacionGrupo(String GUID, String result, String evaluacionId)
        {
            return null;
        }

        public ActionResult FinalizarEvaluacion(String GUID, String result, String evaluacionId)
        {

            var TipoEvaluacion = Session["Tipo_Evaluacion_" + GUID];

            if (TipoEvaluacion == null)
            {
                PostMessage("No tiene permisos para acceder a esta acción.", MessageType.Error);
            }
            else if (TipoEvaluacion.Equals("GRUPO"))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var doubleResult = 0.0;
                        var GrupoId = Session["Grupo_" + GUID].ToInteger();

                        Double.TryParse(result, out doubleResult);

                        var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
                        Grupo.Nota = doubleResult.ToString("F2");
                        Grupo.EvaluacionId = evaluacionId.ToInteger();
                        ePortafolioRepositoryFactory.GetGruposRepository().Update(Grupo);

                        var AlumnosGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => x.GrupoId == Grupo.GrupoId);

                        foreach (var AlumnoGrupo in AlumnosGrupo)
                        {
                            AlumnoGrupo.Nota = Grupo.Nota;
                            AlumnoGrupo.EvaluacionId = evaluacionId.ToInteger();
                        }

                        ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().Update(AlumnosGrupo);

                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();

                        PostMessage(String.Format("El grupo ha sido evaluado exitosamente. La nota registara es {0}.", doubleResult.ToString("F2")), MessageType.Success);

                        if(Grupo.ExtraTrabajo.EsGrupal)
                            return RedirectToAction("EditarEvaluacionGrupo", "Profesor", new { GrupoId = Grupo.GrupoId});
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
                else if (TipoEvaluacion.Equals("MIEMBRO_GRUPO"))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var doubleResult = 0.0;
                        var GrupoId = Session["Grupo_" + GUID].ToInteger();
                        var AlumnoId = Session["Alumno_" + GUID].ToString();

                        Double.TryParse(result, out doubleResult);

                        var AlumnoGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetOne(AlumnoId,GrupoId);
                        AlumnoGrupo.Nota = doubleResult.ToString("F2");
                        AlumnoGrupo.EvaluacionId = evaluacionId.ToInteger();
                        ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().Update(AlumnoGrupo);

                        var AlumnosGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => x.GrupoId == GrupoId);

                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();

                        PostMessage(String.Format("El alumno ha sido evaluado exitosamente. La nota registara es {0}.", doubleResult.ToString("F2")), MessageType.Success);

                        return RedirectToAction("EditarEvaluacionGrupo", "Profesor", new { GrupoId = GrupoId});
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
            else if (TipoEvaluacion.Equals("LOGRO"))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var doubleResult = 0.0;
                        var OutcomeId = Session["Outcome_" + GUID].ToInteger();
                        var AlumnoId = Session["Alumno_" + GUID].ToString();
                        var ProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
                        var PeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();

                        Double.TryParse(result, out doubleResult);

                        var EvaluacionProfesor = ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().GetOne(AlumnoId, OutcomeId, PeriodoId, ProfesorId);
                        EvaluacionProfesor.Nota = doubleResult.ToString("F2");
                        EvaluacionProfesor.EvaluacionId = evaluacionId.ToInteger();
                        ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().Update(EvaluacionProfesor);

                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();
                        PostMessage(String.Format("El outcome ha sido evaluado exitosamente. La nota registara es {0}.", doubleResult.ToString("F2")), MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
            return View();
        }

    }
}
