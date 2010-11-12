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
            //Lista de grupos del trabajo que tienen archivos subidos;
            DetalleTrabajoProfesorViewModel.ListGruposEntregado = ePortafolioDAO.Grupos.Where(g => g.TrabajoId == id && g.ArchivosGrupos.Count!=0).ToList();
            //Lista de grupos del trabajo que no tienen archivos subidos;
            DetalleTrabajoProfesorViewModel.ListGruposPendiente = ePortafolioDAO.Grupos.Where(g => g.TrabajoId == id && g.ArchivosGrupos.Count == 0).ToList();
            
            

            var CursoId = DetalleTrabajoProfesorViewModel.Trabajo.CursoId;
            var TrabajoId = id;

            //Obtiene los estudiantes del curso que no esten en ningun grupo para este trabajo
            DetalleTrabajoProfesorViewModel.ListAlumnosSinGrupo = ePortafolioDAO.Alumnos.Where(a =>
                        ePortafolioDAO.AlumnosCursos.Any(ac => ac.CursoId == CursoId && ac.AlumnoId == a.AlumnoId) &&
                        !ePortafolioDAO.AlumnosGrupos.Any(ag => ag.Grupo.Trabajo.TrabajoId == TrabajoId && ag.AlumnoId == a.AlumnoId)).ToList();

            foreach (Alumno alumno in DetalleTrabajoProfesorViewModel.ListAlumnosSinGrupo)
            {
                var Grupo = new Grupo();
                Grupo.AlumnosGrupos.Add(new AlumnosGrupo() { Alumno = alumno});
                Grupo.Nota="NA";

                DetalleTrabajoProfesorViewModel.ListGruposPendiente.Add(Grupo);
            }

            // Crea una vista para un Trabajo
            return View(DetalleTrabajoProfesorViewModel);
        }


        public ActionResult GradeAssignment(int TrabajoId, int GrupoId, String Origen, bool Editable)
        {
            CalificarTrabajoProfesorViewModel CalificarTrabajoProfesorViewModel = new CalificarTrabajoProfesorViewModel();

            var Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId == GrupoId);
            var Trabajo = ePortafolioDAO.Trabajos.FirstOrDefault(t => t.TrabajoId == TrabajoId);
            CalificarTrabajoProfesorViewModel.Grupo = Grupo;
            CalificarTrabajoProfesorViewModel.Trabajo = Trabajo;
            CalificarTrabajoProfesorViewModel.ListRubricas = ePortafolioDAO.Rubricas.Where(r => r.RubricasTrabajos.Any(rt=>rt.TrabajoId==TrabajoId)).ToList();
            CalificarTrabajoProfesorViewModel.ListResultados = ePortafolioDAO.ResultadosRubricaGrupos.Where(r => r.GrupoId == GrupoId).ToList();
            CalificarTrabajoProfesorViewModel.Origen = Origen;
            CalificarTrabajoProfesorViewModel.Editable= Editable;

            return View(CalificarTrabajoProfesorViewModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult GradeAssignment(int TrabajoId, int GrupoId,String Origen,bool Editable, FormCollection formValues)
        {
            var Grupo = ePortafolioDAO.Grupos.FirstOrDefault(g => g.GrupoId == GrupoId);
            
            if (Editable)
            {
                //Elimina todos los puntajes anteriores asignados
                ePortafolioDAO.ResultadosRubricaGrupos.DeleteAllOnSubmit(ePortafolioDAO.ResultadosRubricaGrupos.Where(rrg => rrg.GrupoId == GrupoId));

                Double Nota = 0;

                foreach (String Key in formValues)
                {
                    int CriterioSeleccionado = Convert.ToInt32(formValues[Key]);
                    ePortafolioDAO.ResultadosRubricaGrupos.InsertOnSubmit(new ResultadosRubricaGrupo { CriterioId = CriterioSeleccionado, GrupoId = GrupoId, RubricaId = Convert.ToInt32(Key) });
                    Nota += Convert.ToDouble(ePortafolioDAO.CriteriosRubricas.SingleOrDefault(cr => cr.CriterioId == CriterioSeleccionado).Valor);
                }
                
                var RubricasTrabajo = ePortafolioDAO.RubricasTrabajos.Where(r => r.TrabajoId == Grupo.TrabajoId);

                if (formValues.Count == RubricasTrabajo.Count()) //Existe una calificacion para cara critero de la rubrica, es posible dar una nota final
                    Grupo.Nota = Nota.ToString("F2");
                else if (formValues.Count != 0)
                    Grupo.Nota = "IN";

                ePortafolioDAO.SubmitChanges();
            }
            switch (Origen)
            {
                case "Details": return RedirectToAction("Details", new { id = Grupo.Trabajo.TrabajoId });
                case "Index": return RedirectToAction("Index");
            }
            return null;
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
            Trabajo.Instrucciones = formValues["trabajo.Instrucciones"];

            DateTime FechaInicio;
            if (DateTime.TryParse(formValues["FechaInicio"], out FechaInicio))
                Trabajo.FechaInicio = FechaInicio;
            else
                Trabajo.FechaInicio = null;

            DateTime FechaFin;
            if (DateTime.TryParse(formValues["FechaFin"], out FechaFin))
                Trabajo.FechaFin = FechaFin;
            else
                Trabajo.FechaFin = null;

            //Hace el commit del trabajo con los valores actualizados
            ePortafolioDAO.SubmitChanges();

            //Redirige a la accion Index del ProfessorController
            return RedirectToAction("Index", "Professor");
        }

        public ActionResult Grades(int TrabajoId,String Origen)
        {
            var Trabajo = ePortafolioDAO.Trabajos.SingleOrDefault(t => t.TrabajoId == TrabajoId);

            ReporteNotasTrabajoProfesorViewModel ReporteNotasTrabajoProfesorViewModel = new ReporteNotasTrabajoProfesorViewModel();

            ReporteNotasTrabajoProfesorViewModel.Trabajo = Trabajo;

            ReporteNotasTrabajoProfesorViewModel.ListAlumnoNotas = new List<AlumnoNota>();

            foreach(var grupo in ePortafolioDAO.Grupos.Where(g=>g.TrabajoId == TrabajoId))
            {
                foreach(var alumno in grupo.AlumnosGrupos)
                {
                    ReporteNotasTrabajoProfesorViewModel.ListAlumnoNotas.Add(new AlumnoNota(){Alumno=alumno.Alumno,Nota=grupo.Nota});
                }
            }

                        //Obtiene los estudiantes del curso que no esten en ningun grupo para este trabajo
            List<Alumno> ListAlumnosSinGrupo = ePortafolioDAO.Alumnos.Where(a =>
                        ePortafolioDAO.AlumnosCursos.Any(ac => ac.CursoId == Trabajo.CursoId && ac.AlumnoId == a.AlumnoId) &&
                        !ePortafolioDAO.AlumnosGrupos.Any(ag => ag.Grupo.Trabajo.TrabajoId == TrabajoId && ag.AlumnoId == a.AlumnoId)).ToList();

            foreach(var alumno in ListAlumnosSinGrupo)
            {
            ReporteNotasTrabajoProfesorViewModel.ListAlumnoNotas.Add(new AlumnoNota(){Alumno=alumno,Nota="NA"});
            }

            ReporteNotasTrabajoProfesorViewModel.ListAlumnoNotas.Sort(delegate(AlumnoNota a1, AlumnoNota a2) { return a1.Alumno.Nombre.CompareTo(a2.Alumno.Nombre); });

            ReporteNotasTrabajoProfesorViewModel.Origen = Origen;

            return View(ReporteNotasTrabajoProfesorViewModel);
        }
    }
}
