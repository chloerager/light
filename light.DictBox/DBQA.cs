using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light.DictBox
{
   public class DBQA
   {
      public static string DBCS_DICT_CY
      {
         get
         {
            return QA.GetConfig("DBCS_DICT_CY", "");
         }
      }
   }
}
