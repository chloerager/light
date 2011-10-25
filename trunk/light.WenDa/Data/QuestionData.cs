using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using light.Data;
using light.WenDa.Entities;

namespace light.WenDa.Data
{
   public sealed class QuestionData
   {
      public static int Create(QuestionEntity entity)
      {
         return EB<QuestionEntity>.Create(QA.DBCS_MAIN, entity);
      }

      public static int Update(QuestionEntity entity)
      {
         return EB<QuestionEntity>.Update(QA.DBCS_MAIN, entity);
      }

      public static QuestionEntity Get(int id)
      {
         return EB<QuestionEntity>.Get("SELECT * FROM question WHERE id=@id", new SqlParameter("@id", id));
      }

      public static IList<QuestionEntity> List(int count)
      {
         return EB<QuestionEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP " + count + " id,authorid,good,likes,created,author,title FROM question ORDER BY created DESC");
      }

      public static IList<QuestionEntity> Page(int size, int page)
      {
         return null;
      }
   }
}
