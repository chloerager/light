using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace light.Ajax
{
   public class SRHandler : IAjaxMethods
   {
      public void Get(string name, string key)
      { 
         
      }

      public int RegisterMethod(IDictionary<string, AjaxMethod> methods)
      {
         methods.Add("display", Display);
         return 0;
      }

      public void Display(HttpContext context)
      { 
      
      }
   }
}
