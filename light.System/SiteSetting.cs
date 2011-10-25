using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light
{
   public sealed class SiteSetting
   {
      public static string GetString(string name)
      {
         string ckey = "_setting_" + name;
         string value = CacheService.Get(ckey) as string;

         if (string.IsNullOrEmpty(value))
         {
            value = SiteData.GetSetting(name);
            if (value != null) CacheService.AddSliding(ckey, value);
         }

         return value;
      }

      public int SetString(string name)
      {
         string ckey = "_setting_" + name;
         return 0;
      }
   }
}
