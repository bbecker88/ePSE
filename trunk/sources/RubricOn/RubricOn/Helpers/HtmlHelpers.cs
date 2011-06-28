using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricOn.Models;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace RubricOn.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ShowTempMessage(this HtmlHelper html)
        {
            var htmlToDisplay = "";

            var TempMessage = (TempMessage)html.ViewContext.TempData["TempMessage"];

            if (TempMessage != null)
            {
                var clase = "";

                switch (TempMessage.MessageType)
                {
                    case MessageType.Error: clase = "mensaje msj_error"; break;
                    case MessageType.Info: clase = "mensaje msj_info"; break;
                    case MessageType.Success: clase = "mensaje msj_check"; break;
                }

                htmlToDisplay += String.Format("<div id=\"TempMessage\" class=\"{0}\">", clase);
                htmlToDisplay += html.Encode(TempMessage.Message);
                htmlToDisplay += "</div>";
                html.ViewContext.TempData["TempMessage"] = null;
            }

            return MvcHtmlString.Create(htmlToDisplay);
        }

        public static HtmlHelper GetHtmlHelper()
        {
            return new HtmlHelper(new ViewContext(new ControllerContext(), new WebFormView("omg"), new ViewDataDictionary(), new TempDataDictionary(), new StringWriter()), new ViewPage());
        }

        public static UrlHelper GetUrlHelper()
        {
            var context = HttpContext.Current;

            if(context == null)
            {
                var request = new HttpRequest("/","http://example.com","");
                var response = new HttpResponse(new StringWriter());
                context = new HttpContext(request,response);
            }

            var httpContextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase,routeData);

            return new UrlHelper(requestContext);
        }

        public static String ShowTempMessage(String ContainerId, TempDataDictionary TempData)
        {
            var htmlToDisplay = "";

            var TempMessage = (TempMessage)TempData["TempMessage"];

            if (TempMessage != null)
            {
                var clase = "";

                switch (TempMessage.MessageType)
                {
                    case MessageType.Error: clase = "mensaje msj_error"; break;
                    case MessageType.Info: clase = "mensaje msj_info"; break;
                    case MessageType.Success: clase = "mensaje msj_check"; break;
                }

                htmlToDisplay += String.Format("<div id=\\\"TempMessage\\\" class=\\\"{0}\\\">", clase);
                htmlToDisplay += GetHtmlHelper().Encode(TempMessage.Message);
                htmlToDisplay += "</div>";
                TempData["TempMessage"] = null;
            }

            return String.Format("$('#{0}').html(\"{1}\");", ContainerId, htmlToDisplay);
        }

        public static MvcHtmlString ShowTempMessage(this HtmlHelper html, String Id)
        {
            var htmlToDisplay = "";
            htmlToDisplay += "<div id=" + Id + ">";
            var TempMessage = (TempMessage)html.ViewContext.TempData["TempMessage"];

            if (TempMessage != null)
            {
                var clase = "";

                switch (TempMessage.MessageType)
                {
                    case MessageType.Error: clase = "mensaje msj_error"; break;
                    case MessageType.Info: clase = "mensaje msj_info"; break;
                    case MessageType.Success: clase = "mensaje msj_check"; break;
                }

                htmlToDisplay += String.Format("<div id=\"TempMessage\" class=\"{0}\">", clase);
                htmlToDisplay += html.Encode(TempMessage.Message);
                htmlToDisplay += "</div>";
                html.ViewContext.TempData["TempMessage"] = null;
            }
            htmlToDisplay += "</div>";
            return MvcHtmlString.Create(htmlToDisplay);
        }

        public static String ShowTempJavascript(this HtmlHelper html)
        {
            var TempMessage = html.ViewContext.TempData["TempJavaScript"];

            var htmlToDisplay = "";
            htmlToDisplay += "<script type=\"text/javascript\">";
            if (TempMessage != null)
                htmlToDisplay += TempMessage.ToString();
            htmlToDisplay += "</script>";
            html.ViewContext.TempData["TempJavaScript"] = null;
            return htmlToDisplay;
        }

        public static String Submit(this HtmlHelper html, String value)
        {
            return String.Format("<input type=\"submit\" value=\"{0}\"/>", value);
        }

        public static String File(this HtmlHelper html, String name, String value)
        {

            return String.Format("<input name=\"{0}\" type=\"file\" value=\"{1}\" />",name, value);
        }

        public static string GetControllerName(HtmlHelper htmlHelper)
        {
            RouteData routeData = htmlHelper.ViewContext.RouteData;
            return routeData.GetRequiredString("controller");
        }

        public static String Link(this HtmlHelper html, String text, String link, object htmlAttributes)
        {
            var linkTag = new TagBuilder("a");
            linkTag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            linkTag.MergeAttribute("href", link);
            linkTag.SetInnerText(text);
            return linkTag.ToString(TagRenderMode.Normal);
        }
    }
}
