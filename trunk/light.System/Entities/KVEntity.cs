using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light.System.Entities
{
   /// <summary>
   ///  key_value
   /// </summary>
   public class KVEntity
   {
      /// <summary>
      ///  key
      /// </summary>
      public string Name;

      /// <summary>
      ///  value
      /// </summary>
      public string Value;

      /// <summary>
      ///  过期时间(小时)
      /// </summary>
      public int Expiration;

      /// <summary>
      /// 更新时间
      /// </summary>
      public int UpdateTimes;

      /// <summary>
      ///  最后更新日期
      /// </summary>
      public DateTime UpdateDate;
   }
}
