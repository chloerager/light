using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace light
{
   public class MU
   {
      private static Random random = new Random((int)DateTime.Now.Ticks);
      public static int Next(int minValue,int maxValue)
      {
         lock (random)
         {
            return random.Next(minValue,maxValue);
         }
      }

      /// <summary>
      /// 使用MD5加密算法加密字符串
      /// </summary>
      public static string MD5(string source)
      {
         return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
      }
   }

   public class RecentList<T> : List<T>
   {
      private string _format = null;
      private int _max;
      private string recent = null;

      public RecentList(string format,int max)
      {
         _format = format;
         _max = max;
      }

      
      public new void Add(T key)
      {
         if(this.Count >0 && this.Contains(key)) return;
         if (this.Count >= _max)
         {
            this.RemoveAt(_max - 1);
            recent = null;
            for (int i = 0; i < this.Count; i++) //使用for避免foreach异步变更
            {
               recent += string.Format(_format, this[i]);
            }
         }
         else
         {
            recent = string.Format(_format, key) + recent;
         }
         
         this.Insert(0,key);
      }

      public override string ToString()
      {
         if (string.IsNullOrEmpty(recent))
         {
            if (this.Count > 0)
            {
               for(int i=0;i<this.Count;i++) //使用for避免foreach异步变更
               {
                  recent += string.Format(_format, this[i]);
               }

               return recent;
            }
            else return string.Empty;
         }
         else
         { 
            return recent;
         }
      }

      public string ToMiniString()
      {
         string mini = null;
         if (this.Count > 0)
         {
            int length = 0;
            for (int i = 0; i < this.Count; i++)
            {
               mini += string.Format(_format, this[i]);
               length += this[i].ToString().Length;
               if ((i + 1) < this.Count && (length += this[i + 1].ToString().Length + 1) > 40) break;
            }
         }

         return mini;
      }
   }

   public struct KeyValueItem<TKey, TValue>
   {
      public TKey Key;
      public TValue Value;

      public KeyValueItem(TKey key, TValue value)
      {
         this.Key = key;
         this.Value = value;
      }

      public override string ToString()
      {
         return Key.ToString();
      }
   }
}
