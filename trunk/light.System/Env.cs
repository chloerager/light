using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light
{
   /// <summary>
   ///  系统环境相关操作
   /// </summary>
   public sealed class Env
   {
      /// <summary>
      ///  获取用户目录起始位置，按用户id进行分区
      /// </summary>
      /// <param name="id">用户标识</param>
      /// <returns>返回对应的用户目录名称</returns>
      public static string GetUserDirStart(int id)
      {
         if (id < 1000) return "bless";
         else if (id < 2000) return "nice";
         else if (id < 3000) return "great";
         else return "atlantis";
      }

      /// <summary>
      ///  获取用户目录的全路径
      /// </summary>
      /// <param name="id"></param>
      /// <param name="userdir"></param>
      /// <param name="url"></param>
      /// <returns></returns>
      public static string GetFullPath(int id, string userdir, bool url)
      {
         if (url) return "/s/u/" + GetUserDirStart(id) + "/" + userdir + "/";
         else return "\\s\\u\\" + GetUserDirStart(id) + "\\" + userdir + "\\";
      }
   }
}
