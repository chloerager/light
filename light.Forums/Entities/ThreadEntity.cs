using System;
using light.Data;

namespace light.Forums.Entities
{
   /// <summary>
   ///  论坛主题贴实体定义
   /// </summary>
   [Table("forum_thread")]
   public class ThreadEntity
   {
      /// <summary>
      ///  标识
      /// </summary>
      [Field("tid",true,true,true)]
      public int tid;

      /// <summary>
      ///  所属论坛标识
      /// </summary>
      [Field("bid")]
      public int bid;

      /// <summary>
      /// 
      /// </summary>
      [Field("posttableid")]
      public int posttableid;

      [Field("type")]
      public int type;

      //[Field("sortid")]
      //public int sortid;

      //[Field("readperm")]
      //public byte readperm;


      [Field("icon")]
      public int icon;

      [Field("uid")]
      public int uid;

      [Field("views")]
      public int views;

      [Field("replies")]
      public int replies;

      [Field("displayorder")]
      public int displayorder;

      [Field("highlight")]
      public byte highlight;

      [Field("digest")]
      public byte digest; //精华1，精华2

      [Field("rate")]
      public int rate;

      //[Field("special")]
      //public byte special;
      
      [Field("attachment")]
      public bool attachment;

      [Field("image")]
      public byte image;

      [Field("moderated",true)]
      public DateTime moderated; //修改时间

      //[Field("closed")]
      //public bool closed;

      //[Field("stickreply")]
      //public byte stickreply;

      //[Field("recommends")]
      //public byte recommends;

      [Field("status")]
      public byte status;

      //[Field("favtimes")]
      //public int favtimes; //分享次数

      //[Field("sharetimes")]
      //public int sharetimes;  //收藏次数

      [Field("uname")]
      public string uname;

      [Field("name")]
      public string name;

      [Field("created", true)]
      public DateTime created;

      [Field("lastposter",true,allowNulls:true)]
      public string lastposter;
   }
}
