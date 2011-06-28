using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RubricOnServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RubricOnService : IRubricOnService
    {
        /// <summary>
        /// El Servicio permite a un cliente evaluar una rúbrica. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para evalauar una rúbrica.</param>
        /// <returns>Devuelve la String que permite evaluar la rubrica.</returns>
        public String GetRutaEvaluarRubrica(EvaluarRubricaParam Param)
        {
            try
            {
                var ParamsToEncrypt = String.Format("RubricaId={0}&TipoArtefacto={1}&CodigoEvaluadoId={2}&CodigoEvaluadorId={3}&ParametroRespuesta={4}&RutaRetorno={5}",
                                                Param.RubricaId, Param.TipoArtefacto, Param.CodigoEvaluado, Param.CodigoEvaluador, Param.ParametroRespuesta, Param.RutaRetorno);

                var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

                var EncryptedParams = new RubricOn.Logic.Encryption64().Encrypt(ParamsToEncrypt, key);

                var context = System.ServiceModel.OperationContext.Current;

                if (context.Host.BaseAddresses.Count == 1)
                {
                    var BaseAddress = context.Host.BaseAddresses.First();
                    return String.Format("{0}://{1}/Expose/EvaluarRubrica?k={2}&p={3}", BaseAddress.Scheme, BaseAddress.Authority, key, EncryptedParams).Replace("RubricOn","RubricOnServiceLibrary");
                }
                else
                    return "";
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
        public String GetRutaVerRubrica(VerRubricaParam Param)
        {
            try
            {
                var ParamsToEncrypt = String.Format("RubricaId={0}&TipoArtefacto={1}",
                                    Param.RubricaId, Param.TipoArtefacto);

                var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

                var EncryptedParams = new RubricOn.Logic.Encryption64().Encrypt(ParamsToEncrypt, key);

                var context = System.ServiceModel.OperationContext.Current;

                if (context.Host.BaseAddresses.Count == 1)
                {
                    var BaseAddress = context.Host.BaseAddresses.First();
                    return String.Format("{0}://{1}/Expose/VerRubrica?k={2}&p={3}", BaseAddress.Scheme, BaseAddress.Authority, key, EncryptedParams).Replace("RubricOn", "RubricOnServiceLibrary"); 
                }
                else
                    return "";
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
        public String GetRutaVerRubricaEvaluada(VerRubricaEvaluadaParam Param)
        {
            try
            {
                var ParamsToEncrypt = String.Format("RubricaId={0}&TipoArtefacto={1}&CodigoEvaluadoId={2}",
                                    Param.RubricaId, Param.TipoArtefacto, Param.CodigoEvaluado);

                var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

                var EncryptedParams = new RubricOn.Logic.Encryption64().Encrypt(ParamsToEncrypt, key);

                var context = System.ServiceModel.OperationContext.Current;

                if (context.Host.BaseAddresses.Count == 1)
                {
                    var BaseAddress = context.Host.BaseAddresses.First();
                    return String.Format("{0}://{1}/Expose/VerRubricaEvaluada?k={2}&p={3}", BaseAddress.Scheme, BaseAddress.Authority, key, EncryptedParams).Replace("RubricOn", "RubricOnServiceLibrary"); 
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// El Servicio permite a un cliente evaluar una rúbrica. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para evalauar una rúbrica.</param>
        /// <returns>Devuelve un diccionartio con las String que permiten evaluar las rubricas.</returns>
        public Dictionary<EvaluarRubricaParam, String> GetRutaEvaluarRubricaList(List<EvaluarRubricaParam> Params)
        {
            var Dictionary = new Dictionary<EvaluarRubricaParam, String>();
            foreach (var Param in Params)
                Dictionary.Add(Param, GetRutaEvaluarRubrica(Param));
            return Dictionary;
        }
        /// <summary>
        /// El Servicio permite a un cliente ver una rúbrica. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para ver una rúbrica.</param>
        /// <returns>Devuelve un diccionartio con las String que permiten ver las rubricas.</returns>
        public Dictionary<VerRubricaParam, String> GetRutaVerRubricaList(List<VerRubricaParam> Params)
        {
            var Dictionary = new Dictionary<VerRubricaParam, String>();
            foreach (var Param in Params)
                Dictionary.Add(Param, GetRutaVerRubrica(Param));
            return Dictionary;
        }
        /// <summary>
        /// El Servicio permite a un cliente ver una rúbrica evaluada. 
        /// </summary>
        /// <param name="Param">Contiene la información necesaria para obtener la ruta para ver una rúbrica evalauada.</param>
        /// <returns>Devuelve un diccionartio con las String que permiten ver las rubricas evaluadas.</returns>
        public Dictionary<VerRubricaEvaluadaParam, String> GetRutaVerRubricaEvaluadaList(List<VerRubricaEvaluadaParam> Params)
        {
            var Dictionary = new Dictionary<VerRubricaEvaluadaParam, String>();
            foreach (var Param in Params)
                Dictionary.Add(Param, GetRutaVerRubricaEvaluada(Param));
            return Dictionary;
        }
    }
}
