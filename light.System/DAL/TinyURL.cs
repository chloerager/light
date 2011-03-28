using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace light.System.DAL
{
    public sealed class TinyURL
    {
        public static string GetCode(int num)
        {
           return DBH.GetString(QA.DBCS_STATIC, CommandType.Text, "SELECT code FROM uid WHERE id=@id", new SqlParameter("@id", num));
        }
    }
}
