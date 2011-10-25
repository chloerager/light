using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace light.Ajax
{
   public class GoHandler : IHttpHandler
   {
      public void ProcessRequest(HttpContext context)
      {
         string k = context.Request.QueryString["k"];
         if (string.IsNullOrEmpty(k) == false)
         {
            string url = Link.GetUrl(k);
            if (url != null) context.Response.Redirect(url, true);
         }

         context.Response.Redirect("/sorry/404");
      }

      public bool IsReusable
      {
         get
         {
            return true;
         }
      }
   }
}
