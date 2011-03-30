using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using light;
using System.Data.SqlClient;
using System.Reflection;
using light.System.Entities;

namespace light
{
   /// <summary>
   ///  Entity Builder
   /// </summary>
   /// <typeparam name="T">目标实体类型</typeparam>
   public class EB<T> where T : class,new()
   {
      /// <summary>
      ///  定义一个转换数据源到实体对象的委托。
      /// </summary>
      /// <param name="dr">包含数据源的DataReader</param>
      /// <returns>返回对应的实体对象</returns>
      public delegate T ToEntityCallback(IDataReader dr);

      private EB() { }

      /// <summary>
      ///  根据查询条件获取指定类型的对象，需要传递Entity Build Callback
      /// </summary>
      /// <param name="connectionString">连接串</param>
      /// <param name="commandType"></param>
      /// <param name="commandText"></param>
      /// <param name="callback">Entity Build方法</param>
      /// <param name="commandParameters"></param>
      /// <returns></returns>
      public static T Get(string connectionString, CommandType commandType, string commandText, ToEntityCallback callback, params SqlParameter[] commandParameters)
      {
         T t = null;
         using (IDataReader dr = DBH.ExecuteReader(connectionString, commandType, commandText, commandParameters))
         {
            if (dr != null)
            {
               if (dr.Read())
               {
                  t = callback(dr);
               }

               dr.Close();
            }
         }

         return t;
      }

      /// <summary>
      ///  根据查询条件自动生成对应的对象，不需要传递Entity Build Callback，但需要实体对象定义对应的属性。
      /// </summary>
      /// <param name="connectionString"></param>
      /// <param name="commandType"></param>
      /// <param name="commandText"></param>
      /// <param name="commandParameters"></param>
      /// <returns></returns>
      public static T Get(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
         T t = null;
         using (IDataReader dr = DBH.ExecuteReader(connectionString, commandType, commandText, commandParameters))
         {
            if (dr != null)
            {
               if (dr.Read())
               {
                  t = Build(dr);
               }

               dr.Close();
            }
         }

         return t;
      }

      public static IList<T> List(string connectionString, CommandType commandType, string commandText, ToEntityCallback callback, params SqlParameter[] commandParameters)
      {
         IList<T> tList = null;
         using (IDataReader dr = DBH.ExecuteReader(connectionString, commandType, commandText, commandParameters))
         {
            if (dr != null)
            {
               try
               {
                  tList = new List<T>();
                  while (dr.Read())
                  {
                     T t = callback(dr);
                     tList.Add(t);
                  }
               }
               catch { }
               finally { dr.Close(); }
            }
         }

         return tList;
      }

      public static IList<T> Paging(string connectionString, out int recordCount, string tableName, string fields, string sortfield, int start, int end, string orderType, string whereSQL, ToEntityCallback callback)
      {
         IList<T> list = null;

         recordCount = DBH.Count(connectionString, tableName, whereSQL);
         using (IDataReader dr = DBH.Paging(connectionString, tableName, fields, sortfield, start, end, orderType, whereSQL))
         {
            if (dr != null)
            {
               list = new List<T>();
               while (dr.Read()) { list.Add(callback(dr)); }
               dr.Close();
            }
         }

         return list;
      }



      /*
      /// <summary>
      /// 
      /// </summary>
      /// <param name="oid"></param>
      /// <param name="tableName"></param>
      /// <param name="callback"></param>
      /// <param name="viewCount"></param>
      /// <returns></returns>
      public static T Read(string oid, string tableName, ToEntityCallbackEx callback,bool viewCount)
      {
         T t = default(T) ;
         using (IDataReader dr = DBH.ExecuteReader(SQL.Select(tableName, DF.ALL, " WHERE oid ='" + oid + "'")))
         {
            if (dr != null)
            {
               try 
               {
                  if (dr.Read())
                  {
                     t = callback(dr, true);
                     if (viewCount) IncView(tableName, oid);
                  }
               }
               catch { }finally { dr.Close(); }
            }
         }

         return t;
      }

      public static T Read(string oid, string tableName, ToEntityCallbackEx callback)
      {
         return Read(oid, tableName, callback, false);
      }

      public static T ReadByURL(string urlName, string tableName, ToEntityCallbackEx callback,bool viewCount)
      {
         T t = default(T);
         using (IDataReader dr = DBH.ExecuteReader(SQL.Select(tableName, DF.ALL, " WHERE url_name ='" + urlName + "'")))
         {
            if (dr != null)
            {
               try 
               {
                  if (dr.Read())
                  {
                     t = callback(dr, true);
                     if (viewCount) IncViewByURL(tableName, urlName);
                  }
               }
               catch { }
               finally { dr.Close(); }
            }
         }

         return t;
      }

      public static T ReadByURL(string oname, string tableName,ToEntityCallbackEx callback)
      {
         return ReadByURL(oname, tableName, callback, false);
      }

      public static T ReadWhere(string tableName, string whereSQL, ToEntityCallbackEx callback)
      {
         T t = default(T);
         using (IDataReader dr = DBH.ExecuteReader(SQL.Select(tableName, DF.ALL, whereSQL)))
         {
            if (dr != null)
            {
               try { if (dr.Read()) t = callback(dr, true); }
               catch { }
               finally { dr.Close(); }
            }
         }
         return t;
      }

      

      public static IList<T> ReadCount(int count,string tableName,string fields,string whereSQL, ToEntityCallbackEx callback)
      {
         IList<T> tList = null;
         using (IDataReader dr = DBH.ExecuteReader(SQL.Select(tableName, "top " + count + " " + fields, whereSQL)))
         {
            if (dr != null)
            {
               try
               {
                  tList = new List<T>();
                  while (dr.Read()) { T t = callback(dr, false); tList.Add(t); }
               }
               catch { }
               finally { dr.Close(); }
            }
         }

         return tList;
      }

      public static IList<T> ReadCount(int count, string tableName, string fields, string whereSQL, ToEntityCallback callback)
      {
         IList<T> tList = null;
         using (IDataReader dr = DBH.ExecuteReader(SQL.Select(tableName, "top " + count + " " + fields, whereSQL)))
         {
            if (dr != null)
            {
               try
               {
                  tList = new List<T>();
                  while (dr.Read()) { T t = callback(dr); tList.Add(t); }
               }
               catch { }
               finally { dr.Close(); }
            }
         }

         return tList;
      }

      

      //C////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      public static int Create(string storeProc,string[] values)
      {
         return DBH.GetIntValue(CommandType.StoredProcedure,SQL.SPCombine(storeProc, values),null);
      }

      //U///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      public static int Update(string tableName,string[] fields,string[] values,string oid)
      {
         return DBH.GetIntValue(CommandType.Text,SQL.Update(tableName, fields, values, "oid=" + oid),null);
      }

      //S//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      public static int SetState(string tableName, string oid, string state)
      {
         return DBH.GetIntValue(CommandType.Text,SQL.Update(tableName, "state", state, "oid=" + oid),null);
      }

      /// <summary>
      /// 增加点击次数
      /// </summary>
      /// <param name="tableName">表名</param>
      /// <param name="oid">表名</param>
      /// <returns></returns>
      public static int IncView(string tableName, string oid)
      {
         return DBH.Inc(tableName, "views", "oid", oid);
      }

      private static int IncViewByURL(string tableName, string urlName)
      {
         return DBH.Inc(tableName, "views", "url_name", urlName);
      }
        */

