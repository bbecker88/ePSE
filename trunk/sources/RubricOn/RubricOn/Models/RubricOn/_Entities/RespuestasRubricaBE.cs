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
    public partial class RespuestasRubricaBE
    {	
		public Int32 CriterioRubricaId { get; set; }
		public Int32 EvaluacionId { get; set; }
		public CriterioRubricaBE ExtraCriterioRubrica { get; set; }
		public EvaluacionesBE ExtraEvaluacion { get; set; }
    }
}
