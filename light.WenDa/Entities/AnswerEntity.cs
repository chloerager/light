using System;
using light.Data;

namespace light.WenDa.Entities
{
   /// <summary>
   ///  问题答案实体
   /// </summary>
   [Table("question_answer")]
   public class AnswerEntity
   {
      /// <summary>
      ///  答案标识
      /// </summary>
      [Field("id")]
      public int id;

      /// <summary>
      ///  问题标识
      /// </summary>
      [Field("qid")]
      public int qid;

      /// <summary>
      ///  回复标识
      /// </summary>
      [Field("rid")]
      public int rid;

      /// <summary>
      ///  作者标识
      /// </summary>
      [Field("authorid")]
      public int authorid;

      /// <summary>
      /// 创建时间
      /// </summary>
      [Field("created",true)]
      public DateTime created;

      /// <summary>
      ///  作者名字
      /// </summary>
      [Field("authorname")]
      public string authorname;

      /// <summary>
      ///  回复内容
      /// </summary>
      [Field("story")]
      public string story;
   }
}
