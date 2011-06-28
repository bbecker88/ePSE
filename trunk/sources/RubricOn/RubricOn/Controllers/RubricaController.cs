using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricOn.Models.RubricOn;
using RubricOn.ViewModel;
using System.Transactions;
using RubricOn.Models;
using RubricOn.Models.RubricOn.Entities;
using RubricOn.Helpers;
using RubricOn.Logic;

namespace RubricOn.Controllers
{
    public class RubricaController : BaseController
    {
        //
        // GET: /Rubrica/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarRubricas()
        {
            var ListarRubricasViewModel = new ListarRubricasViewModel();
            return View(ListarRubricasViewModel);
        }

        public ActionResult ListarRubricasArtefacto(String TipoArtefacto)
        {
            var ListarRubricasArtefactoViewModel = new ListarRubricasArtefactoViewModel(TipoArtefacto);
            return PartialView(ListarRubricasArtefactoViewModel);
        }

        public ActionResult ListarVersionesRubrica(String RubricaId, String TipoArtefacto)
        {
            var ListarVersionesRubricaViewModel = new ListarVersionesRubricaViewModel(RubricaId, TipoArtefacto);
            return View(ListarVersionesRubricaViewModel);
        }

        public ActionResult CrearVersionRubrica(String RubricaId, String TipoArtefacto)
        {
            var CrearVersionRubricaViewModel = new CrearVersionRubricaViewModel(RubricaId, TipoArtefacto);
            return View(CrearVersionRubricaViewModel);
        }

