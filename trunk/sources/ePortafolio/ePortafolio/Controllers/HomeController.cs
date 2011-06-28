using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.Logic;
using ePortafolio.Helpers;
using ePortafolio.Models.SSIA;
using ePortafolio.Models;
using ePortafolio.ViewModel;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio;

namespace ePortafolio.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Home()
        {
            if (!PermitirAcceso())
                return RedirigirFaltaAcceso();

            var PeriodoActualId = Session.Get(GlobalKey.ActualPeriodoId).ToInteger();
            var RolUsuario = (Roles)Session.Get(GlobalKey.Rol);

            switch (RolUsuario)
            {
                case Roles.Estudiante: return RedirectToAction("MostrarTrabajos", "Estudiante", new { PeriodoId = PeriodoActualId });
                case Roles.Profesor: return RedirectToAction("MostrarTrabajos", "Profesor", new { PeriodoId = PeriodoActualId });
                case Roles.Coordinador: return RedirectToAction("MostrarEvaluacionesOutcomes", "Coordinador", new { PeriodoId = PeriodoActualId });
                default: return RedirectToAction("Index");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            var ValidationLogic = new ValidationLogic(ModelState);
            ValidationLogic.Valid(model.Usuario, "Usuario", ValidationOption.IsNotEmpty);
            ValidationLogic.Valid(model.Contraseña, "Contraseña", ValidationOption.IsNotEmpty);

            //TODO: Validar Credenciales;
            // Si EsCoordinador esta activado, entonces no validar.

            if (!ModelState.IsValid)
                return View();            

            var PeriodoActual = SSIARepositoryFactory.GetPeriodosRepository().GetWhere(x => x.EsActual == true); 
           
            if (PeriodoActual.Count != 1)
            {
                PostMessage("No se puedo determinar el Período actual. No se puede continuar.", MessageType.Error);
                return View();
            }
            var PeriodoActualId  = PeriodoActual.First().PeriodoId;
            
            Roles RolUsuario;

            var AlumnoBE = SSIARepositoryFactory.GetAlumnosRepository().GetOne(model.Usuario);
            var ProfesorBE = SSIARepositoryFactory.GetProfesoresRepository().GetOne(model.Usuario);
            var CoordinadorBE = SSIARepositoryFactory.GetCoordinadoresRepository().GetOne(model.Usuario);

            if (AlumnoBE != null)
                RolUsuario = Roles.Estudiante;
            else if (ProfesorBE != null)
                RolUsuario = Roles.Profesor;
            else if (CoordinadorBE != null)
                RolUsuario = Roles.Coordinador;
            else
            {
                PostMessage("No se puedo determinar el rol del usuario. No se puede continuar.", MessageType.Error);
                return View();
            }

            var Periodos = new List<PeriodosBE>();
            var Nombre = "";
            String CoordinadorId = null;

            switch (RolUsuario)
            { 
                case Roles.Estudiante:
                    var PeriodosId = ePortafolioRepositoryFactory.GetTrabajosRepository().GetTrabajosEntregados(model.Usuario).Select(x => x.PeriodoId).Distinct();
                    Periodos = SSIARepositoryFactory.GetPeriodosRepository().GetWhere(x => PeriodosId.Contains(x.PeriodoId));
                    Periodos = Periodos.OrderByDescending(x => x.PeriodoId).ToList();
                    Nombre = AlumnoBE.Nombre;
                    break;

                case Roles.Profesor:
                    Periodos = SSIARepositoryFactory.GetPeriodosRepository().PeriodosDictados(model.Usuario);
                    Nombre = ProfesorBE.Nombre;
                    break;

                case Roles.Coordinador:
                    Periodos = new List<PeriodosBE>();
                    Nombre = CoordinadorBE.Nombre;
                    CoordinadorId = model.Usuario;
                    break;
            }

            Session.RemoveAll();

            if (!Periodos.Any(x => x.PeriodoId == PeriodoActualId))
            {
                Periodos.Add(PeriodoActual.First());
                Periodos = Periodos.OrderByDescending(x => x.PeriodoId).ToList();
            }

            Session.Set(GlobalKey.ActualPeriodoId, PeriodoActualId);
            Session.Set(GlobalKey.PeriodosMatriculados, Periodos);
            Session.Set(GlobalKey.Rol, RolUsuario);
            Session.Set(GlobalKey.UsuarioId, model.Usuario);
            Session.Set(GlobalKey.Nombre, Nombre);
            Session.Set(GlobalKey.CoordinadorId, CoordinadorId);
            
            switch (RolUsuario)
            {
                case Roles.Estudiante: return RedirectToAction("MostrarTrabajos", "Estudiante", new { PeriodoId = PeriodoActualId });
                case Roles.Profesor: return RedirectToAction("MostrarTrabajos", "Profesor", new { PeriodoId = PeriodoActualId });
                case Roles.Coordinador: return RedirectToAction("MostrarEvaluacionesOutcomes", "Coordinador", new { PeriodoId = PeriodoActualId });
            }

            PostMessage("Ha ocurrido un error.", MessageType.Error);
            return View();
        }

    }
}
