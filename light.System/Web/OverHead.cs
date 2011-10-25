using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace light.Web
{
   public class OverHead : WebControl
   {
      protected override void Render(HtmlTextWriter output)
      {
         output.Write("<div id=\"overhead\"><h1><a href=\"/\" title=\"\" id=\"logo\"></a></h1><div id=\"oh-info\">");
         output.Write("<div class=\"fleft\">" + SiteSetting.GetString("site_navlink") + "</div>");
         if (Context.Request.IsAuthenticated)
         {
            output.Write("<div class=\"fright\"><a href=\"/home/\" id=\"setting\" class=\"noborder\">个人中心</a><a href=\"/home/setting\" id=\"setting\">设置</a><a href=\"javascript:;\" id=\"logout\">退出</a></div>");
         }
         else
         {
            output.Write("<div class=\"fright\"><a href=\"/login\" class=\"noborder\">登录</a></div>");
         }
         output.Write("</div></div>");
      }
   }
}