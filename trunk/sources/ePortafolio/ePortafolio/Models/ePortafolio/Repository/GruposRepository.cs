using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class GruposRepository 
    {
        /// <summary>
        ///  Devuelve los grupos en los que se encuentre un alumno en un período.
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <param name="AlumnoId"></param>
        /// <returns></returns>
        public List<GruposBE> GetGruposPeriodo(String PeriodoId, String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
            var Grupos = from x in DataContextObject.Grupos
                         where x.Trabajos.PeriodoId == PeriodoId && x.AlumnosGrupo.Any(ag=>ag.AlumnoId == AlumnoId)
                         select GetLinq(x);
            return Grupos.ToList();      
        }

        /// <summary>
        ///  Devuelve los grupos con archivos entregados un alumno en un período.
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <param name="AlumnoId"></param>
        /// <returns></returns>
        public List<GruposBE> GetGruposPeriodoConArchivosEntregados(String PeriodoId, String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
            var Grupos = from x in DataContextObject.Grupos
                         where x.Trabajos.PeriodoId == PeriodoId && x.AlumnosGrupo.Any(ag => ag.AlumnoId == AlumnoId)
                         select GetLinq(x);
            return Grupos.ToList();
        }

        /// <summary>
        ///  Devuelve los grupos con archivos entregados de un trabajo.
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <param name="AlumnoId"></param>
        /// <returns></returns>
        public List<GruposBE> GetGruposTrabajoConArchivosEntregados(int TrabajoId)
        {
            var DataContextObject = GetDataContextObject();
            var Grupos = from x in DataContextObject.Grupos
                         where x.TrabajoId == TrabajoId && x.ArchivosGrupo.Count>0
                         select GetLinq(x);
            return Grupos.ToList();
        }

        /// <summary>
        ///  Devuelve el grupo para un trabajo del alumno.
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <param name="AlumnoId"></param>
        /// <returns></returns>
        public GruposBE GetGrupoTrabajo(int TrabajoId, String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
            var Grupo = (from x in DataContextObject.Grupos
                         where x.Trabajos.TrabajoId == TrabajoId && x.AlumnosGrupo.Any(ag => ag.AlumnoId == AlumnoId)
                         select GetLinqFK(x)).FirstOrDefault();

            return Grupo;
        }

        /// <summary>
        /// Devuelve los grupos independientes en los que se encuentre un alumno.
        /// </summary>
        /// <param name="AlumnoId"></param>
        /// <returns></returns>
        public List<GruposBE> GetGruposIndependientes(String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
            var Grupos = from x in DataContextObject.Grupos
                         where x.Trabajos.Iniciativa != "UPC" && x.AlumnosGrupo.Any(ag=>ag.AlumnoId == AlumnoId)
                         select GetLinq(x);
            return Grupos.ToList();      
        }

        /// <summary>
        /// Devuelve los grupos de evaluacion extraordinaria para un profesor en un período.
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <param name="ProfesorId"></param>
        /// <returns></returns>
        public List<GruposBE> GetGruposEvaluacionExtraordinarios(String PeriodoId, String ProfesorId)
        {
            var DataContextObject = GetDataContextObject();
            var Grupos = from x in DataContextObject.EvaluacionesGruposProfesor
                         where x.Grupos.Trabajos.PeriodoId == PeriodoId && x.ProfesorId == ProfesorId
                         select GetLinqFK(x.Grupos);
            return Grupos.ToList();      
        }


        
        
    }
}
