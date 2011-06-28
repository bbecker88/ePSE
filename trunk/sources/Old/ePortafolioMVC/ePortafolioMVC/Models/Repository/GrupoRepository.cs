using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class GrupoRepository : ePortafolioMVC.Models.Repository.IGrupoRepository
    {
        public void SetLiderGrupo(int GrupoId, String AlumnoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            BEAlumno AlumnoLider = RepositoryFactory.GetAlumnoRepository().GetLiderGrupo(GrupoId);

            var AlumnoGrupoLiderAntiguo = ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId == GrupoId && ag.AlumnoId == AlumnoLider.AlumnoId);
            var AlumnoGrupoLiderNuevo = ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId == GrupoId && ag.AlumnoId == AlumnoId);

            if (AlumnoGrupoLiderAntiguo != null)
            {
                AlumnoGrupoLiderAntiguo.EsLider = false;
            }

            if (AlumnoGrupoLiderNuevo != null)
            {
                AlumnoGrupoLiderNuevo.EsLider = true;
            }

            ePortafolioDAO.SubmitChanges();
        }

        public void SetNotaGrupo(int GrupoId, String Nota)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId == GrupoId);

            if (Grupo != null)
            {
                Grupo.Nota = Nota;
                ePortafolioDAO.SubmitChanges();
            }
        }

        public bool AddAlumnoGrupo(int GrupoId, String AlumnoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var AlumnoGrupo = new AlumnosGrupo();
            if ((RepositoryFactory.GetAlumnoRepository().GetAlumno(AlumnoId) != null )&& !(RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(GrupoId).Any(x=>x.AlumnoId == AlumnoId)))
            {
                AlumnoGrupo.GrupoId = GrupoId;
                AlumnoGrupo.AlumnoId = AlumnoId;
                AlumnoGrupo.EsLider = false;

                ePortafolioDAO.AlumnosGrupos.InsertOnSubmit(AlumnoGrupo);
                ePortafolioDAO.SubmitChanges();
                return true;
            }

            return false;
        }

        public void CreateGrupo(int TrabajoId, String AlumnoLiderId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupo = new Grupo();

            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajoNoFK(TrabajoId);
            BESeccion Seccion = RepositoryFactory.GetSeccionRepository().GetSeccionCursoAlumno(AlumnoLiderId,Trabajo.Curso.CursoId,Trabajo.Periodo.PeriodoId);
            
            Grupo.TrabajoId = TrabajoId;
            Grupo.Nota = "NE";
            Grupo.AlumnosGrupos.Add(new AlumnosGrupo { AlumnoId = AlumnoLiderId, EsLider = true });
            Grupo.SeccionId = Seccion != null ? Seccion.SeccionId : "GEN";

            ePortafolioDAO.Grupos.InsertOnSubmit(Grupo);
            ePortafolioDAO.SubmitChanges();
        }

        public void DeleteGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId == GrupoId);

            if (Grupo != null)
            {
                ePortafolioDAO.Grupos.DeleteOnSubmit(Grupo);
                ePortafolioDAO.SubmitChanges();
            }
        }
        
        public void DeleteAlumnosGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var AlumnosGrupo = ePortafolioDAO.AlumnosGrupos.Where(ag => ag.GrupoId == GrupoId);

            if (AlumnosGrupo != null)
            {
                ePortafolioDAO.AlumnosGrupos.DeleteAllOnSubmit(AlumnosGrupo);
                ePortafolioDAO.SubmitChanges();
            }
        }

        public void DeleteAlumnoGrupo(int GrupoId, String AlumnoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var AlumnoGrupo = ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId == GrupoId && ag.AlumnoId == AlumnoId);

            if (AlumnoGrupo != null)
            {
                ePortafolioDAO.AlumnosGrupos.DeleteOnSubmit(AlumnoGrupo);
                ePortafolioDAO.SubmitChanges();
            }
        }
        
        public BEGrupo GetGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId == GrupoId);

            if (Grupo != null)
            { 
                return new BEGrupo
                    {
                        GrupoId = Grupo.GrupoId,
                        Nota = Grupo.Nota,
                        Seccion = RepositoryFactory.GetSeccionRepository().GetSeccionNoFK(Grupo.SeccionId),
                        Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajoNoFK(Grupo.TrabajoId),
                        Lider = RepositoryFactory.GetAlumnoRepository().GetLiderGrupo(Grupo.GrupoId)
                    };
            }
            return null;
        }

        public BEGrupo GetGrupoNoFK(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId == GrupoId);

            if (Grupo != null)
            {
                return new BEGrupo
                {
                    GrupoId = Grupo.GrupoId,
                    Nota = Grupo.Nota,
                    Seccion = new BESeccion { SeccionId = Grupo.SeccionId },
                    Trabajo = new BETrabajo { TrabajoId = Grupo.TrabajoId },
                    Lider = RepositoryFactory.GetAlumnoRepository().GetLiderGrupo(Grupo.GrupoId)
                };
            }
            return null;
        }

        public BEGrupo GetGrupoAlumno(int TrabajoId, String AlumnoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var AlumnosGrupo = ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.AlumnoId == AlumnoId && ag.Grupo.TrabajoId == TrabajoId);
            var Grupo = AlumnosGrupo != null ? AlumnosGrupo.Grupo : null;
            if (Grupo != null)
            {
                return RepositoryFactory.GetGrupoRepository().GetGrupoNoFK(Grupo.GrupoId);
            }

            return null;
        }

        public List<BEGrupo> GetGruposPendientes(int TrabajoId, String SeccionId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var GruposPendiente = from g in ePortafolioDAO.Grupos
                                  where g.TrabajoId == TrabajoId &&
                                        g.SeccionId == SeccionId &&
                                        g.ArchivosGrupos.Count == 0
                                  select RepositoryFactory.GetGrupoRepository().GetGrupoNoFK(g.GrupoId);

            return GruposPendiente.ToList();
        }

        public List<BEGrupo> GetGruposEntregados(int TrabajoId, String SeccionId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var GruposEntregados = from g in ePortafolioDAO.Grupos
                                   where g.TrabajoId == TrabajoId &&
                                         g.SeccionId == SeccionId &&
                                         g.ArchivosGrupos.Count != 0
                                   select RepositoryFactory.GetGrupoRepository().GetGrupoNoFK(g.GrupoId);

            return GruposEntregados.ToList();
        }

        public List<BEGrupo> GetGruposTrabajosEntregados(String AlumnoId,String PeriodoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();
            
            var GruposTrabajosEntregados = from g in ePortafolioDAO.Grupos
                                           where g.Trabajo.PeriodoId == PeriodoId && 
                                           g.AlumnosGrupos.Any(ag => ag.AlumnoId == AlumnoId && ag.GrupoId == g.GrupoId) &&
                                           g.ArchivosGrupos.Any(ag => ag.GrupoId == g.GrupoId)
                                           select RepositoryFactory.GetGrupoRepository().GetGrupoNoFK(g.GrupoId);

            return GruposTrabajosEntregados.ToList();
        }

        public List<BEGrupo> GetGruposTrabajosPendientes(String AlumnoId, String PeriodoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var GruposTrabajosPendientes = from g in ePortafolioDAO.Grupos
                                           where g.Trabajo.PeriodoId == PeriodoId &&
                                           g.AlumnosGrupos.Any(ag => ag.AlumnoId == AlumnoId && ag.GrupoId == g.GrupoId ) &&
                                           !g.ArchivosGrupos.Any(ag => ag.GrupoId == g.GrupoId)
                                           select RepositoryFactory.GetGrupoRepository().GetGrupoNoFK(g.GrupoId);

            return GruposTrabajosPendientes.ToList();
        }

        public List<BEGrupo> GruposTrabajosIndependientes(String AlumnoId, String PeriodoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var GruposTrabajosIndependientes = from g in ePortafolioDAO.Grupos
                                           where g.Trabajo.PeriodoId == PeriodoId &&
                                           g.AlumnosGrupos.Any(ag => ag.AlumnoId == AlumnoId && ag.GrupoId == g.GrupoId ) &&
                                           g.Trabajo.Iniciativa == "EST"
                                           select RepositoryFactory.GetGrupoRepository().GetGrupoNoFK(g.GrupoId);

            return GruposTrabajosIndependientes.ToList();
        }

        
    }
}
