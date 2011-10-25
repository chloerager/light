using System.Data;
using System.Data.SqlClient;
using light.Entities;

namespace light.Data
{
   public class FileDB
   {
      public static int CreateAttachment(AttachmentEntity attach)
      {
         return EB<AttachmentEntity>.Create(QA.DBCS_MAIN,attach);
      }

      internal static AttachmentEntity Get(int type, int uid, int referid)
      {
         return EB<AttachmentEntity>.Get("SELECT TOP 1 * FROM attachment WHERE type=@type AND uid=@uid AND refer_id=@refer_id", new SqlParameter("@type", type), new SqlParameter("@uid", uid), new SqlParameter("@referid", referid));
      }

      internal static string GetFilePhysicalPath(int type, int uid)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP 1 physicalpath FROM attachment WHERE type=@type AND uid=@uid ORDER BY created DESC", new SqlParameter("@type", type), new SqlParameter("@uid", uid));
      }

      public static bool ExistUploadInfo(int uid, int referid, int type)
      {
         return DBH.GetBoolean(QA.DBCS_MAIN, CommandType.Text, "SELECT COUNT(*) FROM attachment WHERE uid=@uid AND referid=@referid AND type=@type",
            new SqlParameter("@uid", uid),
            new SqlParameter("@referid", referid),
            new SqlParameter("@type", type));
      }
   }
}
