using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.Models.SSIA;
using ePortafolio.ViewModel;
using ePortafolio.Helpers;
using ePortafolio.Models;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.ePortafolio.Entities;
using System.Transactions;
using ePortafolio.ViewModel.Coordinador;
using ePortafolio.Logic;
using ePortafolio.Models.SSIA.Entities;

namespace ePortafolio.Controllers
{
    public class CoordinadorController : BaseController
    {
        //
        // GET: /Coordinador/

        public ActionResult EditarEvaluadorGrupos()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            return View();
        }

        public ActionResult MostrarEvaluacionesOutcomes(String AlumnoId, String ProfesorId, String PeriodoId, Int32? OutcomeId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var MostrarEvaluacionesOutcomesViewModel = new MostrarEvaluacionesOutcomesViewModel(AlumnoId, ProfesorId, PeriodoId, OutcomeId);

            return View(MostrarEvaluacionesOutcomesViewModel);
        }

        public ActionResult MostrarEvaluacionesOutcomesFiltradas(String AlumnoId, String ProfesorId, String PeriodoId, Int32? OutcomeId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var MostrarEvaluacionesOutcomesFiltradasViewModel = new MostrarEvaluacionesOutcomesFiltradasViewModel(AlumnoId, ProfesorId, PeriodoId, OutcomeId);

            return View(MostrarEvaluacionesOutcomesFiltradasViewModel);
        }

        public ActionResult AgregarEvaluacionesOutcomes()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualPeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();
            var AgregarEvaluacionesOutcomesViewModel = new AgregarEvaluacionesOutcomesViewModel(ActualPeriodoId);

