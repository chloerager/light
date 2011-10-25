using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light.Entities
{
   public class EventType
   {
      /// <summary>
      ///  普通消息
      /// </summary>
      public const byte Normal = 0;

      /// <summary>
      ///  置顶消息，顶部显示
      /// </summary>
      public const byte Stick = 1;

      /// <summary>
      ///  重要消息
      /// </summary>
      public const byte Important = 2;

      /// <summary>
      ///  已关闭的消息
      /// </summary>
      public const byte Closed = 4;
   }

   /// <summary>
   ///  传播方式
   /// </summary>
   public class EventSpreadType
   {
      /// <summary>
      ///  传播给指定的用户(touid)
      /// </summary>
      public const byte Specified = 0;

      /// <summary>
      ///  传播给所有关注者
      /// </summary>
      public const byte Follow = 1;

      /// <summary>
      ///  传播的参与者双方
      /// </summary>
      public const byte FromAndTo = 2;

      /// <summary>
      /// 传播给好友
      /// </summary>
      public const byte Friends = 3;

      /// <summary>
      ///  传播给关注者和好友
      /// </summary>
      public const byte FollowAndFriends = 4;

      /// <summary>
      ///  只发送给自己
      /// </summary>
      public const byte Self = 5;
   }
}
