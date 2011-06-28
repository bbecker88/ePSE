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
    public partial class EvaluacionesBE
    {	
		public String CodigoEvaluadoId { get; set; }
		public String CodigoEvaluadorId { get; set; }
		public Int32 EvaluacionId { get; set; }
		public DateTime FechaEvaluacion { get; set; }
		public String RubricaId { get; set; }
		public String TipoArtefacto { get; set; }
		public String Version { get; set; }
    }
}
