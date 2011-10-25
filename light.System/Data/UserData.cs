using System.Data;
using System.Data.SqlClient;
using light.Data;
using light.Entities;
using System.Collections.Generic;
using System;

namespace light.Data
{
   /// <summary>
   ///  账户相关数据访问
   /// </summary>
   public sealed class UserData
   {
      /// <summary>
      ///  根据用户名和密码获取对应的账户实体对象.
      /// </summary>
      /// <param name="name">用户名</param>
      /// <param name="passwd">密码</param>
      /// <returns>如果无匹配的对象则返回null.</returns>
      public static UserEntity LoginByName(string name, string passwd)
      {
         return EB<UserEntity>.Get("SELECT * FROM user_account WHERE name=@name AND passwd=@passwd AND status=0", new SqlParameter("@name", name), new SqlParameter("@passwd", passwd));
      }

      /// <summary>
      ///  根据用户名和密码获取对应的账户实体对象.
      /// </summary>
      /// <param name="name">用户名</param>
      /// <param name="passwd">密码</param>
      /// <returns>如果无匹配的对象则返回null.</returns>
      public static UserEntity LoginByEmail(string email, string passwd)
      {
         return EB<UserEntity>.Get("SELECT * FROM user_account WHERE email=@email AND passwd=@passwd AND  status=0", new SqlParameter("@email", email), new SqlParameter("@passwd", passwd));
      }

      /// <summary>
      ///  创建账户实体对象
      /// </summary>
      /// <param name="entity"></param>
      /// <returns></returns>
      public static int Create(UserEntity entity)
      {
         return EB<UserEntity>.Create(QA.DBCS_MAIN, entity);
      }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal static UserEntity Get(int id)
      {
         return EB<UserEntity>.Get("SELECT * FROM user_account WHERE id=@id", new SqlParameter("@id", id));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      internal static UserEntity FromUrlKey(string key)
      {
         return EB<UserEntity>.Get("SELECT * FROM user_account WHERE www=@www", new SqlParameter("@www", key));
      }

      internal static bool ExistWWW(string www)
      {
         return DBH.GetBoolean(QA.DBCS_MAIN, CommandType.Text, "SELECT COUNT(id) FROM user_account WHERE www=@www", new SqlParameter("@www", www));
      }

      internal static void ExistNameOREmail(string name, out bool bName, string email, out bool bEmail)
      {
         DBH.Out(QA.DBCS_MAIN, CommandType.Text, "SELECT (SELECT COUNT(name) FROM user_account WHERE name=@name),(SELECT COUNT(email)  FROM user_account WHERE email=@email)", out bName, out bEmail,
            new SqlParameter("@name", name),
            new SqlParameter("@email", email));
      }

      internal static int Update(UserEntity entity)
      {
         return EB<UserEntity>.Update(QA.DBCS_MAIN, entity);
      }

      internal static string GetUrl(int id)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT www FROM user_account WHERE id=@id", new SqlParameter("@id", id));
      }

      /// <summary>
      ///  设置用户图标
      /// </summary>
      /// <param name="icon"></param>
      /// <param name="id"></param>
      public static int SetAvatar(string avatar, int id)
      {
         return DBH.ExecuteText(QA.DBCS_MAIN, "UPDATE user_account SET avatar=@avatar WHERE id=@id", new SqlParameter("@avatar", avatar), new SqlParameter("@id", id));
      }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="uid1"></param>
      /// <param name="mid"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public static int BeingFriend(int uid, int mid, byte type)
      {
         return DBH.ExecuteSP(QA.DBCS_MAIN, "usp_being_friend", 
            new SqlParameter("@uid1",uid),
            new SqlParameter("@uid2", mid),
            new SqlParameter("@type",type));
      }
      
      /// <summary>
      ///  uid follow followid
      /// </summary>
      /// <param name="uid"></param>
      /// <param name="followid"></param>
      /// <returns></returns>
      public static int Follow(int uid, int followid)
      {
         return DBH.ExecuteText(QA.DBCS_MAIN, "INSERT INTO follow(uid,followid) VALUES(@uid,@followid)",
            new SqlParameter("@uid", uid),
            new SqlParameter("@followid", followid));
      }

      internal static bool CanApplyFriend(int uid, int tid)
      {
         if (IsFriend(uid, tid)) return false; //已经是朋友了

         int eid = DBH.GetInt32(QA.DBCS_MAIN,CommandType.Text,"SELECT id FROM event WHERE uid=@uid AND touid=@tid AND tmplname='apply_friend'",new SqlParameter("@uid", uid), new SqlParameter("@tid", tid));

         if(eid>0) //发送过邀请
         {
            bool hasEventIndex = DBH.GetBoolean(QA.DBCS_MAIN, CommandType.Text, "SELECT COUNT(*) event_index WHERE uid=@tid AND eid=@eid", new SqlParameter("@tid", tid), new SqlParameter("@eid", eid));
            if (hasEventIndex) return false; //对方还没处理
         }

         return true;
      }

