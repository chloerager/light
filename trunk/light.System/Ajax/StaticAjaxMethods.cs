using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using light.Entities;
using light;
using light;
using light.Ajax;

namespace light.Ajax
{
   public class StaticAjaxMethods
   {
      public static void GetDistrict(HttpContext context)
      {
         string level = context.Request.QueryString["l"];
         string pid = context.Request.QueryString["p"];
         int l, p;
         int.TryParse(level, out l);
         int.TryParse(pid, out p);

         IList<DistrictEntity> list = District.ListDistrict(l, p);

         if (list != null && list.Count > 0)
         {
            string json = JSON.Instance.ToJSON(list);
            context.Response.Write(JU.BuildJSON(true, json));
            return;
         }
         
         context.Response.Write(JU.AJAX_FAIL);
      }
   }
}
