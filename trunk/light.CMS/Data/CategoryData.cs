using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using light.CMS.Entities;
using light.Data;

namespace light.CMS.Data
{
   public class CategoryData
   {
      public static IList<CategoryEntity> GetTopCategories(string tableName)
      {
         return EB<CategoryEntity>.List(QA.DBCS_CMS, CommandType.Text, "SELECT id,pid,name,count FROM " + tableName + " WHERE pid=0 ORDER BY count DESC");
      }

      public static IList<UserCategoryEntity> GetUserCategories(int uid,string tableName)
      {
         return EB<UserCategoryEntity>.List(QA.DBCS_CMS, CommandType.Text, "SELECT id,pid,uid,name,count FROM " + tableName + " WHERE uid=@uid ORDER BY displayorder ASC,id ASC", new SqlParameter("@uid", uid));
      }

      public static int CreateUserCategory(string tableName,int pid,int uid, string name,string pinyin=null,string pinyinabbr=null,string ename=null)
      {
         if (pinyin == null) pinyin = HZ.ToPinYin(name, true);
         if (pinyinabbr == null) pinyinabbr = PinYin.GetInitial(name);
         if (ename == null) ename = pinyin;

         return EB<UserCategoryEntity>.Create(QA.DBCS_CMS, new UserCategoryEntity() { 
            uid=uid,
            pid=pid,
            name=name,
            pinyin=pinyin,
            pinyinabbr=pinyinabbr,
            ename=ename
         }, tableName);
      }

      public static void DeleteUserCategory(int id, string categoryTable, string entityTable)
      {
         int pid = GetUserCategoryPid(id, categoryTable);

         if (pid >= 0)
         {
            if (DBH.ExecuteText(QA.DBCS_CMS, "UPDATE " + entityTable + " SET ucid=@pid WHERE ucid=@id", new SqlParameter("@pid", pid), new SqlParameter("@id", id)) >= 0)
            {
               DBH.ExecuteText(QA.DBCS_CMS, "DELETE " + categoryTable + " WHERE id=@id", new SqlParameter("@id", id));
            }
         }
      }

      private static int GetUserCategoryPid(int id, string categoryTable)
      {
         return DBH.GetInt32(QA.DBCS_CMS, CommandType.Text, "SELECT pid FROM " + categoryTable + " WHERE id=@id", new SqlParameter("@id", id));
      }

      public static int UpdateUserCategory(string tableName, int id, int pid, int uid,string name,string pinyin=null,string pinyinabbr=null,string ename=null )
      {
         if (pinyin == null) pinyin = HZ.ToPinYin(name, true);
         if (pinyinabbr == null) pinyinabbr = PinYin.GetInitial(name);
         if (ename == null) ename = pinyin;

         //check pid & uid
         if (pid != 0 && !DBH.GetBoolean(QA.DBCS_CMS, CommandType.Text, "SELECT COUNT(id) FROM " + tableName + " WHERE pid=@pid AND uid=@uid", new SqlParameter("@pid", pid), new SqlParameter("@uid", uid))) return -1;

         return EB<UserCategoryEntity>.Update(QA.DBCS_CMS, new UserCategoryEntity()
         {
            id=id,
            uid = uid,
            pid = pid,
            name = name,
            pinyin = pinyin,
            pinyinabbr = pinyinabbr,
            ename = ename
         }, tableName);
      }

      public static int RenameUserCategory(string tableName, int id, string name)
      {
         string pinyin = HZ.ToPinYin(name, true);
         string pinyinabbr = PinYin.GetInitial(name);

         return DBH.ExecuteText(QA.DBCS_CMS, "UPDATE " + tableName + " SET name=@name,pinyin=@pinyin,pinyinabbr=@pinyinabbr,ename=@ename WHERE id=@id",
            new SqlParameter("@name", name), new SqlParameter("@pinyin", pinyin), new SqlParameter("@pinyinabbr", pinyinabbr), new SqlParameter("@ename", pinyin), new SqlParameter("@id", id));
      }

      public static int ChangeUserCategoryPid(string tableName, int id, int pid)
      {
         return DBH.ExecuteText(QA.DBCS_CMS, "UPDATE " + tableName + " SET pid=@pid WHERE id=@id", new SqlParameter("@pid", pid), new SqlParameter("@id", id));
      }

      public static int IncCategoryCount(string tableName, int id)
      {
         return DBH.ExecuteText(QA.DBCS_CMS, "UPDATE " + tableName + " SET count=count+1 WHERE id=@id", new SqlParameter("@id", id));
      }
   }
}
