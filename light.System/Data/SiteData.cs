using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace light.Data
{
   public sealed class SiteData
   {
      public static string GetSetting(string name)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT svalue FROM setting WHERE skey=@name", new SqlParameter("@name", name));
      }
   }
}
