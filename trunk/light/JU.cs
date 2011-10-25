using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace light
{
   /// <summary>
   ///  JSON Utilities
   /// </summary>
   public sealed class JU
   {
      public static string Array(string[] k, string[] v)
      {
         string json = "{";

         if (k.Length == v.Length)
         {
            int i = 0;
            for (; i < k.Length - 1; i++)
            {
               json += "\"" + k[i] + "\":\"" + v[i].Replace("\"", "\\\"") + "\",";
            }

            json += "\"" + k[i] + "\":\"" + v[i].Replace("\"", "\\\"") + "\"}";
         }

         return json;
      }

      public static string Array(IList<TPair<string, string>> list)
      {
         string json = "{";
         int i=0;
         for (; i < list.Count-1; i++)
         {
            json += "\"" + list[i].k + "\":\"" + list[i].v.Replace("\"", "\\\"") + "\",";
         }

         json += "\"" + list[i].k + "\":\"" + list[i].v.Replace("\"", "\\\"") + "\"}";

         return json;
      }

      public static string Array(IDictionary<string, string> dict)
      {
         string json = null;

         foreach (string k in dict.Keys)
         {
            if (json == null) json = "{" + "\"" + k + "\":\"" + WriteString(dict[k]) + "\"";
            else json += ",\"" + k + "\":\"" + WriteString(dict[k]) + "\"";
         }

         return json + "}";
      }

      public static string Build(bool success, string data)
      {
         return "({'success':" + (success ? "true" : "false") + ",'data':'" + WriteString(data) + "'})";
      }

      public static string Build(bool success, int code, string data)
      {
         return "({'success':" + (success ? "true" : "false") + ",'code':" + code + ",'data':'" + WriteString(data) + "'})";
      }

      public static string BuildJSON(bool success, string json)
      {
         return "({'success':" + (success ? "true" : "false") + ",'data':" + json + "})";
      }

      /// <summary>
      ///  单个key,value转换成JSON格式，不对value做处理
      /// </summary>
      /// <param name="key"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public static string Single(string key, string value)
      {
         return "{'" + key + "':'" + WriteString(value) + "'}";
      }

      public static string SingleWithoutBrackets(string key, string value)
      {
         return "'" + key + "':'" + WriteString(value) + "'";
      }

      private static string WriteString(string s)
      {
         StringBuilder _output = new StringBuilder();
         int runIndex = -1;

         for (var index = 0; index < s.Length; ++index)
         {
            var c = s[index];

            if (c >= ' ' && c < 128 && c != '\"' && c != '\\')
            {
               if (runIndex == -1) runIndex = index;
               continue;
            }

            if (runIndex != -1)
            {
               _output.Append(s, runIndex, index - runIndex);
               runIndex = -1;
            }

            switch (c)
            {
               case '\t': _output.Append("\\t"); break;
               case '\r': _output.Append("\\r"); break;
               case '\n': _output.Append("\\n"); break;
               case '"':
               case '\\': _output.Append('\\'); _output.Append(c); break;
               default:
                  _output.Append("\\u");
                  _output.Append(((int)c).ToString("X4", NumberFormatInfo.InvariantInfo));
                  break;
            }
         }

         if (runIndex != -1) _output.Append(s, runIndex, s.Length - runIndex);

         return _output.ToString();
      }

      public const string AJAX_FAIL = "({'success':false})";
      public const string AJAX_SUCCESS = "({'success':true})";
   }
}
