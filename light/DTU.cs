using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light
{
   /// <summary>
   ///  Date Time Utilities
   /// </summary>
   public class DTU
   {
      public int TicksFrom2011(DateTime dt)
      { 
         const int t2011= 111;
         return (int)(dt.Ticks - t2011);
      }
   }
}
