using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace light.Data
{
   public class KeywordData
   {
      public static HashSet<string> GetReserve()
      {
         return DBH.GetHashSet<string>(QA.DBCS_MAIN, CommandType.Text, "SELECT word FROM keyword WHERE type=0");
      }
   }
}
