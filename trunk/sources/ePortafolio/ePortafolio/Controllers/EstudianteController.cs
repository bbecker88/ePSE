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


namespace ePortafolio.Controllers
{
    [HandleError]
    public class EstudianteController : BaseController
    {
        public ActionResult MostrarTrabajos(String PeriodoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var AlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var MostrarTrabajosViewModel = new MostrarTrabajosEstudianteViewModel(PeriodoId, AlumnoId);

            return View(MostrarTrabajosViewModel);
        }

        public ActionResult MostrarTrabajosIndependientes()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var AlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var MostrarTrabajosIndependientesViewModel = new MostrarTrabajosIndependientesViewModel(AlumnoId);

            return View(MostrarTrabajosIndependientesViewModel);
        }

        public ActionResult MostrarDetalleTrabajo(int TrabajoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var AlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var MostrarDetalleTrabajoViewModel = new MostrarDetalleTrabajoEstudianteViewModel(TrabajoId, AlumnoId);

            return View(MostrarDetalleTrabajoViewModel);
        }

        public ActionResult EliminarGrupo(int GrupoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var AlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);

            if (Grupo.LiderId != AlumnoId)
                PostMessage("No tiene los permisos necesarios para eliminar el grupo.", MessageType.Error);
            else
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var ArchivosGrupo = ePortafolioRepositoryFactory.GetArchivosGrupoRepository().GetWhere(x => x.GrupoId == GrupoId);
                        foreach (var archivoGrupo in ArchivosGrupo)
                        {
                            if (System.IO.File.Exists(archivoGrupo.ExtraArchivo.Ruta))
                                System.IO.File.Delete(archivoGrupo.ExtraArchivo.Ruta);
                        }

                        ePortafolioRepositoryFactory.GetEvaluacionesGruposProfesorRepository().DeleteWhere(x => x.GrupoId == GrupoId);
                        ePortafolioRepositoryFactory.GetArchivosGrupoRepository().DeleteWhere(x => x.GrupoId == GrupoId);
                        ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().DeleteWhere(x => x.GrupoId == GrupoId);
                        ePortafolioRepositoryFactory.GetGruposRepository().DeleteWhere(x => x.GrupoId == GrupoId);
                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        scope.Complete();

                        if (Grupo.ExtraTrabajo.Iniciativa == "UPC")
                            PostMessage("Se ha eliminado el grupo exitosamente.", MessageType.Success);
                        else
                            PostMessage("Se ha eliminado el trabajo exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
            if (Grupo.ExtraTrabajo.Iniciativa == "UPC")
                return RedirectToAction("MostrarTrabajos", new { PeriodoId = Grupo.ExtraTrabajo.PeriodoId });
            else
                return RedirectToAction("MostrarTrabajosIndependientes");
        }

        public ActionResult EliminarAlumnoGrupo(int GrupoId, String AlumnoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);

