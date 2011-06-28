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
    public partial class RubricasBE
    {	
		public String RubricaId { get; set; }
		public String TipoArtefacto { get; set; }
		public TiposArtefactoBE ExtraTipoArtefacto { get; set; }
    }
}
