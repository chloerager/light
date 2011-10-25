using light.Data;
using System;

namespace light.Forums.Entities
{
   /// <summary>
   ///  论坛版块实体
   /// </summary>
   [Table("forum_board")]
   public sealed class BoardEntity
   {
      /// <summary>
      ///  论坛标识
      /// </summary>
      [Field("bid", true, true, true)]
      public int bid;

      /// <summary>
      ///  版块类型：群组(0)，论坛分类(1)，论坛(2)，子论坛(3)
      /// </summary>
      [Field("type")]
      public byte type;

      /// <summary>
      ///  版块状态：0正常，1隐藏，2关闭，3只读...
      /// </summary>
      [Field("status")]
      public byte status;

      /// <summary>
      ///  显示顺序
      /// </summary>
      [Field("displayorder")]
      public int displayorder;

      /// <summary>
      ///  帖子总数
      /// </summary>
      [Field("threads")]
      public int threads;

      /// <summary>
      ///  回复总数
      /// </summary>
      [Field("posts")]
      public int posts;

      /// <summary>
      ///  今天版块发帖量
      /// </summary>
      [Field("todayposts")]
      public int todayposts;

      /// <summary>
      ///  昨天版块发帖量
      /// </summary>
      [Field("yesterdayposts")]
      public int yesterdayposts;

      /// <summary>
      ///  允许表情
      /// </summary>
      [Field("allowsmilies", true)]
      public bool allowsmilies;

      /// <summary>
      ///  允许html
      /// </summary>
      [Field("allowhtml", true)]
      public bool allowhtml;

      /// <summary>
      ///  允许bbcode
      /// </summary>
      [Field("allowbbcode", true)]
      public bool allowbbcode;

      /// <summary>
      ///  允许图像代码
      /// </summary>
      [Field("allowimgcode", true)]
      public bool allowimgcode;

      /// <summary>
      ///  允许媒体代码
      /// </summary>
      [Field("allowmediacode", true)]
      public bool allowmediacode;

      /// <summary>
      ///  允许匿名访问
      /// </summary>
      [Field("allowanonymous", true)]
      public bool allowanonymous;

      /// <summary>
      ///  允许种子
      /// </summary>
      [Field("allowfeed", true)]
      public bool allowfeed;

      /// <summary>
      ///  允许边栏
      /// </summary>
      [Field("allowside", true)]
      public bool allowside;

      /// <summary>
      ///  删除是否回收
      /// </summary>
      [Field("recyclebin", true)]
      public bool recyclebin;

      /// <summary>
      ///  是否启用图片水印
      /// </summary>
      [Field("enablewatermark", true)]
      public bool enablewatermark;

      /// <summary>
      ///  是否启用内容干扰
      /// </summary>
      [Field("jammer", true)]
      public bool jammer;

      /// <summary>
      ///  版块名称
      /// </summary>
      [Field("name")]
      public string name;

      /// <summary>
      /// 描述
      /// </summary>
      [Field("description")]
      public string description;

      /// <summary>
      ///  规则条例
      /// </summary>
      [Field("regulations",allowNulls:true)]
      public string regulations;

      [Field("bmlist", allowNulls: true)]
      public string bmlist;

      /// <summary>
      ///  最新回复
      /// </summary>
      [Field("ltuid", allowNulls: true)]
      public int ltuuid;

      [Field("ltid", allowNulls: true)]
      public int ltid;

      [Field("ltuser", allowNulls: true)]
      public string ltuser;

      [Field("ltname", allowNulls: true)]
      public string ltname;

      [Field("ltcreated", allowNulls: true)]
      public DateTime ltcreated;
   }
}
