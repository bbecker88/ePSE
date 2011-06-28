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
    public partial class CursosPeriodosBE
    {	
		public String CodigoCurso { get; set; }
		public String CoordinadorId { get; set; }
		public Int32 CursoId { get; set; }
		public String NombreCoordinador { get; set; }
		public String NombreCurso { get; set; }
		public String PeriodoId { get; set; }
    }
}
