using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace light
{
   public class URLite
   {
      private string cookieHeader = string.Empty;

      public URLite(){}

      public string Login(string url,byte[] data)
      {
         HttpWebResponse res = null;
         string result = "";
         try
         {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.AllowAutoRedirect= true;
            req.KeepAlive = true;
            CookieContainer cookieCon = new CookieContainer();
            req.CookieContainer = cookieCon;
            req.ContentLength = data.Length;
            Stream newStream = req.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            res = (HttpWebResponse)req.GetResponse();
            cookieHeader = req.CookieContainer.GetCookieHeader(new Uri(url));
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream);
            result = sr.ReadToEnd();
         }
         catch (Exception e)
         {
            result = e.ToString();
         }
         finally
         {
            if (res != null) res.Close();
         }

         return result;
      }

      public string Get(string url,Encoding encode)
      {
         HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
         req.Referer = "http://www.kooioo.com/";
         req.Method = "GET";
         req.KeepAlive = true;
         req.ContentType = "text/html";
         if(!string.IsNullOrEmpty(cookieHeader)) req.Headers.Add("Cookie", cookieHeader);

         HttpWebResponse res = (HttpWebResponse)req.GetResponse();
         StreamReader sr = new StreamReader(res.GetResponseStream(), encode);
         string strResult = sr.ReadToEnd();
         sr.Close();
         return strResult;
      }

      public void Post(string url, byte[] data)
      {
         HttpWebResponse res = null;
         string result = "";
         try
         {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.AllowAutoRedirect = true;
            req.KeepAlive = true;
            if (!string.IsNullOrEmpty(cookieHeader)) req.Headers.Add("Cookie", cookieHeader);
            req.ContentLength = data.Length;
            Stream newStream = req.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            res = (HttpWebResponse)req.GetResponse();
            cookieHeader = req.CookieContainer.GetCookieHeader(new Uri(url));
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream);
            result = sr.ReadToEnd();
         }
         catch (Exception e)
         {
            result = e.ToString();
         }
         finally
         {
            if (res != null) res.Close();
         }
      }

      public bool NoLogin
      {
         get
         {
            if (string.IsNullOrEmpty(cookieHeader)) return true;
            else return false;
         }
      }
   }
}
