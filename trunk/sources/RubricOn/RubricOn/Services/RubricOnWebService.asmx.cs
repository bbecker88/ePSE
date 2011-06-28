using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Mvc;
using RubricOn.Models.RubricOn;
using System.IO;
using System.Configuration;

namespace RubricOn.Services
{
    /// <summary>
    /// Summary description for RubricOnWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RubricOnWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// El Servicio permite a un cliente evaluar una rúbrica. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para evalauar una rúbrica.</param>
        /// <returns>Devuelve la String que permite evaluar la rubrica.</returns>
        [WebMethod]
        public String GetRutaEvaluarRubrica(EvaluarRubricaParam Param)
        {
            try
            {
                var ParamsToEncrypt = String.Format("RubricaId={0}&TipoArtefacto={1}&CodigoEvaluadoId={2}&CodigoEvaluadorId={3}&ParametroResultado={4}&ParametroCodigoEvaluacion={5}&RutaRetorno={6}&RutaCancelado={7}&CodigoEvaluacionPlantilla={8}",
                                                Param.RubricaId, Param.TipoArtefacto, Param.CodigoEvaluado, Param.CodigoEvaluador, Param.ParametroResultado, Param.ParametroCodigoEvaluacion, Param.RutaRetorno, Param.RutaCancelado, Param.CodigoEvaluacionPlantilla);

                var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

                var EncryptedParams = new RubricOn.Logic.Encryption64().Encrypt(ParamsToEncrypt, key);

                return ConfigurationManager.AppSettings.Get("ServerPath") + Helpers.HtmlHelpers.GetUrlHelper().Action("EvaluarRubrica", "Expose", new { k = key, p = EncryptedParams });
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// El Servicio permite a un cliente ver una rúbrica. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para ver una rúbrica.</param>
        /// <returns>Devuelve la String que permite ver la rubrica.</returns>
        [WebMethod]
        public String GetRutaVerRubrica(VerRubricaParam Param)
        {
            try
            {
                var ParamsToEncrypt = String.Format("RubricaId={0}&TipoArtefacto={1}&RutaRetorno={2}",
                                    Param.RubricaId, Param.TipoArtefacto,Param.RutaRetorno);

                var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

                var EncryptedParams = new RubricOn.Logic.Encryption64().Encrypt(ParamsToEncrypt, key);

                return ConfigurationManager.AppSettings.Get("ServerPath") + Helpers.HtmlHelpers.GetUrlHelper().Action("VerRubrica", "Expose", new { k = key, p = EncryptedParams });
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// El Servicio permite a un cliente ver una rúbrica evaluada. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para ver una rúbrica evalauada.</param>
        /// <returns>Devuelve la String que permite ver la rubrica evaluada.</returns>
        [WebMethod]
        public String GetRutaVerRubricaEvaluada(VerRubricaEvaluadaParam Param)
        {
            try
            {
                var ParamsToEncrypt = String.Format("EvaluacionId={0}&RutaRetorno={1}",
                                    Param.EvaluacionId, Param.RutaRetorno);

                var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

                var EncryptedParams = new RubricOn.Logic.Encryption64().Encrypt(ParamsToEncrypt, key);

                return ConfigurationManager.AppSettings.Get("ServerPath") + Helpers.HtmlHelpers.GetUrlHelper().Action("VerRubricaEvaluada", "Expose", new { k = key, p = EncryptedParams });
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public List<RespuestasEvaluacionResult> GetRespuestasEvaluacion(RespuestasEvaluacionParam Param)
        {
            try
            {
                var EvaluacionesId = Param.EvaluacionesId.Distinct();
                var Respuestas = RubricOnRepositoryFactory.GetRespuestasRubricaRepository().GetWhere(x => EvaluacionesId.Contains(x.EvaluacionId));
                var AspectosId = Respuestas.Select(x => x.ExtraCriterioRubrica.AspectoRubricaId);
                var Aspectos = RubricOnRepositoryFactory.GetAspectosRubricaRepository().GetWhere(x => AspectosId.Contains(x.AspectoRubricaId));
                var Criterios = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetWhere(x => AspectosId.Contains(x.AspectoRubricaId));
                var Result = new List<RespuestasEvaluacionResult>();

                foreach (var EvaluacionId in EvaluacionesId)
                {
                    var RespuestasEvaluacion = Respuestas.Where(x => x.EvaluacionId == EvaluacionId).ToList();

                    foreach(var Respuesta in RespuestasEvaluacion)
                    {
                        var Aspecto = Aspectos.FirstOrDefault(x=>x.AspectoRubricaId == Respuesta.ExtraCriterioRubrica.AspectoRubricaId);
                        
                        var ValorMinimo = Criterios.Where(c => c.AspectoRubricaId == Respuesta.ExtraCriterioRubrica.AspectoRubricaId).Min(m => m.Valor);
                        var ValorMaximo = Criterios.Where(c => c.AspectoRubricaId == Respuesta.ExtraCriterioRubrica.AspectoRubricaId).Max(m => m.Valor);

                        Result.Add(new RespuestasEvaluacionResult(){
                                AspectoId = Aspecto.AspectoRubricaId,
                                CategoriaId = Aspecto.CategoriaRubricaId,
                                CriterioSeleccionadoId = Respuesta.CriterioRubricaId,
                                EvaluacionId = EvaluacionId,
                                ValorSeleccionado = Respuesta.ExtraCriterioRubrica.Valor.ToString(),
                                ValorMinimo = ValorMinimo.ToString(),
                                ValorMaximo = ValorMaximo.ToString(),
                                OutcomeId = Aspecto.ExtraCategoriaRubrica.OutcomeId
                                });
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
    
    


    public class VerRubricaParam
    {
        /// <summary>
        /// La rúbrica a evaluar.
        /// </summary>
        public String RubricaId;

        /// <summary>
        /// El tipo de artefacto de la rúbrica a evaluar.
        /// </summary>
        public String TipoArtefacto;

        /// <summary>
        /// La ruta de retorno
        /// </summary>
        public String RutaRetorno;
    }

    public class EvaluarRubricaParam
    {
        /// <summary>
        /// La rúbrica a evaluar.
        /// </summary>
        public String RubricaId;

        /// <summary>
        /// El tipo de artefacto de la rúbrica a evaluar.
        /// </summary>
        public String TipoArtefacto;

        /// <summary>
        /// Código del evaluado
        /// </summary>
        public String CodigoEvaluado;

        /// <summary>
        /// Código del evaluador
        /// </summary>
        public String CodigoEvaluador;

        /// <summary>
        /// Nombre del parámetro que contendrá la resultado (Si el parámetro es vacío no se incluirá la respuesta en la ruta). Ejm: http://rutaRetorno/?ParametroRespuesta=14.5
        /// </summary>
        public String ParametroResultado;

        /// <summary>
        /// Nombre del parámetro que contendrá el codigo de la evaluacion (Si el parámetro es vacío no se incluirá la respuesta en la ruta). Ejm: http://rutaRetorno/?ParametroCodigoEvaluacion=41
        /// </summary>
        public String ParametroCodigoEvaluacion;

        /// <summary>
        /// La String a la que se enviará la petición GET si la rúbrica es evaluada correctamente. Se anexará al QueryString el ParametroRespuesta con el valor obenido en la evaluación.
        /// </summary>
        public String RutaRetorno;

        /// <summary>
        /// La ruta a seguir si el usuario desea cancelar la evaluacion.
        /// </summary>
        public String RutaCancelado;
        
        /// <summary>
        /// Codigo de la plantilla a utilizar.
        /// </summary>
        public Int32? CodigoEvaluacionPlantilla;
        
        
    }

    public class VerRubricaEvaluadaParam
    {
        /// <summary>
        /// El Id de la evaluacion.
        /// </summary>
        public int EvaluacionId;

        /// <summary>
        /// La ruta de retorno
        /// </summary>
        public String RutaRetorno;

    }


    public class RespuestasEvaluacionParam
    {
        /// <summary>
        /// El codigo de la evaluacion
        /// </summary>
        public List<int> EvaluacionesId;
    }

    public class RespuestasEvaluacionResult
    {
        /// <summary>
        /// El codigo de la evaluacion
        /// </summary>
        public int EvaluacionId;

        /// <summary>
        /// El codigo de la categoria
        /// </summary>
        public int CategoriaId;

        /// <summary>
        /// El codigo del aspecto
        /// </summary>
        public int AspectoId;

        /// <summary>
        /// El codigo del criterio seleccionado
        /// </summary>
        public int CriterioSeleccionadoId;

        /// <summary>
        /// Los parametros de la categoria
        /// </summary>
        public String OutcomeId;

        /// <summary>
        /// El valor seleccionado
        /// </summary>
        public String ValorSeleccionado;

        /// <summary>
        /// Valor maximo (si no son numericos enotnces el de menor orden, el de la izquierda)
        /// </summary>
        public String ValorMinimo;

        /// <summary>
        /// Valor maximo (si no son numericos enotnces el de menor orden, el de la derecha)
        /// </summary>
        public String ValorMaximo;
    }
}
