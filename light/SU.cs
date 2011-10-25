using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace light
{
   /// <summary>
   /// 关于字符串的一些操作
   /// </summary>
   public class SU
   {
      public static string CleanHTML(string src)
      { 
         return Regex.Replace(src,"(<[^>]*>)|&[a-z#0-9]+;","");
      }

      /// <summary>
      /// 清楚;:!,.'\^&~'*?" /等符号
      /// </summary>
      /// <param name="src"></param>
      /// <returns></returns>
      public static string CleanSpecialSign(string src)
      {
         return Regex.Replace(src, "([;:!,.`\\^\\&~'*?'\"/\\\\])", "");
      }

      /// <summary>
      /// 多个空格合成一个空格
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
      /// 截取length指定的字符串长度
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
      /// 移除字符串中的空格，如果字符串为null,不进行处理
      /// </summary>
      /// <param name="src">待处理德字符串</param>
      /// <returns>移除字符串两边空格后的结果</returns>
      public static string Trim(string src)
      {
         if (src == null) return null;
         else return src.Trim();
      }

      /// <summary>
      /// 清除SQL字符串可能包含的特殊字符
      /// </summary>
      /// <param name="src">源字符串</param>
      /// <returns>输出可以直接插入数据库的字符串</returns>
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
      /// 除了正常的替换和过滤非法字符外，还转换字符串中的换行符为"<br/>"
      /// </summary>
      /// <param name="src">源字符串</param>
      /// <returns>返回源转换后的字符串</returns>
      public static string SQLLineBreak(string src)
      {
         return SQL(src).Replace("\r\n", "<br/>");
      }

      public static string Tags(string s)
      {
         if (string.IsNullOrEmpty(s)) return null;
         if (s.Contains(",")) s = s.Replace(',', ' ');
         if (s.Contains("，")) s = s.Replace('，', ' ');
         return s;
      }
   }
}
