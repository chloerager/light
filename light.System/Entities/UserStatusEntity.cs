using light.Data;
using light.Entities;

namespace light.Entities
{
    [Table("user_status")]
    public class UserStatusEntity : IUserStatusEntity
    {
        /// <summary>
        ///  用户帐户标识
        /// </summary>
       [Field("uid", false, true)]
       public int id;

       /// <summary>
       ///  角色扩展
       /// </summary>
       [Field("roleex")]
       public bool roleex;

        /// <summary>
        ///  已邀请的人数
        /// </summary>
       [Field("invited")]
       public int invited;

       /// <summary>
       ///  最大允许邀请的人数
       /// </summary>
       [Field("maxinvites")]
       public int maxinvites;

       /// <summary>
       ///  拥有的金钱数量
       /// </summary>
       [Field("money")]
       public int money;

       /// <summary>
       ///  信誉值
       /// </summary>
       [Field("credits")]
       public int credits;
       
       /// <summary>
       ///  好友数
       /// </summary>
       [Field("friends")]
       public int friends;

       /// <summary>
       ///  关注数
       /// </summary>
       [Field("follow")]
       public int follow;

       /// <summary>
       ///  关注我的人数
       /// </summary>
       [Field("followme")]
       public int followme;



        /// <summary>
        ///  发起的活动次数
        /// </summary>
       [Field("launchactivities")]
       public int launchactivities;

        /// <summary>
        ///  参与活动次数
        /// </summary>
       [Field("activities")]
       public int activities;

        /// <summary>
        ///  成就点数
        /// </summary>
       [Field("archievement")]
       public int archievement;

       /// <summary>
       ///  日志数
       /// </summary>
       [Field("logs")]
       public int logs;

       /// <summary>
       ///  相片总数
       /// </summary>
       [Field("photos")]
       public int photos;

       /// <summary>
       ///  评论数
       /// </summary>
       [Field("comments")]
       public int comments;

       /// <summary>
       ///  主题贴
       /// </summary>
       [Field("threads")]
       public int threads;

       /// <summary>
       /// 总贴子
       /// </summary>
       [Field("posts")]
       public int posts;

       /// <summary>
       ///  是否有扩展角色
       /// </summary>
       public bool hasRoleEx
       {
          get { return roleex; }
       }
    }
}
