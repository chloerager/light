using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Data;
using System.Web.Security;
using System.Web;
using light;
using light.Entities;
using System.Collections;

namespace light
{
   /// <summary>
   ///  账户控制器
   /// </summary>
   public sealed class UserAccount
   {
      /// <summary>
      ///  用户登录
      /// </summary>
      /// <param name="name"></param>
      /// <param name="pwd"></param>
      /// <returns></returns>
      public static bool Login(string name, string pwd)
      {
         UserEntity entity = null;
         pwd = MU.MD5(pwd);
         try
         {
            if (name.Contains("@")) entity = UserData.LoginByEmail(name, pwd);
            else entity = UserData.LoginByName(name, pwd);

            //write cookie
            if (entity != null)
            {
               CacheService.Add(CNC.ACCOUNT_ENTITY_ID + entity.id, entity);
               string id = entity.id.ToString(); // id 仅作内部使用，不对外公开
               QA.SetCookie(FormsAuthentication.FormsCookieName,
                  FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, id, DateTime.Now, DateTime.MaxValue, true, id, FormsAuthentication.FormsCookiePath))
                  , DateTime.MaxValue); //设置永久保留登录信息，之后版本可考虑进行配置

               QA.SetCookie(SC.CN.A_NAME, HttpUtility.UrlEncode(entity.name), DateTime.MaxValue);
               string cookie = QA.GetCookie(SC.CN.FROM);

               //TODO: write log,add to task.
               Log.Login(entity);
            }

         }
         catch { }

