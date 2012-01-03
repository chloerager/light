namespace light
{
   /// <summary>
   ///  系统常量
   /// </summary>
   public struct SC
   {
      /// <summary>
      /// 时间格式
      /// </summary>
      public const string FM_DATETIME = "yyyy.MM.dd HH:mm";

      /// <summary>
      /// 年
      /// </summary>
      public const string FM_DT_YEAR = "yyyy";

      /// <summary>
      /// 月
      /// </summary>
      public const string FM_DT_MONTH = "MM";

      /// <summary>
      /// 日
      /// </summary>
      public const string FM_DT_DAY = "dd";

      /// <summary>
      /// 每页最多显示10条信息
      /// </summary>
      public const string PAGER = "10";

      /// <summary>
      /// 每页最多显示10条信息
      /// </summary>
      public const int PAGE_SIZE = 10;

      /// <summary>
      /// 默认个人主页
      /// </summary>
      public const string WWW = "http://www.azmo.cn";

      /// <summary>
      ///  Cookie KEY常量，存储登录来源
      /// </summary>
      public const string CK_LOGIN_FROM = "login_from";

      /// <summary>
      /// 中文月
      /// </summary>
      public readonly static string[] MONTH_CN = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二"};

      /// <summary>
      /// 字符串布尔true
      /// </summary>
      public const string STR_TRUE = "1";

      /// <summary>
      /// 字符串布尔false
      /// </summary>
      public const string STR_FALSE = "0";

      public struct Sex
      {
         /// <summary>
         /// 男性
         /// </summary>
         public const int MALE = 1;

         /// <summary>
         /// 女性
         /// </summary>
         public const int FEMALE = 0;

         /// <summary>
         /// 无性别的人
         /// </summary>
         public const int UNKOWN = 2;
      }

      public struct IncField
      {
         public const string VIEWS = "views";
         public const string DING = "ding";
         public const string MARS = "mars";
         public const string COMMENTS = "comments";
      }

      /// <summary>
      /// 实体的状态
      /// </summary>
      public struct EntityState
      {

         /// <summary>
         /// 已发布
         /// </summary>
         public const string Published = "0";

         /// <summary>
         /// 审批中
         /// </summary>
         public const string Apply = "1";

         /// <summary>
         /// 草稿
         /// </summary>
         public const string Draft = "2";

         /// <summary>
         /// 垃圾
         /// </summary>
         public const string Trash = "3";

         /// <summary>
         /// 系统垃圾
         /// </summary>
         public const string SysTrash = "4";

         public readonly static string[] States = new string[] { "已发布", "审盒中", "草稿", "垃圾箱", "系统垃圾" };
      }

      /// <summary>
      ///  Error Info
      /// </summary>
      public struct EI
      {
         public const string ENTITY_NOT_EXIST = "对不起，文件已被删除或变更位置！";
      }

      /// <summary>
      ///  Cookie Name
      /// </summary>
      /// <remarks>部分名字使用反转方式进行混淆</remarks>
      public struct CN
      {
         /// <summary>
         ///  账户名称
         /// </summary>
         public const string A_NAME = "a_eman";

         /// <summary>
         ///  帐号标识
         /// </summary>
         public const string A_ID = "a_di";

         /// <summary>
         ///  来源地址
         /// </summary>
         public const string FROM = "f";
      }

      /// <summary>
      ///  URL KEY
      /// </summary>
      public struct UK
      {
         /// <summary>
         ///  来源地址
         /// </summary>
         public const string FROM = "f";
      }

      /// <summary>
      ///  系统设置变量
      /// </summary>
      public struct Settings
      {
         /// <summary>
         ///  注册需要邀请
         /// </summary>
         public const string SIGNUP_NEED_INVITED = "__signup_need_invited";

         /// <summary>
         ///  是否允许注册
         /// </summary>
         public const string ALLOW_SIGNUP = "__allow_signup";
      }
   }

   
}
