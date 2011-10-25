using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace light
{
   /// <summary>
   /// �����ַ�����һЩ����
   /// </summary>
   public class SU
   {
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
      /// <param name="s"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public static string Cut(string s, int length)
      {
         if (string.IsNullOrEmpty(s)) return s;
         if (s.Length <= length) return s;
         return s.Substring(0, length);
      }

      public static string DottedCut(string s, int length)
      {
         if (string.IsNullOrEmpty(s)) return s;
         if (s.Length <= length) return s;
         return s.Substring(0, length) + "...";
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

      public static string Tags(string s)
      {
         if (string.IsNullOrEmpty(s)) return null;
         if (s.Contains(",")) s = s.Replace(',', ' ');
         if (s.Contains("��")) s = s.Replace('��', ' ');
         return s;
      }
   }
}
