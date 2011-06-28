using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricOn.ViewModel;
using RubricOn.Models.RubricOn;
using RubricOn.Models;
using RubricOn.Models.RubricOn.Entities;
using RubricOn.Helpers;
using System.Transactions;

namespace RubricOn.Controllers
{
    public class ExposeController : BaseController
    {
        public ActionResult VerRubrica(String k, String p)
        {
            try
            {
                var DecryptedParams = new RubricOn.Logic.Encryption64().Decrypt(p.Replace(" ", "+"), k);
                var tokens = DecryptedParams.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                var RubricaId = tokens[0].Substring("RubricaId=".Length);
                var TipoArtefacto = tokens[1].Substring("TipoArtefacto=".Length);
                var RutaRetorno = tokens[2].Substring("RutaRetorno=".Length);

                var VersionRubrica = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.EsActual == true).FirstOrDefault();

                if (VersionRubrica == null)
                {
                    PostMessage(String.Format("No se encontro una rúbrica asociada a '{0}' del artefacto '{1}'.", RubricaId, TipoArtefacto), MessageType.Info);
                }
                else
                {
                    var VerRubricaExposeViewModel = new VerRubricaExposeViewModel(VersionRubrica.RubricaId, VersionRubrica.Version, VersionRubrica.TipoArtefacto, 0, RutaRetorno);

                    return View("VerRubrica", VerRubricaExposeViewModel);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Oops! Al parecer ha ocurrido un error.", MessageType.Error);
            }

            return View("Mensaje");
        }

