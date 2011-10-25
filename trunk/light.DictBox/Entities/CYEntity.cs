using light;
using light.Data;
using System;
namespace light.DictBox
{
   public class CYEntity
   {
      [Field("id",true,true,true)]
      public int id;

      [Field("uid")]
      public int uid;

      [Field("status")]
      public int status;

      [Field("created",true)]
      public DateTime created;

      [Field("url")]
      public string url;

      /// <summary>
      /// 拼音缩写
      /// </summary>
      [Field("pinyinabbr")]
      public string pinyinabbr;

      [Field("name")]
      public string name;

      /// <summary>
      /// 对应拼音
      /// </summary>
      [Field("pinyin")]
      public string pinyin;
      
      /// <summary>
      /// 释义
      /// </summary>
      [Field("mean",allowNulls:true)]
      public string mean;

      /// <summary>
      /// 出处
      /// </summary>
      [Field("derivation",allowNulls:true)]
      public string derivation;

      /// <summary>
      /// 示例
      /// </summary>
      [Field("sample",allowNulls:true)]
      public string sample;

      /// <summary>
      /// 同义词
      /// </summary>
      [Field("synonym",allowNulls:true)]
      public string synonym;

      /// <summary>
      /// 反义词
      /// </summary>
      [Field("antonym",allowNulls:true)]
      public string antonym;

      /// <summary>
      ///  纠正
      /// </summary>
      [Field("correct",allowNulls:true)]
      public string correct;

      /// <summary>
      ///  用法
      /// </summary>
      [Field("usages",allowNulls:true)]
      public string usages;

      [Field("difference", allowNulls: true)]
      public string difference;

      /// <summary>
      /// 英语中的成语谚语
      /// </summary>
      [Field("english", allowNulls: true)]
      public string english;

      /// <summary>
      /// 成语谜语
      /// </summary>
      [Field("riddle",allowNulls:true)]
      public string riddle;

      /// <summary>
      /// 歇后语
      /// </summary>
      [Field("xhy",allowNulls:true)]
      public string xhy;

      /// <summary>
      /// 成语趣解
      /// </summary>
      [Field("jokemean",allowNulls:true)]
      public string jokemean;

      /// <summary>
      ///  成语故事
      /// </summary>
      [Field("story",allowNulls:true)]
      public string story;

      #region EXT

      /// <summary>
      /// 持久链接
      /// </summary>
      public string URL
      {
         get{return string.Concat("/dict/chengyu/", pinyinabbr, "-", id, "/");}
      }

      /// <summary>
      /// 4字成语链接
      /// </summary>
      public string ShortLink
      {
         get
         {
            return string.Concat("<a href=\"/dict/chengyu/", pinyinabbr, "-", id, "/", "\" title=\"成语", name, "的详细释义及成语故事\">", SU.Cut(name, 4), "</a>");
         }
      }

      public string EditLink
      {
         get
         {
            if (QA.IsAuthenticated) return "<a href=\"/my/dict/chengyu/edit.aspx?oid=" + this.id + "\" target=\"_blank\">[编辑]</a>";
            return string.Empty;
         }
      }

      #endregion

   }
}

