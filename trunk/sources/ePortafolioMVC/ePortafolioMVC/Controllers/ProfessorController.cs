using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models;
using ePortafolioMVC.ViewModels;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ePortafolioMVC.Models.Repository;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Controllers
{
    public class ProfessorController : Controller
    {


        //
        // GET: /Professor/
        // Muestra la pagina principal de Professor
        // Crea una vista para los Cursos del profesor
        public ActionResult Index()
        {
            BEProfesor Profesor = ((BEProfesor)Session["ActualProfesor"]);
            BEPeriodo Periodo = ((BEPeriodo)Session["ActualPeriodo"]);
            List<BECurso> Cursos = RepositoryFactory.GetCursoRepository().GetCursosDictados(Profesor.ProfesorId, Periodo.PeriodoId);

            Cursos = Cursos.OrderBy(x => x.Codigo).ToList();

            ProfessorIndexViewModel ProfessorIndexViewModel = new ProfessorIndexViewModel() { ActualProfesor = Profesor };

            foreach (BECurso Curso in Cursos)
            {
                List<BETrabajo> Trabajos = RepositoryFactory.GetTrabajoRepository().GetTrabajosCurso(Curso.CursoId, Periodo.PeriodoId);
                Trabajos = Trabajos.OrderBy(x => x.Nombre).ToList();
                ProfessorIndexViewModel.TrabajosCurso.Add(Curso, Trabajos);
            }
            // Crea la Vista para el ViewModel 
            return View(ProfessorIndexViewModel);
        }

        //
        // GET: /Professor/
        // Muestra la pagina para editar las características de un trabajo
        // Crea una vista para un Trabajo
        public ActionResult Edit(int TrabajoId)
        {
            //Devuelve el TrabajoViewModel para el trabajo seleccionado
            var Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);
            // Crea una vista para un Trabajo
            return View(Trabajo);
        }

        //
        // POST: /Professor/Edit/
        // Aplica las modificaciones realizadas en la página de Edit
        // Redirige a la accion de Index
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int TrabajoId, FormCollection formValues)
        {
            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);

            Trabajo.Instrucciones = formValues["Instrucciones"];
            Trabajo.EsGrupal = Convert.ToBoolean(formValues["EsGrupal"]);

            DateTime FechaInicio;
            DateTime FechaFin;

            Trabajo.FechaInicio = null;
            Trabajo.FechaFin = null;

            if (DateTime.TryParse(formValues["FechaInicio"], out FechaInicio))
                Trabajo.FechaInicio = FechaInicio;
            if (DateTime.TryParse(formValues["FechaFin"], out FechaFin))
                Trabajo.FechaFin = FechaFin;

            RepositoryFactory.GetTrabajoRepository().SaveTrabajo(Trabajo);

            //Redirige a la accion Index del ProfessorController
            return RedirectToAction("Index", "Professor");
        }

        //
        // GET: /Professor/
        // Muestra la pagina de detalles de un trabajo
        // Crea una vista para un Trabajo
        public ActionResult Details(int TrabajoId)
        {
            ProfessorDetailsViewModel ProfessorDetailsViewModel = new ProfessorDetailsViewModel();

            BEProfesor Profesor = ((BEProfesor)Session["ActualProfesor"]);
            BEPeriodo Periodo = ((BEPeriodo)Session["ActualPeriodo"]);

            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);
            BECurso Curso = RepositoryFactory.GetCursoRepository().GetCurso(Trabajo.Curso.CursoId, Periodo.PeriodoId);
            BEResultadoPrograma ResultadoPrograma = RepositoryFactory.GetResultadoProgramaRepository().GetResultadoPrograma(Curso.CursoId, Periodo.PeriodoId);
            List<BESeccion> SeccionesDictado = RepositoryFactory.GetSeccionRepository().GetSeccionesDictado(Trabajo.Curso.CursoId, Profesor.ProfesorId, Periodo.PeriodoId);
            

            ProfessorDetailsViewModel.Curso = Curso;
            ProfessorDetailsViewModel.Trabajo = Trabajo;
            ProfessorDetailsViewModel.Secciones = SeccionesDictado.OrderBy(x => x.SeccionId).ToList();
            ProfessorDetailsViewModel.ResultadoPrograma = ResultadoPrograma;
            

            foreach (BESeccion Seccion in SeccionesDictado)
            {
                List<BEGrupo> GruposEntregados = RepositoryFactory.GetGrupoRepository().GetGruposEntregados(Trabajo.TrabajoId, Seccion.SeccionId);
                List<BEGrupo> GruposPendientes = RepositoryFactory.GetGrupoRepository().GetGruposPendientes(Trabajo.TrabajoId, Seccion.SeccionId);
                List<BEAlumno> AlumnosSinGrupo = RepositoryFactory.GetAlumnoRepository().GetAlumnosSinGrupoSeccionTrabajo(Trabajo.TrabajoId, Seccion.SeccionId);

                foreach (BEGrupo Grupo in GruposEntregados)
                {
                    ProfessorDetailsViewModel.AlumnosGrupoEntregados.Add(Grupo, RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId).OrderBy(x => x.Nombre).ToList());
                }

                foreach (BEGrupo Grupo in GruposPendientes)
                {
                    ProfessorDetailsViewModel.AlumnosGrupoPendientes.Add(Grupo, RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId).OrderBy(x => x.Nombre).ToList());
                }

                foreach (BEAlumno Alumno in AlumnosSinGrupo)
                {
                    List<BEAlumno> AlumnosGrupo = new List<BEAlumno>();
                    AlumnosGrupo.Add(Alumno);
                    ProfessorDetailsViewModel.AlumnosGrupoPendientes.Add(new BEGrupo { Nota = "NE", Seccion = Seccion, Lider = Alumno }, AlumnosGrupo);
                }
            }

            ProfessorDetailsViewModel.AlumnosGrupoEntregados.OrderBy(x => x.Key.Lider.Nombre);
            ProfessorDetailsViewModel.AlumnosGrupoPendientes.OrderBy(x => x.Key.Lider.Nombre);

            // Crea una vista para un Trabajo
            return View(ProfessorDetailsViewModel);
        }


        public ActionResult GradeAssignment(int TrabajoId, int GrupoId, String Origen, bool EsEditable)
        {
            ProfessorGradeAssignment ProfessorGradeAssignment = new ProfessorGradeAssignment();

            BEGrupo Grupo = GrupoId != 0 ? RepositoryFactory.GetGrupoRepository().GetGrupo(GrupoId) : null;
            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);
            BECurso Curso = RepositoryFactory.GetCursoRepository().GetCurso(Trabajo.Curso.CursoId, Trabajo.Periodo.PeriodoId);

            List<BEArchivo> Archivos = RepositoryFactory.GetArchivoRepository().GetArchivosGrupo(GrupoId);
            List<BERubrica> Rubricas = RepositoryFactory.GetRubricaRepository().GetRubricasTrabajo(TrabajoId);

            foreach (BERubrica Rubrica in Rubricas)
            {
                List<BECriterio> Criterios = RepositoryFactory.GetCriterioRubricaRepository().GetCiteriosRubrica(Rubrica.RubricaId);
                ProfessorGradeAssignment.CriteriosRubrica.Add(Rubrica, Criterios);
            }

            List<BEResultadoRubrica> ResultadosRubricas = RepositoryFactory.GetResultadoRubricaRepository().GetResultadosRubricaGrupo(GrupoId);

            ProfessorGradeAssignment.Curso = Curso;
            ProfessorGradeAssignment.Grupo = Grupo;
            ProfessorGradeAssignment.Trabajo = Trabajo;
            ProfessorGradeAssignment.ResultadosRubricas = ResultadosRubricas;
            ProfessorGradeAssignment.Archivos = Archivos.OrderBy(x => x.Nombre).ToList();
            ProfessorGradeAssignment.Origen = Origen;
            ProfessorGradeAssignment.EsEditable = EsEditable;

            return View(ProfessorGradeAssignment);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult GradeAssignment(int TrabajoId, int GrupoId, String Origen, bool EsEditable, FormCollection formValues)
        {
            if (EsEditable)
            {
                Decimal Nota = 0;
                String NotaGrupo = "NE";

                List<BERubrica> Rubricas = RepositoryFactory.GetRubricaRepository().GetRubricasTrabajo(TrabajoId);

                foreach (String Key in formValues)
                {
                    BEResultadoRubrica ResultadoRubrica = new BEResultadoRubrica();
                    ResultadoRubrica.CriterioId = Convert.ToInt32(formValues[Key]);
                    ResultadoRubrica.GrupoId = GrupoId;
                    ResultadoRubrica.RubricaId = Convert.ToInt32(Key);

                    List<BECriterio> Criterios = RepositoryFactory.GetCriterioRubricaRepository().GetCiteriosRubrica(ResultadoRubrica.RubricaId);
                    RepositoryFactory.GetResultadoRubricaRepository().SaveResultadoRubrica(ResultadoRubrica);

                    Nota += Criterios.SingleOrDefault(c => c.CriterioId == ResultadoRubrica.CriterioId).Valor;
                }

                if (formValues.Count == Rubricas.Count()) //Existe una calificacion para cara critero de la rubrica, es posible dar una nota final
                    NotaGrupo = Nota.ToString("F2");
                else if (formValues.Count != 0)
                    NotaGrupo = "EI";

                RepositoryFactory.GetGrupoRepository().SetNotaGrupo(GrupoId, NotaGrupo);
            }

            switch (Origen)
            {
                case "Details": return RedirectToAction("Details", new { TrabajoId = TrabajoId });
                case "Index": return RedirectToAction("Index");
            }

            return null;
        }


        public ActionResult DownloadFiles(int GrupoId)
        {
            BEGrupo Grupo = RepositoryFactory.GetGrupoRepository().GetGrupo(GrupoId);
            List<BEAlumno> Alumnos = RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(GrupoId);
            List<BEArchivo> Archivos = RepositoryFactory.GetArchivoRepository().GetArchivosGrupo(GrupoId);

            String zipFilename = Path.GetTempPath() + Guid.NewGuid() + ".zip";
            String integrantesFilename = Path.GetTempPath() + Guid.NewGuid() + ".txt";

            StreamWriter streamWriter = System.IO.File.CreateText(integrantesFilename);

            foreach (BEAlumno Alumno in Alumnos)
            {
                if (Alumno.AlumnoId == Grupo.Lider.AlumnoId)
                    streamWriter.WriteLine(Alumno.Nombre + " (LIDER)");
                else
                    streamWriter.WriteLine(Alumno.Nombre);
            }

            streamWriter.Flush();
            streamWriter.Close();

            ZipFile z = ZipFile.Create(zipFilename);
            z.BeginUpdate();
            foreach (BEArchivo Archivo in Archivos)
            {
                z.Add(Server.MapPath("~") + Archivo.Ruta, Archivo.Nombre);
            }
            z.Add(integrantesFilename, "Integrantes.txt");
            z.CommitUpdate();
            z.Close();

            var FilePathResult = new FilePathResult(zipFilename, ".zip");
            FilePathResult.FileDownloadName = "archivos_grupo.zip";
            return FilePathResult;
        }

        public ActionResult Grades(int TrabajoId, String Origen)
        {
            ProfessorGradesViewModel ProfessorGradesViewModel = new ProfessorGradesViewModel();

            BEProfesor Profesor = ((BEProfesor)Session["ActualProfesor"]);
            BEPeriodo Periodo = ((BEPeriodo)Session["ActualPeriodo"]);

            BETrabajo Trabajo = RepositoryFactory.GetTrabajoRepository().GetTrabajo(TrabajoId);
            BECurso Curso = RepositoryFactory.GetCursoRepository().GetCurso(Trabajo.Curso.CursoId, Periodo.PeriodoId);

            List<BESeccion> SeccionesDictado = RepositoryFactory.GetSeccionRepository().GetSeccionesDictado(Curso.CursoId, Profesor.ProfesorId, Periodo.PeriodoId);

            foreach (BESeccion Seccion in SeccionesDictado)
            {
                List<BEGrupo> GruposEntregados = RepositoryFactory.GetGrupoRepository().GetGruposEntregados(Trabajo.TrabajoId, Seccion.SeccionId);
                List<BEGrupo> GruposPendientes = RepositoryFactory.GetGrupoRepository().GetGruposPendientes(Trabajo.TrabajoId, Seccion.SeccionId);
                List<BEAlumno> AlumnosSinGrupo = RepositoryFactory.GetAlumnoRepository().GetAlumnosSinGrupoSeccionTrabajo(Trabajo.TrabajoId, Seccion.SeccionId);

                foreach (BEGrupo Grupo in GruposEntregados)
                {
                    foreach (BEAlumno Alumno in RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId))
                    {
                        ProfessorGradesViewModel.AlumnosNotas.Add(Alumno, Grupo.Nota);
                    }
                }

                foreach (BEGrupo Grupo in GruposPendientes)
                {
                    foreach (BEAlumno Alumno in RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId))
                    {
                        ProfessorGradesViewModel.AlumnosNotas.Add(Alumno, Grupo.Nota);
                    }
                }

                foreach (BEAlumno Alumno in AlumnosSinGrupo)
                {
                    ProfessorGradesViewModel.AlumnosNotas.Add(Alumno, "NE");
                }
            }
            ProfessorGradesViewModel.Trabajo = Trabajo;
            ProfessorGradesViewModel.Curso = Curso;
            ProfessorGradesViewModel.Origen = Origen;
            ProfessorGradesViewModel.Secciones = SeccionesDictado.OrderBy(x => x.Nombre).ToList();
            ProfessorGradesViewModel.AlumnosNotas = ProfessorGradesViewModel.AlumnosNotas.OrderBy(x=>x.Key.Nombre).ToDictionary(x=>x.Key,x=>x.Value);

            return View(ProfessorGradesViewModel);
        }

        public ActionResult History()
        {
            BEPeriodo Periodo = ((BEPeriodo)Session["ActualPeriodo"]);
            BEProfesor Profesor = ((BEProfesor)Session["ActualProfesor"]);

            ProfessorHistoryViewModel ProfessorHistoryViewModel = new ProfessorHistoryViewModel();

            List<BEPeriodo> Periodos = RepositoryFactory.GetPeriodoRepository().GetPeriodosEvaluados(Profesor.ProfesorId);
            List<BECurso> Cursos = new List<BECurso>();
            List<BETrabajo> Trabajos = new List<BETrabajo>();

            foreach (BEPeriodo PeriodoEvaluado in Periodos)
            {
                List<BECurso> CursosPeriodo = RepositoryFactory.GetCursoRepository().GetCursosEvaluados(Profesor.ProfesorId, PeriodoEvaluado.PeriodoId);
                foreach (BECurso CursoPeriodo in CursosPeriodo)
                {
                    if (!Cursos.Any(c => c.CursoId == CursoPeriodo.CursoId))
                        Cursos.Add(CursoPeriodo);

                    Trabajos.AddRange(RepositoryFactory.GetTrabajoRepository().GetTrabajosCurso(CursoPeriodo.CursoId, PeriodoEvaluado.PeriodoId));
                }
            }

            Periodos.RemoveAll(x => !Trabajos.Any(t => t.Periodo.PeriodoId == x.PeriodoId));

            ProfessorHistoryViewModel.ActualProfesor = Profesor;
            ProfessorHistoryViewModel.Periodos = Periodos.OrderByDescending(x => x.PeriodoId).ToList();
            ProfessorHistoryViewModel.Cursos = Cursos.OrderBy(x => x.Codigo).ToList();
            ProfessorHistoryViewModel.Trabajos = Trabajos.OrderBy(x => x.Nombre).ToList();

            return View(ProfessorHistoryViewModel);
        }
    }
}