      public static T Build(IDataReader dr)
      {
         T entity = new T();

         if (entity != null)
         {
            Type t = entity.GetType();
            object[] tables = t.GetCustomAttributes(typeof(TableAttribute), false);
            if (tables != null && tables.Length == 1)
            {
               string tableName = (tables[0] as TableAttribute).Name;
               if (tableName != null)
               {
                  FieldInfo[] fields = t.GetFields();
                  foreach (FieldInfo f in fields)
                  {
                     object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
                     if (attrs.Length == 1)
                     {
                        FieldAttribute fa = attrs[0] as FieldAttribute;

                        try
                        {
                           object value = dr[dr.GetOrdinal(fa.Name)];
                           if (value != null && !(value is DBNull)) f.SetValue(entity, value);
                        }
                        catch (Exception) { } //字段不存在
                     }
                  }
               }
            }
         }
         else { throw new Exception(typeof(T).ToString()); }

         return entity;
      }

      public static int Create(string connectingString, T entity)
      {
         int ret = -1;

         Type t = entity.GetType();

         object[] tables = t.GetCustomAttributes(typeof(TableAttribute), false);

         if (tables != null && tables.Length == 1)
         {
            string tableName = (tables[0] as TableAttribute).Name;
            if (tableName != null)
            {
               string sql = "INSERT INTO " + tableName + "(";
               string keys = string.Empty;
               string values = string.Empty;
               
               List<SqlParameter> parameters = new List<SqlParameter>();

               FieldInfo[] fields = t.GetFields();

               foreach (FieldInfo f in fields)
               {
                  object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
                  if (attrs.Length == 1)
                  {
                     FieldAttribute fa = attrs[0] as FieldAttribute;

                     if (!fa.AutoCreated) 
                     {
                        if (!string.IsNullOrEmpty(keys)) { keys += ","; values += ","; }
                        keys += fa.Name;
                        values += "@" + fa.Name;
                        parameters.Add(new SqlParameter("@" + fa.Name, f.GetValue(entity)));
                     }
                  }
               }

               sql = sql + keys + ") VALUES(" + values + ");SELECT SCOPE_IDENTITY();";
               ret = DBH.GetInt32(connectingString, CommandType.Text, sql, parameters.ToArray());
            }
         }

         return ret;
      }


      public static int Update(string connectingString, T entity)
      {
         int ret = -1;

         Type t = entity.GetType();

         object[] tables = t.GetCustomAttributes(typeof(TableAttribute), false);

         if (tables != null && tables.Length == 1)
         {
            string tableName = (tables[0] as TableAttribute).Name;
            if (tableName != null)
            {
               string sql = "UPDATE " + tableName + " SET ";
               string sets = string.Empty;
               string where = null;
               List<SqlParameter> parameters = new List<SqlParameter>();

               FieldInfo[] fields = t.GetFields();

               foreach (FieldInfo f in fields)
               {
                  object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
                  if (attrs.Length == 1)
                  {
                     FieldAttribute fa = attrs[0] as FieldAttribute;

                     if (fa.PrimaryKey) where = " WHERE " + fa.Name + "=@" + fa.Name;
                     else
                     {
                        if (!string.IsNullOrEmpty(sets)) sets += ",";
                        sets += fa.Name + "=@" + fa.Name;
                     }

                     parameters.Add(new SqlParameter("@" + fa.Name, f.GetValue(entity)));
                  }
               }

               sql = sql + sets + where;
               ret = DBH.ExecuteText(connectingString, sql, parameters.ToArray());
            }
         }

         return ret;
      }

      
   }
}
