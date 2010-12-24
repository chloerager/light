using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light
{
   /// <summary>
   ///  SQL Builder
   /// </summary>
   public sealed class SB
   {
      public static string Select(string tableName, string filedName)
      {
         return string.Concat("SELECT ", filedName, " FROM ", tableName);
      }

      public static string Select(string tableName,string fieldName,string where)
      {
         return string.Concat("SELECT ", fieldName, " FROM ", tableName, " WHERE ", where);
      }
   }
}
