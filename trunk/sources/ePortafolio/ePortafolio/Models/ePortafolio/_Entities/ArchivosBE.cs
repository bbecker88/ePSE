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
    public partial class ArchivosBE
    {	
		public String AlumnoId { get; set; }
		public Int32 ArchivoId { get; set; }
		public DateTime? FechaSubida { get; set; }
		public String Nombre { get; set; }
		public String Ruta { get; set; }
    }
}
