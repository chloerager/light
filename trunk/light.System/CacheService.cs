using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace light.System
{
   public sealed class CacheService
   {
      private readonly static Cache cache = HttpContext.Current.Cache;

      private CacheService() { }

      #region support-cache-framework

      public static string Get(string key)
      {
         return cache.Get(key) as string;
      }

      public static void Add(string key, string content)
      {
         cache.Add(key, content, null, DateTime.Now.AddMilliseconds(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
      }

      /// <summary>
      ///  Get the cache content associated with the specified key from the cache host.
      /// </summary>
      /// <param name="key">the string key of the cache content. </param>
      /// <param name="callback">a callback method to invoke when the cache content isn't exsit in cache host.</param>
      /// <returns>return cache content</returns>
      public static string Get(string key, Func<string> callback)
      {
         string content = cache.Get(key) as string;

         if (content == null)
         {
            content = callback();
            cache.Add(key, content, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
         }

         return content;
      }

      /// <summary>
      /// Get the cache content associated with the specified key from the cache host.
      /// </summary>
      /// <param name="key">the string key of the cache content. </param>
      /// <param name="count">number of generating cache content to pass as argument to the callback methdo.</param>
      /// <param name="callback">a callback method to invoke when the cache content isn't exsit in cache host.</param>
      /// <returns>return cache content</returns>
      public static string Get(string key, int count, Func<int, string> callback)
      {
         string content = cache.Get(key) as string;

         if (content == null)
         {
            content = callback(count);
            if (content != null) cache.Add(key, content, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
         }

         return content;
      }

      /// <summary>
      /// Get the cache content associated with the specified key from the cache host.
      /// </summary>
      /// <param name="key">the string key of the cache content. </param>
      /// <param name="count">number of generating cache content to pass as argument to the callback methdo.</param>
      /// <param name="callback">a callback method to invoke when the cache content isn't exsit in cache host.</param>
      /// <returns>return cache content</returns>
      public static string Get(string key, string code, Func<string, string> callback)
      {
         string content = cache.Get(key) as string;

         if (content == null)
         {
            content = callback(code);
            if (content != null) cache.Add(key, content, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
         }

         return content;
      }

      public static string Get(string key, int count, string code, Func<int, string, string> callback)
      {
         string content = cache.Get(key) as string;

         if (content == null)
         {
            content = callback(count, code);
            if (content != null) cache.Add(key, content, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
         }

         return content;
      }

      public static string Get<T>(string key, int count, Func<int, IList<T>> listCallback, Func<T, string> contentCallback)
      {
         string content = cache.Get(key) as string;
         if (content == null)
         {
            IList<T> list = listCallback(count);

            if (list != null && list.Count > 0)
            {
               foreach (T t in list)
               {
                  content += contentCallback(t);// string.Concat("&nbsp;<span style=\"font-family:宋体;\">·</span><a href=\"", o.URL, "\" title=\"诗词『", o.Name, "』的内容、释义及赏析\">《", o.Name, "》- ", o.Author, "</a><br/>");
               }
            }

            if (content != null) cache.Add(key, content, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
         }

         return content;
      }

      #endregion
   }
}


