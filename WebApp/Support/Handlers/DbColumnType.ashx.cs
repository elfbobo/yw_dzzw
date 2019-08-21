using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbColumnType 的摘要说明
    /// </summary>
    public class DbColumnType : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        string ModelGuid = string.Empty;
        string TableName = string.Empty;
        string DBConn = string.Empty;
        string Name = string.Empty;
        string DataType = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            DBConn = context.Request["DBConn"] == null ? "" : context.Request["DBConn"];
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            TableName = context.Request["TableName"] == null ? "" : context.Request["TableName"];
            Name = context.Request["Name"] == null ? "" : context.Request["Name"];
            DataType = context.Request["DataType"] == null ? "" : context.Request["DataType"];
            int t = 0;
            try
            {
                database = DatabaseFactory.CreateDatabase(DBConn, "Yawei.DataAccess.SqlClient.SqlDatabase");
                database.ExecuteDataSet("select Convert(" + DataType + ",[" + Name + "]) from Sys_DataColumn WHERE ModelGuid='" + ModelGuid + "' AND TableName='" + TableName + "' AND Name='" + Name + "'");
            }
            catch
            {
                t = -1;
            }
            context.Response.Clear();
            context.Response.Write(t);
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