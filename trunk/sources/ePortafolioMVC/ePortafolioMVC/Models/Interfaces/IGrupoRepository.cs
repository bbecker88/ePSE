using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IGrupoRepository
    {
        void AddAlumnoGrupo(int GrupoId, string AlumnoId);
        void CreateGrupo(int TrabajoId, string AlumnoLiderId);
        void DeleteAlumnoGrupo(int GrupoId, string AlumnoId);
        void DeleteAlumnosGrupo(int GrupoId);
        void DeleteGrupo(int GrupoId);
        ePortafolioMVC.Models.Entities.BEGrupo GetGrupo(int GrupoId);
        ePortafolioMVC.Models.Entities.BEGrupo GetGrupoAlumno(int TrabajoId, string AlumnoId);
        ePortafolioMVC.Models.Entities.BEGrupo GetGrupoNoFK(int GrupoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEGrupo> GetGruposEntregados(int TrabajoId, string SeccionId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEGrupo> GetGruposPendientes(int TrabajoId, string SeccionId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEGrupo> GetGruposTrabajosEntregados(string AlumnoId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEGrupo> GetGruposTrabajosPendientes(string AlumnoId, string PeriodoId);
        void SetLiderGrupo(int GrupoId, string AlumnoId);
        void SetNotaGrupo(int GrupoId, string Nota);
    }
}
