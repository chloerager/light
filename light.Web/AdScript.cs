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
    public class AdScript : Control
    {
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public AdSize Size
        {
            get;
            set;
        }

        protected override void Render(HtmlTextWriter writer)
        {
#if !DEBUG
            switch (Size)
            {
                case AdSize.G_250_250:
                    writer.Write("<script type=\"text/javascript\">google_ad_client = \"pub-5121995024841266\";google_ad_slot = \"1013399356\";google_ad_width = 250;google_ad_height = 250;</script><script type=\"text/javascript\" src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\"></script>");
                    break;
                default:
                    break;
            }
#endif
            base.Render(writer);
        }
    }

    public enum AdSize
    {
        G_250_250,
        _360_180
    }
}