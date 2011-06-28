using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RubricOnServiceLibrary
{

    [DataContract]
    public class VerRubricaParam
    {
        /// <summary>
        /// La rúbrica a evaluar.
        /// </summary>
        [DataMember]
        public String RubricaId;
        
        /// <summary>
        /// El tipo de artefacto de la rúbrica a evaluar.
        /// </summary>
        [DataMember]
        public String TipoArtefacto;
    }

    [DataContract]
    public class EvaluarRubricaParam
    {
        /// <summary>
        /// La rúbrica a evaluar.
        /// </summary>
        [DataMember]
        public String RubricaId;

        /// <summary>
        /// El tipo de artefacto de la rúbrica a evaluar.
        /// </summary>
        [DataMember]
        public String TipoArtefacto;

        /// <summary>
        /// Código del evaluado
        /// </summary>
        [DataMember]
        public String CodigoEvaluado;

        /// <summary>
        /// Código del evaluador
        /// </summary>
        [DataMember]
        public String CodigoEvaluador;

        /// <summary>
        /// Nombre del parámetro que contendrá la respuesta (Si el parámetro es vacío no se incluirá la respuesta en la ruta). Ejm: http://rutaRetorno/?ParametroRespuesta=14.5
        /// </summary>
        [DataMember]
        public String ParametroRespuesta;

        /// <summary>
        /// La String a la que se enviará la petición GET si la rúbrica es evaluada correctamente. Se anexará al QueryString el ParametroRespuesta con el valor obenido en la evaluación.
        /// </summary>
        [DataMember]
        public String RutaRetorno;
    }

    [DataContract]
    public class VerRubricaEvaluadaParam
    {
        /// <summary>
        /// La rúbrica a evaluar.
        /// </summary>
        [DataMember]
        public String RubricaId;

        /// <summary>
        /// El tipo de artefacto de la rúbrica a evaluar.
        /// </summary>
        [DataMember]
        public String TipoArtefacto;

        /// <summary>
        /// Código del evaluado.
        /// </summary>
        [DataMember]
        public String CodigoEvaluado;
    }
}
