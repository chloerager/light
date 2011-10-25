using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Data.SqlClient;

namespace light.Tools
{
   public class ImportMySQL
   {
      public static void Import()
      {

         OdbcConnection odbc = new OdbcConnection("DRIVER={MySQL ODBC 5.1 Driver};SERVER=218.29.115.154;DATABASE=dongman;UID=root;PASSWORD=1qaz!QAZ;OPTION=3;");
         OdbcCommand cmd = new OdbcCommand("select * from common_district", odbc);

         OdbcDataAdapter da = new OdbcDataAdapter(cmd);
         DataSet ds = new DataSet("district");
         da.Fill(ds);

         int count = ds.Tables[0].Rows.Count;

         SqlBulkCopy copy = new SqlBulkCopy("Data Source=.;Initial Catalog=TZ_MAIN;Integrated Security=SSPI;Connect Timeout=30;Pooling=true");
         copy.ColumnMappings.Add("id", "id");
         copy.ColumnMappings.Add("name", "name");
         copy.ColumnMappings.Add("level", "class");
         copy.ColumnMappings.Add("upid", "pid");
         copy.DestinationTableName = "district";
         copy.WriteToServer(ds.Tables[0]);

      }
   }
}
