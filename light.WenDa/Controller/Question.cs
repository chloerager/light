using light.WenDa.Data;
using light.WenDa.Entities;

namespace light.WenDa.Controller
{
   public class Question
   {
      public static QuestionEntity Get(int id)
      {
         //TODO: check cache & cache
         return QuestionData.Get(id);
      }
   }
}
