using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light.CMS.Entities
{
   public class ContentEntity
   {
      /// <summary>
      ///  内容标识
      /// </summary>
      [Field("id",true,true,true)]
      public int id;

      /// <summary>
      ///  系统分类标识
      /// </summary>
      [Field("cid")]
      public int cid;

      /// <summary>
      ///  用户分类标识
      /// </summary>
      [Field("ucid")]
      public int ucid;

      [Field("uid")]
      public int uid;

      [Field("status")]
      public int status;

      [Field("created",true)]
      public DateTime created;

      [Field("name")]
      public string name;

      [Field("url")]
      public string url;

      [Field("uname")]
      public string uname;

      [Field("source",allowNulls:true)]
      public string source;

      [Field("sourceurl",allowNulls:true)]
      public string sourceurl;

      [Field("summary",allowNulls:true)]
      public string summary;

      /// <summary>
      ///  标签
      /// </summary>
      [Field("tags", allowNulls: true)]
      public string tags;

      [Field("keyword",allowNulls:true)]
      public string keyword;

      [Field("story")]
      public string story;
   }
}
