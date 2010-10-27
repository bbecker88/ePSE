using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;

namespace ePortafolioMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        // Crea la vista para el Index
        public ActionResult Index()
        {

            return View();
        }

    }
}
