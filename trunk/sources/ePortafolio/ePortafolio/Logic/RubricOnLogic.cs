using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.RubricOnService;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using ePortafolio.RubricOnService;

namespace ePortafolio.Logic
{
    public class RubricOnLogic
    {
        public string GetVerRubricaUrl(String RubricaId, String TipoArtefacto, String RutaRetorno, bool ThrowException)
        {
            try
            {
                var Servicio = new RubricOnWebService();

                return Servicio.GetRutaVerRubrica(new VerRubricaParam()
                {
                    RubricaId = RubricaId,
                    TipoArtefacto = TipoArtefacto,
                    RutaRetorno = RutaRetorno
                });
            }
            catch (Exception ex)
            {
                if (ThrowException)
                    throw ex;
                return "";
            }
        }



        public string GetRutaEvaluarRubricaUrl(String RubricaId, String TipoArtefacto, String CodigoEvaluado, String CodigoEvaluador, String GUID, Int32? CodigoEvaluacionPlantilla,String RutaCancelado, bool ThrowException)
        {
            try
            {
                var httpContext = HttpContext.Current;

                if (httpContext == null) {
                  var request = new HttpRequest("/", "http://example.com", "");
                  var response = new HttpResponse(new StringWriter());
                  httpContext = new HttpContext(request, response);
                }

                var httpContextBase = new HttpContextWrapper(httpContext);
                var routeData = new RouteData();
                var requestContext = new RequestContext(httpContextBase, routeData);

                var urlHelper =  new UrlHelper(requestContext);

                var Servicio = new RubricOnWebService();
                return Servicio.GetRutaEvaluarRubrica(new EvaluarRubricaParam()
                {
                    RubricaId = RubricaId,
                    TipoArtefacto = TipoArtefacto,
                    CodigoEvaluado = CodigoEvaluado,
                    CodigoEvaluador = CodigoEvaluador,
                    ParametroResultado = "result",
                    ParametroCodigoEvaluacion = "evaluacionId",
                    CodigoEvaluacionPlantilla = CodigoEvaluacionPlantilla,
                    RutaCancelado = RutaCancelado,
                    RutaRetorno = (urlHelper).Action("FinalizarEvaluacion", "Expose", new { GUID = GUID }, "http")
                });
            }
            catch (Exception ex)
            {
                if (ThrowException)
                    throw ex;
                return "";
            }
        }


        public string GetRutaVerRubricaEvaluadaUrl(int EvaluacionId,String RutaRetorno, bool ThrowException)
        {
            try
            {
                var Servicio = new RubricOnWebService();
                return Servicio.GetRutaVerRubricaEvaluada(new VerRubricaEvaluadaParam()
                {
                    EvaluacionId = EvaluacionId,
                    RutaRetorno = RutaRetorno,
                });
            }
            catch (Exception ex)
            {
                if (ThrowException)
                    throw ex;
                return "";
            }
        }
    }
}
