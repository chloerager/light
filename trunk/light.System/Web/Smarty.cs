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
   public class Smarty : Control,INamingContainer
   {
      protected override void RenderContents(HtmlTextWriter output)
      {
      }

      [TemplateContainer(typeof(Smarty))]
      [PersistenceMode(PersistenceMode.InnerProperty)]
      public ITemplate Template
      { 
      
      }
   }
}
