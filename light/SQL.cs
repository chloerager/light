/*
 * ��Ȩ��Ϣ����������Ȩ���������С�(C) ��ʯ����Ƽ� http://www.docknow.com
 *
 * ������Ϣ������/azmo.wl@gmail.com/http://www.azmo.cn
 *                                                                
 * �������ڣ�2006-06-08 �޸����ڣ�$Modtime: 07-09-02 2:31 $     
 * 
 * �޶���$Revision: 1 $ 
 * 
 */

using System;
using System.Text;

namespace light
{
   /// <summary>
   /// SQL����װ�˴󲿷�ֱ�ӷ������ݵķ����Լ�һЩ�����ݲ����Ķ���
   /// </summary>
   public sealed class SQL
   {
      /// <summary>
      /// ƴ�����ݿ�SQL�������"'"��","
      /// </summary>
      /// <param name="list">�ַ���</param>
      /// <returns>������ӹ�"'"��","���ַ�����</returns>
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
      /// ƴ�����ݿ�SQL�������"'"��","
      /// </summary>
      /// <param name="spname">�洢�����б�</param>
      /// <param name="list">�ַ���</param>
      /// <returns>������ӹ�"'"��","���ַ��������ֵΪ�յ����򷵻�null</returns>
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
      /// ƴ�����ݿ�SQL�������"'"��","
      /// </summary>
      /// <param name="spname">�洢�����б�</param>
      /// <param name="list">�ַ���</param>
      /// <returns>������ӹ�"'"��","���ַ��������ֵΪ�յ����򷵻�''</returns>
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
      /// ���ɲ���SQL���ַ���
      /// </summary>
      /// <param name="tableName">�������ݵ�Ŀ�����ݱ�</param>
      /// <param name="fields">�����Ŀ���ֶμ�</param>
      /// <param name="values">������ֶζ�Ӧ��ֵ</param>
      /// <returns>���ض�Ӧ���ַ���</returns>
      public static string Insert(string tableName, string fields, string values)
      {
         return string.Concat("INSERT INTO ", tableName, "(", fields, ") VALUES(", values, ")");
      }

      /// <summary>
      /// ����ѡ��SQL���ַ���
      /// </summary>
      /// <param name="tableName">����</param>
      /// <param name="fields">�ֶ��б�</param>
      /// <returns>�������ɵ�SQL�ַ���</returns>
      public static string Select(string tableName, string fields)
      {
         return string.Concat("SELECT ", fields, " FROM ", tableName);
      }

      /// <summary>
      /// ����ѡ��SQL���ַ�����֧����չ��䣬��WHERE��ORDER���Ӿ�
      /// </summary>
      /// <param name="tableName">����</param>
      /// <param name="fields">�ֶ��б�</param>
      /// <param name="extend">��չ��䣬��WHERE��ORDER���Ӿ�</param>
      /// <returns>���ض�Ӧ��SQL�ַ���</returns>
      public static string Select(string tableName, string fields, string extend)
      {
         return string.Concat("SELECT ", fields, " FROM ", tableName," ",extend);
      }

      /// <summary>
      /// ���ɸ��µ�SQL�ַ���
      /// </summary>
      /// <param name="tableName">����</param>
      /// <param name="fields">�ֶ��б�</param>
      /// <param name="values">ֵ�б�</param>
      /// <param name="where">WHERE�Ӿ�(����дWHERE)</param>
      /// <returns>���ض�Ӧ��SQL�ַ���</returns>
      public static string Update(string tableName, string[] fields, string[] values, string where)
      {
         string ret = null;

         if (fields.Length != values.Length)
         {
            throw new Exception("����fields��ƥ��");
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
      /// <param name="value">null����Empty�����null</param>
      /// <param name="where">WHERE�Ӿ�(����дWHERE)</param>
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
