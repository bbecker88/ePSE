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

        public static string PostMessage(this HtmlHelper helper)
        {
            var message = helper.ViewContext.TempData["TempPostMessage"];
            var messageType = helper.ViewContext.TempData["TempPostMessageType"];

            if (message != null)
            {
                return String.Format("<div class=\"{0}\">{1}</div>", messageType.ToString(), helper.Encode(message));
            }
            return String.Empty;
        }


        public static string Image(string relativePath, string title)
        {
            if (title != "")
                return "<img src=\"" + relativePath + "\" title=\"" + title + "\" />";
            else
                return "<img src=\"" + relativePath + "\"/>";
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
