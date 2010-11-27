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
using ePortafolioMVC.Models.Repository;
using ePortafolioMVC.Models.Entities;

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
            StudentIndexViewModel StudentIndexViewModel = new StudentIndexViewModel();

            BEAlumno Alumno = ((BEAlumno)Session["ActualAlumno"]);
            BEPeriodo Periodo = ((BEPeriodo)Session["ActualPeriodo"]);

            List<BECurso> CursosMatriculados = RepositoryFactory.GetCursoRepository().GetCursosMatriculados(Alumno.AlumnoId, Periodo.PeriodoId);
            List<BEGrupo> GruposTrabajosEntregados = RepositoryFactory.GetGrupoRepository().GetGruposTrabajosEntregados(Alumno.AlumnoId, Periodo.PeriodoId);
            List<BEGrupo> GruposTrabajosPendientes = RepositoryFactory.GetGrupoRepository().GetGruposTrabajosPendientes(Alumno.AlumnoId, Periodo.PeriodoId);

            StudentIndexViewModel.GruposTrabajosEntregados = GruposTrabajosEntregados;
            StudentIndexViewModel.GruposTrabajosPendientes = GruposTrabajosPendientes;
            StudentIndexViewModel.ActualAlumno = Alumno;

            CursosMatriculados = CursosMatriculados.OrderBy(x => x.Codigo).ToList();

            foreach (BECurso Curso in CursosMatriculados)
            {
                List<BETrabajo> Trabajos = RepositoryFactory.GetTrabajoRepository().GetTrabajosCurso(Curso.CursoId, Periodo.PeriodoId);
                Trabajos = Trabajos.OrderBy(x => x.Nombre).ToList();
                StudentIndexViewModel.TrabajosCurso.Add(Curso, Trabajos);
            }

            //Crea la vista para los Cursos matriculados
            return View(StudentIndexViewModel);
        }

        //
        // GET: /Student/Details/5
        // Muestra la pagina de detalles de un trabajo de un alumno. Muestra los integrntes, personas que pueden ser
        // incorporadas, los archivos subidos y las instrucciones
        // Crea la vista para el GrupoTrabajoViewModel
        public ActionResult Details(int TrabajoId)
        {
            BEAlumno Alumno = ((BEAlumno)Session["ActualAlumno"]);
            BEPeriodo Periodo = ((BEPeriodo)Session["ActualPeriodo"]);

            BEGrupo Grupo = RepositoryFactory.GetGrupoRepository().GetGrupoAlumno(TrabajoId, Alumno.AlumnoId);
            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);

            if (Grupo == null && Trabajo.EsGrupal == false)
            {
                return RedirectToAction("GroupCreate", new { TrabajoId = TrabajoId });
            }

            BECurso Curso = RepositoryFactory.GetCursoRepository().GetCurso(Trabajo.Curso.CursoId, Periodo.PeriodoId);
            BESeccion Seccion = RepositoryFactory.GetSeccionRepository().GetSeccionCursoAlumno(Alumno.AlumnoId, Curso.CursoId, Periodo.PeriodoId);
            BEResultadoPrograma ResultadoPrograma = RepositoryFactory.GetResultadoProgramaRepository().GetResultadoPrograma(Curso.CursoId, Periodo.PeriodoId);

            List<BEAlumno> AlumnosGrupo = null;
            List<BEAlumno> AlumnosSinGrupo = null;
            List<BEArchivo> ArchivosGrupo = null;
            SelectList AlumnosSinGrupoSelectList = null;

            if (Grupo != null)
            {
                AlumnosGrupo = RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId);
                AlumnosSinGrupo = RepositoryFactory.GetAlumnoRepository().GetAlumnosSinGrupoSeccionTrabajo(Trabajo.TrabajoId, Seccion.SeccionId);
                ArchivosGrupo = RepositoryFactory.GetArchivoRepository().GetArchivosGrupo(Grupo.GrupoId);

                AlumnosGrupo = AlumnosGrupo.OrderBy(x => x.Nombre).ToList();
                AlumnosSinGrupo = AlumnosSinGrupo.OrderBy(x => x.Nombre).ToList();
                ArchivosGrupo = ArchivosGrupo.OrderBy(x => x.Nombre).ToList();

                AlumnosSinGrupoSelectList = new SelectList(AlumnosSinGrupo, "AlumnoId", "Nombre");
            }

            StudentDetailsViewModel StudentDetailsViewModel = new StudentDetailsViewModel();

            StudentDetailsViewModel.Alumno = Alumno;
            StudentDetailsViewModel.Grupo = Grupo;
            StudentDetailsViewModel.Trabajo = Trabajo;
            StudentDetailsViewModel.Curso = Curso;
            StudentDetailsViewModel.Seccion = Seccion;
            StudentDetailsViewModel.ResultadoPrograma = ResultadoPrograma;

            StudentDetailsViewModel.AlumnosGrupo = AlumnosGrupo;
            StudentDetailsViewModel.ArchivosGrupo = ArchivosGrupo;
            StudentDetailsViewModel.AlumnosSinGrupo = AlumnosSinGrupoSelectList;
            

            if (StudentDetailsViewModel.AlumnosSinGrupo != null && StudentDetailsViewModel.AlumnosSinGrupo.Count() > 0)
                StudentDetailsViewModel.AlumnosSinGrupo.First().Selected = true;

            return View(StudentDetailsViewModel);
        }

        //
        // POST: /Student/AddStudent/
        // Realiza la incorporacion de un estudiante al grupo
        // Redirige a la accion de Details
        [HttpPost]
        public ActionResult AddStudent(int TrabajoId, FormCollection formValues)
        {
            RepositoryFactory.GetGrupoRepository().AddAlumnoGrupo(Convert.ToInt32(formValues["GrupoId"]), formValues["AlumnoId"]);

            return RedirectToAction("Details", new { TrabajoId = TrabajoId });
        }

        //
        // POST: /Student/UploadFile/
        // Realiza la subida de un archivo para el grupo
        // Redirige a la accion de Details
        [HttpPost]
        public ActionResult UploadFile(int TrabajoId, FormCollection formValues)
        {

            //Si existen multiples archivos, se procesa cada uno por separado
            foreach (string file in Request.Files)
            {

                BEAlumno Alumno = ((BEAlumno)Session["ActualAlumno"]);
                BEGrupo Grupo = RepositoryFactory.GetGrupoRepository().GetGrupoAlumno(TrabajoId, Alumno.AlumnoId);
                BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);
                BECurso Curso = RepositoryFactory.GetCursoRepository().GetCurso(Trabajo.Curso.CursoId, Trabajo.Periodo.PeriodoId);

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
                String FileVirtualPathOnDisk = "Files\\#CICLO\\#CURSO\\#TRABAJO\\#GRUPO\\".Replace("#CICLO", "2010-II").Replace("#CURSO", Trabajo.Curso.CursoId.ToString()).Replace("#TRABAJO", Trabajo.Nombre).Replace("#GRUPO", Grupo.GrupoId.ToString());
                //Directorio fisica del archivo
                String FilePathOnDisk = "#BASEDIR#FILEVIRTUALPATH".Replace("#BASEDIR", Server.MapPath("~/")).Replace("#FILEVIRTUALPATH", FileVirtualPathOnDisk);
                //Ruta virtual del archivo
                String CompleteFileVirtualPathOnDisk = "\\" + FileVirtualPathOnDisk + FileGUIDName + "_" + FileNameOnDisk;
                //Ruta fisica del archivo
                String CompleteFilePathPathOnDisk = FilePathOnDisk + FileGUIDName + "_" + FileNameOnDisk;
                //Crea el directorio fisico del archivo
                Directory.CreateDirectory(FilePathOnDisk);
                //Graba el archivo en la ruta fisica del archivo
                hpf.SaveAs(CompleteFilePathPathOnDisk);
                //Crea el archivo para insertar

                BEArchivo Archivo = new BEArchivo
                    {
                        FechaSubido = DateTime.Now,
                        Nombre = FileNameOnDisk,
                        Ruta = CompleteFileVirtualPathOnDisk,
                        Alumno = Alumno
                    };

                RepositoryFactory.GetArchivoRepository().AddArchivo(Grupo.GrupoId, Archivo);

                List<BEAlumno> AlumnosGrupo = RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId);

                foreach (BEAlumno AlumnoGrupo in AlumnosGrupo)
                {
                    String Mensaje = "Estimado #NOMBRE, </br>" +
                                     "Se ha agregado el archivo <b>#ARCHIVO</b> al trabajo <b>#TRABAJO</b> del curso <b>#CURSO</b>";
                    Mensaje = Mensaje.Replace("#NOMBRE", AlumnoGrupo.Nombre);
                    Mensaje = Mensaje.Replace("#ARCHIVO", FileNameOnDisk);
                    Mensaje = Mensaje.Replace("#TRABAJO", Trabajo.Nombre);
                    Mensaje = Mensaje.Replace("#CURSO", Curso.Nombre);

                    EmailHelper.SendMail(AlumnoGrupo.CorreoElectronico, "ePSE - Archivo agregado", Mensaje);
                }
            }

            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { TrabajoId = TrabajoId });
        }

        //
        // GET: /Student/GroupCreate/5
        // Crea un grupo para el trabajo seleccionado
        // Redirige a la accion de Details
        public ActionResult GroupCreate(int TrabajoId)
        {
            BEAlumno Alumno = ((BEAlumno)Session["ActualAlumno"]);

            RepositoryFactory.GetGrupoRepository().CreateGrupo(TrabajoId, Alumno.AlumnoId);

            return RedirectToAction("Details", new { TrabajoId = TrabajoId });
        }

        //
        // GET: /Student/DeleteStudent/
        // Elimina un estudiante del grupo
        // Redirige a la accion de Details
        public ActionResult DeleteStudent(int TrabajoId, int GrupoId, String AlumnoId)
        {
            RepositoryFactory.GetGrupoRepository().DeleteAlumnoGrupo(GrupoId, AlumnoId);

            return RedirectToAction("Details", new { TrabajoId = TrabajoId });
        }

        //
        // GET: /Student/MakeLeader/
        // Delega el lider a un estudiante del grupo
        // Redirige a la accion de Details
        public ActionResult MakeLeader(int GrupoId, String AlumnoId, int TrabajoId)
        {
            RepositoryFactory.GetGrupoRepository().SetLiderGrupo(GrupoId, AlumnoId);

            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return RedirectToAction("Details", new { TrabajoId = TrabajoId });
        }

        //
        // GET: /Student/WithdrawStudent/
        // Retira al alumno del curso
        // Redirige a la accion de Index
        public ActionResult WithdrawStudent(int GrupoId, String AlumnoId)
        {
            RepositoryFactory.GetGrupoRepository().DeleteAlumnoGrupo(GrupoId, AlumnoId);

            //Redirecciona a la accion Index de Student
            return RedirectToAction("Index");

        }

        //
        // GET: /Student/DeleteGroup/
        // Elimina el grupo de trabajo
        // Redirige a la accion de Index
        public ActionResult DeleteGroup(int GrupoId)
        {
            //
            // TODO: Eliminar los archivos del disco y depurar la tabla de Archivos Eliminados
            //
            RepositoryFactory.GetArchivoRepository().DeleteArchivosGrupo(GrupoId);
            RepositoryFactory.GetGrupoRepository().DeleteAlumnosGrupo(GrupoId);
            RepositoryFactory.GetGrupoRepository().DeleteGrupo(GrupoId);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteFile(int TrabajoId, int ArchivoId)
        {
            //
            // TODO: Eliminar los archivos del disco
            //

            BEArchivo Archivo = RepositoryFactory.GetArchivoRepository().GetArchivo(ArchivoId);

            RepositoryFactory.GetArchivoRepository().DeleteArchivo(ArchivoId);

            return RedirectToAction("Details", new { TrabajoId = TrabajoId });
        }

        public ActionResult History()
        {
            BEAlumno Alumno = ((BEAlumno)Session["ActualAlumno"]);

            StudentHistoryViewModel StudentHistoryViewModel = new StudentHistoryViewModel();

            List<BEPeriodo> Periodos = new List<BEPeriodo>();
            List<BECurso> Cursos = new List<BECurso>();
            List<BEGrupo> Grupos = new List<BEGrupo>();

            List<BETrabajo> Trabajos = RepositoryFactory.GetTrabajoRepository().GetTrabajosHistoricoAlumno(Alumno.AlumnoId);
            Dictionary<BEGrupo, List<BEAlumno>> AlumnosGrupo = new Dictionary<BEGrupo, List<BEAlumno>>();

            foreach (BETrabajo Trabajo in Trabajos)
            {
                if (!Periodos.Any(p => p.PeriodoId == Trabajo.Periodo.PeriodoId))
                    Periodos.Add(RepositoryFactory.GetPeriodoRepository().GetGetPeriodoNoFK(Trabajo.Periodo.PeriodoId));
                if (!Cursos.Any(c => c.CursoId == Trabajo.Curso.CursoId))
                    Cursos.Add(RepositoryFactory.GetCursoRepository().GetCursoNoFK(Trabajo.Curso.CursoId, Trabajo.Periodo.PeriodoId));
                if (!Grupos.Any(g => g.Trabajo.TrabajoId == Trabajo.TrabajoId))
                    Grupos.Add(RepositoryFactory.GetGrupoRepository().GetGrupoAlumno(Trabajo.TrabajoId, Alumno.AlumnoId));
            }

            foreach (BEGrupo Grupo in Grupos)
            {
                AlumnosGrupo.Add(Grupo, RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId));
            }

            StudentHistoryViewModel.ActualAlumno = Alumno;
            StudentHistoryViewModel.Periodos = Periodos.OrderByDescending(x => x.PeriodoId).ToList();
            StudentHistoryViewModel.Cursos = Cursos.OrderBy(x => x.Codigo).ToList();
            StudentHistoryViewModel.Trabajos = Trabajos.OrderBy(x => x.Nombre).ToList(); ;
            StudentHistoryViewModel.Grupos = Grupos;
            StudentHistoryViewModel.AlumnosGrupo = AlumnosGrupo;

            return View(StudentHistoryViewModel);
        }

    }
}
