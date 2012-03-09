using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace light.SR
{
   public class Reposity
   {
      public static int Operate(int mid, NameValueCollection collection)
      {
         int rtn = 0;

         //get reposity_meta
         ReposityMeta rm = GetMeta(mid);

         string sql = null;
         IList<SqlParameter> spList = new List<SqlParameter>();

         if (rm.OP == 0)
         {

            rtn = DBH.GetInt32(QA.DBCS_MAIN, CommandType.Text, sql, spList.ToArray<SqlParameter>());
         }
         else if (rm.OP == 2)
         {
            rtn = DBH.ExecuteText(QA.DBCS_MAIN, sql, spList.ToArray<SqlParameter>());
         }


         return rtn;
      }

      private static string BuildIUSQL(ReposityMeta rm, NameValueCollection collection)
      {
         return string.Empty;
      }

      private static string BuildDeleteSQL(ReposityMeta rm, NameValueCollection collection)
      {
         return string.Empty;
      }

      private static string BuildUpdateSQL(ReposityMeta rm, NameValueCollection collection)
      {
         return string.Empty;
      }

      private static string BuildInsertSQL(ReposityMeta rm, NameValueCollection collection)
      {
         string sql = "INSERT INTO " + rm.Table1 + " VALUES(";
         

         return null;
      }

      public static ReposityMeta GetMeta(int mid)
      {
         ReposityMeta rm = CacheService.Get("MID_" + mid) as ReposityMeta;

         if (rm == null)
         {
            string sql = "SELECT * FROM meta_reposity WHERE id=@id;" +
               "SELECT * FROM meta_reposity_map WHERE rid=@id ORDER BY id";

            using (IDataReader dr = DBH.ExecuteReader(QA.DBCS_MAIN, CommandType.Text, sql, new SqlParameter("@id", mid)))
            {
               if (dr != null)
               {
                  if (dr.Read())
                  {
                     rm = new ReposityMeta();
                     rm.Id = dr.GetInt32(dr.GetOrdinal("id"));
                     rm.OP = dr.GetByte(dr.GetOrdinal("op"));
                     rm.Total = dr.GetInt32(dr.GetOrdinal("total"));
                     rm.Name = dr.GetString(dr.GetOrdinal("name"));
                     rm.Table1 = dr.GetString(dr.GetOrdinal("table1"));
                     rm.Table2 = (dr["table2"] is DBNull) ? null : dr.GetString(dr.GetOrdinal("table2"));
                     rm.Table3 = (dr["table3"] is DBNull) ? null : dr.GetString(dr.GetOrdinal("table3"));

                     if (dr.NextResult())
                     {
                        rm.Maps = new List<ReposityMap>();
                        while (dr.Read())
                        {
                           ReposityMap map = new ReposityMap();
                           map.Id = dr.GetInt32(dr.GetOrdinal("id"));
                           map.RId = dr.GetInt32(dr.GetOrdinal("rid"));
                           map.Name = dr.GetString(dr.GetOrdinal("name"));
                           map.Field = dr.GetString(dr.GetOrdinal("field"));
                           map.TableIndex = dr.GetInt32(dr.GetOrdinal("tableindex"));
                           map.PK = dr.GetBoolean(dr.GetOrdinal("pk"));
                           map.AutoId = dr.GetBoolean(dr.GetOrdinal("autoid"));
                           rm.Maps.Add(map);
                        }
                     }
                  }
               }
            }//using
         }

         return rm;
      }
   }
}
