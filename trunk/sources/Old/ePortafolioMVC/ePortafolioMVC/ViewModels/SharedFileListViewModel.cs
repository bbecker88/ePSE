using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.ViewModels
{
    public class SharedFileListViewModel
    {
        public BETrabajo Trabajo { get; set; }
        public List<BEArchivo> ArchivosGrupo { get; set; }
    }
}
