/*
 * 版权信息：著作所有权归作者所有。(C) 晓石网络科技 http://www.docknow.com
 *
 * 作者信息：万磊/azmo.wl@gmail.com/http://www.azmo.cn
 *                                                                
 * 创建日期：2006-06-08 修改日期：$Modtime: 07-09-02 2:31 $     
 * 
 * 修订：$Revision: 1 $ 
 * 
 */

using System;
using System.Text;

namespace light
{
   /// <summary>
   /// SQL，封装了大部分直接访问数据的方法以及一些简化数据操作的对象。
   /// </summary>
   public sealed class SQL
   {
      /// <summary>
      /// 拼接数据库SQL串，添加"'"和","
      /// </summary>
      /// <param name="list">字符串</param>
      /// <returns>返回添加过"'"和","的字符串。</returns>
      public static string Combine(params string[] list)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(" ");

         int i = 0;
         int count = list.Length - 1;
         for (; i < count; i++)
         {
            sb.Append(CU.DBNULL(list[i]));
            sb.Append(",");
         }

         return sb.Append(CU.DBNULL(list[i])).ToString();
      }

      public static string[] STRCombine(params string[] list)
      {
         return list;
      }

      /// <summary>
      /// 拼接数据库SQL串，添加"'"和","
      /// </summary>
      /// <param name="spname">存储过程列表</param>
      /// <param name="list">字符串</param>
      /// <returns>返回添加过"'"和","的字符串，如果值为空的项则返回null</returns>
      public static string SPCombine(string spname, params string[] list)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(spname);
         sb.Append(" ");

         int i = 0;
         int count = list.Length - 1;
         for (; i < count; i++)
         {
            sb.Append(CU.DBNULL(list[i]));
            sb.Append(",");
         }

         return sb.Append(CU.DBNULL(list[i])).ToString();
      }

      /// <summary>
      /// 拼接数据库SQL串，添加"'"和","
      /// </summary>
      /// <param name="spname">存储过程列表</param>
      /// <param name="list">字符串</param>
      /// <returns>返回添加过"'"和","的字符串，如果值为空的项则返回''</returns>
      public static string SPCombineNotNull(string spname, params string[] list)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(spname);
         sb.Append(" ");

         int i = 0;
         int count = list.Length - 1;
         for (; i < count; i++)
         {
            sb.Append("'" + list[i] + "'");
            sb.Append(",");
         }

         return sb.Append("'" + list[i] + "'").ToString();
      }

      /// <summary>
      /// 生成插入SQL的字符串
      /// </summary>
      /// <param name="tableName">插入数据的目标数据表</param>
      /// <param name="fields">插入的目标字段集</param>
      /// <param name="values">插入的字段对应的值</param>
      /// <returns>返回对应的字符串</returns>
      public static string Insert(string tableName, string fields, string values)
      {
         return string.Concat("INSERT INTO ", tableName, "(", fields, ") VALUES(", values, ")");
      }

      /// <summary>
      /// 生成选中SQL的字符串
      /// </summary>
      /// <param name="tableName">表名</param>
      /// <param name="fields">字段列表</param>
      /// <returns>返回生成的SQL字符串</returns>
      public static string Select(string tableName, string fields)
      {
         return string.Concat("SELECT ", fields, " FROM ", tableName);
      }

      /// <summary>
      /// 生成选中SQL的字符串，支持扩展语句，像WHERE、ORDER等子句
      /// </summary>
      /// <param name="tableName">表名</param>
      /// <param name="fields">字段列表</param>
      /// <param name="extend">扩展语句，像WHERE、ORDER等子句</param>
      /// <returns>返回对应的SQL字符串</returns>
      public static string Select(string tableName, string fields, string extend)
      {
         return string.Concat("SELECT ", fields, " FROM ", tableName," ",extend);
      }

      /// <summary>
      /// 生成更新的SQL字符串
      /// </summary>
      /// <param name="tableName">表名</param>
      /// <param name="fields">字段列表</param>
      /// <param name="values">值列表</param>
      /// <param name="where">WHERE子句(无须写WHERE)</param>
      /// <returns>返回对应的SQL字符串</returns>
      public static string Update(string tableName, string[] fields, string[] values, string where)
      {
         string ret = null;

         if (fields.Length != values.Length)
         {
            throw new Exception("参数fields不匹配");
         }
         else
         {
            ret = string.Concat("UPDATE ", tableName, " SET ");

            int i = 0;
            for (; i < fields.Length - 1; i++)
            {
               if (string.IsNullOrEmpty(values[i])) ret = string.Concat(ret, fields[i], "=null,");
               else ret = string.Concat(ret, fields[i], "='", values[i], "',");
            }

            if (string.IsNullOrEmpty(values[i])) ret = string.Concat(ret, fields[i], "=null WHERE ", where);
            else ret = string.Concat(ret, fields[i], "='", values[i], "' WHERE ", where);
         }

         return ret;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="tableName"></param>
      /// <param name="field"></param>
      /// <param name="value">null或者Empty会插入null</param>
      /// <param name="where">WHERE子句(无须写WHERE)</param>
      /// <returns></returns>
      public static string Update(string tableName, string field, string value, string where)
      {
         if (string.IsNullOrEmpty(value)) return string.Concat("UPDATE ", tableName, " SET ", field, "=null WHERE ", where);
         return string.Concat("UPDATE ", tableName, " SET ", field, "='", value, "' WHERE ", where);
      }

      public static string Delete(string tableName,string field, string value)
      {
         return string.Concat("DELETE ", tableName, " WHERE ", field, "='", value, "'");
      }

      public static string BatchDelete(string tableName, string field, string values)
      {
         return string.Concat("DELETE ", tableName, " WHERE ", field, " in (", values, ")");
      }
   }
}
