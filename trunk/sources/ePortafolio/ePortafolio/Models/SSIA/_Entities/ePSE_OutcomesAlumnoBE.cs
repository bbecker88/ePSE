using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Entities
{
    public partial class OutcomesAlumnoBE
    {	
		public String AlumnoId { get; set; }
		public String CorreoElectronico { get; set; }
		public String DescripcionOutcome { get; set; }
		public String Nombre { get; set; }
		public String NombreOutcome { get; set; }
		public Int32 OutcomeId { get; set; }
    }
}
