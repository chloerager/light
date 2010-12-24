using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace light
{
   /// <summary>
   ///  Simple Pattern Match.
   /// </summary>
   public class SPM
   {
      private ICollection<string> hashKeyWords = null;
      private int min;
      private int max;

      public void Initialize(string[] keywords)
      {
         hashKeyWords = new HashSet<string>(keywords);
         min = hashKeyWords.Min<string>(s => s.Length);
         max = hashKeyWords.Max<string>(s => s.Length);
      }
      public void Initialize(ICollection<string> iCollection)
      {
         hashKeyWords = iCollection;
         min = hashKeyWords.Min<string>(s => s.Length);
         max = hashKeyWords.Max<string>(s => s.Length);
      }

      /// <summary>
      /// 最小模式匹配关键词
      /// </summary>
      /// <param name="target">目标串</param>
      /// <param name="mutex">是否多重匹配</param>
      /// <param name="replace">替换方法</param>
      /// <returns>返回匹配并替换后的字符串</returns>
      public string SearchMin(string target, bool mutex, Func<string,string> replace)
      {
         string output = string.Empty;
         bool isMatch = false;

         int i = 0;
         for (i = 0; i < target.Length - min; i++)
         {
            for (int k = min; k <= max && (i + k) <= target.Length; k++)
            {
               string key = target.Substring(i, k);
               if (hashKeyWords.Contains(key))
               {
                  if (mutex)
                  {
                     i = i + k - 1; //减去自动增加的
                     isMatch = true;
                     output += "[" + replace(key) + "]";
                     break;
                  }
               }
            }
            if (!isMatch)
            {
               output += target[i].ToString();
            }
            isMatch = false;
         }
         if (i < target.Length) output += target.Substring(i);


         return output;
      }

      /// <summary>
      /// 最大模式匹配关键词
      /// </summary>
      /// <param name="target">目标串</param>
      /// <param name="mutex">是否多重匹配</param>
      /// <param name="replace">替换方法</param>
      /// <returns>返回匹配并替换后的字符串</returns>
      public string SearchMax(string target, bool mutex, Func<string, string> replace)
      {
         string output = string.Empty;
         bool isMatch = false;

         int i = 0;
         for (i = 0; i < target.Length - min; i++)
         {
            int k = (max + i) > target.Length ? (target.Length - i) : max;
            for (; k >= min; k--)
            {
               string key = target.Substring(i, k);
               if (hashKeyWords.Contains(key))
               {
                  if (mutex)
                  {
                     i = i + k - 1; //减去自动增加的
                     isMatch = true;
                     output += replace(key);
                     break;
                  }
               }
            }

            if (!isMatch)
            {
               output += target[i].ToString();
            }
            isMatch = false;
         }
         if (i < target.Length) output += target.Substring(i);


         return output;
      }

      public string SearchMax(string target, bool mutex, Func<string, string> replace, IDictionary<string, string> keys)
      {
         string output = string.Empty;
         bool isMatch = false;

         int i = 0;
         for (i = 0; i < target.Length - min; i++)
         {
            int k = (max + i) > target.Length ? (target.Length - i) : max;
            for (; k >= min; k--)
            {
               string key = target.Substring(i, k);
               if (hashKeyWords.Contains(key))
               {
                  if (mutex)
                  {
                     i = i + k - 1; //减去自动增加的
                     isMatch = true;

                     if (!keys.Keys.Contains(key)) keys.Add(key, replace(key));
                     output += keys[key];
                     break;
                  }
               }
            }

            if (!isMatch)
            {
               output += target[i].ToString();
            }
            isMatch = false;
         }
         if (i < target.Length) output += target.Substring(i);


         return output;
      }
   }
}
