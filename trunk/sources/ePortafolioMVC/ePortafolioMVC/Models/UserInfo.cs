using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models
{

    public enum RolDescription {Profesor,Evaluador,Estudiante}

    public class UserInfo
    {
        public String Codigo{get;set;}
        public String Nombre{get;set;}
        public RolDescription Rol { get; set; }
    }
}
