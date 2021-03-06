using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Entities
{
    public partial class VersionesRubricasBE
    {	
		public String CreadorId { get; set; }
		public String Descripcion { get; set; }
		public Boolean EsActual { get; set; }
		public DateTime FechaCreacion { get; set; }
		public String RubricaId { get; set; }
		public String TipoArtefacto { get; set; }
		public String TipoRubrica { get; set; }
		public String Version { get; set; }
    }
}
