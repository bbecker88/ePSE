using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolio.Models;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Text;

namespace ePortafolio.Helpers
{
    public static class HtmlHelpers
    {
        public static String ShowTempMessage(this HtmlHelper html)
        {
            return html.ShowTempMessage("TempMessage");
        }

        public static HtmlHelper GetHtmlHelper()
        {
            return new HtmlHelper(new ViewContext(new ControllerContext(), new WebFormView("omg"), new ViewDataDictionary(), new TempDataDictionary(), new StringWriter()), new ViewPage());
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

                htmlToDisplay += String.Format("<div id=\\\"TempMessage\\\" class=\\\"temp-message-container {0}\\\">", clase);
                htmlToDisplay += GetHtmlHelper().Encode(TempMessage.Message);
                htmlToDisplay += "</div>";
                TempData["TempMessage"] = null;
            }

            return String.Format("$('#{0}').html(\"{1}\");", ContainerId, htmlToDisplay);
        }

        public static String ShowTempMessage(this HtmlHelper html, String Id)
        {
            var htmlToDisplay = "";
            var TempMessage = (TempMessage)html.ViewContext.TempData["TempMessage"];
            var GUID = Guid.NewGuid().ToString().Replace("-","").ToUpper();

            if (TempMessage != null)
            {
                var clase = "";

                switch (TempMessage.MessageType)
                {
                    case MessageType.Error: clase = "mensaje msj_error"; break;
                    case MessageType.Info: clase = "mensaje msj_info"; break;
                    case MessageType.Success: clase = "mensaje msj_check"; break;
                }

                htmlToDisplay += String.Format("<div id=\"{0}\" class=\"temp-message-container-{1} {2}\">", Id, GUID, clase);
                htmlToDisplay += html.Encode(TempMessage.Message);
                htmlToDisplay += "</div>";
                html.ViewContext.TempData["TempMessage"] = null;
            }
            /*
            var GUID = Guid.NewGuid().ToString();
            return String.Format("<div id=\"{0}\"></div><script>$('.temp-message-container').hide(); $('#{0}').append('{1}')</script>",GUID, MvcHtmlString.Create(htmlToDisplay).ToHtmlString());
             */
            return "<script>setTimeout( function(){$('.temp-message-container-"+GUID+"').hide('fold');},5000)</script>" + MvcHtmlString.Create(htmlToDisplay).ToHtmlString();

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

        public static String Submit(this HtmlHelper html, String value,object htmlAttributes)
        {

            var input = new TagBuilder("input");
            input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            input.MergeAttribute("type","submit");
            input.MergeAttribute("value",value);
            return input.ToString(TagRenderMode.SelfClosing);
        }

        public static String Link(this HtmlHelper html, String text, String link)
        {
            return Link(html,text,link,false,null);
        }

        public static String Link(this HtmlHelper html, String text, String link, Boolean popup)
        {
            return Link(html, text, link, popup, null);
        }
        public static String Link(this HtmlHelper html, String text, String link, object htmlAttributes)
        {
            return Link(html, text, link, false, htmlAttributes);
        }

        public static String Link(this HtmlHelper html, String text, String link, Boolean popup, object htmlAttributes)
        {
            var onClick = "";

            if (popup)
            {
                onClick = String.Format("window.open('{0}','','scrollbars=yes,menubar=no,height=600,width=800,resizable=yes,toolbar=no,location=no,status=no, top = '+((screen.height/2)-(600/2))+', left = '+((screen.width/2)-(800/2))); return false;", link);
            }

            var linkTag = new TagBuilder("a");
            linkTag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            linkTag.MergeAttribute("href", link);
            linkTag.MergeAttribute("onclick", onClick);
            linkTag.SetInnerText(text);
            return linkTag.ToString(TagRenderMode.Normal);
        }

        public static String File(this HtmlHelper html, String name, String value)
        {

            return String.Format("<input name=\"{0}\" type=\"file\" value=\"{1}\" />",name, value);
        }

        public static String File(this HtmlHelper html, String name, String value, object htmlAttributes)
        {

            var input = new TagBuilder("input");
            input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            input.MergeAttribute("type", "file");
            input.MergeAttribute("name", name);
            input.MergeAttribute("value", value);
            return input.ToString(TagRenderMode.SelfClosing);
        }
        
        public static string GetControllerName(HtmlHelper htmlHelper)
        {
            RouteData routeData = htmlHelper.ViewContext.RouteData;
            return routeData.GetRequiredString("controller");
        }

        public static string GroupDropList(this HtmlHelper helper, string name, IEnumerable<GroupDropListItem> data, string SelectedValue, object htmlAttributes)
        {
            if (data == null && helper.ViewData != null)
                data = helper.ViewData.Eval(name) as IEnumerable<GroupDropListItem>;
            if (data == null) return string.Empty;

            var select = new TagBuilder("select");

            if (htmlAttributes != null)
                select.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            select.GenerateId(name);
            select.MergeAttribute("name", name);

            var optgroupHtml = new StringBuilder();
            var groups = data.ToList();
            foreach (var group in data)
            {
                var groupTag = new TagBuilder("optgroup");
                groupTag.Attributes.Add("label", group.Name);
                var optHtml = new StringBuilder();
                foreach (var item in group.Items)
                {
                    var option = new TagBuilder("option");
                    option.Attributes.Add("value", item.Value);
                    if (SelectedValue != null && item.Value == SelectedValue)
                        option.Attributes.Add("selected", "selected");
                    option.InnerHtml = item.Text;
                    optHtml.AppendLine(option.ToString(TagRenderMode.Normal));
                }
                groupTag.InnerHtml = optHtml.ToString();
                optgroupHtml.AppendLine(groupTag.ToString(TagRenderMode.Normal));
            }
            select.InnerHtml = optgroupHtml.ToString();
            return select.ToString(TagRenderMode.Normal);
        }
    }

    public class GroupDropListItem
    {
        public string Name { get; set; }
        public List<OptionItem> Items { get; set; }
    }

    public class OptionItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}


