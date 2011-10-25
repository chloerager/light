using System.Collections.Generic;
using System.Linq;
using light.Data;
using light.Entities;
using System.Text;

namespace light
{
   /// <summary>
   ///  事件相关操作
   /// </summary>
   public sealed class EventFeed
   {
      /// <summary>
      /// create a event feed and send event index to special user.
      /// </summary>
      /// <param name="type">EventType,Normal:0,Stick:1,Important:2,Close:4</param>
      /// <param name="spread">EventSpreadType,Specified:0,Follow:1,FromAndTo:2,Friends:3,FollowAndFriends:4,Self:5</param> 
      /// <param name="uid">event lanucher's id</param>
      /// <param name="touid">event target's id</param>
      /// <param name="tmplName">event display template's name</param>
      /// <param name="data">data for display event.</param>
      /// <returns>if failed return 0 or -1</returns>
      public static int CreateEvent(byte type, byte spread, int uid, int touid, string tmplName, string data = null)
      {
         return EventData.CreateEvent(type, spread, uid, touid, tmplName, data);
      }

      /// <summary>
      ///  获取指定用户最近的N条事件消息
      /// </summary>
      /// <param name="uid">用户标识</param>
      /// <param name="count">事件数</param>
      /// <param name="eid">事件起始标识</param>
      /// <returns></returns>
      public static IList<EventEntity> ListEvent(int uid, int count, int eid)
      {
         return EventData.ListEvent(uid, count, eid);
      }

      public static string BuildNewsFeed(UserEntity entity)
      {
         StringBuilder sb = new StringBuilder();

         IList<EventEntity> list = EventFeed.ListEvent(entity.id, 100, 0);
         IEnumerable<EventEntity> stick = from e in list where e.type == EventType.Important select e;
         IEnumerable<EventEntity> normal = from e in list where e.type == EventType.Normal select e;


         //stick 消息
         if (stick != null && stick.Count() > 0)
         {
            sb.Append("<div class=\"s_story\"><div class=\"s_title\">重要的消息</div>");
            foreach (EventEntity se in stick)
            {
               EventTemplateData ede = new EventTemplateData(se);
               sb.Append(EventTemplate.Build(ede));
            }
            sb.Append("</div>");
         }

         //普通消息
         if (normal != null && normal.Count() > 0)
         {
            foreach (EventEntity ee in normal)
            {
               EventTemplateData ede = new EventTemplateData(ee);
               sb.Append("<div class=\"s_story\"><div class=\"ef\">");
               sb.Append(EventTemplate.Build(ede));
               sb.Append("</div></div>");
            }
         }

         return sb.ToString();
      }

      public static string BuildRecentFeed(UserEntity entity)
      {
         StringBuilder sb = new StringBuilder();
         IList<EventEntity> list = EventData.ListUserPublicEvent(entity.id, 20);

         if (list != null && list.Count > 0)
         {
            foreach (EventEntity ee in list)
            {
               EventTemplateData ede = new EventTemplateData(ee);
               sb.Append("<div class=\"s_story\"><div class=\"ef\">");
               sb.Append(EventTemplate.Build(ede));
               sb.Append("</div></div>");
            }
         }

         return sb.ToString();
      }

      public static int CreateSignupEvent(int uid)
      {
         IDictionary<string, string> data = new Dictionary<string, string>();
         string suid = uid.ToString();
         data.Add("name_self", suid);
         data.Add("www_self", suid);
         data.Add("icon", suid);
         return CreateEvent(EventType.Normal, EventSpreadType.Self, uid, uid, "signup", JU.Array(data));
      }
   }
}
