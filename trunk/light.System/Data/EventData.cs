using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using light.Data;
using light.Entities;

namespace light.Data
{
   /// <summary>
   ///  event 's data processor.
   /// </summary>
   public class EventData
   {
      /// <summary>
      ///  创建一个事件
      /// </summary>
      /// <param name="entity"></param>
      /// <returns></returns>
      public static int CreateEvent(byte type, byte spread, int uid, int touid, string tmplName, string data = null)
      {
         return DBH.ExecuteSP(QA.DBCS_MAIN, "usp_event_create",
            new SqlParameter("@type", type),
            new SqlParameter("@spread", spread),
            new SqlParameter("@uid", uid),
            new SqlParameter("@touid", touid),
            new SqlParameter("@tmplname", tmplName),
            new SqlParameter("@data", data));
      }

      internal static IList<EventEntity> ListEvent(int uid, int count, int eid)
      {
         return EB<EventEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP " + count + " event.* FROM event_index INNER JOIN event ON event_index.eid=event.id WHERE event_index.uid=@uid AND event_index.eid>@eid",
            new SqlParameter("@uid", uid),
            new SqlParameter("@eid", eid));
      }

      internal static string GetTemplate(string templateName)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT tmpl FROM template WHERE name=@name", new SqlParameter("@name", templateName));
      }

      internal static void Close(int eid)
      {
         DBH.ExecuteText(QA.DBCS_MAIN, "DELETE event_index WHERE eid=@eid", new SqlParameter("@eid", eid));
      }

      internal static void Hide(int eid)
      {
         DBH.ExecuteText(QA.DBCS_MAIN, "UPDATE event_index SET hide=1 WHERE eid=@eid", new SqlParameter("@eid", eid));
      }

      internal static IList<EventEntity> ListUserPublicEvent(int uid, int count)
      {
         return EB<EventEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP " + count + " * FROM event WHERE uid=@uid AND type=0", new SqlParameter("@uid", uid));
      }
   }
}
