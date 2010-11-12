using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;
using ePortafolioMVC.ViewModels;

namespace ePortafolioMVC.Controllers
{
    public class ProfessorController : Controller
    {
        ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();
        
        //
        // GET: /Professor/
        // Muestra la pagina principal de Professor
        // Crea una vista para los Cursos del profesor
        public ActionResult Index()
        {
            var ProfesorId = ((UserInfo)Session["UserInfo"]).Codigo;
            //Devuelve los cursos para el profesor registrado
            var Cursos = ePortafolioDAO.Cursos.Where(c => c.ProfesorId.ToString() == ProfesorId).ToList();

            return View(Cursos);
        }

        //
        // GET: /Professor/
        // Muestra la pagina para editar las características de un trabajo
        // Crea una vista para un Trabajo
        public ActionResult Edit(int id)
        {
            //Devuelve el TrabajoViewModel para el trabajo seleccionado
            var Trabajo = new TrabajoViewModel(ePortafolioDAO.Trabajos.Single(t => t.TrabajoId == id));

            return View(Trabajo);
        }

        //
        // POST: /Professor/Edit/
        // Aplica las modificaciones realizadas en la página de Edit
        // Redirige a la accion de Index
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formValues)
       {
            //Devuelve el trabajo seleccionado
            var Trabajo = ePortafolioDAO.Trabajos.Single(t => t.TrabajoId == id);
            //Actualiza el trabajo seleccionado con los datos del formulario
            UpdateModel(Trabajo);

            //Actualiza el trabajo seleccionado con los datos del formulario que no se actualizaron con UpdateModel
            Trabajo.Nombre = formValues["trabajo.Nombre"];
            Trabajo.Instrucciones = formValues["trabajo.Instrucciones"];
            
            DateTime FechaInicio;
            if(DateTime.TryParse(formValues["trabajo.FechaInicio"], out FechaInicio))
                Trabajo.FechaInicio = FechaInicio;
            else
                Trabajo.FechaInicio = null;
            
            DateTime FechaFin;
            if (DateTime.TryParse(formValues["trabajo.FechaFin"], out FechaFin))
                Trabajo.FechaFin = FechaFin;
            else
                Trabajo.FechaFin = null;

            //Hace el commit del trabajo con los valores actualizados
            ePortafolioDAO.SubmitChanges();

            //Redirige a la accion Index del ProfessorController
            return RedirectToAction("Index", "Professor");
        }
    }
}
