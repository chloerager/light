using System.Linq;
using System.Collections.Generic;
using light.DictBox.Data;
using System.Text;
namespace light.DictBox
{
   public class CY
   {
      public static string RenderDailyContent(int count)
      {
         StringBuilder sb = new StringBuilder(60);

         IList<CYEntity> list = CYData.List(count);

         if (list != null && list.Count > 0)
         { 
            foreach(CYEntity cy in list)
            {
               sb.Append(" " + cy.ShortLink);
            }
         }

         return sb.ToString();
      }
   }
}
