using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class TrabajoRepository : ePortafolioMVC.Models.Repository.ITrabajoRepository 
    {
        public void SaveTrabajo(BETrabajo Trabajo)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var UpdateTrabajo = ePortafolioDAO.Trabajos.SingleOrDefault(t => t.TrabajoId == Trabajo.TrabajoId);

            if (UpdateTrabajo != null)
            {
                UpdateTrabajo.Instrucciones = Trabajo.Instrucciones;
                UpdateTrabajo.FechaInicio = Trabajo.FechaInicio;
                UpdateTrabajo.FechaFin = Trabajo.FechaFin;
                UpdateTrabajo.EsGrupal = Trabajo.EsGrupal;

                ePortafolioDAO.SubmitChanges();
            }
        }

        public BETrabajo GetTrabajo(int TrabajoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Trabajo = ePortafolioDAO.Trabajos.SingleOrDefault(t => t.TrabajoId == TrabajoId);

            if (Trabajo != null)
            {
                return new BETrabajo
                {
                    Curso = RepositoryFactory.GetCursoRepository().GetCursoNoFK(Trabajo.CursoId,Trabajo.PeriodoId),
                    Periodo = RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(Trabajo.PeriodoId),
                    EsGrupal = Trabajo.EsGrupal,
                    FechaFin = Trabajo.FechaFin,
                    FechaInicio = Trabajo.FechaInicio,
                    Instrucciones = Trabajo.Instrucciones,
                    Nombre = Trabajo.Nombre,
                    TrabajoId = Trabajo.TrabajoId
                };
            }

            return null;
        }

        public BETrabajo GetTrabajoNoFK(int TrabajoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Trabajo = ePortafolioDAO.Trabajos.SingleOrDefault(t => t.TrabajoId == TrabajoId);

            if (Trabajo != null)
            {
                return new BETrabajo
                {
                    Curso = new BECurso { CursoId = Trabajo.CursoId },
                    Periodo = RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(Trabajo.PeriodoId),
                    EsGrupal = Trabajo.EsGrupal,
                    FechaFin = Trabajo.FechaFin,
                    FechaInicio = Trabajo.FechaInicio,
                    Instrucciones = Trabajo.Instrucciones,
                    Nombre = Trabajo.Nombre,
                    TrabajoId = Trabajo.TrabajoId
                };
            }

            return null;
        }

        public List<BETrabajo> GetTrabajosCurso(int CursoId, String PeriodoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var TrabajosCurso = from t in ePortafolioDAO.Trabajos
                                where t.CursoId == CursoId && t.PeriodoId == PeriodoId
                                select RepositoryFactory.GetTrabajoRepository().GetTrabajoNoFK(t.TrabajoId);

            return TrabajosCurso.ToList();
        }

        public List<BETrabajo> GetTrabajosHistoricoAlumno(String AlumnoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupos = ePortafolioDAO.Grupos.Where(g => g.AlumnosGrupos.Any(ag => ag.AlumnoId == AlumnoId) && g.ArchivosGrupos.Count > 0);
            var Trabajos = ePortafolioDAO.Trabajos.Where(t => Grupos.Any(g => g.TrabajoId == t.TrabajoId));

            var TrabajosAlumno = from t in Trabajos
                                 select RepositoryFactory.GetTrabajoRepository().GetTrabajoNoFK(t.TrabajoId);

            return TrabajosAlumno.ToList();
        }
    }
}
