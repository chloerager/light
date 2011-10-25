using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using light;
using light.Data;
using light.Ajax;

namespace light.Ajax
{
   public class AjaxFactory
   {
      /// <summary>
      ///  Ajax Handler Factory
      /// </summary>
      /// <param name="cmd"></param>
      /// <param name="context"></param>
      /// <returns></returns>
      public static AjaxMethod Create(string cmd,HttpContext context)
      {
         IDictionary<string, AjaxMethod> methods =context.Application["AJAX_METHOD"] as IDictionary<string, AjaxMethod>;

         if (methods == null)
         {
            methods = BuildMethodDict();
            if (methods != null) context.Application["AJAX_METHOD"] = methods;
         }

         if (methods != null && methods.ContainsKey(cmd)) return methods[cmd];

         return null;
      }

      /// <summary>
      ///  
      /// </summary>
      /// <returns></returns>
      private static IDictionary<string, AjaxMethod> BuildMethodDict()
      {
         IDictionary<string, AjaxMethod> methods = new Dictionary<string, AjaxMethod>();

         methods.Add("login", UserAjaxMethods.Login);
         methods.Add("logout", UserAjaxMethods.Logout);
         methods.Add("c_name_email", UserAjaxMethods.CheckNameAndEmail);
         methods.Add("savebaseinfo", UserAjaxMethods.SaveBaseInfo);
         methods.Add("saverole", UserAjaxMethods.SaveRole);
         methods.Add("event_close", EventRequestHandler.Close);
         methods.Add("event_friend", EventRequestHandler.BeFriend);
         methods.Add("invitecode", UserAjaxMethods.InviteCode);
         methods.Add("signup", UserAjaxMethods.Signup);
         methods.Add("change_pwd", UserAjaxMethods.ChangePassword);
         methods.Add("getdistrict", StaticAjaxMethods.GetDistrict);
         methods.Add("saveavatar", ImageAjaxMethods.SaveAvatar);
         
         //load from db.
         LoadRegister(methods);

         return methods;
      }

      private static void LoadRegister(IDictionary<string, AjaxMethod> methods)
      {
         IList<string> types = AjaxData.List();

         if (types != null && types.Count > 0)
         {
            foreach (string t in types)
            {
               Type type = Type.GetType(t, false);
               if (type != null)
               {
                  IAjaxMethods r = Activator.CreateInstance(type) as IAjaxMethods;
                  if (r != null) r.RegisterMethod(methods);
               }
            }
         }
      }
   }
}
