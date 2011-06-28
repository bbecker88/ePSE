using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.Models;
using ePortafolio.Helpers;
using System.Text.RegularExpressions;
using System.IO;

namespace ePortafolio.Controllers
{
    public class BaseController : Controller
    {
        protected void PostMessage(String Message, MessageType MessageType)
        {
            TempData["TempMessage"] = new TempMessage() { Message = Message, MessageType = MessageType };
        }

        protected void PostJavaScript(String JavaScript)
        {
            TempData["TempJavaScript"] = JavaScript;
        }

        protected bool PermitirAcceso()
        {
            var ControllerName = ControllerContext.RouteData.Values["controller"].ToString();

            if (Session.Get(GlobalKey.Rol) == null)
                return false;

            switch (ControllerName)
            {
                case "Profesor": return ((Roles)Session.Get(GlobalKey.Rol) == Roles.Profesor);
                case "Coordinador": return Session.Get(GlobalKey.CoordinadorId) != null;
            }

            return Session.Get(GlobalKey.UsuarioId) != null ;
        }


        protected static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            name = Regex.Replace(name, invalidReStr, "_");

            if (name.Length > 120)
                name.Substring(0, 120);

            return name;
        }

        protected ActionResult RedirigirFaltaAcceso()
        {
            if (Request.IsAjaxRequest())
            {
                PostMessage("Es necesario estar logueado.",MessageType.Error);
                return View("Empty");
            }
            else
                return RedirectToAction("Login", "Home");
        }

    }
}