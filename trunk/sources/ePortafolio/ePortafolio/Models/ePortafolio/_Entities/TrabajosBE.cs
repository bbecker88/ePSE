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
    public partial class TrabajosBE
    {	
		public String Codigo { get; set; }
		public Int32 CursoId { get; set; }
		public Boolean EsGrupal { get; set; }
		public DateTime? FechaFin { get; set; }
		public DateTime? FechaInicio { get; set; }
		public String Iniciativa { get; set; }
		public String Instrucciones { get; set; }
		public String Nombre { get; set; }
		public String PeriodoId { get; set; }
		public Int32 TrabajoId { get; set; }
    }
}
