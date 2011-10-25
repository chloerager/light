using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace light.Data
{
   public class AjaxData
   {

      internal static IList<string> List()
      {
         return DBH.GetList<string>(QA.DBCS_MAIN, CommandType.Text, "SELECT typename FROM ajaxmethods");
      }
   }
}
