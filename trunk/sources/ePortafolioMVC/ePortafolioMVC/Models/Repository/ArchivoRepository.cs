using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class ArchivoRepository : ePortafolioMVC.Models.Repository.IArchivoRepository
    {
        public void AddArchivo(int GrupoId, BEArchivo Archivo)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var ArchivoInsert = new Models.Archivo();

            ArchivoInsert.AlumnoId = Archivo.Alumno.AlumnoId;
            ArchivoInsert.ArchivoId = Archivo.ArchivoId;
            ArchivoInsert.Nombre = Archivo.Nombre;
            ArchivoInsert.Ruta = Archivo.Ruta;
            ArchivoInsert.FechaSubida = Archivo.FechaSubido;

            var ArchivoGrupoInsert = new Models.ArchivosGrupo();

            ArchivoGrupoInsert.GrupoId = GrupoId;
            ArchivoGrupoInsert.Archivo = ArchivoInsert;

            ePortafolioDAO.ArchivosGrupos.InsertOnSubmit(ArchivoGrupoInsert);

            ePortafolioDAO.SubmitChanges();
        }

        public void DeleteArchivo(int ArchivoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Archivo = ePortafolioDAO.Archivos.SingleOrDefault(a => a.ArchivoId == ArchivoId);
            var ArchivosGrupo = ePortafolioDAO.ArchivosGrupos.Where(ag => ag.ArchivoId == ArchivoId);

            if (Archivo != null)
            {
                ePortafolioDAO.Archivos.DeleteOnSubmit(Archivo);
            }

            if (ArchivosGrupo != null)
            {
                ePortafolioDAO.ArchivosGrupos.DeleteAllOnSubmit(ArchivosGrupo);
            }

            ePortafolioDAO.SubmitChanges();
        }
        
        public void DeleteArchivosGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var ArchivosGrupo = ePortafolioDAO.ArchivosGrupos.Where(ag => ag.GrupoId == GrupoId);
            var Archivos = ePortafolioDAO.Archivos.Where(a => ArchivosGrupo.Any(ag=>ag.ArchivoId == a.ArchivoId));

            if (Archivos != null)
            {
                ePortafolioDAO.Archivos.DeleteAllOnSubmit(Archivos);
            }

            if (ArchivosGrupo != null)
            {
                ePortafolioDAO.ArchivosGrupos.DeleteAllOnSubmit(ArchivosGrupo);
            }

            ePortafolioDAO.SubmitChanges();
        }

        public BEArchivo GetArchivo(int ArchivoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Archivo = ePortafolioDAO.Archivos.SingleOrDefault(a => a.ArchivoId == ArchivoId);

            if (Archivo != null)
            {
                return new BEArchivo
                {
                    ArchivoId = Archivo.ArchivoId,
                    Nombre = Archivo.Nombre,
                    Ruta = Archivo.Ruta,
                    FechaSubido = Archivo.FechaSubida,
                    Alumno = RepositoryFactory.GetAlumnoRepository().GetAlumno(Archivo.AlumnoId)
                };
            }

            return null;
        }

        public BEArchivo GetArchivoNoFK(int ArchivoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var Archivo = ePortafolioDAO.Archivos.SingleOrDefault(a => a.ArchivoId == ArchivoId);

            if (Archivo != null)
            {
                return new BEArchivo
                {
                    ArchivoId = Archivo.ArchivoId,
                    Nombre = Archivo.Nombre,
                    Ruta = Archivo.Ruta,
                    FechaSubido = Archivo.FechaSubida,
                    Alumno = new BEAlumno { AlumnoId = Archivo.AlumnoId}
                };
            }

            return null;
        }

        public List<BEArchivo> GetArchivosGrupo(int GrupoId)
        {
            ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

            var ArchivosGrupo = from ag in ePortafolioDAO.ArchivosGrupos
                               where ag.GrupoId == GrupoId
                                select RepositoryFactory.GetArchivoRepository().GetArchivoNoFK(ag.ArchivoId);

            return ArchivosGrupo.ToList();
        }

    }
}
