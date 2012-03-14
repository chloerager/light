using System.Collections.Generic;
using light.Content.Entities;
using light.Content.Data;

namespace KB
{
   public class Category
   {
      public static IList<CategoryEntity> GetTopCategories()
      {
         return CategoryData.GetTopCategories("digest_category");
      }

      public static IList<UserCategoryEntity> GetUserCategories(int uid)
      {
         return CategoryData.GetUserCategories(uid, "digest_user_category");
      }

      public static int CreateUserCategory(int pid,int uid,string name,string ename=null)
      {
         return CategoryData.CreateUserCategory("digest_user_category", pid, uid,name, null, null, ename);
      }

      internal static int RenameDigestUserCategory(int id, string name)
      {
         return CategoryData.RenameUserCategory("digest_user_category", id, name);
      }

      internal static int ChangeDigestUserCategoryPid(int id, int pid)
      {
         return CategoryData.ChangeUserCategoryPid("digest_user_category", id, pid);
      }

      internal static int IncUserCategoryCount(int ucid)
      {
         return CategoryData.IncCategoryCount("digest_user_category", ucid);
      }

      internal static int IncCategoryCount(int cid)
      {
         return CategoryData.IncCategoryCount("digest_category", cid);
      }
   }
}
