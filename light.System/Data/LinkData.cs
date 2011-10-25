using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace light.Data
{
   public class LinkData
   {
      public static string GetUrl(string tableName, string name)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT url FROM " + tableName + " WHERE name=@name", new SqlParameter("@name", name));
      }
   }
}
