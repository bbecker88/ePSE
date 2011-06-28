using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Entities
{
    public partial class ArchivosGrupoBE
    {	
		public Int32 ArchivoId { get; set; }
		public Int32 GrupoId { get; set; }
		public ArchivosBE ExtraArchivo { get; set; }
		public GruposBE ExtraGrupo { get; set; }
    }
}
