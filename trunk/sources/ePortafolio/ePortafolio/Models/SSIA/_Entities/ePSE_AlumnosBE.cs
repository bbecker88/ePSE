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
    public partial class AlumnosBE
    {	
		public String AlumnoId { get; set; }
		public String CorreoElectronico { get; set; }
		public String Nombre { get; set; }
		public String NombreCarrera { get; set; }
    }
}
