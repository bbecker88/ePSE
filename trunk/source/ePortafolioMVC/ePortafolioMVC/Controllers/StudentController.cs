using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;
using ePortafolioMVC.ViewModels;
using System.IO;
using System.Security.AccessControl;
using ePortafolioMVC.Helpers;

namespace ePortafolioMVC.Controllers
{
    public class StudentController : Controller
    {
        ePortafolioDBDataContext ePortafolioDAO = new ePortafolioDBDataContext();

        //
        // GET: /Student/
        // Muestra la pagina principal de Student
        // Crea una vista para Cursos
        public ActionResult Index()
        {
            //Obtiene el ID del estudiante registrado
            var EstudianteId = ((UserInfo)Session["UserInfo"]).Codigo;
            //Obtiene los cursos matriculados del alumno
            var Cursos = ePortafolioDAO.Cursos.Where(c => c.AlumnosCursos.Any(a => a.AlumnoId.ToString() == EstudianteId)).ToList();
            //Crea la vista para los Cursos matriculados
            return View(Cursos);
        }

        //
        // Devuelve una lista de todos los alumnos que esten en el curso pro no esten en ningun grupo
        private SelectList GetSelectListSinGrupo(String CursoId, String TrabajoId)
        {
            //Obtiene los estudiantes del curso que no esten en ningun grupo para este trabajo
            var ListaAlumnosSinGrupo = ePortafolioDAO.Alumnos.Where(a =>
                        ePortafolioDAO.AlumnosCursos.Any(ac => ac.CursoId == CursoId && ac.AlumnoId == a.AlumnoId) &&
                        !ePortafolioDAO.AlumnosGrupos.Any(ag => ag.Grupo.Trabajo.TrabajoId.ToString() == TrabajoId && ag.AlumnoId == a.AlumnoId)).ToList();
            //Crea el SelectList con value=AlumnoId y data=Nombre
            return new SelectList(ListaAlumnosSinGrupo, "AlumnoId", "Nombre");
        }

        //
        // GET: /Student/Details/5
        // Muestra la pagina de detalles de un trabajo de un alumno. Muestra los integrntes, personas que pueden ser
        // incorporadas, los archivos subidos y las instrucciones
        // Crea la vista para el GrupoTrabajoViewModel
        public ActionResult Details(String id)
        {
            var TrabajoId = id;
            //Obtiene el ID del estudiante registrado
            var EstudianteId = ((UserInfo)Session["UserInfo"]).Codigo;
            //Crea el GrupoTrabajoViewModel con el Grupo en el que se encuentra el estudiante en este trabajo. 
            //Tambien se incluye la informacion del Trabajo
            GrupoTrabajoViewModel GrupoTrabajoViewModel = new GrupoTrabajoViewModel
            {
                Grupo = ePortafolioDAO.Grupos.SingleOrDefault(g => g.TrabajoId.ToString() == TrabajoId && g.AlumnosGrupos.Any(ag => ag.AlumnoId.ToString() == EstudianteId)),
                Trabajo = ePortafolioDAO.Trabajos.SingleOrDefault(t => t.TrabajoId.ToString() == TrabajoId)
            };
            //Se incorpora la lista de alumnos sin grupo al GrupoTrabajoViewModel 
            GrupoTrabajoViewModel.AlumnosSinGrupo = GetSelectListSinGrupo(GrupoTrabajoViewModel.Trabajo.CursoId, GrupoTrabajoViewModel.Trabajo.TrabajoId.ToString());
            //Si tiene Grupo
            if (GrupoTrabajoViewModel.Grupo != null && GrupoTrabajoViewModel.Grupo.ArchivosGrupos != null)
            {
                //Se incorpora la lista de archivos del grupo al GrupoTrabajoViewModel 
                GrupoTrabajoViewModel.Archivos = ePortafolioDAO.Archivos.Where(a => ePortafolioDAO.ArchivosGrupos.Any(ag => ag.ArchivoId == a.ArchivoId && ag.GrupoId==GrupoTrabajoViewModel.Grupo.GrupoId)).ToList();
            }
            //Si existen alumnos sin grupo se marco como seleccionado el primero
            if (GrupoTrabajoViewModel.AlumnosSinGrupo.Count() > 0)
                GrupoTrabajoViewModel.AlumnosSinGrupo.First().Selected = true;
            //Si existe un Grupo se incuye al GrupoTrabajoViewModel la lista de AlumnosGrupo
            if (GrupoTrabajoViewModel.Grupo != null)
                GrupoTrabajoViewModel.AlumnosGrupo = ePortafolioDAO.AlumnosGrupos.Where(ag => ag.GrupoId == GrupoTrabajoViewModel.Grupo.GrupoId).ToList();
            //Se crea la vista para el GrupoTrabajoViewModel
            return View(GrupoTrabajoViewModel);
        }