        public ActionResult EvaluarRubrica(String k, String p)
        {
            try
            {
                var DecryptedParams = new RubricOn.Logic.Encryption64().Decrypt(p.Replace(" ", "+"), k);
                var tokens = DecryptedParams.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                var RubricaId = tokens[0].Substring("RubricaId=".Length);
                var TipoArtefacto = tokens[1].Substring("TipoArtefacto=".Length);
                var CodigoEvaluadoId = tokens[2].Substring("CodigoEvaluadoId=".Length);
                var CodigoEvaluadorId = tokens[3].Substring("CodigoEvaluadorId=".Length);
                var ParametroResultado = tokens[4].Substring("ParametroResultado=".Length);
                var ParametroCodigoEvaluacion = tokens[5].Substring("ParametroCodigoEvaluacion=".Length);
                var RutaRetorno = tokens[6].Substring("RutaRetorno=".Length);
                var RutaCancelado = tokens[7].Substring("RutaCancelado=".Length);
                var CodigoEvaluacionPlantilla = tokens[8].Substring("CodigoEvaluacionPlantilla=".Length).ToInteger();
                
                for (int i = 6; i < tokens.Length; i++)
                    RutaRetorno += "&" + tokens[i];

                var GUID = Guid.NewGuid().ToString();
                var Rubrica = RubricOnRepositoryFactory.GetVersionesRubricasRepository().GetWhere(x => x.RubricaId == RubricaId && x.TipoArtefacto == TipoArtefacto && x.EsActual == true).FirstOrDefault();


                if (Rubrica == null)
                {
                    PostMessage(String.Format("No se encontro una rúbrica asociada a '{0}' del artefacto '{1}'.",RubricaId,TipoArtefacto), MessageType.Info);
                }
                else
                {
                    Session[GUID + "RubricaId"] = RubricaId;
                    Session[GUID + "TipoArtefacto"] = TipoArtefacto;
                    Session[GUID + "CodigoEvaluadoId"] = CodigoEvaluadoId;
                    Session[GUID + "CodigoEvaluadorId"] = CodigoEvaluadorId;
                    Session[GUID + "ParametroResultado"] = ParametroResultado;
                    Session[GUID + "ParametroCodigoEvaluacion"] = ParametroCodigoEvaluacion;
                    Session[GUID + "RutaRetorno"] = RutaRetorno;
                    Session[GUID + "Version"] = Rubrica.Version;
                    Session[GUID + "RutaCancelado"] = RutaCancelado;
                    Session[GUID + "CodigoEvaluacionPlantilla"] = CodigoEvaluacionPlantilla;

                    var EvaluarRubricaExposeViewModel = new EvaluarRubricaExposeViewModel(RubricaId, TipoArtefacto, CodigoEvaluadoId, CodigoEvaluadorId, GUID, CodigoEvaluacionPlantilla, RutaCancelado);

                    return View("EvaluarRubrica", EvaluarRubricaExposeViewModel);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Oops! Al parecer ha ocurrido un error.", MessageType.Error);
            }

            return View("Mensaje");
        }

        [HttpPost]
        public ActionResult EvaluarRubrica(String GUID, FormCollection formCollection)
        {
            try
            {
                var RubricaId = Session[GUID + "RubricaId"].ToString();
                var TipoArtefacto = Session[GUID + "TipoArtefacto"].ToString();
                var CodigoEvaluadoId = Session[GUID + "CodigoEvaluadoId"].ToString();
                var CodigoEvaluadorId = Session[GUID + "CodigoEvaluadorId"].ToString();
                var ParametroResultado = Session[GUID + "ParametroResultado"].ToString();
                var ParametroCodigoEvaluacion = Session[GUID + "ParametroCodigoEvaluacion"].ToString();
                var RutaRetorno = Session[GUID + "RutaRetorno"].ToString();
                var Version = Session[GUID + "Version"].ToString();

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

                            var UriBuilder = new UriBuilder(RutaRetorno);
                            if (ParametroResultado != String.Empty)
                                UriBuilder.Query += String.Format("&{0}={1}", ParametroResultado, ResultadoString);
                            if (ParametroCodigoEvaluacion != String.Empty)
                                UriBuilder.Query += String.Format("&{0}={1}", ParametroCodigoEvaluacion, Evaluacion.EvaluacionId);
                            UriBuilder.Query = UriBuilder.Query.Replace("?", "");

                            foreach (var respuesta in Respuestas)
                                respuesta.EvaluacionId = Evaluacion.EvaluacionId;
                            Resultado.EvaluacionId = Evaluacion.EvaluacionId;

                            RubricOnRepositoryFactory.GetResultadosRubricasRepository().Insert(Resultado);
                            RubricOnRepositoryFactory.GetRespuestasRubricaRepository().Insert(Respuestas);
                            RubricOnRepositoryFactory.SubmitChanges(true);

                            scope.Complete();

                            return Redirect(UriBuilder.Uri.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        PostMessage("Oops! Al parecer ha ocurrido un error.", MessageType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                PostMessage("Oops! Al parecer ha ocurrido un error.", MessageType.Error);
            }

            return View("Mensaje");
        }

        public ActionResult VerRubricaEvaluada(String k, String p)
        {
            try
            {
                var DecryptedParams = new RubricOn.Logic.Encryption64().Decrypt(p.Replace(" ", "+"), k);
                var tokens = DecryptedParams.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                var EvaluacionId = tokens[0].Substring("EvaluacionId=".Length).ToInteger();
                var RutaRetorno = tokens[1].Substring("RutaRetorno=".Length);
                
                var EvaluacionRubrica = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetOne(EvaluacionId);

                if (EvaluacionRubrica == null)
                {
                    PostMessage(String.Format("No se encontro la rúbrica."), MessageType.Info);
                }
                else
                { 
                    var VerRubricaExposeViewModel = new VerRubricaExposeViewModel(EvaluacionRubrica.RubricaId, EvaluacionRubrica.Version, EvaluacionRubrica.TipoArtefacto, EvaluacionRubrica.EvaluacionId,RutaRetorno);

                    return View("VerRubrica",VerRubricaExposeViewModel);
                }
            }
            catch (Exception ex)
            {
                PostMessage("Oops! Al parecer ha ocurrido un error.", MessageType.Error);
            }

            return View("Mensaje");
        }
    }
}
