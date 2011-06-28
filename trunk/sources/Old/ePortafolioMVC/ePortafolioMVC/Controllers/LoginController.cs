using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;
using ePortafolioMVC.Models.Repository;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Controllers
{
    public class LoginController : BaseController
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
                    UserInfo UserInfo = new UserInfo();
                    UserInfo.Codigo = userAutentication.User;

                    Session["ActualPeriodo"] = RepositoryFactory.GetPeriodoRepository().GetGetPeriodoActual();
                    Session["VistaPeriodo"] = RepositoryFactory.GetPeriodoRepository().GetGetPeriodoActual();

                    if (RepositoryFactory.GetAlumnoRepository().GetAlumno(userAutentication.User) != null)
                    {
                        UserInfo.Nombre = RepositoryFactory.GetAlumnoRepository().GetAlumno(userAutentication.User).Nombre;
                        UserInfo.Rol = RolDescription.Estudiante;
                        Session["ActualAlumno"] = RepositoryFactory.GetAlumnoRepository().GetAlumno(userAutentication.User);

                        List<BEPeriodo> PeriodosEstudiados = RepositoryFactory.GetPeriodoRepository().GetPeriodosEstudiados(UserInfo.Codigo);
                        if (!PeriodosEstudiados.Any(x => x.PeriodoId == ((BEPeriodo)Session["ActualPeriodo"]).PeriodoId))
                            PeriodosEstudiados.Add((BEPeriodo)Session["ActualPeriodo"]);

                        Session["PeriodosEstudiados"] = PeriodosEstudiados.OrderByDescending(x => x.PeriodoId).ToList();

                        Session["TrabajosPendientes"] = RepositoryFactory.GetTrabajoRepository().GetTrabajosPendientes(UserInfo.Codigo, ((BEPeriodo)Session["ActualPeriodo"]).PeriodoId);
                    }
                    else if (RepositoryFactory.GetProfesorRepository().GetProfesor(userAutentication.User) != null)
                    {
                        UserInfo.Nombre = RepositoryFactory.GetProfesorRepository().GetProfesor(userAutentication.User).Nombre;
                        UserInfo.Rol = RolDescription.Profesor;
                        Session["ActualProfesor"] = RepositoryFactory.GetProfesorRepository().GetProfesor(userAutentication.User);

                        List<BEPeriodo> PeriodosEvaluados = RepositoryFactory.GetPeriodoRepository().GetPeriodosEvaluados(UserInfo.Codigo);
                        if (!PeriodosEvaluados.Any(x => x.PeriodoId == ((BEPeriodo)Session["ActualPeriodo"]).PeriodoId))
                            PeriodosEvaluados.Add((BEPeriodo)Session["ActualPeriodo"]);

                        Session["PeriodosEvaluados"] = PeriodosEvaluados.OrderByDescending(x => x.PeriodoId).ToList();
                        Session["TrabajosPendientes"] = RepositoryFactory.GetTrabajoRepository().GetTrabajosPendientes(UserInfo.Codigo, ((BEPeriodo)Session["ActualPeriodo"]).PeriodoId);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                    Session["UserInfo"] = UserInfo;
                    
                    switch (UserInfo.Rol)
                    {
                        case RolDescription.Profesor: return RedirectToAction("Index", "Professor");
                        case RolDescription.Estudiante: return RedirectToAction("Index", "Student");
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
            return RedirectToAction("Index","Login");
        }




    }
}
