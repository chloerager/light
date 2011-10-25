using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace light
{
   /// <summary>
   ///  Defines some constants that used frequently data fields.
   /// </summary>
   public struct DF
   {
      /// <summary>
      /// 所有字段
      /// </summary>
      public const string ALL = "*";

      /// <summary>
      /// 对象标识
      /// </summary>
      public const string ID = "id";

      /// <summary>
      /// 对象名称
      /// </summary>
      public const string NAME = "name";

      /// <summary>
      /// 发布时间
      /// </summary>
      public const string PUBDATE = "pubdate";

      /// <summary>
      /// 最后编辑时间
      /// </summary>
      public const string EDITDATE = "editdate";

      /// <summary>
      /// 点击次数
      /// </summary>
      public const string VIEWS = "views";

      /// <summary>
      /// the comments field 's name
      /// </summary>
      public const string COMMENTS = "comments";

      /// <summary>
      ///  the state field's name
      /// </summary>
      public const string STATE = "state";

      /// <summary>
      ///  创建时间
      /// </summary>
      public const string CREATED = "created";

      /// <summary>
      ///  内容
      /// </summary>
      public const string STORY = "story";

      /// <summary>
      ///  标题
      /// </summary>
      public const string TITLE = "title";

      /// <summary>
      /// 帐户标识
      /// </summary>
      public const string ACCOUNTID = "account_id";
   }

   public struct UBP
   {
      public const string EXSIT = "sp_exsit";

      /// <summary>
      /// 获取分页数据的存储过程
      /// </summary>
      public const string PAGING = "sp_paging";
   }

   public struct OrderType
   {
      public const string DESC = "1";

      public const string ASC = "0";
   }
}