            if (Grupo.LiderId != ActualAlumnoId)
                PostMessage("No tiene los permisos necesarios para eliminar a un estudiante del grupo.", MessageType.Error);
            else if (AlumnoId == ActualAlumnoId)
                PostMessage("No se puede eliminar al lider del grupo.", MessageType.Error);
            else
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().DeleteWhere(x => x.AlumnoId == AlumnoId && x.GrupoId == GrupoId);
                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        scope.Complete();
                        PostMessage("Se ha eliminado al alumno exitosamente.", MessageType.Success);
                    }

                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            var MostrarDetalleGrupoViewModel = new MostrarDetalleGrupoViewModel(Grupo.TrabajoId, ActualAlumnoId);

            return PartialView("MostrarDetalleGrupo", MostrarDetalleGrupoViewModel);
        }

        public ActionResult CambiarLiderGrupo(int GrupoId, String AlumnoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);

            if (Grupo.LiderId != ActualAlumnoId)
                PostMessage("No tiene los permisos necesarios para cambiar al lider del grupo.", MessageType.Error);
            else
            {
                Grupo.LiderId = AlumnoId;

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetGruposRepository().Update(Grupo);
                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        scope.Complete();
                        PostMessage("Se ha cambiado el lider exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            var MostrarDetalleGrupoViewModel = new MostrarDetalleGrupoViewModel(Grupo.TrabajoId, ActualAlumnoId);

            return PartialView("MostrarDetalleGrupo", MostrarDetalleGrupoViewModel);
        }

        public ActionResult RetirarseGrupo(int GrupoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var AlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);

            if (Grupo.LiderId == AlumnoId)
                PostMessage("No se puede retirar al lider del grupo.", MessageType.Error);
            else
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().DeleteWhere(x => x.AlumnoId == AlumnoId && x.GrupoId == GrupoId);
                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        scope.Complete();
                        PostMessage("Se ha retirado del grupo exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            return RedirectToAction("MostrarTrabajos", new { PeriodoId = Grupo.ExtraTrabajo.PeriodoId });
        }

        [HttpPost]
        public ActionResult AgregarAlumnosGrupo(int GrupoId, FormCollection formCollection)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var AlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
            var EsIndependiente = Grupo.ExtraTrabajo.Iniciativa != "UPC";

            if (Grupo.LiderId != AlumnoId)
                PostMessage("No tiene los permisos necesarios agregar estudiantes al grupo.", MessageType.Error);
            else
            {
                List<String> AlumnosNuevos = new List<String>();

                AlumnosNuevos = (formCollection["IndependieteAlumnos"] ?? "").Split(new char[] { ',', ';', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (!EsIndependiente)
                {
                    foreach (var key in formCollection)
                    {
                        var stringKey = key.ToString();

                        if (!stringKey.StartsWith("A"))
                            continue;

                        bool isChecked = false;
                        if (Boolean.TryParse(formCollection[stringKey], out isChecked) == false)
                            AlumnosNuevos.Add(stringKey.Substring(1));
                    }
                }

                try
                {
                    AlumnosNuevos = AlumnosNuevos.Distinct().ToList();
                    var AlumnosValidos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosNuevos.Contains(x.AlumnoId)).Select(x => x.AlumnoId).ToList();

                    using (TransactionScope scope = new TransactionScope())
                    {
                        var AlumnosConGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetAlumnosGrupoTrabajo(Grupo.TrabajoId);

                        var MensajeError = "";


                        if (AlumnosConGrupo.Any(x => AlumnosValidos.Contains(x.AlumnoId)))
                            MensajeError += "Alguno de los estudiantes seleccionados ya tiene grupo. ";

                        if (AlumnosValidos.Count != AlumnosNuevos.Count)
                            MensajeError += "Alguno de los codigos ingresados no existe. ";

                        if (MensajeError != "")
                        {
                            MensajeError += "No todos los estudiantes han sido agregados.";
                            PostMessage(MensajeError, MessageType.Error);
                        }

                        AlumnosValidos = AlumnosValidos.Where(x => !AlumnosConGrupo.Any(ag => ag.AlumnoId == x)).ToList();

                        foreach (var AlumnoNuevo in AlumnosValidos)
                        {
                            ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().Insert(new AlumnosGrupoBE()
                                                                                {
                                                                                    AlumnoId = AlumnoNuevo,
                                                                                    GrupoId = Grupo.GrupoId,
                                                                                    Nota = "NE",
                                                                                    EvaluacionId = null
                                                                                });
                        }

                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        scope.Complete();

                        if (MensajeError == "")
                            PostMessage("Los alumnos han sido agregados exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            var MostrarDetalleGrupoViewModel = new MostrarDetalleGrupoViewModel(Grupo.TrabajoId, AlumnoId);

            return PartialView("MostrarDetalleGrupo", MostrarDetalleGrupoViewModel);
        }

        [HttpPost]
        public ActionResult CrearGrupoIndependiente(FormCollection formCollection)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ValidationLogic = new ValidationLogic();
            var NombreTrabajo = formCollection["NombreTrabajoIndependiente"];
            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var ActualPeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();

            if (!ValidationLogic.Valid(NombreTrabajo, ValidationOption.IsNotEmpty))
            {
                PostMessage("El nombre del trabajo independiente es invalido. No se puede continuar.", MessageType.Error);
                return RedirectToAction("MostrarTrabajosIndependientes");
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var Trabajo = new TrabajosBE()
                    {
                        CursoId = 0,
                        EsGrupal = true,
                        FechaFin = null,
                        FechaInicio = null,
                        Iniciativa = "EST",
                        Instrucciones = "",
                        Nombre = NombreTrabajo,
                        PeriodoId = ActualPeriodoId,
                    };

                    ePortafolioRepositoryFactory.GetTrabajosRepository().InsertIdentity(Trabajo, true);

                    var Grupo = new GruposBE()
                    {
                        LiderId = ActualAlumnoId,
                        Nota = "NE",
                        SeccionId = "GEN",
                        TrabajoId = Trabajo.TrabajoId,
                        NombreTrabajo = NombreTrabajo,
                        EvaluacionId = null
                    };

                    ePortafolioRepositoryFactory.GetGruposRepository().InsertIdentity(Grupo, true);

                    var AlumnoGrupo = new AlumnosGrupoBE()
                    {
                        AlumnoId = ActualAlumnoId,
                        GrupoId = Grupo.GrupoId,
                        Nota = "NE",
                        EvaluacionId = null
                    };

                    ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().Insert(AlumnoGrupo);

                    ePortafolioRepositoryFactory.SubmitChanges(true);

                    scope.Complete();

                    PostMessage("El trabajo se ha creado exitosamente.", MessageType.Success);
                }

            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error.", MessageType.Error);
            }

            return RedirectToAction("MostrarTrabajosIndependientes");
        }

        [HttpPost]
        public ActionResult AgregarArchivoGrupo(int GrupoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
            var Curso = SSIARepositoryFactory.GetCursosRepository().GetOne(Grupo.ExtraTrabajo.CursoId);

            try
            {
                HttpPostedFileBase file = Request.Files.Count > 0 ? Request.Files[0] : null;

                if (file == null || file.ContentLength == 0)
                {
                    PostMessage("No se ha cargado ningun archivo.", MessageType.Error);
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var RutaCiclo = Grupo.ExtraTrabajo.PeriodoId;
                        var RutaCurso = Curso == null ? "INDEPENDIENTES" : Curso.Codigo.Trim();
                        var RutaTrabajo = Grupo.TrabajoId;
                        var RutaGrupo = Grupo.GrupoId;
                        var RutaArchivo = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 6) + "-" + Path.GetFileName(file.FileName);

                        var RutaVirtual = String.Format("\\Files\\{0}\\{1}\\{2}\\{3}\\{4}", RutaCiclo, RutaCurso, RutaTrabajo, RutaGrupo, RutaArchivo);
                        var RutaFisica = String.Format("{0}\\Files\\{1}\\{2}\\{3}\\{4}\\{5}", Server.MapPath("~/"), RutaCiclo, RutaCurso, RutaTrabajo, RutaGrupo, RutaArchivo);

                        Directory.CreateDirectory(Path.GetDirectoryName(RutaFisica));
                        file.SaveAs(RutaFisica);

                        var Archivo = new ArchivosBE
                        {
                            FechaSubida = DateTime.Now,
                            Nombre = Path.GetFileName(file.FileName),
                            Ruta = RutaVirtual,
                            AlumnoId = ActualAlumnoId
                        };

                        ePortafolioRepositoryFactory.GetArchivosRepository().InsertIdentity(Archivo, true);

                        var ArchivosGrupoBE = new ArchivosGrupoBE
                        {
                            ArchivoId = Archivo.ArchivoId,
                            GrupoId = GrupoId,
                        };

                        ePortafolioRepositoryFactory.GetArchivosGrupoRepository().Insert(ArchivosGrupoBE);
                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        //TODO: Enviar correos a los alumnos del grupo.
                        /*
                        List<Alumnos> AlumnosGrupo = RepositoryFactory.GetAlumnoRepository().GetAlumnosGrupo(Grupo.GrupoId);

                        foreach (BEAlumno AlumnoGrupo in AlumnosGrupo)
                        {
                            String Mensaje = "Estimado #NOMBRE, </br>" +
                                             "Se ha agregado el archivo <b>#ARCHIVO</b> al trabajo <b>#TRABAJO</b> del curso <b>#CURSO</b>";
                            Mensaje = Mensaje.Replace("#NOMBRE", AlumnoGrupo.Nombre);
                            Mensaje = Mensaje.Replace("#ARCHIVO", FileNameOnDisk);
                            Mensaje = Mensaje.Replace("#TRABAJO", Trabajo.Nombre);
                            Mensaje = Mensaje.Replace("#CURSO", Curso.Nombre);

                            EmailLogic.SendMail(AlumnoGrupo.CorreoElectronico, "ePSE - Archivo agregado", Mensaje);
                        }
                        */

                        scope.Complete();
                        PostMessage("El archivo ha sido agregado exitosamente.", MessageType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                PostMessage("Ha ocurrido un error. " + ex.Message, MessageType.Error);
            }
            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return PartialView("MostrarDetalleArchivos", new MostrarDetalleArchivosViewModel(Grupo.TrabajoId, ActualAlumnoId));
        }

        public ActionResult EliminarArchivoGrupo(int GrupoId, int ArchivoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var Archivo = ePortafolioRepositoryFactory.GetArchivosRepository().GetOne(ArchivoId);
            var AlumnoGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetOne(ActualAlumnoId, GrupoId);
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);

            if (AlumnoGrupo == null)
                PostMessage("No tiene los permisos necesarios para eliminar el archivo.", MessageType.Error);
            else
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var RutaFisica = String.Format("{0}\\{1}", Server.MapPath("~/"), Archivo.Ruta);

                        if (System.IO.File.Exists(RutaFisica))
                            System.IO.File.Delete(RutaFisica);

                        ePortafolioRepositoryFactory.GetArchivosGrupoRepository().DeleteWhere(x => x.ArchivoId == ArchivoId);
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        ePortafolioRepositoryFactory.GetArchivosRepository().DeleteWhere(x => x.ArchivoId == ArchivoId);
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();
                        PostMessage("El archivo ha sido eliminado exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
            //Redirecciona a la accion Details de Student con el ID del Trabajo
            return PartialView("MostrarDetalleArchivos", new MostrarDetalleArchivosViewModel(Grupo.TrabajoId, ActualAlumnoId));
        }

        public ActionResult DescargarArchivo(int GrupoId, int ArchivoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var ArchivosGrupo = ePortafolioRepositoryFactory.GetArchivosGrupoRepository().GetOne(ArchivoId, GrupoId);
            var AlumnoGrupo = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetOne(ActualAlumnoId, GrupoId);

            if (AlumnoGrupo == null)
                PostMessage("No tiene los permisos necesarios para eliminar el grupo.", MessageType.Error);
            else if (ArchivosGrupo == null || !System.IO.File.Exists(Server.MapPath("~") + ArchivosGrupo.ExtraArchivo.Ruta))
                PostMessage("No se encuentra el archivo", MessageType.Error);
            else 
            {
                var FilePathResult = new FilePathResult(Server.MapPath("~") + ArchivosGrupo.ExtraArchivo.Ruta, "application/octet-stream");
                FilePathResult.FileDownloadName = ArchivosGrupo.ExtraArchivo.Nombre;
                return FilePathResult;
            }
            
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DescargarArchivos(int GrupoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetOne(GrupoId);
            var AlumnosGrupoId = ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().GetWhere(x => x.GrupoId == GrupoId).Select(x => x.AlumnoId);
            var Alumnos = SSIARepositoryFactory.GetAlumnosRepository().GetWhere(x => AlumnosGrupoId.Contains(x.AlumnoId), x => x.Nombre);
            var Archivos = ePortafolioRepositoryFactory.GetArchivosRepository().GetArchivosGrupo(GrupoId);

            String zipFilename = Path.GetTempPath() + Guid.NewGuid() + ".zip";
            String integrantesFilename = Path.GetTempPath() + Guid.NewGuid() + ".txt";

            StreamWriter streamWriter = System.IO.File.CreateText(integrantesFilename);

            var NombreAlumnos = "";

            foreach (var Alumno in Alumnos)
            {
                NombreAlumnos += ", " + Alumno.AlumnoId;

                if (Alumno.AlumnoId == Grupo.LiderId)
                    streamWriter.WriteLine(String.Format("{0}\t{1} ({2}) ^ Lider", Alumno.AlumnoId, Alumno.Nombre, Alumno.NombreCarrera));
                else
                    streamWriter.WriteLine(String.Format("{0}\t{1} ({2})", Alumno.AlumnoId, Alumno.Nombre, Alumno.NombreCarrera));
            }

            if (NombreAlumnos.Length > 0)
                NombreAlumnos = NombreAlumnos.Remove(0, 2);
            else
                NombreAlumnos = "Archivos";

            streamWriter.Flush();
            streamWriter.Close();

            ZipFile z = ZipFile.Create(zipFilename);
            z.BeginUpdate();

            var id = 0;
            Archivos = Archivos.OrderBy(x => x.Nombre).ToList();
            foreach (var Archivo in Archivos)
            {
                var FilePath = Server.MapPath("~") + Archivo.Ruta;
                if (System.IO.File.Exists(FilePath))
                    z.Add(FilePath, (++id).ToString("D3") + " " + Archivo.Nombre);
            }
            z.Add(integrantesFilename, "Integrantes.txt");
            z.CommitUpdate();
            z.Close();

            var FilePathResult = new FilePathResult(zipFilename, ".zip");
            FilePathResult.FileDownloadName = MakeValidFileName(Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 6) + " - Trabajo " + Grupo.ExtraTrabajo.Codigo + " " + NombreAlumnos) + ".zip";

            return FilePathResult;
        }

        [HttpPost]
        public ActionResult CrearGrupo(String NombreTrabajo, int TrabajoId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();
            var ActualPeriodoId = Session.Get(GlobalKey.ActualPeriodoId).ToString();
            var Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetGrupoTrabajo(TrabajoId, ActualAlumnoId);
            var Trabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetOne(TrabajoId);

            if (Grupo != null)
                PostMessage("Ya tiene un grupo para este trabajo.", MessageType.Error);
            else
            {
                try
                {
                    var AlumnoCurso = SSIARepositoryFactory.GetAlumnosCursoRepository().GetWhere(x => x.AlumnoId == ActualAlumnoId && x.PeriodoId == ActualPeriodoId && x.CursoId == Trabajo.CursoId).FirstOrDefault();

                    using (TransactionScope Scope = new TransactionScope())
                    {
                        Grupo = new GruposBE()
                        {
                            LiderId = ActualAlumnoId,
                            Nota = "NE",
                            EvaluacionId = null,
                            SeccionId = AlumnoCurso != null ? AlumnoCurso.SeccionId : "N/A",
                            TrabajoId = TrabajoId,
                            NombreTrabajo = NombreTrabajo
                        };

                        ePortafolioRepositoryFactory.GetGruposRepository().InsertIdentity(Grupo, true);

                        var AlumnoGrupo = new AlumnosGrupoBE()
                        {
                            AlumnoId = ActualAlumnoId,
                            GrupoId = Grupo.GrupoId,
                            Nota = "NE",
                            EvaluacionId = null
                        };

                        ePortafolioRepositoryFactory.GetAlumnosGrupoRepository().Insert(AlumnoGrupo);
                        ePortafolioRepositoryFactory.SubmitChanges(true);

                        Scope.Complete();
                        PostMessage("El grupo ha sido creado exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }
            return RedirectToAction("MostrarDetalleTrabajo", new { TrabajoId = TrabajoId });
        }

        public ActionResult MostrarPortafolioEvaluacion()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();

            var MostrarPortafolioEvaluacionViewModel = new MostrarPortafolioEvaluacionEstudianteViewModel(ActualAlumnoId);
            return View(MostrarPortafolioEvaluacionViewModel);
        }

        [HttpPost]
        public ActionResult AgregarTrabajoOutcome(int TrabajoId, int OutcomeId, FormCollection formCollection)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();

            var TrabajosEntregados = ePortafolioRepositoryFactory.GetTrabajosRepository().GetTrabajosEntregados(ActualAlumnoId);
            var Outcome = SSIARepositoryFactory.GetOutcomesAlumnoRepository().GetWhere(x => x.AlumnoId == ActualAlumnoId && x.OutcomeId == OutcomeId).ToList();

            if (!TrabajosEntregados.Any(x => x.TrabajoId == TrabajoId) || Outcome.Count() == 0)
            {
                PostMessage("No tiene los permisos necesarios para agregar el trabajo.", MessageType.Error);
            }
            else
            {
                try
                {
                    var TrabajosOutcomeAlumno = new TrabajosOutcomeAlumnoBE()
                    {
                        AlumnoId = ActualAlumnoId,
                        OutcomeId = OutcomeId,
                        TrabajoId = TrabajoId,
                    };

                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetTrabajosOutcomeAlumnoRepository().InsertOrUpdate(TrabajosOutcomeAlumno);
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();
                        PostMessage("El trabajo ha sido agregado exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            return PartialView("MostrarDetalleOutcome", new MostrarDetalleOutcomeEstudianteViewModel(OutcomeId, ActualAlumnoId));
        }

        public ActionResult EliminarTrabajoOutcome(int TrabajoId, int OutcomeId)
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var ActualAlumnoId = Session.Get(GlobalKey.UsuarioId).ToString();

            var TrabajoOutcomeAlumno = ePortafolioRepositoryFactory.GetTrabajosOutcomeAlumnoRepository().GetOne(ActualAlumnoId, OutcomeId, TrabajoId);

            if (TrabajoOutcomeAlumno == null)
            {
                PostMessage("No tiene los permisos necesarios para eliminar el trabajo.", MessageType.Error);
            }
            else
            {
                try
                {
                    var TrabajosOutcomeAlumno = new TrabajosOutcomeAlumnoBE()
                    {
                        AlumnoId = ActualAlumnoId,
                        OutcomeId = OutcomeId,
                        TrabajoId = TrabajoId,
                    };

                    using (TransactionScope scope = new TransactionScope())
                    {
                        ePortafolioRepositoryFactory.GetTrabajosOutcomeAlumnoRepository().Delete(TrabajosOutcomeAlumno);
                        ePortafolioRepositoryFactory.SubmitChanges(true);
                        scope.Complete();
                        PostMessage("El trabajo ha sido eliminado exitosamente.", MessageType.Success);
                    }
                }
                catch (Exception ex)
                {
                    PostMessage("Ha ocurrido un error.", MessageType.Error);
                }
            }

            return PartialView("MostrarDetalleOutcome", new MostrarDetalleOutcomeEstudianteViewModel(OutcomeId, ActualAlumnoId));
        }
    }
}
