using System;
using System.Collections.Generic;
using System.Text;

namespace light
{
   public class HTML
   {
      private const string PAGE_CURRENT_FORMAT = "<li><span class=\"current\">{0}</span></li>";
      private const string PAGE_DISABLED_FORMAT = "<li><span class=\"disabled\">{0}</span></li>";
      private const string PAGE_DOTTED = "<li><span class=\"dotted\">…</span></li>";
      private const string TAG_FORMAT = "<a href=\"{0}{1}/\">{2}</a> ";

      public static string BuildPager(int recordcount, int index, int size,string format,string firstFormat)
      {
         StringBuilder sb = new StringBuilder();
         format = "<li>" + format + "</li>";
         firstFormat = "<li>" + firstFormat + "</li>";

         if (recordcount > size)
         {
            int pagecount = recordcount / size + ((recordcount % size) > 0 ? 1 : 0);
            if (index <= 0) index = 1;
            if (index > pagecount) index = pagecount;

            //prev
            if (index > 1)
            {
               if (index == 2) sb.AppendFormat(firstFormat,"&lt;"); //永远是1，直接显示主路径
               else sb.AppendFormat(format, index - 1, "&lt;");
            }
            else sb.AppendFormat(PAGE_DISABLED_FORMAT, "&lt;");

            //next
            if (index < pagecount) sb.AppendFormat(format,index + 1, "&gt;");
            else sb.AppendFormat(PAGE_DISABLED_FORMAT, "&gt;");

            //first
            if (index > 1) sb.AppendFormat(firstFormat,"1"); //永远是1，直接显示主路径
            else sb.AppendFormat(PAGE_CURRENT_FORMAT, "1");

            //two
            if (index == 2) sb.AppendFormat(PAGE_CURRENT_FORMAT, 2);
            else if (pagecount >= 2) sb.AppendFormat(format,2, 2);

            //three
            int i, k;
            if (pagecount >= 13)
            {
               if (index > 8)
               {
                  sb.Append(PAGE_DOTTED);
                  if (pagecount - index > 4) i = index - 5;
                  else i = pagecount - 9;
                  k = index + 5;
               }
               else
               {
                  i = 3;
                  k = 10;
               }
            }
            else
            {
               i = 3;
               k = 13;
            }

            for (; i <= pagecount && i <= k; i++)
            {
               if (i != index) sb.AppendFormat(format, i, i);
               else sb.AppendFormat(PAGE_CURRENT_FORMAT, i);
            }

            if (pagecount >= 13)
            {
               if (i <= pagecount - 2) sb.Append(PAGE_DOTTED);
               if (i <= pagecount - 1) sb.AppendFormat(format,pagecount - 1, pagecount - 1);
               if (i <= pagecount) sb.AppendFormat(format,pagecount, pagecount);
            }

         }
         else
         {
            sb.AppendFormat(PAGE_DISABLED_FORMAT, "&lt;");
            sb.AppendFormat(PAGE_DISABLED_FORMAT, "&gt;");
            sb.AppendFormat(PAGE_CURRENT_FORMAT, "1");
         }

         return sb.ToString();
      }

      public static string BuildTags(string tags, string urlprefix)
      {
         string tagHTML = string.Empty;

         if (tags != null && tags != string.Empty)
         {
            string[] tagList = tags.Split(' ');

            foreach (string t in tagList)
            {
               tagHTML += string.Format(TAG_FORMAT, urlprefix, t.Replace("+","%2b"),t);
            }
         }

         return tagHTML;
      }


   }
}
