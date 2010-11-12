using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;

namespace ePortafolioMVC.Controllers
{
    public class LoginController : Controller
    {
        ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();
        
        //
        // GET: /Login/
        // Crea la vista para el Index de Login
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Login/
        // Hace el LogIn del usuario
        // Redirige a la accion de Index del controlador Home
        [HttpPost]
        public ActionResult Index(UserAutentication userAutentication)
        {
            if (ModelState.IsValid)
            {
                //Verifica si el usuario esta registrado
                if (userAutentication.Autenticate())
                {
                    if (userAutentication.Password != null) //Se trata de un profesor
                    {
                        //Session["UserInfo"] != null indica que hay usuario registrado
                        Session["UserInfo"] = new UserInfo { Codigo = userAutentication.User, Nombre = ePortafolioDAO.Profesores.SingleOrDefault(p => p.ProfesorId.ToString() == userAutentication.User).Nombre + " (Profesor)", Rol = RolDescription.Profesor };
                        return RedirectToAction("Index", "Professor");
                    }
                    else //Se trata de un alumno
                    {
                        //Session["UserInfo"] != null indica que hay usuario registrado
                        Session["UserInfo"] = new UserInfo { Codigo = userAutentication.User, Nombre = ePortafolioDAO.Alumnos.SingleOrDefault(a => a.AlumnoId.ToString() == userAutentication.User).Nombre + " (Estudiante)", Rol = RolDescription.Estudiante };
                        return RedirectToAction("Index", "Student");
                    }
                }

                //Session["UserInfo"] == null indica que no hay usuario registrado
                Session["UserInfo"] = null;
                return RedirectToAction("Index", "Home");
            }
            //Mostrar la página con los errores
            return View();
        }

        //
        // GET: /Login/LogOut/
        // Hace el LogOut del usuario
        // Redirige a la accion de Index del controlador Home
        public ActionResult LogOut()
        {
            Session["UserInfo"] = null;
            return RedirectToAction("Index", "Home");
        }




    }
}
