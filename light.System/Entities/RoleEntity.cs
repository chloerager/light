using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light.Entities
{
   [Table("role")]
   public class RoleEntity
   {
      [Field("id",false,true)]
      public int id;

      [Field("name")]
      public string name;

      [Field("displayname")]
      public string displayname;

      [Field("description", allowNulls: true)]
      public string description;
   }
}
