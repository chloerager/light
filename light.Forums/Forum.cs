using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Forums.Entities;
using light.Forums.Data;

namespace light.Forums
{
   public class Forum
   {
      public static  IList<BoardEntity> GetBoardList()
      {
         return ForumData.GetBoardList();
      }

      public static BoardEntity GetBoard(int bid)
      {
         return ForumData.GetBoard(bid);
      }

      public static IList<PostEntity> GetThreadPosts(int id)
      {
         return ForumData.GetThreadPosts(id);
      }

      public static IList<ThreadEntity> GetThreads()
      {
         return ForumData.GetThreads();
      }

      internal static void SaveThread(int p, string p_2, string name, string story)
      {
         throw new NotImplementedException();
      }

      public static ThreadEntity GetThread(int tid)
      {
         return ForumData.GetThread(tid);
      }
   }
}