         return (entity != null);
      }

      public static UserEntity Get(int id)
      {
         string key = CNC.ACCOUNT_ENTITY_ID + id;
         UserEntity entity = CacheService.Get(key) as UserEntity;

         if (entity == null)
         {
            entity = UserData.Get(id);
            if (entity != null) CacheService.Add(key, entity);
         }

         return entity;
      }

      private struct CNC
      {
         public const string ACCOUNT_ENTITY_ID = "account_entity_";
      }

      public static bool Logout()
      {
         FormsAuthentication.SignOut();
         HttpContext.Current.Response.Cookies.Remove(SC.CN.A_NAME);
         return true;
      }

      public static int Create(string name, string pwd, string email,string www)
      {
         return UserData.Create(new UserEntity() { 
             name=name,
             password= MU.MD5(pwd),
             email=email,
             www=www,
             avatar=" "
         });
      }

      public static UserEntity FromUrlKey(string key)
      {
         return UserData.FromUrlKey(key);  
      }

      /// <summary>
      ///  获取用户的个性域名
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static string GetWWW(string name)
      {
         string www = HZ.ToPinYin(name);
         if (www.Length > 20) PinYin.GetInitial(name);

         if (Keyword.Reserve(www) || UserData.ExistWWW(www))
         { 
            int i=1;
            string w = www + "_" + i;
            while (Keyword.Reserve(w) || UserData.ExistWWW(w))
            {
               i++;
               w = www + "_" + i;
            }
            www = w;
         }

         return www;
      }

      public static void CheckNameAndEmail(string name, out bool bName, string email, out bool bEmail)
      {
         UserData.ExistNameOREmail(name, out bName, email, out bEmail);
      }

      public static UserEntity Current
      {
         get 
         {
            HttpContext context = HttpContext.Current;

            if (context.Request.IsAuthenticated)
            {
               int id = 0;
               string sid = context.User.Identity.Name;
               if (!string.IsNullOrEmpty(sid)) int.TryParse(sid, out id);
               if (id > 0)
               {
                  UserEntity CurrentAccount = UserAccount.Get(id);
                  return CurrentAccount;
               }
            }
            return null;
         }
      }

      public static int Update(UserEntity entity)
      {
         return UserData.Update(entity);  
      }

      public static string GetUrl(int id)
      {
         return UserData.GetUrl(id);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="user"></param>
      /// <param name="uid"></param>
      /// <returns></returns>
      public static int ApplyFriend(int uid, int tid)
      {
         bool noApply = UserData.CanApplyFriend(uid, tid);

         if (noApply)
         {
            IDictionary<string, string> data = new Dictionary<string, string>();
            string sid = uid.ToString();
            data.Add("name", sid);
            data.Add("www", sid);
            data.Add("uid", sid);
            data.Add("tid", tid.ToString());
            return EventData.CreateEvent(EventType.Important, EventSpreadType.Specified, tid, uid, "apply_friend", JU.Array(data));
         }
         return 0;
      }

      public static void ConfirmFriend(int uid, int mid)
      {
         int ret = UserData.BeingFriend(uid, mid, 2);

         if (ret > 0) // build event
         {
            string suid = uid.ToString();
            string smid = mid.ToString();
            /// uid be friend with mid
            IDictionary<string, string> data = new Dictionary<string, string>();
            data.Add("name", smid);
            data.Add("www", smid);
            data.Add("name_self", suid);
            data.Add("www_self", suid);
            data.Add("icon", suid);
            EventData.CreateEvent(EventType.Normal, EventSpreadType.Specified, uid, uid, "be_friend", JU.Array(data));

            // mid be friend with uid
            IDictionary<string, string> datam = new Dictionary<string, string>();
            datam.Add("name", suid);
            datam.Add("www", suid);
            datam.Add("name_self", smid);
            datam.Add("www_self", smid);
            datam.Add("icon", smid);
            EventData.CreateEvent(EventType.Normal, EventSpreadType.Specified, mid, mid, "be_friend", JU.Array(datam));
         }
      }

      public static UserStatusEntity FromCode(string code)
      {
         return UserData.FromCode(code);
      }

      /// <summary>
      ///  获取可用的邀请码，如果没有返回NULL.
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public static IList<string> GetInviteCode(int id)
      {
         IList<string> list = UserData.GetInviteCode(id);

         if (list == null || list.Count == 0)
         {
            //检查是否未分配邀请码
            UserStatusEntity info = UserData.GetStatus(id);

            if (info.invited < info.maxinvites) // 没有分配邀请码，分配邀请码
            {
               list = UserData.SetInviteCode(info);
            }
         }

         return list;
      }

      private static IList<string> SetInviteCode(UserStatusEntity info)
      {
         return UserData.SetInviteCode(info);
      }

      /// <summary>
      ///  获取用户的信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public static UserStatusEntity GetStatus(int id)
      {
         return UserData.GetStatus(id);
      }


      //TODO:移到存储过程里
      /// <summary>
      ///  完成注册及用户初始化相关操作
      /// </summary>
      /// <param name="name"></param>
      /// <param name="pwd"></param>
      /// <param name="email"></param>
      /// <param name="code"></param>
      /// <param name="srcid"></param>
      /// <returns></returns>
      public static bool Signup(string name, string pwd, string email, string code, string sid)
      {
         string www = UserAccount.GetWWW(name); //normal signup
         int id = -1;
         if (string.IsNullOrEmpty(code))
         {
            id = UserAccount.Create(name, pwd, email, www);
            if (id > 0) UserData.Initialize(name, www, id, sid, null);
         }
         else
         {
            if (UserData.LockCode(code, sid)) //invited signup
            {
               id = UserAccount.Create(name, pwd, email, www);
               if (id > 0) UserData.Initialize(name, www, id, sid, code);
               else UserData.UnLockCode(code);
            }
         }

         if (id > 0)
         {
            //create feed of signup event
            EventFeed.CreateEvent(0, 5, id, -1, "signup", "");

            //write cookie
            string nid = id.ToString();
            QA.SetCookie(FormsAuthentication.FormsCookieName,
               FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, nid, DateTime.Now, DateTime.Now.AddDays(1.0), true, nid, FormsAuthentication.FormsCookiePath))
               , DateTime.MaxValue); //设置永久保留登录信息，之后版本可考虑进行配置
            QA.SetCookie(SC.CN.A_NAME, HttpUtility.UrlEncode(name), DateTime.MaxValue);
            string cookie = QA.GetCookie(SC.CN.FROM);

            return true;
         }

         return false;
      }

      public static bool LockCode(string code, string id)
      {
         return UserData.LockCode(code, id);
      }

      /// <summary>
      ///  获取最近加入组织的用户信息(JSON格式数组)
      /// </summary>
      /// <param name="count">获取的最大数量</param>
      /// <returns>如果成功返回包含用户信息的JSON字符串</returns>
      public static string GetRecent(int count)
      {
         IList<UserStatusEntity> list = UserData.GetRecent(count);
         return JSON.Instance.ToJSON(list, false, false, false, false);
      }

      /// <summary>
      ///  获取邀请的好友列表JSON数据。
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public static string GetInvited(int id)
      {
         IList<UserEntity> list = UserData.GetInvited(id);

         return JSON.Instance.ToJSON(list, false, false, false, false, ",avatar,id,www,name,");
      }

      public static UserStatusEntity CurrentStatus
      {
         get
         {
            if (HttpContext.Current == null) return null;
            int id = CU.ToInt(HttpContext.Current.User.Identity.Name);
            if (id > 0) return UserData.GetStatus(id);
            return null;
         }
      }

      public static IList<UserEntity> GetFriends(int count,int uid)
      {
         return UserData.GetFriends(count,uid);
      }

      public static int SetAvatar(string url, int uid)
      {
         int ret = UserData.SetAvatar(url, uid);

         if (ret > 0)
         {
            string key = CNC.ACCOUNT_ENTITY_ID + uid;
            CacheService.Remove(key); //更新缓存
         }

         return ret;
      }
   }
}
