using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.ViewModels.Classes
{
    public class Valor
    {
        public Boolean ID { get; set; }
        public String Descripcion { get; set; }
        
        public Valor(Boolean ID, String Descripcion)
        {
            this.ID = ID;
            this.Descripcion = Descripcion;
        }
    }
}
