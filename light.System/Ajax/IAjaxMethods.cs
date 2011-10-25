using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace light.Ajax
{
   public delegate void AjaxMethod(HttpContext context);

   public interface IAjaxMethods
   {
      int RegisterMethod(IDictionary<string, AjaxMethod> methods);
   }
}
