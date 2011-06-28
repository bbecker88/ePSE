using System;
using System.Collections.Generic;
using ePortafolioMVC.Models.Entities;
namespace ePortafolioMVC.Models.Repository
{
    interface ITrabajoRepository
    {
        ePortafolioMVC.Models.Entities.BETrabajo GetTrabajo(int TrabajoId);
        ePortafolioMVC.Models.Entities.BETrabajo GetTrabajoNoFK(int TrabajoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BETrabajo> GetTrabajosCurso(int CursoId, string PeriodoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BETrabajo> GetTrabajosHistoricoAlumno(string AlumnoId);
        void SaveTrabajo(ePortafolioMVC.Models.Entities.BETrabajo Trabajo);
        List<BETrabajo> GetTrabajosPendientes(String AlumnoId, String PeriodoId);
        List<BETrabajo> GetTrabajosIndependientes(String AlumnoId, String PeriodoId);
    }
}
