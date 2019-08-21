using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbDel 的摘要说明
    /// </summary>
    public class DbDel : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        Database db = DatabaseFactory.CreateDatabase();
        string ModelGuid = string.Empty;
        string TableName = string.Empty;
        string Type = string.Empty;
        string Name = string.Empty;
        string OtherSql = string.Empty;
        string Guid = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            Guid = context.Request["Guid"] == null ? "" : context.Request["Guid"];
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            TableName = context.Request["TableName"] == null ? "" : context.Request["TableName"];
            Type = context.Request["Type"] == null ? "" : context.Request["Type"];
            Name = context.Request["Name"] == null ? "" : context.Request["Name"];
            if (!string.IsNullOrEmpty(Name))
                OtherSql = " and Name='" + context.Request["Name"] + "'";
            int t = 0;
            try
            {
                string sql = "";
                if (Type == "db")
                    sql = "delete from Sys_DataModel where Guid='" + Guid + "'";
                if (Type == "table")
                {
                    sql = "delete from Sys_DataTable where ModelGuid='" + ModelGuid + "' and TableName='" + TableName + "'";
                    string DBConnection = database.ExecuteScalar("SELECT DBConnection FROM Sys_DataModel WHERE Guid='" + ModelGuid + "'").ToString();
                    db = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
                    object obj = db.ExecuteScalar("select COUNT(*) from sys.tables WHERE name='" + TableName + "'");
                    if (int.Parse(obj.ToString()) > 0)
                        db.ExecuteNonQuery("drop table " + TableName + "");
                }
                if (Type == "column")
                    sql = "delete from Sys_DataColumn where ModelGuid='" + ModelGuid + "' and TableName='" + TableName + "'" + OtherSql + "";
                t = database.ExecuteNonQuery(sql);
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