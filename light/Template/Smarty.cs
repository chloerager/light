using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace light.Template
{
   public sealed class Smarty
   {
      private static readonly SafeDictionary<string, Func<IDictionary<string, object>, string>> tmplDict = new SafeDictionary<string, Func<IDictionary<string, object>, string>>();

      public static string Render(string tmplName, string tmpl, IDictionary<string, object> data)
      {
         Func<IDictionary<string, object>, string> tmplBuilder = null;
         if (!tmplDict.TryGetValue(tmplName, out tmplBuilder)) tmplBuilder = GetTemplateBuilder(tmplName,tmpl);
         if (tmplBuilder != null) return tmplBuilder(data);
         return string.Empty;
      }

      public static string RepeatRender(string tmplName, string tmpl, IList<IDictionary<string, object>> list)
      {
         StringBuilder sb = new StringBuilder();

         Func<IDictionary<string, object>, string> tmplBuilder = null;
         if (!tmplDict.TryGetValue(tmplName, out tmplBuilder)) tmplBuilder = GetTemplateBuilder(tmplName, tmpl);
         if (tmplBuilder != null)
         {
            foreach (IDictionary<string, object> data in list) sb.Append(tmplBuilder(data));
         }
         return sb.ToString();
      }

      private static Func<IDictionary<string, object>, string> GetTemplateBuilder(string tmplName, string tmpl)
      {
         Func<IDictionary<string, object>, string> tmplBuilder = null;

         if (!string.IsNullOrEmpty(tmpl))
         {
            string[] ts = Regex.Split(tmpl, "(\\$\\{[a-z_]+\\})");
            tmplBuilder = delegate(IDictionary<string, object> data)
            {
               string ret = null; foreach (string s in ts) { if (s.StartsWith("${")) ret += data[s]; else ret += s; } return ret;
            };

            if (tmplBuilder != null) tmplDict.Add(tmplName, tmplBuilder);
         }

         return tmplBuilder;
      }
   }
}
