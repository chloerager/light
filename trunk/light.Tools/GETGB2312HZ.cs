using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace light.Tools
{
   public class GETGB2312HZ
   {
      public void Parse()
      {
         string file = @"E:\研究\网络\软件\本地\light\light.Tools\GB2312.txt";
        string file2 = @"E:\研究\网络\软件\本地\light\light.Tools\GB2.txt";

        IDictionary<string, KeyValuePair<string, string>> lt = new Dictionary<string,KeyValuePair<string,string>>();
        using (StreamReader sr2 = new StreamReader(file2))
        {
            while (sr2.Peek()>0)
            {
                string line = sr2.ReadLine();
                string[] lll = line.Split(' ');
                lt.Add(lll[2], new KeyValuePair<string, string>(lll[0], lll[1]));
            }
        }


        StringBuilder sb = new StringBuilder();
        int length = 0;
        int length2 = 0;
        HashSet<string> uuid = new HashSet<string>();
        string output = null;

         using (StreamReader sr = new StreamReader(file))
         {
            string txt = sr.ReadToEnd();

            txt = txt.Replace("　 \r\n", "　 ");
             txt = txt.Replace("　 ", "|");
            Regex reg = new Regex("<td bgcolor=\"#dfdfdf\">([A-Z]+) <a name=\"([A-Z]+)\"></a></td>\r\n\r\n\r\n\r\n(.+)</td>");
            Match m = reg.Match(txt);
            while (m.Success)
            {
                string k1=null, k2=null;
                string k = m.Groups[3].Value;

                string[] st = k.Split('|');

                foreach (string z in st)
                {
                    if (z.Trim() != string.Empty)
                    {
                        string[] y = z.Split(' ');
                        k1 += y[0];
                        k2 += y[1];
                        if (!uuid.Contains(y[1]))
                        {
                            uuid.Add(y[1]);
                        }
                        else
                        {
                            //
                        }
                        length2++;
                    }
                }

                if (lt.ContainsKey(m.Groups[1].Value))
                {
                    k1 += lt[m.Groups[1].Value].Value;
                    k2 += lt[m.Groups[1].Value].Key;
                    length2++;
                }


                sb.Append("new string[]{\"" + m.Groups[1].Value + "\",\"" + k1 + "\"},\r\n");
                length += k1.Length;

                m = m.NextMatch();
            }

            //int c = uuid.Count;

            //for (int i = 16; i <= 87; i++)
            //{
            //    for (int j = 1; j <= 94; j++)
            //    {
            //        int x = i*100+j;
            //        if (!uuid.Contains(x.ToString()))
            //        {
            //            byte[] zi = new byte[] { (byte)(i + 0xa0), (byte)(j + 0xa0)};
            //            output += x + " " + Encoding.GetEncoding("gb2312").GetString(zi) + "\r\n";
            //            c++;
            //        }
            //    }
            //}

            using (StreamWriter sw = new StreamWriter(@"c:\2.txt"))
            {
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
         }
      }
   }
}
