using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.System.Data;
using System.Web.Security;
using System.Web;
using light;
using light.System.Entities;

namespace light.System.Controller
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
             www=www
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
   }
}
