using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.System.Data;

namespace light.System.Controller
{
   public class Keyword
   {
      private const string RESERVE_KEYWORD = "RESERVE_KEYWORD";
      public static bool Reserve(string word)
      {
         if (word.Length < 3) return true;
         HashSet<string> h = null;
         if (CacheService.Contains("RESERVE_KEYWORD")) h = CacheService.Get(RESERVE_KEYWORD) as HashSet<string>;
         else { h = KeywordData.GetReserve(); CacheService.Add(RESERVE_KEYWORD, h); }

         if (h != null) return h.Contains(word);
         return false;
      }
   }
}
