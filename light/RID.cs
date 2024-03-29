﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light
{
   public sealed class RID
   {
      private static readonly char[] _rid = { 'w', 'l', '8', 'x', 'b', '6', 'z', 'j', '9', 'y', 'h', '0', 's', 'q', 'k', '2', 'i', 'd', 'c', '4', 't', 'o', 'a', '3', 'm', 'e', '5', 'p', 'f', '7', 'v', 'r', '1', 'n', 'u', 'g' };

      public static string ToRID(uint id)
      {
         if (id < 36) return _rid[id].ToString();
         else
         {
            string rid = string.Empty;
            uint r = id;
            while(r>=36)
            {
               uint m = r % 36;
               r = r / 36;
               rid = _rid[m] + rid;
            }

            rid = _rid[r] + rid;

            return rid;
         }
      }

      public static uint ToID(string rid)
      {
         uint id = 0;

         if (!string.IsNullOrEmpty(rid))
         {
            rid = rid.ToLower();
            int l = rid.Length;
            foreach (char a in rid)
            {
               if (char.IsLetterOrDigit(a))
               {
                  int idx = Array.IndexOf(_rid, a);

                  if (idx >= 0) { id += (uint)(idx * Math.Pow(36, --l)); }
                  else { --l; }
               }
               else { --l; }
            }
         }

         return id;
      }

      public static string ToSID(string rid)
      {
         return null;
      }
   }
}
