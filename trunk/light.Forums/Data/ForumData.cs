using System.Collections.Generic;
using light.Forums.Entities;
using light.Data;
using System.Data;
using System.Data.SqlClient;

namespace light.Forums.Data
{
   public class ForumData
   {
      //public IList<
      public static IList<BoardEntity> GetBoardList()
      {
         return EB<BoardEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM forum_board WHERE status=0 AND type=2");
      }

      public static BoardEntity GetBoard(int bid)
      {
         return EB<BoardEntity>.Get(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM forum_board WHERE bid=@bid", new SqlParameter("@bid",bid));
      }

      #region Thread CURD

      public static int CreateThread(int bid,byte type,int uid,string uname,string uip,string name,string story)
      {
         return DBH.GetInt32(QA.DBCS_MAIN, CommandType.StoredProcedure, "usp_thread_create",
            new SqlParameter("@bid", bid),
            new SqlParameter("@type", type),
            new SqlParameter("@uid", uid),
            new SqlParameter("@uname", uname),
            new SqlParameter("@uip", uip),
            new SqlParameter("@name",name),
            new SqlParameter("@story", story));
      }

      public static ThreadEntity GetThread(int tid)
      {
         return EB<ThreadEntity>.Get("SELECT * FROM forum_thread WHERE tid=@tid", new SqlParameter("@tid", tid));
      }

      public static int UpdateThread(ThreadEntity threadEntity)
      {
         return EB<ThreadEntity>.Update(QA.DBCS_MAIN, threadEntity);
      }

      public static int DeleteThread(int tid)
      {
         return DBH.ExecuteText(QA.DBCS_MAIN, "DELETE forum_thread WHERE tid=@tid;", new SqlParameter("@tid", tid));
      }

      #endregion

      public static int CreatePost(PostEntity postEntity)
      {
         return EB<PostEntity>.Create(QA.DBCS_MAIN, postEntity);
      }

      public static int UpdatePost(PostEntity postEntity)
      {
         return EB<PostEntity>.Update(QA.DBCS_MAIN, postEntity);
      }

      public static PostEntity GetPost(int pid)
      {
         return EB<PostEntity>.Get("SELECT * FROM forum_post WHERE pid=@pid", new SqlParameter("@pid", pid));
      }

      public static int DeletePost(int pid)
      {
         return DBH.ExecuteText(QA.DBCS_MAIN, "DELETE forum_post WHERE pid=@pid", new SqlParameter("@pid", pid));
      }

      internal static IList<ThreadEntity> GetThreads()
      {
         return EB<ThreadEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM forum_thread ORDER BY created DESC");
      }

      public static IList<PostEntity> GetThreadPosts(int tid)
      {
         return EB<PostEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM forum_post WHERE tid=@tid", new SqlParameter("@tid", tid));
      }
   }
}