            return View(AgregarEvaluacionesOutcomesViewModel);
        }

        public ActionResult MostrarDetalleSuperUser()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            return View();
        }

        [HttpPost]
        public ActionResult MostrarDetalleSuperUser(String UsuarioId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            Roles RolUsuario;

            var PeriodoActual = SSIARepositoryFactory.GetPeriodosRepository().GetWhere(x => x.EsActual == true);

            if (PeriodoActual.Count != 1)
            {
                PostMessage("No se puedo determinar el Período actual. No se puede continuar.", MessageType.Error);
                return View();
            }
            var PeriodoActualId = PeriodoActual.First().PeriodoId;

            var AlumnoBE = SSIARepositoryFactory.GetAlumnosRepository().GetOne(UsuarioId);
            var ProfesorBE = SSIARepositoryFactory.GetProfesoresRepository().GetOne(UsuarioId);
            var CoordinadorBE = SSIARepositoryFactory.GetCoordinadoresRepository().GetOne(UsuarioId);

            if (AlumnoBE != null)
                RolUsuario = Roles.Estudiante;
            else if (ProfesorBE != null)
                RolUsuario = Roles.Profesor;
            else if (CoordinadorBE != null)
                RolUsuario = Roles.Coordinador;
            else
            {
                PostMessage("No se puedo determinar el rol del usuario. No se puede continuar.", MessageType.Error);
                return View();
            }

            var Periodos = new List<PeriodosBE>();
            var Nombre = "";

            switch (RolUsuario)
            {
                case Roles.Estudiante:
                    var PeriodosId = ePortafolioRepositoryFactory.GetTrabajosRepository().GetTrabajosEntregados(UsuarioId).Select(x => x.PeriodoId).Distinct();
                    Periodos = SSIARepositoryFactory.GetPeriodosRepository().GetWhere(x => PeriodosId.Contains(x.PeriodoId));
                    Periodos = Periodos.OrderByDescending(x => x.PeriodoId).ToList();
                    Nombre = AlumnoBE.Nombre;
                    break;

                case Roles.Profesor:
                    Periodos = SSIARepositoryFactory.GetPeriodosRepository().PeriodosDictados(UsuarioId);
                    Nombre = ProfesorBE.Nombre;
                    break;

                case Roles.Coordinador:
                    Periodos = new List<PeriodosBE>();
                    Nombre = CoordinadorBE.Nombre;
                    break;
            }

            if (!Periodos.Any(x => x.PeriodoId == PeriodoActualId))
            {
                Periodos.Add(PeriodoActual.First());
                Periodos = Periodos.OrderByDescending(x => x.PeriodoId).ToList();
            }

            Session.Set(GlobalKey.ActualPeriodoId, PeriodoActualId);
            Session.Set(GlobalKey.PeriodosMatriculados, Periodos);
            Session.Set(GlobalKey.Rol, RolUsuario);
            Session.Set(GlobalKey.UsuarioId, UsuarioId);
            Session.Set(GlobalKey.Nombre, Nombre);

            return View();
        }

        public ActionResult ReestablecerControlCoordinador()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var CoordinadorId = Session.Get(GlobalKey.CoordinadorId).ToString();

            var CoordinadorBE = SSIARepositoryFactory.GetCoordinadoresRepository().GetOne(CoordinadorId);

            Session.Set(GlobalKey.PeriodosMatriculados, new List<PeriodosBE>());
            Session.Set(GlobalKey.Rol, Roles.Coordinador);
            Session.Set(GlobalKey.UsuarioId, CoordinadorId);
            Session.Set(GlobalKey.Nombre, CoordinadorBE.Nombre);
            Session.Set(GlobalKey.CoordinadorId, CoordinadorId);
            Session.Set(GlobalKey.ActualPeriodoId, "20082");
            Session.Set(GlobalKey.PeriodosMatriculados, new List<PeriodosBE>(){ new PeriodosBE(){ EsActual = true, PeriodoId = "20082"}});

            return RedirectToAction("Home","Home");
        }

        [HttpPost]
        public ActionResult AgregarEvaluacionesOutcomes(AgregarEvaluacionesOutcomesViewModel model, FormCollection formCollection)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ValidationLogic = new ValidationLogic(ModelState);
            var AlumnosValidos = new List<String>();
            var AlumnosNuevos = new List<String>();

            ValidationLogic.Valid(model.AlumnosId, "AlumnosId", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(model.OutcomeId, "OutcomeId", ValidationOption.IsNotEmpty | ValidationOption.IsNotZero);
            ValidationLogic.Valid(model.PeriodoId, "PeriodoId", ValidationOption.IsNotEmpty | ValidationOption.IsNotZero);
            ValidationLogic.Valid(model.ProfesorId, "ProfesorId", ValidationOption.IsNotEmpty);

            if (ModelState.IsValid)
            {
                var Outcome = SSIARepositoryFactory.GetOutcomesRepository().GetOne(model.OutcomeId);
                var Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(model.ProfesorId);
                var Periodo = SSIARepositoryFactory.GetPeriodosRepository().GetOne(model.PeriodoId);

                if (Outcome == null)
                    ModelState.AddModelError("OutcomeId", "");
                if (Profesor == null)
                    ModelState.AddModelError("ProfesorId", "");
                if (Periodo == null)
                    ModelState.AddModelError("PeriodoId", "");

                AlumnosNuevos = model.AlumnosId.Split(new char[] { ',', ';', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
                AlumnosValidos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosNuevos.Contains(x.AlumnoId)).Select(x => x.AlumnoId).ToList();

                if (AlumnosValidos.Count != AlumnosNuevos.Count)
                {
                    ModelState.AddModelError("AlumnosId", "");
                    PostMessage("Alguno de los alumnos ingresados no existe.", MessageType.Error);
                }
            }

            if (!ModelState.IsValid)
            {
                if (AlumnosValidos.Count != AlumnosNuevos.Count)
                    PostMessage("Alguno de los alumnos ingresados no existe. Revisar los datos ingresados.", MessageType.Error);
                else
                    PostMessage("Revisar los datos ingresados.", MessageType.Error);

                return View(model);
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var EvaluacionesOutcomeProfesor = new List<EvaluacionesOutcomeProfesorBE>();

                    foreach (var AlumnoNuevo in AlumnosValidos)
                    {
                        EvaluacionesOutcomeProfesor.Add(new EvaluacionesOutcomeProfesorBE()
                                {
                                    AlumnoId = AlumnoNuevo,
                                    ProfesorId = model.ProfesorId,
                                    PeriodoId = model.PeriodoId,
                                    OutcomeId = model.OutcomeId,
                                    Nota = "NE",
                                    EvaluacionId = null
                                });
                    }

                    ePortafolioRepositoryFactory.GetEvaluacionesOutcomeProfesorRepository().InsertOrUpdate(EvaluacionesOutcomeProfesor);

                    ePortafolioRepositoryFactory.SubmitChanges(true);

                    scope.Complete();

                    PostMessage("Los alumnos han sido asociados exitosamente.", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
                return View(model);
            }

            return RedirectToAction("MostrarEvaluacionesOutcomes", new { ProfesorId = model.ProfesorId, PeriodoId = model.PeriodoId, OutcomeId = model.OutcomeId });
        }

        public ActionResult MostrarResultadoBusquedaEvaluadorGrupos(String Filtro)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualPeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();

            var MostrarResultadoBusquedaEvaluadorGruposViewModel = new MostrarResultadoBusquedaEvaluadorGruposViewModel(ActualPeriodoId, Filtro);
            return View(MostrarResultadoBusquedaEvaluadorGruposViewModel);
        }

        public ActionResult MostrarTrabajosCursoEvaluadorGrupos(int CursoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualPeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();

            var MostrarTrabajosCursoEvaluadorGrupos = new MostrarTrabajosCursoEvaluadorGrupos(ActualPeriodoId, CursoId);
            return View(MostrarTrabajosCursoEvaluadorGrupos);
        }

        public ActionResult MostrarGruposCursoEvaluadorGrupos(int? TrabajoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            if (!TrabajoId.HasValue)
            {
                PostMessage("Debe seleccionar un trabajo.", MessageType.Info);
                return View("Empty");
            }
            var MostrarGruposCursoEvaluadorGruposViewModel = new MostrarGruposCursoEvaluadorGruposViewModel(TrabajoId.Value);
            return View(MostrarGruposCursoEvaluadorGruposViewModel);
        }

        [HttpPost]
        public ActionResult MostrarGruposCursoEvaluadorGrupos(MostrarGruposCursoEvaluadorGruposViewModel model, FormCollection formCollection)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var EvaluacionesGruposProfesor = new List<EvaluacionesGruposProfesorBE>();

            foreach (var key in formCollection.Keys)
            {
                var keyStr = key.ToString();

                if (!keyStr.StartsWith("EVAL_"))
                    continue;

                var GrupoId = keyStr.Substring("EVAL_".Length);
                var ProfesorId = formCollection[keyStr];

                if (ProfesorId == null || ProfesorId == "")
                    continue;

                var Profesor = SSIARepositoryFactory.GetProfesoresRepository().GetOne(ProfesorId);

                if (Profesor == null)
                {
                    PostMessage("No se encontro al profesor. Revisar los datos.", MessageType.Error);
                    ModelState.AddModelError(keyStr, "");
                    continue;
                }

                var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId.ToInteger());

                if (Grupo == null)
                {
                    PostMessage("No se encontro el grupo. Revisar los datos.", MessageType.Error);
                    ModelState.AddModelError(keyStr, "");
                    continue;
                }

                EvaluacionesGruposProfesor.Add(new EvaluacionesGruposProfesorBE() { GrupoId = GrupoId.ToInteger(), ProfesorId = ProfesorId });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var GruposTrabajoId = ePortafolioRepositoryFactory.GetGruposRepository().GetWhere(x => x.TrabajoId == model.TrabajoId).Select(x => x.GrupoId);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().DeleteWhere(x => GruposTrabajoId.Contains(x.GrupoId));
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().InsertOrUpdate(EvaluacionesGruposProfesor);
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();
                        PostMessage("Los evaluadores han sido asociados exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            var MostrarGruposCursoEvaluadorGruposViewModel = new MostrarGruposCursoEvaluadorGruposViewModel(model.TrabajoId);

            MostrarGruposCursoEvaluadorGruposViewModel.EvaluacionesGruposProfesor.Clear();
            foreach (var key in formCollection.Keys)
            {
                var keyStr = key.ToString();

                if (!keyStr.StartsWith("EVAL_"))
                    continue;

                var GrupoId = keyStr.Substring("EVAL_".Length);
                var ProfesorId = formCollection[keyStr];

                MostrarGruposCursoEvaluadorGruposViewModel.EvaluacionesGruposProfesor.Add(new EvaluacionesGruposProfesorBE() { GrupoId = GrupoId.ToInteger(), ProfesorId = ProfesorId });
            }

            return PartialView(MostrarGruposCursoEvaluadorGruposViewModel);
        }
    }
}
