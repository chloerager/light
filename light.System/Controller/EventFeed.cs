using System.Collections.Generic;
using light.System.Entities;
using light.System.Data;

namespace light.System.Controller
{
   public sealed class EventFeed
   {
      /// <summary>
      ///  创建事件并发送给事件指定的uid.
      /// </summary>
      /// <param name="to">0 to all follow,1 to self,2 to all(不创建索引)</param>
      /// <param name="entity">事件实体</param>
      public static int CreateEvent(EventEntity entity)
      {
         return EventData.CreateEvent(entity);
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
   }
}
