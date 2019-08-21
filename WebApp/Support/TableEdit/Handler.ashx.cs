using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.SupportCore;

namespace Yawei.App.Support.TableEdit
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string tableName = context.Request["TableName"];
           
            if (!string.IsNullOrEmpty(tableName))
            {
                context.Response.Clear();
                context.Response.Write(TableEditCore.LoadTableInfo(tableName));
                context.Response.Flush();
                context.Response.End();
            }

            if (context.Request["type"] == "path")
            {
                string path = context.Server.MapPath("Index.aspx");
                string json = TableEditCore.GetPathJson(path);
                context.Response.Write(json);
            }
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