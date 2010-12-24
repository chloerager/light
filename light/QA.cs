using System;
using System.Web;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;

namespace light
{
   /// <summary>
   /// Provide methods to lightect or quick access the frequent object and data.
   /// </summary>
   public sealed class QA
   {
      /// <summary>
      /// read the config info from appSettings section in the web.config.
      /// </summary>
      /// <param name="configKey">specified a key in the config section to read.</param>
      /// <param name="defaultValue">specified a value as default,if read failed or the value not exist.</param>
      /// <returns>if succeed,a string in the config section that the configKey specified,or the default value</returns>
      public static string GetConfig(string configKey, string defaultValue=null)
      {
         string configValue = ConfigurationManager.AppSettings[configKey] as string;

         if (configValue != null) return configValue;

         return defaultValue;
      }

      /// <summary>
      /// read the config info from web.config.
      /// </summary>
      /// <param name="configSection">specify a config section to read.</param>
      /// <param name="configKey">specified a key in the config section to read.</param>
      /// <param name="defaultValue">specified a value as default, if read failed or the value not exist.</param>
      /// <returns>if succeed,a string in the config section that the configKey specified,or the default value.</returns>
      public static string GetConfig(string configSection, string configKey, string defaultValue=null)
      {
         // gets the specified configSection 's content and transform to NameValueCollection object.
         NameValueCollection configSettings = ConfigurationManager.GetSection(configSection) as NameValueCollection;

         if (configSettings != null)
         {
            string configValue = configSettings[configKey] as string; //read the value.

            if (configValue != null)
            {
               return configValue;
            }
         }

         return defaultValue;
      }

      /// <summary>
      /// store a cookie to client.
      /// </summary>
      /// <param name="name">cookie 's key</param>
      /// <param name="value"></param>
      /// <param name="expire"></param>
      public static void SetCookie(string name, string value, DateTime expire)
      {
         HttpCookie cookie = new HttpCookie(name, value);
         cookie.Expires = expire;

         HttpContext.Current.Response.SetCookie(cookie);
      }

      public static void ClearCookie(string name)
      {
         HttpCookie cookie = new HttpCookie(name, null);
         cookie.Expires = DateTime.Now.AddYears(-10);
         HttpContext.Current.Response.SetCookie(cookie);
      }

      /// <summary>
      /// gets the value of Cookie that the name speified.
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static string GetCookie(string name)
      {
         HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

         if (cookie != null)
         {
            return cookie.Value;
         }

         return null;
      }

       /// <summary>
      /// 获取主数据库连接串
      /// </summary>
      public static string DBCS_MAIN
      {
         get
         {
            //ip connection string "Data Source=218.29.115.156,1433;Network Library=DBMSSOCN;Initial Catalog=db_name;User ID=sa;Password=XXX;"
            //integrated connection "Data Source=.;Initial Catalog=db_name;Integrated Security=SSPI;Connect Timeout=30;Pooling=true"
            return QA.GetConfig("DBCS_MAIN", "");
         }
      }

      /// <summary>
      /// 返回客户端的IP地址
      /// </summary>
      public static string ClientIP
      {
         get
         {
            string clientip = null;
            try
            {
               clientip = HttpContext.Current.Request.UserHostAddress;
            }
            catch { }

            return clientip;
         }
      }
   }
}
