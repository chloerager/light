using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using light;
using light;
using light.Ajax;
using light.Entities;

namespace light.Ajax
{
   public class ImageAjaxMethods
   {
      /// <summary>
      ///  用户图像保存
      /// </summary>
      /// <param name="context"></param>
      public static void SaveAvatar(HttpContext context)
      {
         int x = CU.ToInt(context.Request.QueryString["x"]);
         int y = CU.ToInt(context.Request.QueryString["y"]);
         int w = CU.ToInt(context.Request.QueryString["w"]);
         int h = CU.ToInt(context.Request.QueryString["h"]);

         if (w == 0 || h == 0) { context.Response.Write(JU.AJAX_FAIL); return; }

         UserEntity user = UserAccount.Current;
         if (user != null)
         {
            //get the originalfile & crop it.
            string filePath = FileController.GetFilePhysicalPath(1, user.id);
            if (File.Exists(filePath))
            {
               string avatarPath = context.Server.MapPath("~/") + @"\s\u\avatar\";
               if (!Directory.Exists(avatarPath)) Directory.CreateDirectory(avatarPath);

               if (IU.Crop(filePath, avatarPath + user.www + ".png", new Rectangle(x, y, w, h), 50, 50))
               {
                  string tURL = "/s/u/avatar/" + user.www + ".png";
                  if (UserAccount.SetAvatar(tURL, user.id) > 0)
                  {
                     context.Response.Write(JU.Build(true, tURL));
                     return;
                  }
               }
            }
         }

         context.Response.Write(JU.AJAX_FAIL);
      }
   }
}
