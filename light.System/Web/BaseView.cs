using System.Web;
using System.Web.UI;
using light.Entities;
using light;

namespace light.Web
{
   /// <summary>
   ///  基础页面
   /// </summary>
   public class BaseView : Page
   {
      protected UserEntity CurrentAccount = null;
      protected UserStatusEntity CurrentUserInfo = null;

      public BaseView()
      {
         HttpContext context = HttpContext.Current;

         if (context.Request.IsAuthenticated)
         {
            int id = 0;
            string sid = context.User.Identity.Name;
            if (!string.IsNullOrEmpty(sid)) int.TryParse(sid, out id);
            if (id > 0)
            {
               CurrentAccount = UserAccount.Get(id);
               Page.Items[SQA.CURRENT_ACCOUNT] = CurrentAccount;
               if (CurrentAccount != null) CurrentUserInfo = UserAccount.GetStatus(CurrentAccount.id);
            }
         }

         //if (!string.IsNullOrEmpty(host) && host.Contains("shuilu.net"))
         //{
         //   SiteName = "水陆联盟";
         //}
         //else
         //{
         //   SiteName = "挑战Me";
         //}
      }
   }
}
