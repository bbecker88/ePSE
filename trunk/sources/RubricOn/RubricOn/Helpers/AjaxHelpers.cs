using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace RubricOn.Helpers
{
    public static class AjaxHelpers
    {
        public static String ShowTempJavascript(this AjaxHelper ajax)
        {
            var TempMessage = ajax.ViewContext.TempData["TempJavaScript"];

            var htmlToDisplay = "";
            htmlToDisplay += "<script type=\"text/javascript\">";
            if(TempMessage!=null)
                htmlToDisplay += ajax.JavaScriptStringEncode(TempMessage.ToString());
            htmlToDisplay += "</script>";
            ajax.ViewContext.TempData["TempJavaScript"] = null;
            return ajax.JavaScriptStringEncode(htmlToDisplay);
        }
      
    }
}