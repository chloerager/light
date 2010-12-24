using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.System.Entities;
using System.Data;
using System.Data.SqlClient;

namespace light.System.DAL
{
   public class KV
   {
      /// <summary>
      ///  获取name指定的KVEntity
      /// </summary>
      /// <param name="name">KVEntity的name</param>
      /// <returns>如果存在返回对应的实体对象，否则返回null</returns>
      public static KVEntity Get(string name)
      {
         return EB<KVEntity>.Get(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM keyvalue WHERE name=@name",ToEntity,
            new SqlParameter("@name", name));
      }

      public static string GetValue(string name)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT value FROM keyvalue WHERE name=@name",
            new SqlParameter("@name", name));
      }

      public static int Update(string name, string value)
      {
         return DBH.ExecuteSP(QA.DBCS_MAIN, "usp_update_keyvalue",
            new SqlParameter("@name", name),
            new SqlParameter("@value", value));
      }

      public static int Update(string name, string value, int expiration)
      {
         return DBH.ExecuteSP(QA.DBCS_MAIN, "usp_update_keyvalue",
            new SqlParameter("@name", name),
            new SqlParameter("@value", value),
            new SqlParameter("@expiration", expiration));
      }

      private static KVEntity ToEntity(IDataReader dr)
      {
         DRM m = new DRM(dr);
         return new KVEntity() { 
            Name = m.Name,
            Value = m.GetString("value"),
            Expiration = m.GetInt32("expiration"),
            UpdateDate = m.GetDateTime("update_date"),
            UpdateTimes = m.GetInt32("update_times")
         };
      }
   }
}
