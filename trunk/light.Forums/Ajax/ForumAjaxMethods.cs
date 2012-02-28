using System.Collections.Generic;
using System.Web;
using light.Ajax;
using light.Entities;

namespace light.Forums.Ajax
{
   public class ForumAjaxMethods : IAjaxMethods
   {
      public void SaveThread(HttpContext context)
      {
         if (context.Request.IsAuthenticated)
         {
            UserEntity u = UserAccount.Current;
            int bid = CU.ToInt(context.Request.Form["bid"]);
            int tid = CU.ToInt(context.Request.Form["tid"]);
            string name = context.Request.Form["name"];
            string story = context.Request.Form["story"];
            string ip = QA.ClientIP;

            if (tid > 0)
            {
               int ret = Forum.SavePost(bid,tid, u.id, u.name, name, ip, story);
               if (ret > 0)
               {
                  //更新统计数据
                  Forum.IncThreadReplies(tid);

                  string url = string.Concat("/bbs/thread/", bid, "_", tid, ".html#", ret);
                  context.Response.Write(JU.Build(true, url));
               }
            }
            else
            {
               int ret = Forum.SaveThread(bid, u.id, u.name, name, ip, story);
               if (ret > 0)
               {
                  string url = string.Concat("/bbs/thread/", bid, "_", ret, ".html");
                  context.Response.Write(JU.Build(true, url));
               }
            }
         }
         else
            context.Response.Write(JU.Build(false, "没有权限发帖"));
      }

      public int RegisterMethod(IDictionary<string, AjaxMethod> methods)
      {
         methods.Add("savethread", SaveThread);
         return 0;
      }
   }
}
