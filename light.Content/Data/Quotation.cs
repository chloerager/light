using System.Collections.Generic;
using System.Data;
using light.Content.Entities;
using light.Data;

namespace light.Content.Data
{
   public class Quotation
   {
      public IList<QuotationEntity> RandomList(int count)
      {
         return EB<QuotationEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP " + count + " * FROM quotation ORDER BY newid()");
      }
   }
}
