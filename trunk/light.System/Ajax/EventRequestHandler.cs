using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using light.Data;

namespace light.Ajax
{
   public class EventRequestHandler
   {
      public static void Close(HttpContext context)
      {
         if (context.Request.IsAuthenticated)
         {
            int eid = CU.ToInt(context.Request.QueryString["eid"]);

            if (eid > 0)
            {
               EventData.Hide(eid);
            }
         }
      }

      public static void BeFriend(HttpContext context)
      {
         if (context.Request.IsAuthenticated)
         {
            int uid = CU.ToInt(context.Request.Form["uid"]);
            int mid = CU.ToInt(context.Request.Form["mid"]);
            int eid = CU.ToInt(context.Request.Form["eid"]);

            UserAccount.ConfirmFriend(uid, mid);
            EventData.Close(eid);
            context.Response.Write(JU.Build(true, ""));
            return;
         }

         context.Response.Write(JU.Build(false, "没有登录或登录已失效，请登录后再试"));
      }
   }
}
