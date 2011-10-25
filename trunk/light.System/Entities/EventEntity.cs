using System;
using light.Data;

namespace light.Entities
{
   [Table("event")]
   public class EventEntity
   {
      /// <summary>
      ///  事件标识，自动增长
      /// </summary>
      [Field("id",true,true,true)]
      public int id;

      /// <summary>
      ///  事件类别
      /// </summary>
      [Field("type")]
      public byte type;

      /// <summary>
      ///  是否过期
      /// </summary>
      [Field("expired")]
      public bool expired;

      /// <summary>
      ///  模板标识
      /// </summary>
      [Field("tmplname")]
      public string tmplname;

      /// <summary>
      ///  创建时间
      /// </summary>
      [Field("created",true)]
      public DateTime created;

      /// <summary>
      ///  扩展数据
      /// </summary>
      [Field("data")]
      public string data;
   }
}
