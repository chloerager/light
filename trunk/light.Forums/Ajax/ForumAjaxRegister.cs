using System.Collections.Generic;
using System.Web;
using light.Ajax;
using light.Entities;

namespace light.Forums.Ajax
{
   public class ForumAjaxRegister : IAjaxMethods
   {
      public void SaveThread(HttpContext context)
      {
         if (context.Request.IsAuthenticated)
         {
            UserEntity u = UserAccount.Current;

            string name = context.Request.Form["name"];
            string story = context.Request.Form["story"];

            Forum.SaveThread(u.id, u.name, name, story);
         }

         context.Response.Write(JU.Build(false, "没有权限发帖"));
      }

      public void SavePost(HttpContext context)
      {
         string story = context.Request.Form["story"];


      }

      public int RegisterMethod(IDictionary<string, AjaxMethod> methods)
      {
         return 0;
      }
   }
}
