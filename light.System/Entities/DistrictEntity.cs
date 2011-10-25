using light.Data;

namespace light.Entities
{
   [Table("district")]
   public class DistrictEntity
   {
      [Field("id", false)]
      public int Id;

      [Field("name")]
      public string Name;

      [Field("levelcode")]
      public int LevelCode;

      [Field("pid")]
      public int Pid;
   }
}
