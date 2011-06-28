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
            else
            {
                var trabajoLinq = new Trabajo();
                trabajoLinq.Iniciativa = Trabajo.Iniciativa;
                trabajoLinq.Instrucciones = Trabajo.Instrucciones;
                trabajoLinq.Nombre = Trabajo.Nombre;
                trabajoLinq.PeriodoId = Trabajo.Periodo.PeriodoId;
                trabajoLinq.EsGrupal = Trabajo.EsGrupal;
                trabajoLinq.FechaFin = Trabajo.FechaFin;
                trabajoLinq.FechaInicio = Trabajo.FechaInicio;
                trabajoLinq.TrabajoId = Trabajo.TrabajoId;
                ePortafolioDAO.Trabajos.InsertOnSubmit(trabajoLinq);

                ePortafolioDAO.SubmitChanges();
                Trabajo.TrabajoId = trabajoLinq.TrabajoId;
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
                    Curso = RepositoryFactory.GetCursoRepository().GetCursoNoFK(Trabajo.CursoId, Trabajo.PeriodoId),
                    Periodo = RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(Trabajo.PeriodoId),
                    EsGrupal = Trabajo.EsGrupal,
                    FechaFin = Trabajo.FechaFin,
                    FechaInicio = Trabajo.FechaInicio,
                    Instrucciones = Trabajo.Instrucciones,
                    Nombre = Trabajo.Nombre,
                    TrabajoId = Trabajo.TrabajoId,
                    Iniciativa = Trabajo.Iniciativa
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
                    TrabajoId = Trabajo.TrabajoId,
                    Iniciativa = Trabajo.Iniciativa
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

        public List<BETrabajo> GetTrabajosPendientes(String AlumnoId, String PeriodoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Cursos = RepositoryFactory.GetCursoRepository().GetCursosMatriculados(AlumnoId, PeriodoId);

            List<BETrabajo> TrabajosPendientes = new List<BETrabajo>();

            foreach (BECurso Curso in Cursos)
            {
                var Trabajos = ePortafolioDAO.Trabajos.Where(t => t.PeriodoId == PeriodoId && (t.FechaFin == null || t.FechaFin >= DateTime.Today) && t.CursoId == Curso.CursoId).ToList();

                foreach (var Trabajo in Trabajos)
                {
                    if (!ePortafolioDAO.AlumnosGrupos.Any(ag => ag.AlumnoId == AlumnoId && ag.Grupo.TrabajoId == Trabajo.TrabajoId))
                    {
                        TrabajosPendientes.Add(RepositoryFactory.GetTrabajoRepository().GetTrabajo(Trabajo.TrabajoId));
                    }
                }
            }

            TrabajosPendientes = TrabajosPendientes.OrderByDescending(x => x.FechaFin).ToList();
            TrabajosPendientes.Reverse(0, TrabajosPendientes.Count(x => x.FechaFin != null));

            return TrabajosPendientes;
        }

        public List<BETrabajo> GetTrabajosIndependientes(String AlumnoId, String PeriodoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupos = ePortafolioDAO.Grupos.Where(g => g.AlumnosGrupos.Any(ag => ag.AlumnoId == AlumnoId));

            var Trabajos = from g in Grupos
                           where g.Trabajo.PeriodoId == PeriodoId && g.Trabajo.Iniciativa == "EST"
                           select RepositoryFactory.GetTrabajoRepository().GetTrabajoNoFK(g.TrabajoId);

            return Trabajos.ToList();
        }
        
    }
}
