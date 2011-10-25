using light.Data;

namespace light.CMS.Entities
{
   [Table("quotation")]
   public class QuotationEntity
   {
      [Field("id",true,true)]
      public int Id;

      [Field("type")]
      public int Type;

      [Field("author_id")]
      public int AuthorId;

      [Field("story")]
      public string Story;

      [Field("author")]
      public string Author;
   }
}
