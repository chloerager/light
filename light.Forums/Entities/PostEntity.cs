using light.Data;
using System;

namespace light.Forums.Entities
{
   /// <summary>
   ///  论坛回帖实体定义
   /// </summary>
   [Table("forum_post")]
   public class PostEntity
   {
      [Field("pid",true,true,true)]
      public int pid;

      [Field("bid")]
      public int bid;

      [Field("tid")]
      public int tid;

      /// <summary>
      /// 回复标识
      /// </summary>
      [Field("rid")]
      public int rid;

      [Field("uid")]
      public int uid;

      /// <summary>
      ///  支持
      /// </summary>
      [Field("agree")]
      public int agree;

      /// <summary>
      ///  反对
      /// </summary>
      [Field("disagree")]
      public int disagree;

      [Field("uname")]
      public string uname;

      [Field("name")]
      public string name;

      [Field("created",true)]
      public DateTime created;

      [Field("uip")]
      public string uip;

      [Field("invisible")]
      public bool invisible;

      [Field("anonymous")]
      public bool anonymous;

      [Field("rate")]
      public int rate; //评分

      [Field("ratetimes")]
      public int ratetimes;

      [Field("status")]
      public byte status;

      [Field("tags",allowNulls:true)]
      public string tags;

      [Field("story")]
      public string story;
   }
}
