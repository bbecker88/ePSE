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
    public partial class GruposBE
    {	
		public Int32? EvaluacionId { get; set; }
		public Int32 GrupoId { get; set; }
		public String LiderId { get; set; }
		public String NombreTrabajo { get; set; }
		public String Nota { get; set; }
		public String SeccionId { get; set; }
		public Int32 TrabajoId { get; set; }
		public TrabajosBE ExtraTrabajo { get; set; }
    }
}
