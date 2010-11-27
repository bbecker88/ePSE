using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Helpers
{
    public static class HtmlHelpers
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }

        public static string Image(string relativePath, string title)
        {
            if (title != "")
                return "<img src=\"" + relativePath + "\" title=\"" + title + "\" />";
            else
                return "<img src=\"" + relativePath + "\"/>";
        }

        public static string ResultadoProgramaTable(this HtmlHelper html, BEResultadoPrograma ResultadoPrograma)
        {
            if (ResultadoPrograma == null)
                return "";

            String Table =  " <br />" +
                            " <table style=\"margin-left:auto; margin-right:auto;\">" +
                            " <tr>" +
                            "     <td style= \"width: 75px;\">#RESULTADOPROGGRAMAID</td>" +
                            "     <td style = \"width: 400px;\">#DESCRIPCION</td>" +
                            " </tr>" +
                            " </table>";
            return Table.Replace("#RESULTADOPROGGRAMAID", html.Encode(ResultadoPrograma.Codigo.ToString())).Replace("#DESCRIPCION", html.Encode(ResultadoPrograma.Descripcion));
        }

        public static string ActionLinkImage(this HtmlHelper html, string imgSrc, string actionName, object routeValues)
        {
            return ActionLinkImage(html, imgSrc, actionName, "", routeValues);
        }

        public static string ActionLinkImage(this HtmlHelper html, string imgSrc, string actionName, string title, object routeValues)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            string imgUrl = urlHelper.Content(imgSrc);
            TagBuilder imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            if (title != "")
                imgTagBuilder.MergeAttribute("title", title);
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);

            string url = urlHelper.Action(actionName, routeValues);

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = img
            };
            tagBuilder.MergeAttribute("href", url);

            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string ActionLinkImage(this HtmlHelper html, string imgSrc, string actionName, string controllerName, string title, object routeValues)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            string imgUrl = urlHelper.Content(imgSrc);
            TagBuilder imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            if (title != "")
                imgTagBuilder.MergeAttribute("title", title);
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);

            string url = urlHelper.Action(actionName, controllerName, routeValues);

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = img
            };
            tagBuilder.MergeAttribute("href", url);

            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}
