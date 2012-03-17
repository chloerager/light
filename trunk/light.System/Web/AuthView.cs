using System.Web;
using System.Web.UI;
using light;
using light.Entities;

namespace light.Web
{
   public class AuthView : Page
   {
      protected UserEntity CurrentAccount = null;
      protected UserStatusEntity CurrentUserStatus = null;

      public AuthView()
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
               if (CurrentAccount != null)
               {
                  Page.Items[SQA.CURRENT_ACCOUNT] = CurrentAccount;
                  CurrentUserStatus = UserAccount.GetStatus(CurrentAccount.id);
                  if (CurrentUserStatus != null) Page.Items[SQA.CURRENT_USER_STATUS] = CurrentUserStatus;
               }
            }
         }
         else context.Response.Redirect("/login");
      }
   }
}
