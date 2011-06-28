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
    public partial class CategoriasRubricasBE
    {	
		public Int32 CategoriaRubricaId { get; set; }
		public String Nombre { get; set; }
		public Int32 Orden { get; set; }
		public String OutcomeId { get; set; }
		public String RubricaId { get; set; }
		public String TipoArtefacto { get; set; }
		public String Version { get; set; }
    }
}