        //
        // POST: /Student/AddStudent/
        // Realiza la incorporacion de un estudiante al grupo
        // Redirige a la accion de Details
        [HttpPost]
        public ActionResult AddStudent(String id, FormCollection formValues)
        {
            //Crea el alumno a insertar
            AlumnosGrupo InsertStudent = new AlumnosGrupo { AlumnoId = formValues["AlumnoId"], EsLider = false, GrupoId = Convert.ToInt32(formValues["GrupoId"]) };
            ePortafolioDAO.AlumnosGrupos.InsertOnSubmit(InsertStudent);
            //Hace el commit de del ingrego del alumno
            ePortafolioDAO.SubmitChanges();
            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { id = id });
        }

        //
        // POST: /Student/AddStudent/
        // Realiza la subida de un archivo para el grupo
        // Redirige a la accion de Details
        [HttpPost]
        public ActionResult UploadFile(String id, FormCollection formValues)
        {
            
            //Si existen multiples archivos, se procesa cada uno por separado
            foreach (string file in Request.Files)
            {
                //Obtiene el codigo del estudiante registrado
                var EstudianteId = ((UserInfo)Session["UserInfo"]).Codigo;
                //Obtiene el Trabajo actual
                var Trabajo = ePortafolioDAO.Trabajos.Single(t => t.TrabajoId.ToString() == id);
                //Obtiene el Grupo para el cual se esta subiendo el archivo
                var Grupo = ePortafolioDAO.Grupos.Single(g => g.TrabajoId.ToString() == id && g.AlumnosGrupos.Any(ag => ag.AlumnoId == EstudianteId));
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                //
                // TODO: Incluir ciclo del trabajo, modificar el GUID
                //Nombre del archivo
                String FileNameOnDisk = "#FILENAME".Replace("#FILENAME", System.IO.Path.GetFileName(hpf.FileName));
                //GUID del archivo
                String FileGUIDName = "#GUID".Replace("#GUID", "GUID");
                //Directorio virtual del archivo
                String FileVirtualPathOnDisk = "Files\\#CICLO\\#CURSO\\#TRABAJO\\#GRUPO\\".Replace("#CICLO", "2010-II").Replace("#CURSO", Trabajo.Curso.CursoId).Replace("#TRABAJO", Trabajo.Nombre).Replace("#GRUPO", Grupo.GrupoId.ToString());
                //Directorio fisica del archivo
                String FilePathOnDisk = "#BASEDIR#FILEVIRTUALPATH".Replace("#BASEDIR", Server.MapPath("~/")).Replace("#FILEVIRTUALPATH", FileVirtualPathOnDisk);
                //Ruta virtual del archivo
                String CompleteFileVirtualPathOnDisk = "\\"+FileVirtualPathOnDisk + FileGUIDName + "_" + FileNameOnDisk;
                //Ruta fisica del archivo
                String CompleteFilePathPathOnDisk = FilePathOnDisk + FileGUIDName + "_" + FileNameOnDisk;
                //Crea el directorio fisico del archivo
                Directory.CreateDirectory(FilePathOnDisk);
                //Graba el archivo en la ruta fisica del archivo
                hpf.SaveAs(CompleteFilePathPathOnDisk);
                //
                // TODO: Incluir el id del tipo de archivo
                //Crea el archivo para insertar
                Archivo archivo = new Archivo() { Nombre = FileNameOnDisk, Extension = Path.GetExtension(hpf.FileName), Ruta = CompleteFileVirtualPathOnDisk, TipoArchivoId = 1, AlumnoId = EstudianteId, FechaSubida = DateTime.Now };
                //Crea el archivoGrupo para insertar
                ArchivosGrupo archivoGrupo = new ArchivosGrupo() { Archivo = archivo, GrupoId = Grupo.GrupoId };
                //Marca el archivoGurpo para insercion
                ePortafolioDAO.ArchivosGrupos.InsertOnSubmit(archivoGrupo);
                //Hace el commit de los cambios
                ePortafolioDAO.SubmitChanges();

                foreach (var alumnoGrupo in Grupo.AlumnosGrupos)
                {
                    String Mensaje = "Estimado #NOMBRE, </br>"+
                                     "Se ha agregado el archivo <b>#ARCHIVO</b> al trabajo <b>#TRABAJO</b> del curso <b>#CURSO</b>";
                    Mensaje = Mensaje.Replace("#NOMBRE", alumnoGrupo.Alumno.Nombre);
                    Mensaje = Mensaje.Replace("#ARCHIVO", FileNameOnDisk);
                    Mensaje = Mensaje.Replace("#TRABAJO", Trabajo.Nombre);
                    Mensaje = Mensaje.Replace("#CURSO", Trabajo.Curso.Nombre);

                    EmailHelper.SendMail(alumnoGrupo.Alumno.Mail, "ePSE - Archivo agregado", Mensaje);
                }
            }

            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { id = id });
        }

        //
        // GET: /Student/GroupCreate/5
        // Crea un grupo para el trabajo seleccionado
        // Redirige a la accion de Details
        public ActionResult GroupCreate(int id)
        {
            var TrabajoId = id;
            //Obtiene el ID del estudiante registrado
            var EstudianteId = ((UserInfo)Session["UserInfo"]).Codigo;
            //Obtiene el trabajo actual
            var Trabajo = ePortafolioDAO.Trabajos.SingleOrDefault(t => t.TrabajoId == TrabajoId);
            //Crea el grupo a insertar
            var GrupoInsertar = new Grupo() { TrabajoId = TrabajoId };
            //Agrega el estudiante actual como lider
            GrupoInsertar.AlumnosGrupos.Add(new AlumnosGrupo { AlumnoId = EstudianteId, EsLider = true });
            //Marca GrupoInsertar para agregar
            Trabajo.Grupos.Add(GrupoInsertar);
            //Agrega el GrupoInsertar
            ePortafolioDAO.SubmitChanges();
            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { id = TrabajoId });
        }

        //
        // GET: /Student/DeleteStudent/
        // Elimina un estudiante del grupo
        // Redirige a la accion de Details
        public ActionResult DeleteStudent(String GrupoId, String TrabajoId, String AlumnoId)
        {
            //Obtiene el estudiante
            var Student = ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.AlumnoId.ToString() == AlumnoId && ag.GrupoId.ToString() == GrupoId);
            //Marca el estudiante para ser eliminado
            ePortafolioDAO.AlumnosGrupos.DeleteOnSubmit(Student);
            //Elimina el estudiante
            ePortafolioDAO.SubmitChanges();
            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { id = TrabajoId });
        }

        //
        // GET: /Student/MakeLeader/
        // Delega el lider a un estudiante del grupo
        // Redirige a la accion de Details
        public ActionResult MakeLeader(String GrupoId, String AlumnoId, String TrabajoId)
        {
            //Obtiene el lider actual y le quita el liderato
            ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId.ToString() == GrupoId && ag.EsLider == true).EsLider = false;
            //Asigna el liderato al alumno actual
            ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId.ToString() == GrupoId && ag.AlumnoId.ToString() == AlumnoId).EsLider = true;
            //Guarda los cambios
            ePortafolioDAO.SubmitChanges();
            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { id = TrabajoId });
        }

        //
        // GET: /Student/WithdrawStudent/
        // Retira al alumno del curso
        // Redirige a la accion de Index
        public ActionResult WithdrawStudent(String GrupoId, String AlumnoId)
        {
            //Busca y marca el estudiante para ser eliminado
            ePortafolioDAO.AlumnosGrupos.DeleteOnSubmit(ePortafolioDAO.AlumnosGrupos.SingleOrDefault(ag => ag.GrupoId.ToString() == GrupoId && ag.AlumnoId.ToString() == AlumnoId));
            //Elimina el estudiante del grupo
            ePortafolioDAO.SubmitChanges();
            //Redirecciona a la accion Index de Student
            return RedirectToAction("Index");

        }

        //
        // GET: /Student/DeleteGroup/
        // Elimina el grupo de trabajo
        // Redirige a la accion de Index
        public ActionResult DeleteGroup(String GrupoId)
        {
            //
            // TODO: Eliminar los archivos del disco y depurar la tabla de Archivos Eliminados
            //

            //Marca los Archivos a ser eliminados
            ePortafolioDAO.Archivos.DeleteAllOnSubmit(ePortafolioDAO.Archivos.Where(a => ePortafolioDAO.ArchivosGrupos.Any(ag => ag.GrupoId.ToString() == GrupoId && ag.ArchivoId==a.ArchivoId)));
            //Marca los ArchivosGrupos a ser eliminados
            ePortafolioDAO.ArchivosGrupos.DeleteAllOnSubmit(ePortafolioDAO.ArchivosGrupos.Where(ag => ag.GrupoId.ToString() == GrupoId));
            //Marca los AlumnosGrupos a ser eliminados
            ePortafolioDAO.AlumnosGrupos.DeleteAllOnSubmit(ePortafolioDAO.AlumnosGrupos.Where(ag => ag.GrupoId.ToString() == GrupoId));
            //Marca el Grupo a ser eliminados
            ePortafolioDAO.Grupos.DeleteOnSubmit(ePortafolioDAO.Grupos.SingleOrDefault(g => g.GrupoId.ToString() == GrupoId));
            //Guarda los cambios
            ePortafolioDAO.SubmitChanges();
            //Redirecciona a la accion Index de Student
            return RedirectToAction("Index");
        }

    }
}
