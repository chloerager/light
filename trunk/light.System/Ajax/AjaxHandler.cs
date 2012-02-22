using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace light.Ajax
{
   public class AjaxHandler : IHttpHandler
   {
      public void ProcessRequest(HttpContext context)
      {
         Handle(context);
      }

      private static void Handle(HttpContext context)
      {
         string cmd = context.Request.Params["m"];
         if (!string.IsNullOrEmpty(cmd))
         {
            AjaxMethod method = AjaxFactory.Create(cmd, context);
            if (method != null) method(context);
            else context.Response.Write(JU.Build(false, 404, "Ajax调用出错"));
         }
      }

      public bool IsReusable
      {
         get { return false; }
      }
   }
}
