using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Content.Data;
using light.Content.Entities;
using light;

namespace KB
{
   public class Digest
   {
      public static int Save(int uid, string uname, int cid, int ucid, string name, string sourcename, string sourceurl, string content)
      {
         return ContentData.Create("digest", new ContentEntity()
         {
            uid = uid,
            uname = uname,
            cid = cid,
            ucid = ucid,
            name = name,
            url = HZ.ToPinYin(name, true, true),
            source = sourcename,
            sourceurl = sourceurl,
            story = content
         });
      }

      public static IList<ContentEntity> List()
      {
         return ContentData.List("digest");
      }

      public static ContentEntity Get(string k)
      {
         return ContentData.Get("digest", k);
      }
   }
}
