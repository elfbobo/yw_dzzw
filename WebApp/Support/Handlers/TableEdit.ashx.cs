using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// TableEdit 的摘要说明
    /// </summary>
    public class TableEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            Database dataBase = DatabaseFactory.CreateDatabase(); 
            if (request["type"] == "Save")
            {
                string tableName = context.Request["TableName"];
                string List = context.Request["List"];
                string Edit = context.Request["Edit"];
                dataBase.ExecuteNonQuery("delete from Sys_TableEdit where TableName='"+tableName+"'");
                dataBase.ExecuteNonQuery("insert into Sys_TableEdit values ('" + List + "','"+Edit+"','"+tableName+"')");
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