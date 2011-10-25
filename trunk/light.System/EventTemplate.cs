using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Entities;
using light.Data;
using System.Text.RegularExpressions;

namespace light
{
   public class EventTemplate
   {
      public static string Build(EventTemplateData ede)
      {
         Func<EventTemplateData, string> template = GetTemplate(ede.TemplateName);
         if (template != null) return template(ede);
         return string.Empty;
      }

      private static Func<EventTemplateData, string> GetTemplate(string templateName)
      {
         Func<EventTemplateData, string> tmpl = CacheService.Get(templateName) as Func<EventTemplateData, string>;
         if (tmpl == null)
         {
            string t = EventData.GetTemplate(templateName);

            if (t != null)
            {
               string[] ts = Regex.Split(t, "(\\$\\{[a-z_]+\\})");
               tmpl = delegate(EventTemplateData ede)
               {
                  string ret = null; foreach (string s in ts) { if (s.StartsWith("${")) ret += ede.Get(s); else ret += s; } return ret;
               };

               if (tmpl != null) CacheService.Add(templateName, tmpl);
            }
         }

         return tmpl;
      }


   }

   public class EventTemplateData
   {
      private IDictionary<string, object> dataDict = null;
      private EventEntity se;
      public EventTemplateData(EventEntity se)
      {
         this.se = se;
         dataDict = JSON.Instance.Parse(se.data) as IDictionary<string, object>;
      }
      public string Get(string name)
      {
         if (name == "${eid}") return se.id.ToString();
         string k = name.Substring(2, name.Length - 3);
         if(k.StartsWith("name")) return GetName(CU.ToInt(dataDict[k]));
         if(k.StartsWith("www")) return GetWWW(CU.ToInt(dataDict[k]));
         if(k.StartsWith("icon")) return GetIcon(CU.ToInt(dataDict[k]));
         if (k.StartsWith("content")) return GetContent();
         if (dataDict.ContainsKey(k)) return dataDict[k].ToString();

         return string.Empty;
      }

      private string GetContent()
      {
         return se.created.ToString("yyyy.M.dd");
      }

      private string GetName(int  id)
      {
         return UserAccount.Get(id).name;   
      }

      private string GetWWW(int id)
      {
         return UserAccount.Get(id).www;
      }

      private string GetIcon(int id)
      {
         UserEntity ue = UserAccount.Get(id);
         if (ue != null) return string.Format("<div class=\"ef_icon\" style=\"\"><a style=\"background-image:url({0})\" href=\"/{1}\" class=\"hover_card\" uid=\"{2}\"></a></div>", ue.avatar, ue.www, ue.id);
         return "<div class=\"ef_icon\" style=\"\"><a href=\"/\" ></a></div>";
      }

      public string TemplateName
      {
         get { return se.tmplname; }
      }
   }
}
