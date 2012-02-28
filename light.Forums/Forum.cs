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

      internal static int SaveThread(int bid,int uid, string uname, string name, string ip,string story)
      {
         return ForumData.CreateThread(bid, 0, uid, uname, ip, name, story);
      }

      public static ThreadEntity GetThread(int tid)
      {
         return ForumData.GetThread(tid);
      }

      internal static int SavePost(int bid, int tid, int uid, string uname, string name, string ip, string story)
      {
         return ForumData.CreatePost(new PostEntity() { 
            bid=bid,
            tid=tid,
            uid=uid,
            uname=uname,
            name=name,
            uip=ip,
            story=story
         });
      }

      internal static void IncThreadReplies(int tid)
      {
         DBH.Inc(QA.DBCS_MAIN, "forum_thread", "replies", "tid", tid.ToString());
      }
   }
}
