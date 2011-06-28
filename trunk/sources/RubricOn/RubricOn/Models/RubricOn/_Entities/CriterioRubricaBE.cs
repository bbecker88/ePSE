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
    public partial class CriterioRubricaBE
    {	
		public Int32 AspectoRubricaId { get; set; }
		public Int32 CriterioRubricaId { get; set; }
		public String Nombre { get; set; }
		public Int32 Orden { get; set; }
		public Double Valor { get; set; }
		public AspectosRubricaBE ExtraAspectoRubrica { get; set; }
    }
}
