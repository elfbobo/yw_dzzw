using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbConn 的摘要说明
    /// </summary>
    public class DbConn : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        Database db = DatabaseFactory.CreateDatabase();
        string ModelGuid = string.Empty;
        string strDBConn = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            try
            {
                strDBConn = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + ModelGuid + "'").ToString();
            }
            catch
            {
                strDBConn = "错误";
            }
            context.Response.Clear();
            context.Response.Write(strDBConn);
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