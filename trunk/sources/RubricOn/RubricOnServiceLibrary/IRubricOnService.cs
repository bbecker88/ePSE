using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RubricOnServiceLibrary
{
    [ServiceContract]
    public interface IRubricOnService
    {
        [OperationContract]
        String GetRutaEvaluarRubrica(EvaluarRubricaParam Param);
        [OperationContract]
        String GetRutaVerRubrica(VerRubricaParam  Param);
        [OperationContract]
        String GetRutaVerRubricaEvaluada(VerRubricaEvaluadaParam Param);

        [OperationContract]
        Dictionary<EvaluarRubricaParam, String> GetRutaEvaluarRubricaList(List<EvaluarRubricaParam> Params);
        [OperationContract]
        Dictionary<VerRubricaParam, String> GetRutaVerRubricaList(List<VerRubricaParam> Params);
        [OperationContract]
        Dictionary<VerRubricaEvaluadaParam, String> GetRutaVerRubricaEvaluadaList(List<VerRubricaEvaluadaParam> Params);
    }
}
