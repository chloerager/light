using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light.SR
{
   public class ReposityMeta
   {
      public int Id;

      public int Total;

      public byte OP; //insert0,update1,delete2,insert&update3

      public string Name;

      public string Table1;

      public string Table2;

      public string Table3;

      public IList<ReposityMap> Maps;
   }

   public class ReposityMap
   {
      public int Id;

      public int RId;

      public int TableIndex;

      public bool PK;

      public bool AutoId;

      public string Name;

      public string Field;
   }
}
