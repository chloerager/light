using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using light.Entities;
using light.Data;

namespace light.Data
{
   public class DistrictData
   {
      internal static IList<DistrictEntity> ListDistrict(int levelcode, int pid)
      {
         return EB<DistrictEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM district WHERE pid=@pid AND levelcode=@levelcode", new SqlParameter("@pid", pid), new SqlParameter("@levelcode", levelcode));
      }
   }
}
