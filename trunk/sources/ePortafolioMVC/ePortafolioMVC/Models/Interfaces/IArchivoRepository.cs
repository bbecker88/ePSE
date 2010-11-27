using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IArchivoRepository
    {
        void AddArchivo(int GrupoId, ePortafolioMVC.Models.Entities.BEArchivo Archivo);
        void DeleteArchivo(int ArchivoId);
        void DeleteArchivosGrupo(int GrupoId);
        ePortafolioMVC.Models.Entities.BEArchivo GetArchivo(int ArchivoId);
        ePortafolioMVC.Models.Entities.BEArchivo GetArchivoNoFK(int ArchivoId);
        System.Collections.Generic.List<ePortafolioMVC.Models.Entities.BEArchivo> GetArchivosGrupo(int GrupoId);
    }
}
