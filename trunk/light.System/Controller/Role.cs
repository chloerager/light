using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.System.Entities;
using light.System.Data;

namespace light.System.Controller
{
   public class Role
   {
      public static IList<ActionEntity> ActionList(int roleid)
      {
         //from cache first

         return RoleData.GetActionList(roleid);
      }

      internal static string GetDisplayName(int roleid)
      {
         return RoleData.GetDisplayName(roleid);
      }
   }
}