      public static bool IsFriend(int uid1, int uid2)
      {
         return DBH.GetBoolean(QA.DBCS_MAIN, CommandType.Text, "SELECT COUNT(*) FROM friend WHERE uid1=@uid1 AND uid2=@uid2", new SqlParameter("@uid1", uid1), new SqlParameter("@uid2", uid2));
      }

      #region INVITE_CODE

      /// <summary>
      ///  获取可用的邀请码
      /// </summary>
      /// <param name="id">用户标识</param>
      /// <returns>返回邀请码列表，如果没有可用的邀请码返回NULL</returns>
      public static IList<string> GetInviteCode(int id)
      {
         return DBH.GetList<string>(QA.DBCS_MAIN, CommandType.Text, "SELECT code FROM inviteinfo WHERE id=@id AND used=0", new SqlParameter("@id", id));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <returns></returns>
      internal static IList<string> SetInviteCode(UserStatusEntity info)
      {
         string SQLF = "INSERT INTO inviteinfo(id,code) VALUES('{0}','{1}');";
         string sql = string.Empty;
         IList<string> codeList = new List<string>();

         for (int i = info.invited; i < info.maxinvites; i++)
         {
            string code = Guid.NewGuid().ToString("N");
            codeList.Add(code);
            sql += string.Format(SQLF, info.id, code);
         }

         if (DBH.ExecuteText(QA.DBCS_MAIN, sql, null) > 0) return codeList;
         else return null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <returns></returns>
      internal static UserStatusEntity FromCode(string code)
      {
         return EB<UserStatusEntity>.Get("SELECT * FROM user_status INNER JOIN inviteinfo ON user_status.uid = inviteinfo.id WHERE inviteinfo.code=@code and used=0", new SqlParameter("@code", code));
      }

      /// <summary>
      ///  检查并锁定邀请码
      /// </summary>
      /// <param name="code">邀请代码</param>
      /// <returns>如果邀请码可用返回true</returns>
      internal static bool LockCode(string code, string sid)
      {
         return DBH.GetBoolean(QA.DBCS_MAIN, CommandType.Text,
            "SELECT COUNT(*) FROM inviteinfo WHERE code=@code AND used=0;UPDATE inviteinfo SET used=1,invited=getdate() WHERE code=@code;",
            new SqlParameter("@code", code),
            new SqlParameter("@id", sid));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      internal static void UnLockCode(string code)
      {
         DBH.GetBoolean(QA.DBCS_MAIN, CommandType.Text, "UPDATE inviteinfo SET used=0 WHERE code=@code",
            new SqlParameter("@code", code));
      }

      #endregion

      /// <summary>
      ///  获取用户的状态数据实体
      /// </summary>
      /// <param name="uid">用户标识</param>
      /// <returns>返回对应的用户状态实体</returns>
      internal static UserStatusEntity GetStatus(int uid)
      {
         return EB<UserStatusEntity>.Get("SELECT * FROM user_status WHERE uid=@uid", new SqlParameter("@uid", uid));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="name"></param>
      /// <param name="www"></param>
      /// <param name="id"></param>
      /// <param name="sid"></param>
      /// <param name="code"></param>
      internal static void Initialize(string name, string www, int id, string sid, string code)
      {
         if (!string.IsNullOrEmpty(code))
         {
            //更新好友数据
            string sql = "UPDATE user_status SET invited=invited+1 WHERE id=@id1;UPDATE inviteinfo SET invited_id=@id2 WHERE id=@id1 AND code=@code;";  //id1 邀请 id2
            DBH.ExecuteText(QA.DBCS_MAIN, sql, new SqlParameter("@id1", sid), new SqlParameter("@id2", id), new SqlParameter("@code", code));
         }

         //写入用户信息
         EB<UserStatusEntity>.Create(QA.DBCS_MAIN, new UserStatusEntity()
         {
            id = id,
         });
      }


      /// <summary>
      ///  获取最近加入组织的用户信息
      /// </summary>
      /// <param name="count">获取的最大数量</param>
      /// <returns>返回实体列表</returns>
      internal static IList<UserStatusEntity> GetRecent(int count)
      {
         return EB<UserStatusEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP " + count + " * FROM user_status ORDER BY created DESC");
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal static IList<UserEntity> GetInvited(int id)
      {
         return EB<UserEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP 20 id,www,name,avatar FROM user_account WHERE id in(SELECT invited_id FROM inviteinfo WHERE id=@id and used=1) ORDER BY id ASC", new SqlParameter("@id", id));
      }

      internal static IList<UserEntity> GetFriends(int count,int uid)
      {
         return EB<UserEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT TOP " + count + " user_account.* FROM user_account INNER JOIN friend ON friend.uid2=user_account.id WHERE friend.uid1=@uid", new SqlParameter("@uid", uid));
      }

      internal static int ChangePassword(int uid, string newpwdmd5)
      {
         return DBH.ExecuteText(QA.DBCS_MAIN, "UPDATE user_account SET passwd=@passwd WHERE id=@id", new SqlParameter("@id", uid), new SqlParameter("@passwd", newpwdmd5));
      }
   }
}
