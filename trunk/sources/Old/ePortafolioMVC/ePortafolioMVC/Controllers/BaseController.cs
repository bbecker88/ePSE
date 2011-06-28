using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ePortafolioMVC.Controllers
{
    public class BaseController : Controller
    {
        public enum MessageType
        {
            Error, Success, Info
        }


        public void PostMessage(String message, MessageType messageType)
        {
            TempData["TempPostMessage"] = message;
            TempData["TempPostMessageType"] = messageType;
        }



    }
}