        [HttpPost]
        public ActionResult CrearVersionRubrica(CrearVersionRubricaViewModel Model)
        {
            var ValidationLogic = new ValidationLogic(ModelState);

            Model.Rubrica.FechaCreacion = DateTime.Now;
            Model.Rubrica.EsActual = true;

            ValidationLogic.Valid(Model.Rubrica.Descripcion, "Rubrica.Descripcion", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(Model.Rubrica.RubricaId, "Rubrica.RubricaId", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(Model.Rubrica.TipoArtefacto, "Rubrica.TipoArtefacto", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(Model.Rubrica.TipoRubrica, "Rubrica.TipoRubrica", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(Model.Rubrica.Version, "Rubrica.Version", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(Model.Rubrica.CreadorId, "Rubrica.CreadorId", ValidationOption.IsNotEmpty);

            if (RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetOne(Model.Rubrica.RubricaId, Model.Rubrica.TipoArtefacto, Model.Rubrica.Version) != null)
            {
                ModelState.AddModelError("Rubrica.Version", "");
            }

            if (!ModelState.IsValid)
            {
                PostMessage("Revise los datos ingresados.", MessageType.Error);
                return View(Model);
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var Actuales = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => x.RubricaId == Model.Rubrica.RubricaId && x.TipoArtefacto == Model.Rubrica.TipoArtefacto && x.EsActual == true);
                    foreach (var actual in Actuales)
                        actual.EsActual = false;

                    RubricOnRepositoryFactory.GetVersionesRubricasRepository().Update(Actuales);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    var Rubrica = new RubricasBE() { RubricaId = Model.Rubrica.RubricaId, TipoArtefacto = Model.Rubrica.TipoArtefacto };

                    RubricOnRepositoryFactory.GetRubricasRepository().InsertOrUpdate(Rubrica);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    RubricOnRepositoryFactory.GetVersionesRubricasRepository().InsertOrUpdate(Model.Rubrica);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    scope.Complete();

                    PostMessage("La version ha sido creada exitosamente.", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
            }

            return RedirectToAction("ListarVersionesRubrica", new { RubricaId = Model.Rubrica.RubricaId, TipoArtefacto = Model.Rubrica.TipoArtefacto });

        }

        public ActionResult CambiarVersionActualRubrica(String RubricaId, String Version, String TipoArtefacto)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var Actuales = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.EsActual == true);
                    foreach (var actual in Actuales)
                        actual.EsActual = false;

                    RubricOnRepositoryFactory.GetVersionesRubricasRepository().Update(Actuales);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    var VersionActual = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetOne(RubricaId, TipoArtefacto, Version);
                    VersionActual.EsActual = true;
                    RubricOnRepositoryFactory.GetVersionesRubricasRepository().Update(VersionActual);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    scope.Complete();

                    PostMessage("La version actual ha sido cambiada exitosamente.", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
            }

            return RedirectToAction("ListarVersionesRubrica", new { RubricaId = RubricaId, TipoArtefacto = TipoArtefacto });
        }

        public ActionResult RegistrarRubrica()
        {
            return View();
        }

        public ActionResult DisenarVersionRubrica(String RubricaId, String Version, String TipoArtefacto)
        {
            var DisenarVersionRubricaViewModel = new DisenarVersionRubricaViewModel(RubricaId, Version, TipoArtefacto);
            return View(DisenarVersionRubricaViewModel);
        }

        public ActionResult DisenarVersionSegundaRubrica(String RubricaId, String Version, String TipoArtefacto)
        {
            var DisenarVersionRubricaViewModel = new DisenarVersionRubricaViewModel(RubricaId, Version, TipoArtefacto);
            return View(DisenarVersionRubricaViewModel);
        }

        [HttpPost]
        public ActionResult DisenarVersionRubrica(String RubricaId, String Version, String TipoArtefacto, FormCollection formCollection)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var categoriaItems = formCollection.AllKeys.Where(x => x.StartsWith("categoriaTemplate[categoriaTemplate][")).Select(x => x.Substring("categoriaTemplate[categoriaTemplate][".Length)).ToList();
                    var categorias = categoriaItems.Select(x => Convert.ToInt32(x.Substring(0, x.IndexOf("]")))).Distinct();
                    var ordenCategoria = 1;

                    foreach (var categoria in categorias)
                    {
                        var categoriaText = formCollection[String.Format("categoriaTemplate[categoriaTemplate][{0}][categoriaText]", categoria)];
                        var CategoriasRubricasBE = new CategoriasRubricasBE()
                                {
                                    RubricaId = RubricaId,
                                    TipoArtefacto = TipoArtefacto,
                                    Version = Version,
                                    Orden = ordenCategoria,
                                    Nombre = categoriaText
                                };

                        RubricOnRepositoryFactory.GetCategoriasRubricasRepository().InsertIdentity(CategoriasRubricasBE, true);

                        var aspectosItems = categoriaItems.Where(x => x.StartsWith(categoria + "][aspectoTemplate][")).Select(x => x.Substring((categoria + "][aspectoTemplate][").Length)).ToList();
                        var aspectos = aspectosItems.Select(x => Convert.ToInt32(x.Substring(0, x.IndexOf("]")))).Distinct();
                        var ordenAspecto = 1;

                        foreach (var aspecto in aspectos)
                        {
                            var criteriosItems = aspectosItems.Where(x => x.StartsWith(aspecto + "][criterio")).Select(x => x.Substring((aspecto + "][criterio").Length)).ToList();

                            var AspectosRubricaBE = new AspectosRubricaBE()
                            {
                                CategoriaRubricaId = CategoriasRubricasBE.CategoriaRubricaId,
                                Orden = ordenAspecto,
                                Nombre = "Aspecto " + ordenAspecto,
                            };

                            RubricOnRepositoryFactory.GetAspectosRubricaRepository().InsertIdentity(AspectosRubricaBE, true);

                            for (int i = 0; i < criteriosItems.Count(); i += 2)
                            {
                                var ordenCriterio = i / 2 + 1;
                                var CriterioText = formCollection[String.Format("categoriaTemplate[categoriaTemplate][{0}][aspectoTemplate][{1}][criterio{2}", categoria, aspecto, criteriosItems[i])];
                                var CriterioValor = formCollection[String.Format("categoriaTemplate[categoriaTemplate][{0}][aspectoTemplate][{1}][criterio{2}", categoria, aspecto, criteriosItems[i + 1])];
                                Double doubleValor = 0.0;

                                Double.TryParse(CriterioValor, out doubleValor);

                                var CriterioRubricaBE = new CriterioRubricaBE()
                                {
                                    AspectoRubricaId = AspectosRubricaBE.AspectoRubricaId,
                                    Orden = ordenCriterio,
                                    Nombre = CriterioText,
                                    Valor = doubleValor,
                                };

                                RubricOnRepositoryFactory.GetCriterioRubricaRepository().InsertIdentity(CriterioRubricaBE, true);
                            }
                            ordenAspecto++;
                        }
                        ordenCategoria++;
                    }
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    scope.Complete();

                    PostMessage("El diseño ha sido guardado exitosamente.", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
            }

            return PartialView("EmptyPartial");
        }

        public ActionResult VerVersionRubrica(String RubricaId, String Version, String TipoArtefacto, int EvaluacionId)
        {
            var VerVersionRubricaViewModel = new VerVersionRubricaViewModel(RubricaId, Version, TipoArtefacto, EvaluacionId);
            return View(VerVersionRubricaViewModel);
        }

        public ActionResult EvaluarRubrica(String RubricaId, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId)
        {
            var EvaluarRubricaViewModel = new EvaluarRubricaViewModel(RubricaId, TipoArtefacto, CodigoEvaluadoId, CodigoEvaluadorId);
            return View(EvaluarRubricaViewModel);
        }

        [HttpPost]
        public ActionResult EvaluarRubrica(String RubricaId, String Version, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId, FormCollection formCollection)
        {
            var Respuestas = new List<RespuestasRubricaBE>();
            var MyBoolean = false;

            foreach (var actualKey in formCollection)
            {
                var key = actualKey.ToString();

                if (!key.StartsWith("EvalA"))
                    continue;

                if (Boolean.TryParse(formCollection[key], out MyBoolean) != false)
                    continue;

                key = key.Remove(0, "EvalA".Length);
                var splitted = key.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (splitted.Count() != 2 || !splitted[1].StartsWith("C"))
                    continue;

                splitted[1] = splitted[1].Remove(0, 1);

                if (!splitted[1].IsInteger())
                    continue;

                Respuestas.Add(new RespuestasRubricaBE() { CriterioRubricaId = splitted[1].ToInteger() });
            }

            if (Respuestas.Count == 0)
            {
                PostMessage("No hay informacion que requiera ser grabada.", MessageType.Info);
            }
            else
            {
                var CriteriosId = Respuestas.Select(x => x.CriterioRubricaId);
                var Criterios = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetWhere(x => CriteriosId.Contains(x.CriterioRubricaId)).ToList();

                var Evaluacion = new EvaluacionesBE()
                    {
                        CodigoEvaluadoId = CodigoEvaluadoId,
                        CodigoEvaluadorId = CodigoEvaluadorId,
                        FechaEvaluacion = DateTime.Now,
                        RubricaId = RubricaId,
                        TipoArtefacto = TipoArtefacto,
                        Version = Version
                    };

                var ResultadoString = "";

                try
                {
                    ResultadoString = Criterios.Sum(x => x.Valor).ToString();
                }
                catch (Exception ex)
                {
                }

                var Resultado = new ResultadosRubricasBE()
                    {
                        Resultado = ResultadoString,
                    };

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        RubricOnRepositoryFactory.GetEvaluacionesRepository().InsertIdentity(Evaluacion, true);

                        foreach (var respuesta in Respuestas)
                            respuesta.EvaluacionId = Evaluacion.EvaluacionId;
                        Resultado.EvaluacionId = Evaluacion.EvaluacionId;

                        RubricOnRepositoryFactory.GetResultadosRubricasRepository().Insert(Resultado);
                        RubricOnRepositoryFactory.GetRespuestasRubricaRepository().Insert(Respuestas);
                        RubricOnRepositoryFactory.SubmitChanges(true);

                        scope.Complete();

                        PostMessage("La rubrica ha sido evaluada exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
            return View("Mensaje");
        }

        public ActionResult ListarEvaluaciones(String RubricaId, String Version, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId, String FechaInicio, String FechaFin)
        {
            var ListarEvaluacionesViewModel = new ListarEvaluacionesViewModel(RubricaId, Version, TipoArtefacto, CodigoEvaluadoId, CodigoEvaluadorId, FechaInicio, FechaFin);
            return View(ListarEvaluacionesViewModel);
        }

        public ActionResult ListarEvaluacionesFiltradas(String RubricaId, String Version, String TipoArtefacto, String CodigoEvaluadoId, String CodigoEvaluadorId, String FechaInicio, String FechaFin)
        {
            var ValidationLogic = new ValidationLogic(ModelState);

            RubricaId = RubricaId ?? "";
            Version = Version ?? "";
            TipoArtefacto = TipoArtefacto ?? "";
            CodigoEvaluadoId = CodigoEvaluadoId ?? "";
            CodigoEvaluadorId = CodigoEvaluadorId ?? "";
            FechaInicio = FechaInicio ?? "";
            FechaFin = FechaFin ?? "";

            ValidationLogic.Valid(FechaInicio, "FechaInicio", ValidationOption.IsDate, ValidationOption.IsNull, ValidationOption.IsEmpty);
            ValidationLogic.Valid(FechaInicio, "FechaFin", ValidationOption.IsDate, ValidationOption.IsNull, ValidationOption.IsEmpty);

            if (!ModelState.IsValid)
            {
                PostMessage("Revise los datos ingresados.", MessageType.Error);
                return PartialView("EmptyPartial");
            }

            var ListarEvaluacionesFiltradasViewModel = new ListarEvaluacionesFiltradasViewModel(RubricaId, Version, TipoArtefacto, CodigoEvaluadoId, CodigoEvaluadorId, FechaInicio, FechaFin);
            return PartialView(ListarEvaluacionesFiltradasViewModel);
        }

        public ActionResult InicioEvaluacionRubrica(String RubricaId, String TipoArtefacto)
        {
            var Rubrica = RubricOnRepositoryFactory.GetRubricasRepository().GetOne(RubricaId, TipoArtefacto);
            return View(Rubrica);
        }

        [HttpPost]
        public ActionResult InicioEvaluacionRubrica(RubricasBE Rubrica, String CodigoEvaluadoId, String CodigoEvaluadorId, FormCollection formCollection)
        {
            var ValidationLogic = new ValidationLogic(ModelState);

            ValidationLogic.Valid(Rubrica.RubricaId, "Rubrica.RubricaId", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(Rubrica.TipoArtefacto, "Rubrica.TipoArtefacto", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(CodigoEvaluadoId, "CodigoEvaluadoId", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(CodigoEvaluadorId, "CodigoEvaluadorId", ValidationOption.IsNotEmpty);

            if (!ModelState.IsValid)
            {
                PostMessage("Revise los datos ingresados.", MessageType.Error);
                return View(Rubrica);
            }

            return RedirectToAction("EvaluarRubrica", new { RubricaId = Rubrica.RubricaId, TipoArtefacto = Rubrica.TipoArtefacto, CodigoEvaluadoId = CodigoEvaluadoId, CodigoEvaluadorId = CodigoEvaluadorId });
        }

        public ActionResult EliminarVersionRubrica(String RubricaId, String Version, String TipoArtefacto)
        {
            var VersionRubrica = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetOne(RubricaId, TipoArtefacto, Version);

            if (VersionRubrica == null)
            {
                PostMessage("Revise los datos ingresados.", MessageType.Error);
                return RedirectToAction("ListarRubricas");
            }

            var Evaluaciones = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.Version == Version).Select(x => x.EvaluacionId);

            if (Evaluaciones.Count() != 0)
            {
                PostMessage("Existen evaluaciones asociadas. No se puede eliminar la rúbrica.", MessageType.Info);
                return RedirectToAction("ListarVersionesRubrica", new { RubricaId = RubricaId, TipoArtefacto = TipoArtefacto });
            }

            var ExistenOtrasVersiones = true;

            var Categorias = RubricOnRepositoryFactory.GetCategoriasRubricasRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.Version == Version);
            var CategoriasId = Categorias.Select(x => x.CategoriaRubricaId);
            var Aspectos = RubricOnRepositoryFactory.GetAspectosRubricaRepository().GetWhere(x => CategoriasId.Contains(x.CategoriaRubricaId));
            var AspectosId = Aspectos.Select(x => x.AspectoRubricaId);
            var Criterios = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetWhere(x => AspectosId.Contains(x.AspectoRubricaId));
            var CriteriosId = Criterios.Select(x => x.CriterioRubricaId);

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    RubricOnRepositoryFactory.GetCriterioRubricaRepository().Delete(Criterios);
                    RubricOnRepositoryFactory.GetAspectosRubricaRepository().Delete(Aspectos);
                    RubricOnRepositoryFactory.GetCategoriasRubricasRepository().Delete(Categorias);
                    RubricOnRepositoryFactory.GetVersionesRubricasRepository().DeleteWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.Version == Version);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    if (VersionRubrica.EsActual)
                    {
                        var OtrasVersiones = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.Version != Version);
                        OtrasVersiones = OtrasVersiones.OrderByDescending(x => x.FechaCreacion).ToList();
                        var NuevaVersionActual = OtrasVersiones.FirstOrDefault();
                        if (NuevaVersionActual == null)
                        {
                            RubricOnRepositoryFactory.GetRubricasRepository().DeleteWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto);
                            ExistenOtrasVersiones = false;
                        }
                        else
                        {
                            NuevaVersionActual.EsActual = true;
                            RubricOnRepositoryFactory.GetVersionesRubricasRepository().InsertOrUpdate(NuevaVersionActual);
                        }
                        RubricOnRepositoryFactory.SubmitChanges(true);
                    }

                    scope.Complete();

                    PostMessage("La versión ha sido eliminada exitosamente.", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
            }

            if (ExistenOtrasVersiones)
                return RedirectToAction("ListarVersionesRubrica", new { RubricaId = RubricaId, TipoArtefacto = TipoArtefacto });
            else
                return RedirectToAction("ListarRubricas");
        }
        
        public ActionResult EliminarEvaluacion(int EvaluacionId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    RubricOnRepositoryFactory.GetRespuestasRubricaRepository().DeleteWhere(x => x.EvaluacionId == EvaluacionId);
                    RubricOnRepositoryFactory.SubmitChanges(true);
                    RubricOnRepositoryFactory.GetResultadosRubricasRepository().DeleteWhere(x => x.EvaluacionId == EvaluacionId);
                    RubricOnRepositoryFactory.SubmitChanges(true);
                    RubricOnRepositoryFactory.GetEvaluacionesRepository().DeleteWhere(x => x.EvaluacionId == EvaluacionId);
                    RubricOnRepositoryFactory.SubmitChanges(true);

                    scope.Complete();

                    PostMessage("La evaluación ha sido eliminada exitosamente.", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
            }

            return RedirectToAction("ListarEvaluaciones");
        }
        
    }
}

