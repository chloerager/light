using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace light
{
	/// <summary>
	/// Convert Utilities
	/// </summary>
	public class CU
   {
      /// <summary>
      /// Convert the value of the specified object to  a 32-bit signed integer.
      /// </summary>
      /// <param name="value">An object to convert.</param>
      /// <returns>A 32-bit signed integer equivalent to value, or 0 if value is null or DBNull.</returns>
      public static int ToInt(object value)
      {
         if (value is DBNull) return 0;
         return Convert.ToInt32(value);
      }

      /// <summary>
      ///  Convert the value of the speicified string representation of a number to an equivalent 32-bit signed integer.
      /// </summary>
      /// <param name="value">A string that contains the number to convert.</param>
      /// <returns>A 32-bit signed integer that is equivalent to the number in value, or 0 if value is null.</returns>
      public static int ToInt(string value)
      {
         if (string.IsNullOrEmpty(value)) return 0;
         int ret = 0;
         int.TryParse(value, out ret);
         return ret;
      }

      /// <summary>
      ///  
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static byte ToByte(string value)
      {
         if (string.IsNullOrEmpty(value)) return 0;
         byte ret = 0;
         byte.TryParse(value, out ret);
         return ret;
      }

      /// <summary>
      /// Convert the value of the speicified object to its equivalent string representation.
      /// </summary>
      /// <param name="value">An object that supplies the value to convert, or null</param>
      /// <returns>The string representation of value, or null if value is null or DBNull or String.Empty.</returns>
		public static string ToStr(object value)
		{
         if (value is DBNull) return null;
         if (value == null) return null;
         return value.ToString();
		}

      public static string Base64(string src)
      {
         byte[] bsrc = Encoding.Unicode.GetBytes(src);
         return Convert.ToBase64String(bsrc);
      }
   }
}
