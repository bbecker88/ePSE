using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;
using ePortafolioMVC.ViewModels;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

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
            //Obtiene el ID del profesor
            var ProfesorId = ((UserInfo)Session["UserInfo"]).Codigo;
            //Devuelve los cursos para el profesor registrado
            var Cursos = ePortafolioDAO.Cursos.Where(c => c.ProfesorId.ToString() == ProfesorId).ToList();
            // Crea una vista para los Cursos del profesor
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
            // Crea una vista para un Trabajo
            return View(Trabajo);
        }

        //
        // GET: /Professor/
        // Muestra la pagina de detalles de un trabajo
        // Crea una vista para un Trabajo
        public ActionResult Details(int id)
        {
            var DetalleTrabajoProfesorViewModel = new DetalleTrabajoProfesorViewModel();
            DetalleTrabajoProfesorViewModel.Trabajo = ePortafolioDAO.Trabajos.Single(t => t.TrabajoId == id);
            DetalleTrabajoProfesorViewModel.ListGrupos = ePortafolioDAO.Grupos.Where(g => g.TrabajoId == id).ToList();
            var CursoId = DetalleTrabajoProfesorViewModel.Trabajo.CursoId;
            var TrabajoId = id;

            //Obtiene los estudiantes del curso que no esten en ningun grupo para este trabajo
            DetalleTrabajoProfesorViewModel.ListAlumnosSinGrupo = ePortafolioDAO.Alumnos.Where(a =>
                        ePortafolioDAO.AlumnosCursos.Any(ac => ac.CursoId == CursoId && ac.AlumnoId == a.AlumnoId) &&
                        !ePortafolioDAO.AlumnosGrupos.Any(ag => ag.Grupo.Trabajo.TrabajoId == TrabajoId && ag.AlumnoId == a.AlumnoId)).ToList();

            // Crea una vista para un Trabajo
            return View(DetalleTrabajoProfesorViewModel);
        }

        public ActionResult DownloadFiles(int GrupoId)
        {
            var Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId == GrupoId);
            
            String zipFilename = Path.GetTempPath() + Guid.NewGuid() + ".zip";
            String integrantesFilename = Path.GetTempPath() + Guid.NewGuid() + ".txt";

            StreamWriter streamWriter = System.IO.File.CreateText(integrantesFilename);

            foreach (var AlumnoGrupo in Grupo.AlumnosGrupos)
            {
                if(AlumnoGrupo.EsLider)
                    streamWriter.WriteLine(AlumnoGrupo.Alumno.Nombre + " (LIDER)");
                else
                streamWriter.WriteLine(AlumnoGrupo.Alumno.Nombre);
            }
            streamWriter.Flush();
            streamWriter.Close();

            ZipFile z = ZipFile.Create(zipFilename);
            z.BeginUpdate();
            foreach (var ArchivoGrupo in Grupo.ArchivosGrupos)
            {
                z.Add(Server.MapPath("~") + ArchivoGrupo.Archivo.Ruta,ArchivoGrupo.Archivo.Nombre);
            }
            z.Add(integrantesFilename,"Integrantes.txt");
            z.CommitUpdate();
            z.Close();

            var FilePathResult = new FilePathResult(zipFilename, ".zip");
            FilePathResult.FileDownloadName="archivos_grupo.zip";
            return FilePathResult;
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
            if (DateTime.TryParse(formValues["trabajo.FechaInicio"], out FechaInicio))
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
