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
    public partial class TrabajosOutcomeAlumnoBE
    {	
		public String AlumnoId { get; set; }
		public Int32 OutcomeId { get; set; }
		public Int32 TrabajoId { get; set; }
		public TrabajosBE ExtraTrabajo { get; set; }
    }
}
