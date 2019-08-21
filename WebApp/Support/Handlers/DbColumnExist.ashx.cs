using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbColumnExist 的摘要说明
    /// </summary>
    public class DbColumnExist : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        Database db = DatabaseFactory.CreateDatabase();
        string TableName = string.Empty;
        string ModelGuid = string.Empty;
        string Name = string.Empty;
        string sql = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            TableName = context.Request["TableName"] == null ? "" : context.Request["TableName"];
            Name = context.Request["Name"] == null ? "" : context.Request["Name"];
            int count = 0;
            try
            {
                string DBConnection = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + ModelGuid + "'").ToString();
                db = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
                count = int.Parse(db.ExecuteScalar("select COUNT(*) from sys.columns where object_id=object_id('" + TableName.ToLower() + "') AND name='" + Name.ToLower() + "'").ToString());
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