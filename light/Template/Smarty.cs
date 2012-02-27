using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace light
{
   public sealed class Smarty
   {
      private static readonly SafeDictionary<string, Func<IDictionary<string, string>, string>> tmplDict = new SafeDictionary<string, Func<IDictionary<string, string>, string>>();

      public static string Render(string tmplName, string tmpl, IDictionary<string, string> data)
      {
         Func<IDictionary<string, string>, string> tmplBuilder = null;
         if (!tmplDict.TryGetValue(tmplName, out tmplBuilder)) tmplBuilder = GetTemplateBuilder(tmplName,tmpl);
         if (tmplBuilder != null) return tmplBuilder(data);
         return string.Empty;
      }

      public static string RepeatRender(string tmplName, string tmpl, IList<IDictionary<string, string>> list)
      {
         StringBuilder sb = new StringBuilder();

         Func<IDictionary<string, string>, string> tmplBuilder = null;
         if (!tmplDict.TryGetValue(tmplName, out tmplBuilder)) tmplBuilder = GetTemplateBuilder(tmplName, tmpl);
         if (tmplBuilder != null)
         {
            foreach (IDictionary<string, string> data in list) sb.Append(tmplBuilder(data));
         }
         return sb.ToString();
      }

      private static Func<IDictionary<string, string>, string> GetTemplateBuilder(string tmplName, string tmpl)
      {
         Func<IDictionary<string, string>, string> tmplBuilder = null;

         if (!string.IsNullOrEmpty(tmpl))
         {
            string[] ts = Regex.Split(tmpl, "(\\$\\{[a-z_]+\\})");
            tmplBuilder = delegate(IDictionary<string, string> data)
            {
               string ret = null; foreach (string s in ts) { if (s.StartsWith("${")) ret += data[s]; else ret += s; } return ret;
            };

            if (tmplBuilder != null) tmplDict.Add(tmplName, tmplBuilder);
         }

         return tmplBuilder;
      }
   }
}
