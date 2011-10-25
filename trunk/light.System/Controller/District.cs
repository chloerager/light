using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.System.Entities;
using light.System.Data;

namespace light.System.Controller
{
   public sealed class District
   {
      public static IList<DistrictEntity> ListDistrict(int level, int pid)
      {
         return DistrictData.ListDistrict(level, pid);
      }

      public static string Build2L(int id, string name, int sid, string sname)
      {
         LocationEntity entity = new LocationEntity()
         {
            id = id,
            name = name,
            sub = new LocationEntity()
            {
               id = sid,
               name = sname
            }
         };

         return JSON.Instance.ToJSON(entity);
      }
   }
}
