using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using light;

namespace light
{
   /// <summary>
   ///  Data Access Helper
   /// </summary>
   public class DBH
   {
      #region EXECUTE_NON_QUERY

      public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, String outParmField, out object outValue, params SqlParameter[] commandParameters)
      {
         SqlCommand cmd = new SqlCommand();
         using (SqlConnection conn = new SqlConnection(connectionString))
         {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            cmd.Parameters[outParmField].Direction = ParameterDirection.Output;
            int val = cmd.ExecuteNonQuery();
            outValue = cmd.Parameters[outParmField].Value;
            cmd.Parameters.Clear();
            return val;
         }
      }

      public static int ExecuteText(string connectionString,string cmdText, params SqlParameter[] commandParameters)
      {
         SqlCommand cmd = new SqlCommand();
         using (SqlConnection conn = new SqlConnection(connectionString))
         {
            PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
         }
      }

      public static int ExecuteSP(string connectionString, string cmdText, params SqlParameter[] commandParameters)
      {
         SqlCommand cmd = new SqlCommand();
         using (SqlConnection conn = new SqlConnection(connectionString))
         {
            PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
         }
      }

      #endregion

      #region EXECUTE_READER

      public static SqlDataReader ExecuteReader(string connectionString, CommandType type,string cmdText, params SqlParameter[] commandParameters)
      {
         SqlCommand cmd = new SqlCommand();
         SqlConnection conn = new SqlConnection(connectionString);

         try
         {
            PrepareCommand(cmd, conn, null, type, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
         }
         catch
         {
            conn.Close();
            throw;
         }
      }

      public static SqlDataReader QueryText(string connectionString, string cmdText, params SqlParameter[] commandParameters)
      {
         SqlCommand cmd = new SqlCommand();
         SqlConnection conn = new SqlConnection(connectionString);

         try
         {
            PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
         }
         catch
         {
            conn.Close();
            throw;
         }
      }

      public static SqlDataReader QuerySP(string connectionString, string cmdText, params SqlParameter[] commandParameters)
      {
         SqlCommand cmd = new SqlCommand();
         SqlConnection conn = new SqlConnection(connectionString);

         try
         {
            PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
         }
         catch
         {
            conn.Close();
            throw;
         }
      }

      #endregion

      #region ExecuteDataSet

      public static DataSet ExecuteDataSet(string dbcs, string dsName, string dtName, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
      {
         DataSet ds = null;
         SqlCommand cmd = new SqlCommand();
         SqlConnection conn = new SqlConnection(dbcs);
         SqlDataAdapter da = null;

         try
         {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            da = new SqlDataAdapter(cmd);

            ds = new DataSet(dsName);
            da.Fill(ds, dtName);
         }
         finally
         {
            conn.Close();
            da.Dispose();
         }

         return ds;
      }

      #endregion

      #region ExecuteDataTable

      public static DataTable ExecuteTable(string dbcs, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
      {
         DataSet ds = null;
         SqlCommand cmd = new SqlCommand();
         SqlConnection conn = new SqlConnection(dbcs);
         SqlDataAdapter da = null;

         try
         {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds, "table");
         }
         finally
         {
            conn.Close();
            da.Dispose();
         }

         return ds.Tables[0];
      }

      #endregion

      #region PrepareCommand

      private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
      {
         if (conn.State != ConnectionState.Open) conn.Open();

         cmd.Connection = conn;
         cmd.CommandType = cmdType;
         cmd.CommandText = cmdText;

         if (trans != null) cmd.Transaction = trans;
         if (cmdParms != null && cmdParms.Length > 0) cmd.Parameters.AddRange(cmdParms);
      }

      #endregion

      #region Utility

      public static int GetInt32(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
      {
         int ret = -1;

         using (IDataReader dr = ExecuteReader(connStr, cmdType, cmdText, cmdParms))
         {
            if (dr != null && dr.Read())
            {
               ret = Convert.ToInt32(dr[0]);
               dr.Close();
            }
         }

         return ret;
      }

      public static string GetString(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
      {
         string ret = null;

         using (IDataReader dr = ExecuteReader(connStr, cmdType, cmdText, cmdParms))
         {
            if (dr != null)
            {
               if (dr.Read()) ret = CU.ToStr(dr[0]);
               dr.Close();
            }
         }

         return ret;
      }

      /// <summary>
      /// 执行SQL语句，直接返回结果的整数形式，要求结果集中第一个字段对应的值。
      /// </summary>
      /// <param name="sql">要执行的SQL语句</param>
      /// <returns>返回的结果集</returns>
      public static string GetList(string connStr, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
      {
         string ret = null;

         using (IDataReader dr = ExecuteReader(connStr, cmdType, cmdText, cmdParms))
         {
            if (dr != null)
            {
               while (dr.Read())
                  ret += CU.ToStr(dr[0]) + ",";
               dr.Close();
            }
         }

         if (string.IsNullOrEmpty(ret)) return null;
         else return ret.TrimEnd(',');
      }

      public static bool GetBoolean(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
      {
         bool ret = false;

         using (IDataReader dr = ExecuteReader(connStr, cmdType, cmdText, cmdParms))
         {
            if (dr != null)
            {
               if (dr.Read()) ret = Convert.ToBoolean((dr[0]));
               dr.Close();
            }
         }

         return ret;
      }

      /// <summary>
      ///  获取分页数据集
      /// </summary>
      /// <param name="tableName">数据源表明</param>
      /// <param name="fields">获取的字段列表</param>
      /// <param name="sortField">排序字段</param>
      /// <param name="start">开始的记录数</param>
      /// <param name="end">结束的记录数</param>
      /// <param name="orderType">默认为0升序排列(ASC)，1为降序排列(DESC)</param>
      /// <param name="where">where子句</param>
      /// <returns>返回排序结果</returns>
      public static IDataReader Paging(string connectionString, string tableName, string fields, string sortField, int start, int end, string orderType, string where)
      {
         SqlParameter whereSQL = new SqlParameter("@where_sql", SqlDbType.NVarChar, 2000);
         whereSQL.Value = where;
         return DBH.ExecuteReader(connectionString, CommandType.StoredProcedure, SP_PAGING, new SqlParameter[]{
            new SqlParameter("@tblname",tableName),
            new SqlParameter("@fields",fields),
            new SqlParameter("@sortfield",sortField),
            new SqlParameter("@start",start),
            new SqlParameter("@end",end),
            new SqlParameter("@is_count","0"),
            new SqlParameter("@ordertype",orderType),
            whereSQL
         });
      }

      /// <summary>
      /// 统计待查询的记录总数
      /// </summary>
      /// <param name="tableName">数据源表名</param>
      /// <param name="where">where子句</param>
      /// <returns>记录条数</returns>
      public static int Count(string connectionString, string tableName, string where)
      {
         SqlParameter whereSQL = new SqlParameter("@where_sql", SqlDbType.NVarChar, 1500);
         whereSQL.Value = where;
         return DBH.GetInt32(connectionString, CommandType.StoredProcedure, SP_PAGING, new SqlParameter[]{
            new SqlParameter("@tblname",tableName),
            new SqlParameter("@fields",""),
            new SqlParameter("@sortfield",""),
            new SqlParameter("@start","0"),
            new SqlParameter("@end","0"),
            new SqlParameter("@is_count","1"),
            new SqlParameter("@ordertype",""),
            whereSQL
         });
      }

      public static int Inc(string connectionString, string tableName, string fieldName, string priKey, string priValue)
      {
         return DBH.ExecuteText(connectionString, "UPDATE " + tableName + " SET " + fieldName + "=" + fieldName + "+1 WHERE " + priKey + "='" + priValue + "'");
      }

      /// <summary>
      ///  检查是否
      /// </summary>
      /// <param name="connectionString"></param>
      /// <param name="table"></param>
      /// <param name="field"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool Exists(string connectionString, string table, string field, string value)
      {
         return DBH.GetBoolean(connectionString, CommandType.Text,"SELECT COUNT(*) AS VALUE FROM " + table + " WHERE " + field + "=@value",
            new SqlParameter("@value", value));
      }

      /// <summary>
      /// 
      /// </summary>
      public const string FALSE = "FALSE";

      /// <summary>
      /// 检测是否某表某字段存在某值
      /// </summary>
      public const string SP_EXIST = "usp_exist";

      /// <summary>
      /// 分页存储过程
      /// </summary>
      public const string SP_PAGING = "usp_paging";

      #endregion
   }
}
