using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;

namespace light.Content.Entities
{
   /// <summary>
   ///  分类实体
   /// </summary>
   public class CategoryEntity
   {
      /// <summary>
      ///  分类标识
      /// </summary>
      [Field("id",true,true,true)]
      public int id;

      [Field("pid")]
      public int pid;

      [Field("count",true)]
      public int count;

      [Field("displayorder")]
      public int displayorder;

      [Field("name")]
      public string name;

      [Field("pinyin")]
      public string pinyin;

      [Field("pinyinabbr")]
      public string pinyinabbr;

      [Field("ename")]
      public string ename;
   }

   public class UserCategoryEntity
   {
      [Field("id",true,true,true)]
      public int id;

      [Field("pid")]
      public int pid;

      [Field("uid")]
      public int uid;

      [Field("count",true)]
      public int count;

      [Field("displayorder")]
      public int displayorder;

      [Field("name")]
      public string name;

      [Field("pinyin")]
      public string pinyin;

      [Field("pinyinabbr")]
      public string pinyinabbr;

      [Field("ename")]
      public string ename;
   }
}
