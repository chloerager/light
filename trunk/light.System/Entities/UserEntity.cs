using System;
using light.Data;

namespace light.Entities
{
   /// <summary>
   ///  用户账户信息实体对象。
   /// </summary>
   [Table("user_account")]
   public class UserEntity
   {
      /// <summary>
      /// 账户标识，自增长，唯一。
      /// </summary>
      [Field("id",true,true,true)]
      public int id;

      /// <summary>
      ///  应用标识
      /// </summary>
      [Field("appid")]
      public int appid;

      /// <summary>
      ///  邮件状态，是否激活
      /// </summary>
      [Field("emailstatus")]
      public bool emailstatus;

      /// <summary>
      ///  性别，女2，男1，未知0
      /// </summary>
      [Field("sex")]
      public byte sex;

      /// <summary>
      ///  用户状态
      /// </summary>
      [Field("status")]
      public byte status;

      /// <summary>
      ///  账户角色标识，默认为普通用户0
      /// </summary>
      [Field("roleid")]
      public int roleid;

      /// <summary>
      /// 注册时间
      /// </summary>
      [Field("created",true)]
      public DateTime created;

      /// <summary>
      ///  登录名
      /// </summary>
      [Field("name")]
      public string name;

      /// <summary>
      ///  个人首页地址
      /// </summary>
      [Field("www")]
      public string www;

      /// <summary>
      ///  邮件地址
      /// </summary>
      [Field("email")]
      public string email;

      /// <summary>
      ///  密码
      /// </summary>
      [Field("passwd")]
      [IgnoreAttribute()]
      public string password;

      [Field("avatar")]
      public string avatar;

      /// <summary>
      ///  出生日期
      /// </summary>
      [Field("birthday", true, allowNulls: true)]
      public DateTime birthday;
      
      /// <summary>
      ///  真实姓名
      /// </summary>
      [Field("realname", true, allowNulls: true)]
      public string realname;

      /// <summary>
      ///  移动电话
      /// </summary>
      [Field("mobile", true, allowNulls: true)]
      public string mobile;

      /// <summary>
      ///  身份证号
      /// </summary>
      [Field("idnumber",true,allowNulls:true)]
      public string idnumber;

      /// <summary>
      ///  所在位置
      /// </summary>
      [Field("location", true, allowNulls: true)]
      public string location;
   }
}
