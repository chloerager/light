using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace light
{
   /// <summary>
   /// �����ַ�����һЩ����
   /// </summary>
   public class STR
   {
      /// <summary>
      /// �ɹ�
      /// </summary>
      public const string JSON_SUCCESS = "{\"success\":\"1\"}";
      /// <summary>
      /// ʧ��
      /// </summary>
      public const string JSON_FAILED = "{\"success\":\"0\"}";
      /// <summary>
      /// �����쳣
      /// </summary>
      public const string JSON_PARMA_EXCEPTION = "{\"success\":\"-1\"}";

      /// <summary>
      /// ����JSON�ַ���
      /// </summary>
      /// <param name="k">���key����","��������,����֤k��v������һ��</param>
      /// <param name="v">key��Ӧ��ֵ</param>
      /// <returns>����������ر�׼��JSON�ַ�����</returns>
      public static string JSON(string k, string v)
      {
         string json = "{";
         string[] keys = k.Split(',');
         string[] values = v.Split(',');

         if (keys.Length == values.Length)
         {
            int i = 0;
            for (; i < keys.Length-1; i++)
            { 
               json += "\"" + keys[i] + "\":\"" + values[i].Replace("\"","\\\"") + "\",";
            }

            json += "\"" + keys[i] + "\":\"" + values[i].Replace("\"", "\\\"") + "\"}";
         }

         return json;
      }

      public static string JSON(int success)
      {
         return string.Format("var result = {{\"success\":\"{0}\",\"message\":\"\"}}", success);
      }

      public static string JSON(int success, string message)
      {
         message = message.Replace("\"", "\\\"");
         return string.Format("var result = {{\"success\":\"{0}\",\"message\":\"{1}\"}}", success, message);
      }

      public static string CleanHTML(string src)
      { 
         return Regex.Replace(src,"(<[^>]*>)|&[a-z#0-9]+;","");
      }

      /// <summary>
      /// ���;:!,.'\^&~'*?" /�ȷ���
      /// </summary>
      /// <param name="src"></param>
      /// <returns></returns>
      public static string CleanSpecialSign(string src)
      {
         return Regex.Replace(src, "([;:!,.`\\^\\&~'*?'\"/\\\\])", "");
      }

      /// <summary>
      /// ����ո�ϳ�һ���ո�
      /// </summary>
      /// <param name="src"></param>
      /// <returns></returns>
      public static string MergeSpace(string src)
      {
         return Regex.Replace(src, "(\\s{2,})", " ");
      }

      public static bool IsNullOREmpty(string src)
      {
         if(src == null || src == string.Empty) return true;

         return false;
      }

      /// <summary>
      /// ��ȡlengthָ�����ַ�������
      /// </summary>
      /// <param name="src"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public static string Cut(string src, int length)
      {
         if (string.IsNullOrEmpty(src)) return src;
         if (src.Length <= length) return src;
         return src.Substring(0, length);
      }

      public static string DottedCut(string src, int length)
      {
         if (string.IsNullOrEmpty(src)) return src;
         if (src.Length <= length) return src;
         return src.Substring(0, length) + "...";
      }

      /// <summary>
      /// �Ƴ��ַ����еĿո�����ַ���Ϊnull,�����д���
      /// </summary>
      /// <param name="src">��������ַ���</param>
      /// <returns>�Ƴ��ַ������߿ո��Ľ��</returns>
      public static string Trim(string src)
      {
         if (src == null) return null;
         else return src.Trim();
      }

      /// <summary>
      /// ���SQL�ַ������ܰ����������ַ�
      /// </summary>
      /// <param name="src">Դ�ַ���</param>
      /// <returns>�������ֱ�Ӳ������ݿ���ַ���</returns>
      public static string SQL(string src)
      {
         if (src == null) return null;
         return src.Replace("'", "''").Replace("<", "&lt").Replace(">", "&gt");
      }

      public static string SQLHTML(string src)
      {
         if (src == null) return null;
         return src.Replace("'", "''");
      }

      /// <summary>
      /// �����������滻�͹��˷Ƿ��ַ��⣬��ת���ַ����еĻ��з�Ϊ"<br/>"
      /// </summary>
      /// <param name="src">Դ�ַ���</param>
      /// <returns>����Դת������ַ���</returns>
      public static string SQLLineBreak(string src)
      {
         return SQL(src).Replace("\r\n", "<br/>");
      }
   }
}
