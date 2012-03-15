using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using light;
using light.Data;
using light.Entities;

namespace light.Web
{
   public class HomeMenu : WebControl
   {
      private UserEntity user = null;
      protected override void OnInit(EventArgs e)
      {
         user = Page.Items[SQA.CURRENT_ACCOUNT] as UserEntity;
         if (user == null) user = UserAccount.Current;
         base.OnInit(e);
      }

      protected override void Render(HtmlTextWriter output)
      {
         if (user == null)
         {
            RenderLogin(output);
         }
         else
         {
            RenderAvatar(output);
            RenderMenuTree(output);
         }
      }

      private void RenderLogin(HtmlTextWriter output)
      {
         output.Write("<div class=\"mt-login\"><a href=\"/login\" class=\"button\">登录</a></div>");
      }

      private void RenderMenuTree(HtmlTextWriter output)
      {
         IUserStatusEntity curUserStatus = Page.Items[SQA.CURRENT_USER_STATUS] as IUserStatusEntity;

         IList<int> roleList = null;

         if (curUserStatus != null && curUserStatus.hasRoleEx)
         {
            roleList = RoleData.GetRoleExList(user.id);
         }

         if (roleList == null) roleList = new List<int>();
         roleList.Insert(0, user.roleid);

         foreach (int roleid in roleList)
         {
            //TODO:CACHE
            IList<AppEntity> list = Role.ActionList(roleid);

            output.Write("<div class=\"f14 fb pl10 mt-title clear\">" + Role.GetDisplayName(roleid) + "</div><div class=\"mt-item-list\">");

            foreach (AppEntity action in list)
            {
               string text = action.name;
               string link = action.url;
               string icon = action.icon;
               string optext = (action.opname == null) ? "" : action.opname;
               string oplink = (action.opurl == null) ? "" : action.opurl;
               output.Write("<div><a class=\"main\" href=\"" + link + "\" style=\"background-image:url('/i/app/" + icon + "');\">" + text + "</a><a class=\"action\" href=\"" + oplink + "\">" + optext + "</a></div>");
            }
            output.Write("</div>");
         }
      }

      private void RenderAvatar(HtmlTextWriter output)
      {
         output.Write("<div class=\"mt-index\"><a class=\"ui-56\" href=\"/home/\" style=\"background-image:url(" + user.avatar + ")\"></a>" +
            "<div class=\"ui-text\"><b>" + user.name + "</b><br /><!--<a href=\"/hezi/\" target=\"_blank\">我的名片</a>--></div></div>");
      }
   }
}