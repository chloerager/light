
using light.Entities;
using light.Data;
namespace light
{
   public sealed class FileController
   {
      public static int CreateAttachment(AttachmentEntity info)
      {
         return FileDB.CreateAttachment(info);
      }

      public static AttachmentEntity GetUploadInfo(int type, int accountid, int referid)
      {
         return FileDB.Get(type, accountid, referid);
      }

      public static string GetFilePhysicalPath(int type, int id)
      {
         return FileDB.GetFilePhysicalPath(type, id);
      }


   }
}
