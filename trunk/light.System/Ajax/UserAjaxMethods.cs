using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using light;
using light;
using light;
using light.Entities;
using light.Data;

namespace light.Ajax
{
   public class UserAjaxMethods
   {
      /// <summary>
      ///  响应账户注册请求
      ///  成功：1 失败：0 邀请码错误：-1
      /// </summary>
      /// <param name="context"></param>
      public static void Signup(HttpContext context)
      {
         string name = context.Request.Form["n"];
         string pwd = context.Request.Form["p"];
         string email = context.Request.Form["e"];
         string code = context.Request.Form["c"];
         string id = context.Request.Form["i"];

         if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(email))
         {
            context.Response.Write(JU.Build(false, "数据输入不完整或者格式不正确!"));
            context.Response.End();
         }

         if (UserAccount.Signup(name, pwd, email, code, id))
         {
            context.Response.Write(JU.Build(true, ""));
            context.Response.End();
         }

         context.Response.Write(JU.Build(false, "注册失败，请联系管理员"));
      }

      /// <summary>
      ///  处理用户登录请求
      /// </summary>
      /// <param name="context"></param>
      public static void Login(HttpContext context)
      {
         string name = context.Request.Form["n"];
         string pwd = context.Request.Form["p"];
         if (UserAccount.Login(name, pwd))
         {
            string from = QA.GetCookie(SC.CK_LOGIN_FROM);
            if (from == null || from.Contains("login")) from = "/home";
            context.Response.Write(JU.Build(true, from));
         }
         else context.Response.Write(JU.Build(false, ""));
      }

      public static void Logout(HttpContext context)
      {
         context.Response.Write(UserAccount.Logout() ? "1" : "0");
      }

      public static void SaveBaseInfo(HttpContext context)
      {
         string sex = context.Request.Form["s"];
         string year = context.Request.Form["y"];
         string month = context.Request.Form["mo"];
         string day = context.Request.Form["d"];
         string province = context.Request.Form["p"];
         string pt = context.Request.Form["pt"];
         string city = context.Request.Form["c"];
         string ct = context.Request.Form["ct"];
         string realname = context.Request.Form["rn"];
         string idnumber = context.Request.Form["i"];
         string mobile = context.Request.Form["mi"];

         UserEntity entity = UserAccount.Current;
         if (entity != null)
         {
            entity.sex = byte.Parse(sex);
            entity.birthday = DateTime.Parse(year + "-" + month + "-" + day);
            entity.location = JSON.Instance.ToJSON(new LocationEntity()
            {
               id = int.Parse(province),
               name = pt,
               sub = new LocationEntity() { id = int.Parse(city), name = ct, sub = null }
            });
            if (!string.IsNullOrEmpty(realname)) entity.realname = realname;
            if (!string.IsNullOrEmpty(idnumber)) entity.idnumber = idnumber;
            if (!string.IsNullOrEmpty(mobile)) entity.mobile = mobile;

            if (UserAccount.Update(entity) > 0)
            {
               context.Response.Write(JU.AJAX_SUCCESS);
               return;
            }
         }

         context.Response.Write(JU.AJAX_FAIL);
      }

      public static void CheckNameAndEmail(HttpContext context)
      {
         string name = context.Request.Form["n"];
         string email = context.Request.Form["e"];

         int ret = 0;

         bool bName, bEmail;
         UserAccount.CheckNameAndEmail(name, out bName, email, out bEmail);
         if (bName && bEmail) ret = 3;
         else if (bName) ret = 1;
         else if (bEmail) ret = 2;

         context.Response.Write(ret);
      }
      public static void ChangePassword(HttpContext context)
      {
         if (context.Request.IsAuthenticated)
         {
            string op = context.Request.Form["op"];
            string np = context.Request.Form["np"];

            if (string.IsNullOrEmpty(op) || string.IsNullOrEmpty(np)) { context.Response.Write(JU.Build(false, "输入不完整，请完整输入当前密码和新密码后再试")); return; }
            
            UserEntity user = UserAccount.Current;
            if (user != null && user.password == MU.MD5(op))
            {
               string passwd = MU.MD5(np);
               if (UserData.ChangePassword(user.id, passwd) > 0)
               {
                  user.password = passwd;
                  //TODO:notify other server change
                  context.Response.Write(JU.AJAX_SUCCESS);
                  return;
               }
            }
            else { context.Response.Write(JU.Build(false, "当前密码错误，请重新输入当前使用的密码!")); return; }
         }

         context.Response.Write(JU.Build(false,"对不起，无法修改密码，请重新登录后再试！"));
      }

      public static void SaveRole(HttpContext context)
      {
         int id = CU.ToInt(context.Request.Form["id"]);
         string name = context.Request.Form["name"];
         string displayname = context.Request.Form["dn"];
         string description = context.Request.Form["desc"];
         string action = context.Request.Form["action"];


            RoleEntity entity = new RoleEntity() { 
               id=id,
               name=name,
               displayname=displayname,
               description=description
            };

            int ret;
            if (action == "edit") ret = RoleData.Update(entity);
            else if(action=="create") ret = RoleData.Create(entity);
      }

      /// <summary>
      ///  处理邀请码
      /// </summary>
      /// <param name="context"></param>
      public static void InviteCode(HttpContext context)
      {
         if (context.Request.IsAuthenticated)
         {
            UserStatusEntity entity = UserAccount.CurrentStatus;
            if (entity != null && entity.invited < entity.maxinvites)
            {
               IList<string> codes = UserAccount.GetInviteCode(entity.id);//获取可用的邀请码
               if (codes != null && codes.Count > 0)
               {
                  string ret = "[";
                  foreach (string code in codes)
                  {
                     if (ret != "[") ret += ",";
                     ret += string.Format("{{\"code\":\"{0}\"}}", code);
                  }
                  ret += "]";

                  context.Response.Write(JU.Build(true, ret));
                  context.Response.End();
               }
            }
         }
         context.Response.Write(JU.Build(false, "没有可用的邀请码!"));
         context.Response.End();
      }
   }
}
