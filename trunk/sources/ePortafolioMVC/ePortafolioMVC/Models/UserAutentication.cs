using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolioMVC.Models
{
    public class UserAutentication
    {
        public String User { get; set; }
        public String Password { get; set; }

        public UserAutentication()
        { }

        public bool Autenticate()
        {
            return true;
        }
    }
}
