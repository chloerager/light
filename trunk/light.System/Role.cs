using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Entities;
using light.Data;

namespace light
{
   public class Role
   {
      public static IList<AppEntity> ActionList(int roleid)
      {
         //from cache first

         return RoleData.GetAppList(roleid);
      }

      internal static string GetDisplayName(int roleid)
      {
         return RoleData.GetDisplayName(roleid);
      }
   }
}
