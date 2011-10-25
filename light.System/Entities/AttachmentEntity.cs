using light.Data;

namespace light.Entities
{
   [Table("attachment")]
   public class AttachmentEntity
   {
      [Field("id",true,true)]
      public int id;

      [Field("uid")]
      public int uid;

      [Field("referid")]
      public int referid;

      [Field("type")]
      public byte type;

      [Field("url")]
      public string url;

      [Field("physicalpath")]
      public string physicalpath;

      [Field("deleted",true)]
      public bool deleted;
   }
}
