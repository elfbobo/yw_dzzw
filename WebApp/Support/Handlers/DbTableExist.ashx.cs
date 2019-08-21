using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbTableExist 的摘要说明
    /// </summary>
    public class DbTableExist : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        Database db = DatabaseFactory.CreateDatabase();
        string TableName = string.Empty;
        string ModelGuid = string.Empty;
        string sql = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            TableName = context.Request["TableName"] == null ? "" : context.Request["TableName"];

            int count = 0;
            try
            {
                string DBConnection = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + ModelGuid + "'").ToString();
                db = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
                count = int.Parse(db.ExecuteScalar("select COUNT(*) from sys.tables WHERE name='" + TableName + "'").ToString());
            }
            catch
            {
                count = -1;
            }
            context.Response.Clear();
            context.Response.Write(count);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}