using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light
{
   /// <summary>
   ///  JSON Utilities
   /// </summary>
   public sealed class JSON
   {
      public static string Array(string[] k, string[] v)
      {
         string json = "{";

         if (k.Length == v.Length)
         {
            int i = 0;
            for (; i < k.Length - 1; i++)
            {
               json += "\"" + k[i] + "\":\"" + v[i].Replace("\"", "\\\"") + "\",";
            }

            json += "\"" + k[i] + "\":\"" + v[i].Replace("\"", "\\\"") + "\"}";
         }

         return json;
      }
   }
}
