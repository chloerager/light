using System.Collections.Generic;
using System.Web;
using light.System;
using light.System.Ajax;
using light.System.Controller;
using light.System.Entities;
using light.WenDa.Data;
using light.WenDa.Entities;

namespace light.WenDa.Ajax
{
   public class AjaxRegister : IAjaxRegister
   {
      #region Question.Save
      public const string QUESTION_SAVE = "question.save";
      public void SaveQuestion(HttpContext context)
      {
         string name = context.Request.Form["n"];
         string story = context.Request.Form["s"];
         string action = context.Request.Form["a"];
         string id = context.Request.Form["id"];

         if (string.IsNullOrEmpty(action)) 
         {
            return;
         }
         else
         {
            UserEntity entity = UserAccount.Current;
            if (entity != null)
            {
               if (action == "create")
               {
                  int ret = QuestionData.Create(new QuestionEntity()
                  {
                     authorid = entity.id,
                     author = entity.name,
                     title = name,
                     story = story
                  });

                  if (ret > 0)
                  {
                     EventFeed.CreateEvent(new EventEntity() { 
                        uid = entity.id,
                        uname = entity.name,
                        www = entity.www,
                        //avatar = entity.avatar,
                     });
                  }
               }
               else if (action == "edit")
               {

               }
            }
         }
      }
      #endregion

      #region Question.Answer

      public const string QUESTION_ANSWER = "question.answer";
      public void AnswerQuestion(HttpContext context)
      { 
         
      }

      #endregion

      public int RegisterMethod(IDictionary<string, AjaxMethod> methods)
      {
         int count = 0;
         if (!methods.ContainsKey(QUESTION_SAVE)) { methods.Add(QUESTION_SAVE, SaveQuestion); count++; }
         return count;
      }
   }
}
