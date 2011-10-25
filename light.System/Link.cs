using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light
{
   public sealed class Link
   {
      public static string GetUrl(string name)
      {
         return LinkData.GetUrl("sitelink", name);
      }
   }
}
