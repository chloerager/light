using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace light
{
   /// <summary>
   /// 获取网站的PageRank和AlexaRank
   /// </summary>
   public sealed class SiteRank
   {
      #region Get PageRank

      /// <summary>
      /// 定义常量0xE6359A60=3862272608
      /// </summary>
      private const long GOOGLE_MAGIC = 0xE6359A60;
      private const string PAGE_RANK_URL_FORMAT = "http://www.google.cn/search?client=navclient-auto&ch={0}&features=Rank&q=info:{1}";

      /// <summary>
      /// 获取PageRank
      /// </summary>
      /// <param name="Url">要获取PR的url地址</param>
      /// <returns>如果url无效或者获取失败返回0，正常返回url对应的PR值</returns>
      public static int GetPageRank(string url)
      {
         int rank = 0;

         if (string.IsNullOrEmpty(url)) return 0;
         if (url.Split('.').Length <= 2 && !url.StartsWith("www")) url = "www." + url; //check url
         string ch = "6" + GoogleCH(StrToAsc("info:" + url), -1, -1).ToString();
         using (WebClient wc = new WebClient())
         {
            try
            {
               string str = wc.DownloadString(string.Format(PAGE_RANK_URL_FORMAT,ch, url));
               string[] rs = str.Split(':');
               int.TryParse(rs[rs.Length - 1], out rank);
            }
            catch { }
         }

         return rank;
      }

      /// <summary>
      /// 将字符串转换为对应的ascii值数组
      /// </summary>
      /// <param name="str">字符串</param>
      /// <returns></returns>
      private static int[] StrToAsc(string str)
      {
         int l = str.Length;
         int[] r = new int[str.Length];
         for (int i = 0; i < l; i++) r[i] = (int)str[i];
         return r;
      }

      private static long GoogleCH(int[] url, int length, long init)
      {
         if (length == -1) length = url.Length;
         if (init == -1) init = GOOGLE_MAGIC;
         long a, b, c = init;
         int[] mix;
         a = b = 0x9E3779B9;
         int len = length, k = 0;
         while (len >= 12)
         {
            a += url[k + 0] + (url[k + 1] << 8) + (url[k + 2] << 16) + (url[k + 3] << 24);
            b += url[k + 4] + (url[k + 5] << 8) + (url[k + 6] << 16) + (url[k + 7] << 24);
            c += url[k + 8] + (url[k + 9] << 8) + (url[k + 10] << 16) + (url[k + 11] << 24);
            mix = Mix(a, b, c);
            a = mix[0]; b = mix[1]; c = mix[2];
            k += 12;
            len -= 12;
         }
         c += length;
         if (len == 11) c += url[k + 10] << 24;
         if (len >= 10) c += url[k + 9] << 16;
         if (len >= 9) c += url[k + 8] << 8;
         if (len >= 8) b += url[k + 7] << 24;
         if (len >= 7) b += url[k + 6] << 16;
         if (len >= 6) b += url[k + 5] << 8;
         if (len >= 5) b += url[k + 4];
         if (len >= 4) a += url[k + 3] << 24;
         if (len >= 3) a += url[k + 2] << 16;
         if (len >= 2) a += url[k + 1] << 8;
         if (len >= 1) a += url[k + 0];
         mix = Mix(a, b, c);
         return mix[2];
      }

      /// <summary>
      /// 返回对a，b，c进行相关操作后的数组
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="c"></param>
      /// <returns></returns>
      private static int[] Mix(long a, long b, long c)
      {
         a -= b; a -= c; a ^= ZeroFill(c, 13);
         b -= c; b -= a; b ^= a << 8;
         c -= a; c -= b; c ^= ZeroFill(b, 13);
         a -= b; a -= c; a ^= ZeroFill(c, 12);
         b -= c; b -= a; b ^= a << 16;
         c -= a; c -= b; c ^= ZeroFill(b, 5);
         a -= b; a -= c; a ^= ZeroFill(c, 3);
         b -= c; b -= a; b ^= a << 10;
         c -= a; c -= b; c ^= ZeroFill(b, 15);
         return new int[] { (int)a, (int)b, (int)c };
      }
      
      /// <summary>
      /// 与，移位，非，或操作
      /// </summary>
      /// <param name="a">要操作的数字</param>
      /// <param name="b"></param>
      /// <returns></returns>
      private static long ZeroFill(long a, int b)
      {
         //注意右移动时要把a，b强制转换为int类型，否则会溢出。
         long z = 0x80000000;/*=2147483648*/
         if ((z & a) != 0)//先和0x80000000进行与操作判断是否为0
         {
            a = a >> 1;
            a &= ~z;
            a |= 0x40000000;
            a = (int)a >> (int)(b - 1);
         }
         else a = (int)a >> (int)b;
         return a;
      }

      #endregion 

      #region Get AlexaRank
      private const string ALEXA_RANK_URL_FORMAT = "http://data.alexa.com/data/?cli=10&dat=snba&ver=7.0&url={0}";

      /// <summary>
      /// 获取Alexa Rank
      /// </summary>
      /// <param name="host"></param>
      /// <returns></returns>
      public static int GetAlexaRank(string host)
      {
         int rank = 0;
         try
         {
            XmlDocument xml = new XmlDocument();
            xml.Load(string.Format(ALEXA_RANK_URL_FORMAT, host));
            XmlNode node = xml.SelectSingleNode("/ALEXA/SD/POPULARITY");
            if (node != null && node.Attributes["TEXT"] != null) int.TryParse(node.Attributes["TEXT"].Value,out rank);
         }
         catch{}
         return rank;
      }
      #endregion
   }
}
