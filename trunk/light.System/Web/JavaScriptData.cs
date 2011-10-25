using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace light.Web
{
   public class JavaScriptData : WebControl
   {
      private StringBuilder data = new StringBuilder("<script type=\"text/javascript\">");
      private IDictionary<string, string> nsData = new Dictionary<string, string>();

      public void Add(string name, string value)
      {
         data.Append(string.Concat("var ", name, "=", value, ";"));
      }

      public void Add(string name, string value, string ns)
      {
         if (nsData.ContainsKey(ns)) nsData[ns] += "," + JU.SingleWithoutBrackets(name, value);
         else nsData.Add(ns, JU.SingleWithoutBrackets(name, value));
      }

      protected override void Render(HtmlTextWriter writer)
      {
         if (nsData.Count > 0)
         {
            foreach (string k in nsData.Keys)
            {
               data.Append(string.Concat("var ", k, "={", nsData[k], "};"));
            }
         }

         data.Append("</script>");
         writer.Write(data.ToString());
      }

   }
}
