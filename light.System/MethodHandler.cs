using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.Security;
using light.System;
namespace light.System
{
   public class MethodHandler : IHttpHandler
   {
      protected HttpContext Context;
      //protected IDictionary<string, RequestHandleCallback> rhDict;
      //public MethodHandler() 
      //{
      //}

      //#region AJAX_ACCOUNT
      //private void Signup(HttpContext context)
      //{
      //   //获取数据
      //   Account m = new Account();
      //   m.RID = Role.NORMAL.ToString();
      //   m.Name = Context.Request.Form["oname"];
      //   m.NickName = Context.Request.Form["nickname"]; ;
      //   m.Password = Context.Request.Form["pwd"];

      //   //写入数据库
      //   m = Account.Signup(m);
      //   if (m.OID > 0)
      //   {
      //      //注册成功。

      //      //发送激活邮件

      //      //写入认证信息
      //      string authCookie = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, m.Name, DateTime.Now, DateTime.Now.AddDays(1), true, m.Name, FormsAuthentication.FormsCookiePath));
      //      QA.SetCookie(FormsAuthentication.FormsCookieName, authCookie, DateTime.Now.AddDays(1));

      //      QA.SetCookie(SC.MEMBER_NICKNAME, m.NickName, DateTime.MaxValue);
      //      //定义JSON返回对象。
      //      Script("$(\\\"#ajaxview\\\").html(\\\"您已成功注册!点击『<a href=\\\\\\\"/my/\\\\\\\">进入</a>』你的个人空间\\\")");
      //   }
      //   else Failed("注册失败，请检查你的注册信息或者联系管理员!");
      //}

      //private void Login(HttpContext context)
      //{
      //   string oname = Context.Request.Form["oname"];
      //   string password = Context.Request.Form["pwd"];

      //   //写入数据库
      //   Account m = Account.Login(oname, password);
      //   if (m != null)
      //   {
      //      string authCookie = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, m.Name, DateTime.Now, DateTime.Now.AddDays(1), true, m.Name, FormsAuthentication.FormsCookiePath));
      //      QA.SetCookie(FormsAuthentication.FormsCookieName, authCookie, DateTime.Now.AddDays(1));

      //      QA.SetCookie(SC.MEMBER_NICKNAME, HttpUtility.UrlEncode(m.NickName), DateTime.MaxValue);
      //      string from = QA.GetCookie(SC.CK_LOGIN_FROM);
      //      if (string.IsNullOrEmpty(from)) Success("登录成功!正在进入个人桌面", "/my/");
      //      else
      //      {
      //         QA.ClearCookie(SC.CK_LOGIN_FROM);
      //         Success("登录成功!正在进入个人桌面", from);
      //      }
      //   }
      //   else Failed("帐号和密码不匹配，请重新输入再尝试！");
      //}
      //#endregion

      //private void CheckNickName(HttpContext context)
      //{
      //   string nickname = Context.Request.QueryString["nickname"];
      //   Context.Response.Write(Account.IsExistNickName(nickname));
      //}

      //private void CheckName(HttpContext context)
      //{
      //   string oname = Context.Request.QueryString["oname"];
      //   Context.Response.Write(Account.IsExsitAccount(oname));
      //}

      //private void ChangePassword(HttpContext context)
      //{
      //   string oldpwd = Context.Request.Form["oldpwd"];
      //   string newpwd = Context.Request.Form["pwd"];

      //   if (!string.IsNullOrEmpty(oldpwd) && !string.IsNullOrEmpty(newpwd))
      //   {
      //      Account m = Account.Read(Context.User.Identity.Name);
      //      if (m != null)
      //      {
      //         if (Account.ChangePassword(m,oldpwd, newpwd)) { Success("密码更换成功!"); return; }
      //      }
      //   }

      //   Failed("密码更换失败，请检测旧密码输入是否正确!");
      //}

      //private void SaveSpecial(HttpContext context)
      //{
      //   int success = 0;
      //   string oid = Context.Request.Form["oid"];
      //   string title = Context.Request.Form["title"];
      //   string titleAlias = Context.Request.Form["title_alias"];
      //   string keywords = Context.Request.Form["keywords"];
      //   string description = Context.Request.Form["description"];
      //   string story = Context.Request.Form["story"];

      //   if (!(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(story)))
      //   {
      //      Account m = Account.Read(Context.User.Identity.Name);
      //      if (!string.IsNullOrEmpty(oid)) //edit
      //      {
      //         if (Special.Update(oid,"0",title,titleAlias,keywords,description,story) >= 1)
      //         {
      //            success = 1;
      //         }
      //      }
      //      else
      //      {
      //         lock (this)
      //         {
      //            int jid = Special.Insert(m.OID,"0",title,titleAlias,keywords,description,story);
      //            if (jid > 0) success = 1;
      //         }
      //      }
      //   }

      //   Context.Response.Write(JSON(success));
      //}

      //#region JSON_HELPER

      //protected string JSON(int success)
      //{
      //   return string.Format("var result = {{\"success\":\"{0}\",\"message\":\"\"}}", success);
      //}

      //protected string JSON(int success, string message)
      //{
      //   message = message.Replace("\"", "\\\"");
      //   return string.Format("var result = {{\"success\":\"{0}\",\"message\":\"{1}\"}}", success, message);
      //}

      //protected void Success()
      //{
      //   Context.Response.Write("var result={\"success\":\"1\"}");
      //}

      //protected void Success(string message)
      //{
      //   Context.Response.Write("var result={\"success\":\"1\",\"message\":\"" + message + "\"}");
      //}
      //protected void Script(string script)
      //{ 
      //   script = "<script type=\\\"text/javascript\\\">" + script + "</script>";
      //   Context.Response.Write("var result={\"success\":\"1\",\"message\":\"" + script + "\"}");
      //}

      //protected void Success(string message, string rdURL)
      //{
      //   message += "<script type=\\\"text/javascript\\\">window.location.assign(unescape(\\\"" + rdURL + "\\\"));</script>";
      //   Context.Response.Write("var result={\"success\":\"1\",\"message\":\"" + message + "\"}");
      //}

      //protected void Failed()
      //{
      //   Context.Response.Write("var result={\"success\":\"0\"}");
      //}

      //protected void Failed(string message)
      //{
      //   Context.Response.Write("var result={\"success\":\"0\",\"message\":\"" + message + "\"}");
      //}

      //#endregion

      public bool IsReusable
      {
         get { return true; }
      }

      public void ProcessRequest(HttpContext context)
      {
         Context = context;

         //string cmd = Context.Request.QueryString["cmd"];

         //if (string.IsNullOrEmpty(cmd))
         //{
         //   //TODO:写入LOG
         //   Context.Response.Write("参数错误");
         //}
         //else
         //{
         //   if (rhDict.ContainsKey(cmd)) rhDict[cmd](context);
         //}
      }

      protected delegate void RequestHandleCallback(HttpContext context);
   }
}
