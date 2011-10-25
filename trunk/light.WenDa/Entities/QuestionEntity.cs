using System;
using light.Data;

namespace light.WenDa.Entities
{
   [Table("question")]
   public class QuestionEntity
   {
      [Field("id",true,true,true)]
      public int id;

      [Field("authorid")]
      public int authorid;

      [Field("good")]
      public int good;

      [Field("likes")]
      public int likes;

      [Field("created",true)]
      public DateTime created;

      [Field("author")]
      public string author;

      [Field("title")]
      public string title;

      [Field("story")]
      public string story;
   }
}
