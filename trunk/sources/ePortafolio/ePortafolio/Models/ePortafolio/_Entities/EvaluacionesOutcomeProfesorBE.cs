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
    public partial class EvaluacionesOutcomeProfesorBE
    {	
		public String AlumnoId { get; set; }
		public Int32? EvaluacionId { get; set; }
		public String Nota { get; set; }
		public Int32 OutcomeId { get; set; }
		public String PeriodoId { get; set; }
		public String ProfesorId { get; set; }
    }
}
