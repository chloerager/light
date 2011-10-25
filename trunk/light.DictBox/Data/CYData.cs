using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;
using System.Data;

namespace light.DictBox.Data
{
   public class CYData
   {

      internal static IList<CYEntity> List(int count)
      {
         return EB<CYEntity>.List(DBQA.DBCS_DICT_CY, CommandType.Text, "SELECT TOP " + count + " id, name,pinyinabbr FROM chengyu ORDER BY newid();");
      }
   }
}
