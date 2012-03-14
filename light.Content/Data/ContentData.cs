using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using light.Content.Entities;
using light.Data;

namespace light.Content.Data
{
   public class ContentData
   {
      public static ContentEntity Get(int id,string tableName)
      {
         return EB<ContentEntity>.Get(QA.DBCS_CMS, CommandType.Text, "SELECT * FROM " + tableName + " WHERE id=@id", new SqlParameter("@id", id));
      }

      public static int Create(string tableName, ContentEntity entity)
      {
         return EB<ContentEntity>.Create(QA.DBCS_CMS, entity, tableName);
      }

      public static int Update(ContentEntity entity, string tableName)
      {
         return EB<ContentEntity>.Update(QA.DBCS_CMS, entity, tableName);
      }

      public static IList<ContentEntity> List(string tableName)
      {
         return EB<ContentEntity>.List(QA.DBCS_CMS, CommandType.Text, "SELECT * FROM " + tableName + " ORDER BY created DESC");
      }

      public static ContentEntity Get(string tableName, string key)
      {
         return EB<ContentEntity>.Get("SELECT * FROM " + tableName + " WHERE url=@url", new SqlParameter("@url", key));
      }

      public static ContentEntity Get(string tableName, int id)
      {
         return EB<ContentEntity>.Get("SELECT * FROM " + tableName + " WHERE id=@id", new SqlParameter("@id", id));
      }
   }
}
