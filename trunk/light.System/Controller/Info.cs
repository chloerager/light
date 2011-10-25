using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.System.Entities;

namespace light.System.Controller
{
    /// <summary>
    ///  系统信息控制器，获取和设置系统配置项、系统统计信息
    /// </summary>
    public class Info
    {
        /// <summary>
        ///  获取指定的统计信息，如果不存在已统计的数据或者已统计的数据过期了，调用Count方法重新进行统计
        /// </summary>
        /// <param name="key">key对应一个被统计项</param>
        /// <param name="Count">统计方法</param>
        /// <returns>返回统计结果</returns>
        public static int GetStatCount(string key, Func<int> Count)
        {
            int total = 0;
            //if (CacheService.Contains(key))
            //    total = CacheService.Get<int>(key);

            //if (total <= 0)
            //{
            //    StatInfoEntity entity = SysInfo.GetStatInfo(key);

            //    if (entity != null && (DateTime.Now.Date - entity.StatDate.Date).Days <= entity.Expiration) total = entity.Value;
            //    else
            //    {
            //        total = Count();
            //        if (total > 0) SysInfo.SetStatInfo(key, total, DateTime.Now);
            //    }

            //    if (total > 0)  CacheService.Add(key, total, DateTimeOffset.Now);
            //}

            return total;
        }

        public static string GetHtml(string key, Func<string> Html, int expiration)
        {
            string html = null;
            //if (CacheService.Contains(key))
            //    html = CacheService.Get<string>(key);

            //if (string.IsNullOrEmpty(html))
            //{
            //    html = Html();
            //    if (!string.IsNullOrEmpty(html)) CacheService.Add(key, html, TimeSpan.FromMinutes(expiration));
            //}

            return html;
        }
    }
}
