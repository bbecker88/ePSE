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
    public partial class CursosBE
    {	
		public String Codigo { get; set; }
		public Int32 CursoId { get; set; }
		public String Nombre { get; set; }
    }
}
