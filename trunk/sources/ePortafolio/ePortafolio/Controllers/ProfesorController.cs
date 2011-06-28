using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.ViewModel;
using ePortafolio.Helpers;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Logic;
using System.Transactions;
using ePortafolio.Models.SSIA;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;

namespace ePortafolio.Controllers
{
    public class ProfesorController : BaseController
    {
        public ActionResult MostrarTrabajos(String PeriodoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
            var MostrarTrabajosProfesorViewModel = new MostrarTrabajosProfesorViewModel(PeriodoId, ActualProfesorId);

            return View(MostrarTrabajosProfesorViewModel);
        }

        public ActionResult MostrarDetalleTrabajo(int TrabajoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
            var MostrarDetalleTrabajoProfesorViewModel = new MostrarDetalleTrabajoProfesorViewModel(TrabajoId, ActualProfesorId);

            return View(MostrarDetalleTrabajoProfesorViewModel);
        }

        public ActionResult MostrarConsolidadoNotas(int TrabajoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
            var MostrarConsolidadoNotasViewModel = new MostrarConsolidadoNotasViewModel(TrabajoId, ActualProfesorId);

            return View(MostrarConsolidadoNotasViewModel);
        }

        public ActionResult EditarTrabajo(int TrabajoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
            var EditarTrabajoViewModel = new EditarTrabajoViewModel(TrabajoId, ActualProfesorId);
            return View(EditarTrabajoViewModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditarTrabajo(int TrabajoId, FormCollection formCollection)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();
            var ActualPeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();

            var ValidationLogic = new ValidationLogic(ModelState);
            var FechaInicio = formCollection["Trabajo.FechaInicio"].ToString();
            var FechaFin = formCollection["Trabajo.FechaFin"].ToString();
            var Nombre = formCollection["Trabajo.Nombre"].ToString();

            ValidationLogic.Valid(FechaInicio, "Trabajo.FechaInicio", ValidationOption.IsEmpty, ValidationOption.IsDate);
            ValidationLogic.Valid(FechaFin, "Trabajo.FechaFin", ValidationOption.IsEmpty, ValidationOption.IsDate);
            ValidationLogic.Valid(Nombre, "Trabajo.Nombre", ValidationOption.IsNotEmpty);


            if (!ModelState.IsValid)
            {
                var EditarTrabajoViewModel = new EditarTrabajoViewModel(TrabajoId, ActualProfesorId);
                TryUpdateModel(EditarTrabajoViewModel);
                return View(EditarTrabajoViewModel);
            }

            var Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);
            var CursosPeriodos = SSIARepositoryFactory.GetCursosPeriodosRepository().GetOne(Trabajo.CursoId, ActualPeriodoId);

            if (CursosPeriodos.CoordinadorId != ActualProfesorId)
                PostMessage("No tiene los permisos necesarios para editar el trabajo.", MessageType.Error);
            else
            {
                Trabajo.FechaInicio = null;
                Trabajo.FechaFin = null;

                if (FechaInicio != String.Empty)
                    Trabajo.FechaInicio = Convert.ToDateTime(FechaInicio);
                if (FechaFin != String.Empty)
                    Trabajo.FechaFin = Convert.ToDateTime(FechaFin);

                if (Trabajo.FechaInicio != null && Trabajo.FechaFin != null && Trabajo.FechaInicio > Trabajo.FechaFin)
                {
                    Trabajo.FechaInicio = Convert.ToDateTime(FechaFin);
                    Trabajo.FechaFin = Convert.ToDateTime(FechaInicio);
                }

                Trabajo.Nombre = Nombre;
                Trabajo.EsGrupal = Convert.ToBoolean(formCollection["Trabajo.EsGrupal"]);
                Trabajo.Instrucciones = formCollection["Trabajo.Instrucciones"].ToString();

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetTrabajosRepository().Update(Trabajo);
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();
                        PostMessage("El trabajo ha sido editado exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            return RedirectToAction("MostrarTrabajos", "Profesor", new { PeriodoId = Trabajo.PeriodoId });
        }

        public ActionResult MostrarPortafolioEvaluacion(String PeriodoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();

            var MostrarPortafolioEvaluacionProfesorViewModel = new MostrarPortafolioEvaluacionProfesorViewModel(PeriodoId, ActualProfesorId);
            return View(MostrarPortafolioEvaluacionProfesorViewModel);
        }

        public ActionResult EditarEvaluacionGrupo(int GrupoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualProfesorId = Session.Get(GlobalKey.UsuarioId).ToString();

            var EditarEvaluacionGrupoViewModel = new EditarEvaluacionGrupoViewModel(GrupoId, ActualProfesorId);

            return View(EditarEvaluacionGrupoViewModel);
        }

        public ActionResult DescargarOutcome(String AlumnoId, int OutcomeId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var TrabajosOutcomeAlumno = ePortafolioRepositoryFactory.GetTrabajosOutcomeAlumnoRepository().GetWhere(x => x.AlumnoId == AlumnoId && x.OutcomeId == OutcomeId);

            String zipFilename = Path.GetTempPath() + Guid.NewGuid() + ".zip";
            ZipFile z = ZipFile.Create(zipFilename);
            z.BeginUpdate();
            StreamWriter streamWriter;

            var Outcome = SSIARepositoryFactory.GetOutcomesRepository().GetOne(OutcomeId);
            var Alumno = SSIARepositoryFactory.GetAlumnosRepository().GetOne(AlumnoId);
            foreach (var trabajo in TrabajosOutcomeAlumno)
            {
                var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetGrupoTrabajo(trabajo.TrabajoId,AlumnoId);
                
                if (Grupo == null)
                    continue;
                
                var Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Grupo.ExtraTrabajo.CursoId);
                var AlumnosGrupoId = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => x.GrupoId == Grupo.GrupoId).Select(x => x.AlumnoId);
                var Alumnos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosGrupoId.Contains(x.AlumnoId), x => x.Nombre);
                var Archivos = ePortafolioRepositoryFactory.GetArchivosRepository().GetArchivosGrupo(Grupo.GrupoId);

                String directoryName = MakeValidFileName(String.Format("{0} {1}-{2} {3}",Grupo.TrabajoId.ToString("D6"), Curso!=null?Curso.Codigo:"INDEP", Grupo.ExtraTrabajo.Codigo??"GEN", Grupo.NombreTrabajo));
                String integrantesFilename = Path.GetTempPath() + Guid.NewGuid() + ".txt";

                streamWriter = System.IO.File.CreateText(integrantesFilename);

                z.AddDirectory(directoryName);

                foreach (var alumno in Alumnos)
                {
                    if (alumno.AlumnoId == Grupo.LiderId)
                        streamWriter.WriteLine(String.Format("{0}\t{1} ({2}) ^ Lider", alumno.AlumnoId, alumno.Nombre, alumno.NombreCarrera));
                    else
                        streamWriter.WriteLine(String.Format("{0}\t{1} ({2})", alumno.AlumnoId, alumno.Nombre, alumno.NombreCarrera));
                }

                streamWriter.Flush();
                streamWriter.Close();

                var id = 0;
                Archivos = Archivos.OrderBy(x => x.Nombre).ToList();
                foreach (var Archivo in Archivos)
                {
                    var FilePath = Server.MapPath("~") + Archivo.Ruta;
                    if (System.IO.File.Exists(FilePath))
                        z.Add(FilePath, directoryName + "\\" + (++id).ToString("D3") +" "+ Archivo.Nombre);
                }
                z.Add(integrantesFilename, directoryName + "\\" + "Integrantes.txt");
            }

            String outcomesFilename = Path.GetTempPath() + Guid.NewGuid() + ".txt";
            streamWriter = System.IO.File.CreateText(outcomesFilename);

            streamWriter.WriteLine(String.Format("Alumno: {0}", Alumno.Nombre));
            streamWriter.WriteLine(String.Format("Carrera: {0}", Alumno.NombreCarrera));
            streamWriter.WriteLine(String.Format("Correo: {0}", Alumno.CorreoElectronico));
            streamWriter.WriteLine("");
            streamWriter.WriteLine(String.Format("Outcome: {0}", Outcome.Outcome));
            streamWriter.WriteLine(String.Format("Descripcion: {0}", Outcome.Descripcion));
            streamWriter.Flush();
            streamWriter.Close();
            z.Add(outcomesFilename, "Informacion del outcome.txt");

            z.CommitUpdate();            
            z.Close();

            var FilePathResult = new FilePathResult(zipFilename, ".zip");
            FilePathResult.FileDownloadName = MakeValidFileName(Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 6) + " - Outcome " + Outcome.Outcome + " " + Alumno.AlumnoId) + ".zip";

            return FilePathResult;
        }
    }
}

