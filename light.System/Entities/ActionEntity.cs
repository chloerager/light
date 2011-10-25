using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light.Entities
{
   [Table("app_action")]
   public sealed class ActionEntity
   {
      [Field("id",true,true,true)]
      public int id;

      [Field("appid")]
      public int appid;

      [Field("iframe")]
      public bool iframe;

      [Field("name")]
      public string name;

      [Field("icon")]
      public string icon;

      [Field("url")]
      public string url;

      [Field("opname",allowNulls:true)]
      public string opname;

      [Field("opurl",allowNulls:true)]
      public string opurl;
   }
}
