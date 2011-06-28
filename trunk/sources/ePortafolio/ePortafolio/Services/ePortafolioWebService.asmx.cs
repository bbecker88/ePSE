using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ePortafolio.Models.SSIA;
using ePortafolio.Models.ePortafolio;

namespace ePortafolio.Services
{
    /// <summary>
    /// Summary description for ePortafolioWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ePortafolioWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<EvaluacionesAlumnosResult> ObtenerEvaluacionesAlumnosPorCursoPorPeriodo(int CursoId, String PeriodoId)
        {
            var AlumnosCurso = SSIARepositoryFactory.GetAlumnosCursoRepository().GetWhere(x => x.CursoId == CursoId && x.PeriodoId == PeriodoId);
            var AlumnosCursoId = AlumnosCurso.Select(x => x.AlumnoId).Distinct();

            var TrabajosCurso = ePortafolioRepositoryFactory.GetTrabajosRepository().GetWhere(x => x.CursoId == CursoId && x.PeriodoId == PeriodoId);

            var Result = new List<EvaluacionesAlumnosResult>();

            foreach (var Trabajo in TrabajosCurso)
            {
                var AlumnosGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetAlumnosGrupoTrabajo(Trabajo.TrabajoId);

                foreach (var AlumnoId in AlumnosCursoId)
                {
                    var AlumnoGrupo = AlumnosGrupo.FirstOrDefault(x => x.AlumnoId == AlumnoId);
                    Result.Add(new EvaluacionesAlumnosResult()
                            {
                                AlumnoId = AlumnoId,
                                CodigoTrabajo = Trabajo.Codigo,
                                CursoId = Trabajo.CursoId,
                                EvaluacionId = AlumnoGrupo != null ? AlumnoGrupo.EvaluacionId : null,
                                NombreTrabajo = Trabajo.Nombre,
                                TrabajoId = Trabajo.TrabajoId,
                                Nota = AlumnoGrupo != null ? AlumnoGrupo.Nota : "NE"
                            });
                }
            }
            return Result;
        }
    }

    public class EvaluacionesAlumnosResult
    {
        public String AlumnoId { get; set; }
        public Int32? EvaluacionId { get; set; }
        public String Nota { get; set; }
        public Int32 CursoId { get; set; }
        public Int32 TrabajoId { get; set; }
        public String CodigoTrabajo { get; set; }
        public String NombreTrabajo { get; set; }
    }
}
