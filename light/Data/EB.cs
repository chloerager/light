using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace light.Data
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

      /// <summary>
      ///  根据查询条件自动生成对应的对象，不需要传递Entity Build Callback，但需要实体对象定义对应的属性。
      /// </summary>
      /// <param name="connectionString"></param>
      /// <param name="commandType"></param>
      /// <param name="commandText"></param>
      /// <param name="commandParameters"></param>
      /// <returns></returns>
      public static T Get(string commandText, params SqlParameter[] commandParameters)
      {
         T t = null;
         using (IDataReader dr = DBH.ExecuteReader(QA.DBCS_MAIN, CommandType.Text, commandText, commandParameters))
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

      public static IList<T> List(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
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
                     tList.Add(Build(dr));
                  }
               }
               catch { }
               finally { dr.Close(); }
            }
         }

         return tList;
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

            IList<string> colNames = GetColumnNames(dr);
            


            //遍历字段
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo f in fields)
            {
               object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
               if (attrs.Length == 1)
               {
                  FieldAttribute fa = attrs[0] as FieldAttribute;

                  try
                  {
                     if (colNames.Contains(fa.Name))
                     {
                        object value = dr[dr.GetOrdinal(fa.Name)];
                        if (value != null && !(value is DBNull)) f.SetValue(entity, value);
                     }
                  }
                  catch (Exception) { } 
               }
            }

            //遍历属性
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo p in properties)
            {
               object[] attrs = p.GetCustomAttributes(typeof(FieldAttribute), false);
               if (attrs.Length == 1)
               {
                  FieldAttribute fa = attrs[0] as FieldAttribute;

                  try
                  {
                     if (colNames.Contains(fa.Name))
                     {
                        object value = dr[dr.GetOrdinal(fa.Name)];
                        if (value != null && !(value is DBNull)) p.SetValue(entity, value, null);
                     }
                  }
                  catch (Exception) { } 
               }
            }

         }
         else { throw new Exception(typeof(T).ToString()); }

         return entity;
      }

      private static IList<string> GetColumnNames(IDataReader dr)
      {
         IList<string> names = new List<string>();
         for (int i = 0; i < dr.FieldCount; i++)
         {
            names.Add(dr.GetName(i));
         }

         return names;
      }

      /// <summary>
      ///  创建用户
      /// </summary>
      /// <param name="connectingString">数据库链接串</param>
      /// <param name="entity">实体对象</param>
      /// <returns>如果创建成功返回对应的实体对象。</returns>
      public static int Create(string connectingString, T entity, string tableName = null)
      {
         int ret = -1;

         try
         {
            Type t = entity.GetType();

            if (string.IsNullOrEmpty(tableName))
            {
               object[] tables = t.GetCustomAttributes(typeof(TableAttribute), false);
               if (tables != null && tables.Length == 1) tableName = (tables[0] as TableAttribute).Name;
            }

            if (tableName != null)
            {
               string sql = "INSERT INTO " + tableName + "(";
               string keys = string.Empty;
               string values = string.Empty;

               List<SqlParameter> parameters = new List<SqlParameter>();

               //遍历字段
               FieldInfo[] fields = t.GetFields();
               foreach (FieldInfo f in fields)
               {
                  object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
                  if (attrs.Length == 1)
                  {
                     FieldAttribute fa = attrs[0] as FieldAttribute;

                     object o = f.GetValue(entity);
                     if (!fa.AutoCreated)
                     {
                        if (fa.AllowNulls && o == null) continue;
                        if (!string.IsNullOrEmpty(keys)) { keys += ","; values += ","; }
                        keys += fa.Name;
                        values += "@" + fa.Name;
                        parameters.Add(new SqlParameter("@" + fa.Name, o));
                     }
                  }
               }

               //遍历属性
               PropertyInfo[] properties = t.GetProperties();
               foreach (PropertyInfo f in properties)
               {
                  object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
                  if (attrs.Length == 1)
                  {
                     FieldAttribute fa = attrs[0] as FieldAttribute;

                     object o = f.GetValue(entity, null);
                     if (!fa.AutoCreated && !fa.Identity)
                     {
                        if (fa.AllowNulls && o == null) continue;
                        if (!string.IsNullOrEmpty(keys)) { keys += ","; values += ","; }
                        keys += fa.Name;
                        values += "@" + fa.Name;
                        parameters.Add(new SqlParameter("@" + fa.Name, o));
                     }
                  }
               }

               sql = sql + keys + ") VALUES(" + values + ");SELECT SCOPE_IDENTITY();";
               ret = DBH.GetInt32(connectingString, CommandType.Text, sql, parameters.ToArray());
            }

         }
         catch { /*TODO:LOG*/}

         return ret;
      }

      private static bool CanCreate(T entity, object o)
      {
         if (o == null) return false;
         if (o is DateTime)
         {
            DateTime ot = (DateTime)o;
            if (ot.Year < 1753) return false;
         }

         return true;
      }

      public static int Update(string connectingString, T entity,string tableName=null)
      {
         int ret = -1;

         Type t = entity.GetType();

         if (string.IsNullOrEmpty(tableName))
         {
            object[] tables = t.GetCustomAttributes(typeof(TableAttribute), false);
            if (tables != null && tables.Length == 1) tableName = (tables[0] as TableAttribute).Name;
         }

         if (tableName != null)
         {
            string sql = "UPDATE " + tableName + " SET ";
            string sets = string.Empty;
            string where = null;
            List<SqlParameter> parameters = new List<SqlParameter>();

            //遍历字段
            FieldInfo[] fields = t.GetFields();
            foreach (FieldInfo f in fields)
            {
               object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
               if (attrs.Length == 1)
               {
                  FieldAttribute fa = attrs[0] as FieldAttribute;
                  object value = f.GetValue(entity);
                  if (!fa.AllowNulls && value == null) continue;  //非null字段，值为null不更新该字段
                  if (value is DateTime && DateTime.MinValue.CompareTo(value) == 0) continue;
                  if (fa.AllowNulls && value == null) value = DBNull.Value;

                  if (fa.PrimaryKey) where = " WHERE " + fa.Name + "=@" + fa.Name;
                  else
                  {
                     if (!string.IsNullOrEmpty(sets)) sets += ",";
                     sets += fa.Name + "=@" + fa.Name;
                  }

                  parameters.Add(new SqlParameter("@" + fa.Name, value));
               }
            }

            //遍历属性
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo f in properties)
            {
               object[] attrs = f.GetCustomAttributes(typeof(FieldAttribute), false);
               if (attrs.Length == 1)
               {
                  FieldAttribute fa = attrs[0] as FieldAttribute;
                  if (!fa.AllowNulls && f.GetValue(entity, null) == null) continue; //非null字段，值为null不更新该字段

                  if (fa.PrimaryKey) where = " WHERE " + fa.Name + "=@" + fa.Name;
                  else
                  {
                     if (!string.IsNullOrEmpty(sets)) sets += ",";
                     sets += fa.Name + "=@" + fa.Name;
                  }

                  parameters.Add(new SqlParameter("@" + fa.Name, f.GetValue(entity, null)));
               }
            }

            sql = sql + sets + where;
            ret = DBH.ExecuteText(connectingString, sql, parameters.ToArray());

         }

         return ret;
      }
   }
}
