using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light.DictBox.Entities
{
   public class ShiCiEntity
   {
      [Field("id",true,true,true)]
      public int id;

      [Field("uid")]
      public int uid;

      [Field("name")]
      public string name;

      [Field("url")]
      public string url;

      [Field("uname")]
      public string uname;

      [Field("era")]
      public string era;

      [Field("category")]
      public string category;

      [Field("story")]
      public string story;

      [Field("mean",allowNulls:true)]
      public string mean;

      [Field("notes",allowNulls:true)]
      public string notes;

      [Field("remarks",allowNulls:true)]
      public string remarks;
   }
}
