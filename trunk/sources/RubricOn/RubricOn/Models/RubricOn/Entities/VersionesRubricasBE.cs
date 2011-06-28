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
        public Dictionary<String, String> TiposRubrica = new Dictionary<String, String>()
        {
            {"ANA","Analítica"},
            {"HOL","Holística"},
        };
    }
}
