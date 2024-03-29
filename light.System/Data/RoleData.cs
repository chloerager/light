﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using light.Entities;
using light.Data;
using System.Data;
using System.Data.SqlClient;

namespace light.Data
{
   public class RoleData
   {
      internal static IList<AppEntity> GetAppList(int roleid)
      {
         return EB<AppEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT app.* FROM role_app INNER JOIN app ON appid=app.id WHERE roleid=@roleid AND allowaccess=1 AND app.enabled=1 ORDER BY displayorder ASC",
            new SqlParameter("@roleid", roleid));
      }

      public static IList<int> GetRoleExList(int uid)
      {
         return DBH.GetList<int>(QA.DBCS_MAIN, CommandType.Text, "SELECT roleid FROM user_role_ex WHERE uid=@uid", new SqlParameter("@uid", uid));
      }

      internal static string GetDisplayName(int roleid)
      {
         return DBH.GetString(QA.DBCS_MAIN, CommandType.Text, "SELECT displayname FROM role WHERE id=@id", new SqlParameter("@id", roleid));
      }

      public static IList<RoleEntity> GetRoleList()
      {
         return EB<RoleEntity>.List(QA.DBCS_MAIN, CommandType.Text, "SELECT * FROM role");
      }

      internal static int Update(RoleEntity entity)
      {
         return EB<RoleEntity>.Update(QA.DBCS_MAIN, entity);
      }

      internal static int Create(RoleEntity entity)
      {
         return EB<RoleEntity>.Create(QA.DBCS_MAIN, entity);
      }

      //internal static int Delete(
   }
}
