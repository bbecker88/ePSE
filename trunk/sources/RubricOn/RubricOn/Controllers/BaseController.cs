using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Reflection;
using RubricOn.Models;



namespace RubricOn.Controllers
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


    }
}