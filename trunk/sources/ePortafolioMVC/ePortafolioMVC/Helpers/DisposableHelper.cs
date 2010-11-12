using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ePortafolioMVC.Helpers
{
    class DisposableHelper:IDisposable
    {
        private Action end;
        public DisposableHelper(Action begin, Action end)
        {
            this.end = end;
            begin();
        }

        public void Dispose()
        {
            end();
        }
    }

    public static class DisposableExtensions
    {
        public static IDisposable BeginBorder(this HtmlHelper htmlHelper)
        { 
        return new DisposableHelper(
            delegate{   var httpResponse = htmlHelper.ViewContext.HttpContext.Response;
            httpResponse.Write("<div class=\"post\">");
            httpResponse.Write("<div class=\"post-bgtop\">");
            httpResponse.Write("<div class=\"post-bgbtm\">");
            },
         delegate{   var httpResponse = htmlHelper.ViewContext.HttpContext.Response;
         httpResponse.Write("</div>");
         httpResponse.Write("</div>");
         httpResponse.Write("</div>");
         });
        
        }
    
    
    }

}
